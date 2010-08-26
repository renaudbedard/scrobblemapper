using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Collections;
using System.Threading.Tasks;
using ScrobbleMapper.Forms;

namespace ScrobbleMapper.LastFm
{
    /// <summary>
    /// The service provider for Last.fm scrobble fetching.
    /// This class, not LastFmClient, is accessed directly by the application.
    /// </summary>
    class ScrobbleFetcher
    {
        /// <summary>
        /// My API key! Don't look!
        /// </summary>
        const string ApiKey = "62b743e5de48ef452744c7e151feca07";

        readonly LastFmClient client = new LastFmClient(ApiKey);

        /// <summary>
        /// Starts the asynchronous reporting task for scrobble fetching
        /// </summary>
        /// <param name="user">The user to query</param>
        /// <returns>The associated task</returns>
        public IReportingTask<FetchResult> FetchAsync(string user)
        {
            var context = new ReportingTask<FetchResult>();
            context.Task = Future.Create(() => Fetch(context, new FetchState(user)));
            return context;
        }

        FetchResult Fetch(ReportingTask<FetchResult> taskContext, FetchState state)
        {
            // Get the charts
            taskContext.Description = "Fetching weekly charts list";
            var chartsResponse = client.UserGetWeeklyChartList(state.User);
            if (chartsResponse.StatusCode == StatusCode.Failed)
                throw new InvalidOperationException("This user does not exist, or it doesn't have any weekly chart generated yet ('" + chartsResponse.Error.Message + "')");

            // Get the tracks...
            taskContext.TotalItems = chartsResponse.Content.Charts.Length;

            // ...in parallel!
            Parallel.ForEach(chartsResponse.Content.Charts, 
            () =>
                {
                    var thisWeek = new List<ScrobbledTrack>();
                    state.WeeklyTracks.Enqueue(thisWeek);
                    return thisWeek;
                },
            (range, index, parallelState) =>
                {
                    if (taskContext.Task.IsCanceled) // Allow mid-operation canceling
                        parallelState.Stop();

                    taskContext.Description = "Fetching weekly chart starting " + range.From.ToShortDateString();

                    var tracksResponse = client.UserGetWeeklyTrackChart(state.User, range.From, range.To);
                    if (tracksResponse.StatusCode == StatusCode.Failed)
                        state.Errors.Enqueue(new QualifiedError(range.From.ToShortDateString() + " to " + range.To.ToShortDateString(), "'" + tracksResponse.Error.Message + "' (scrobbles from that week were ignored)"));
                    else if (tracksResponse.Content.Tracks != null) // This does happen sometimes
                        parallelState.ThreadLocalState.AddRange(from track in tracksResponse.Content.Tracks
                                                                select new ScrobbledTrack(track.Artist.Name, track.Title,
                                                                                          track.PlayCount, range.To));

                    taskContext.ReportItemCompleted();
                });
            // Early out
            if (taskContext.Task.IsCanceled)
                return null;

            // Compose results
            taskContext.Description = "Assembling charts";
            var trackSet = new Dictionary<string, ScrobbledTrack>();
            foreach (var track in state.WeeklyTracks.SelectMany(x => x))
            {
                ScrobbledTrack contained;
                string key = track.Artist + track.Title;
                if (trackSet.TryGetValue(key, out contained))
                {
                    contained.PlayCount += track.PlayCount;
                    if (track.WeekLastPlayed > contained.WeekLastPlayed)
                        contained.WeekLastPlayed = track.WeekLastPlayed;
                }
                else
                    trackSet.Add(key, track);
            }

            return new FetchResult(trackSet.Values.ToArray(), state.Errors.ToArray());
        }

        class FetchState
        {
            public readonly ConcurrentQueue<IEnumerable<ScrobbledTrack>> WeeklyTracks;
            public readonly ConcurrentQueue<QualifiedError> Errors;
            public readonly string User;

            public FetchState(string user)
            {
                WeeklyTracks = new ConcurrentQueue<IEnumerable<ScrobbledTrack>>();
                Errors = new ConcurrentQueue<QualifiedError>();
                User = user;
            }
        }
    }

    class FetchResult
    {
        public readonly IEnumerable<ScrobbledTrack> Scrobbles;
        public readonly IEnumerable<QualifiedError> Errors;

        public FetchResult(IEnumerable<ScrobbledTrack> scrobbles, IEnumerable<QualifiedError> errors)
        {
            Scrobbles = scrobbles;
            Errors = errors;
        }
    }
}