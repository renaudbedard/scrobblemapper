using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScrobbleMapper.Forms;
using ScrobbleMapper.LastFm;
using ScrobbleMapper.Properties;
using System.Collections.Concurrent;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// A base class for both music libraries
    /// </summary>
    abstract class LibraryManager
    {
        /// <summary>
        /// Translates a COM error code (HRESULT) as a text message.
        /// </summary>
        protected abstract string InterpretErrorCode(int errorCode);

        #region Map

        /// <summary>
        /// Starts the asynchronous reporting task for library mapping
        /// </summary>
        /// <param name="scrobbles">The fetched scrobbles</param>
        /// <returns>The associated task</returns>
        public abstract IReportingTask<MapResult> MapAsync(IEnumerable<ScrobbledTrack> scrobbles);

        /// <summary>
        /// Registers a track from a music library to the local lookup database
        /// </summary>
        /// <param name="track">The track to register</param>
        /// <param name="state">The current task's state</param>
        protected static void RegisterLibraryTrack(LibraryTrack track, MapState state)
        {
            // Basically finds a space to put it in, and puts it
            Dictionary<string, List<LibraryTrack>> artistTracks;
            List<LibraryTrack> duplicateTracks;

            if (!state.LibraryArtists.TryGetValue(track.ArtistKey, out artistTracks))
                state.LibraryArtists.Add(track.ArtistKey, artistTracks = new Dictionary<string, List<LibraryTrack>>());

            if (!artistTracks.TryGetValue(track.TitleKey, out duplicateTracks))
                artistTracks.Add(track.TitleKey, duplicateTracks = new List<LibraryTrack>());

            duplicateTracks.Add(track);
        }

        /// <summary>
        /// Finds matches to all scrobbles and updates where needed and possible
        /// </summary>
        /// <param name="taskContext">The current task's context</param>
        /// <param name="state">The current task's state</param>
        protected void FindAndUpdateTracks(ReportingTask<MapResult> taskContext, MapState state)
        {
            Parallel.ForEach(state.Scrobbles, (scrobble, parallelState) =>
            {
                if (taskContext.CancellationTokenSource.Token.IsCancellationRequested)
                    parallelState.Stop();

                taskContext.Description = "Matching scrobble '" + scrobble.Artist + " - " + scrobble.Title + "'";

                // Only exact matches are updated immediately
                var media = Find(scrobble, state);
                if (media != null) 
                {
                    try
                    {
                        if (media.TryUpdate(scrobble))
                            Interlocked.Increment(ref state.Updated);
                        else
                            Interlocked.Increment(ref state.AlreadyUpToDate);
                    }
                    catch (COMException e)
                    {
                        // Log errors to a list
                        var errorString = InterpretErrorCode(e.ErrorCode);
                        var mediaString = media.ToString();
                        var error = new QualifiedError(mediaString, errorString);
                        state.Errors.Enqueue(error);
                        Interlocked.Increment(ref state.UpdateFailed);
                    }
                }

                taskContext.ReportItemCompleted();
            });
        }

        /// <summary>
        /// Tries to finds matches for a scrobbled track
        /// </summary>
        /// <param name="scrobble">The scrobble to search for</param>
        /// <param name="state">The current task's state</param>
        /// <returns>A perfect match if found, or null</returns>
        protected static LibraryTrack Find(ScrobbledTrack scrobble, MapState state)
        {
            List<RelevantLibraryTrack> fuzzyMatches = null;

            Dictionary<string, List<LibraryTrack>> artistTracks;
            if (state.LibraryArtists.TryGetValue(scrobble.ArtistKey, out artistTracks))
            {
                // Direct artist match
                var track = MatchTitle(scrobble, artistTracks, ref fuzzyMatches);
                if (track != null)
                    // Perfect match; return immediately
                    return track;
            }

            var minEditDistance = Settings.Default.MinimumEditDistance;

            // If nothing was found yet,
            if (artistTracks == null || artistTracks.Count == 0 ||
                fuzzyMatches == null || fuzzyMatches.Count == 0)
            {
                // No direct artist match, check for edit distance
                var artistKey = scrobble.ArtistKey;
                foreach (var entry in state.LibraryArtists)
                {
                    var titleDistance = (float)entry.Key.LevenshteinDistance(artistKey) / artistKey.Length;
                    if (titleDistance < minEditDistance)
                        MatchTitle(scrobble, entry.Value, ref fuzzyMatches, titleDistance / minEditDistance);
                }
            }

            // If any fuzzy matches were found
            if (fuzzyMatches != null && fuzzyMatches.Count != 0)
                state.FuzzyMatches.Enqueue(new FuzzyMatch(scrobble, fuzzyMatches.OrderByDescending(x => x.Relevance).ToArray()));
            else
                Interlocked.Increment(ref state.NotFound);

            // No direct match to return, fuzzy matches added to state
            return null;
        }

        /// <summary>
        /// Tries to finds matches for the title of a scrobbled track
        /// </summary>
        /// <returns>A perfect match if any is found, or null</returns>
        protected static LibraryTrack MatchTitle(ScrobbledTrack scrobble, IDictionary<string, List<LibraryTrack>> artistTracks,
                                          ref List<RelevantLibraryTrack> fuzzyMatches)
        {
            return MatchTitle(scrobble, artistTracks, ref fuzzyMatches, 0);
        }
        /// <summary>
        /// Tries to finds matches for the title of a scrobbled track
        /// </summary>
        /// <returns>A perfect match if any is found, or null</returns>
        protected static LibraryTrack MatchTitle(ScrobbledTrack scrobble, IDictionary<string, List<LibraryTrack>> artistTracks,
                                          ref List<RelevantLibraryTrack> fuzzyMatches, float artistDistance)
        {
            List<LibraryTrack> duplicateTracks;
            if (artistTracks.TryGetValue(scrobble.TitleKey, out duplicateTracks))
            {
                // Direct title match
                if (duplicateTracks.Count == 1)
                    // Perfect match; return immediately
                    return duplicateTracks[0];

                // Duplicate titles for direct match
                if (duplicateTracks.Count > 0)
                {
                    if (fuzzyMatches == null)
                        fuzzyMatches = new List<RelevantLibraryTrack>();

                    foreach (var duplicate in duplicateTracks)
                        fuzzyMatches.Add(new RelevantLibraryTrack(duplicate));
                }
            }

            var minEditDistance = Settings.Default.MinimumEditDistance;
            // If nothing was found yet,
            if (duplicateTracks == null || duplicateTracks.Count == 0)
            {
                // No direct title match, check for edit distance
                var titleKey = scrobble.TitleKey;
                foreach (var trackEntry in artistTracks)
                {
                    var titleDistance = (float)trackEntry.Key.LevenshteinDistance(titleKey) / titleKey.Length;
                    if (titleDistance < minEditDistance)
                    {
                        if (fuzzyMatches == null)
                            fuzzyMatches = new List<RelevantLibraryTrack>();

                        // The final relevance is the product of the artist and title relevances
                        var relevance = (1 - artistDistance) * (1 - (titleDistance / minEditDistance));

                        foreach (var duplicate in trackEntry.Value)
                            fuzzyMatches.Add(new RelevantLibraryTrack(duplicate, relevance));
                    }
                }
            }

            // No direct match to return, fuzzy matches added to state
            return null;
        }

        protected class MapState
        {
            public readonly Dictionary<string, Dictionary<string, List<LibraryTrack>>> LibraryArtists;
            public readonly ConcurrentQueue<FuzzyMatch> FuzzyMatches = new ConcurrentQueue<FuzzyMatch>();
            public readonly IEnumerable<ScrobbledTrack> Scrobbles;
            public readonly ConcurrentQueue<QualifiedError> Errors = new ConcurrentQueue<QualifiedError>();

            public int NotFound;
            public int Updated;
            public int AlreadyUpToDate;
            public int UpdateFailed;

            public MapState(IEnumerable<ScrobbledTrack> scrobbles)
            {
                Scrobbles = scrobbles;
                LibraryArtists = new Dictionary<string, Dictionary<string, List<LibraryTrack>>>();
            }
        }

        #endregion

        #region Choose Fuzzy Matches

        /// <summary>
        /// Starts the asynchronous reporting task for choosing fuzzy matches
        /// </summary>
        /// <param name="host">The parent form</param>
        /// <param name="fuzzyMatches">The fuzzy matches to evaluate</param>
        /// <returns>The associated task</returns>
        public IReportingTask<ChooseFuzzyMatchesResult> ChooseFuzzyMatchesAsync(Form host, IEnumerable<FuzzyMatch> fuzzyMatches)
        {
            var context = new ReportingTask<ChooseFuzzyMatchesResult>();
            context.Task = Task.Factory.StartNew(() => ChooseFuzzyMatches(context, new ChooseFuzzyMatchesState(fuzzyMatches, host)), context.CancellationTokenSource.Token);
            return context;
        }

        ChooseFuzzyMatchesResult ChooseFuzzyMatches(ReportingTaskBase taskContext, ChooseFuzzyMatchesState state)
        {
            int left = state.FuzzyMatches.Count();
            taskContext.TotalItems = left;

            bool autoUpdateSingleMatches = false, autoUpdateFullyRelevant = false, discardRemaining = false;

            foreach (var fuzzyMatch in state.FuzzyMatches)
            {
                taskContext.Description = "Choosing fuzzy match for '" + fuzzyMatch.Scrobble.Artist + " - " + fuzzyMatch.Scrobble.Title + "'";

                // Invoke in host form's thread and wait for completion
                state.HostForm.EndInvoke(state.HostForm.BeginInvoke((Action)delegate
                {
                    using (var chooser = new TrackChooser
                    {
                        FilesLeft = left--,
                        FuzzyMatch = fuzzyMatch,
                        DiscardRemaining = discardRemaining,
                        AutoUpdateFullyRelevant = autoUpdateFullyRelevant,
                        AutoUpdateSingleMatches = autoUpdateSingleMatches,
                    })
                    {
                        if ((autoUpdateSingleMatches && fuzzyMatch.Matches.Count() == 1) ||
                            (autoUpdateFullyRelevant && chooser.ChosenLibraryTracks.Count() > 0) ||
                            (!discardRemaining && chooser.ShowDialog(state.HostForm) == DialogResult.OK))
                        {
                            foreach (var media in chooser.ChosenLibraryTracks)
                                try
                                {
                                    if (media.TryUpdate(fuzzyMatch.Scrobble))
                                        state.Updated++;
                                    else
                                        state.AlreadyUpToDate++;
                                }
                                catch (COMException e)
                                {
                                    state.Errors.Enqueue(new QualifiedError(media.ToString(), InterpretErrorCode(e.ErrorCode)));
                                    state.UpdateFailed++;
                                }

                            autoUpdateSingleMatches = chooser.AutoUpdateSingleMatches;
                            autoUpdateFullyRelevant = chooser.AutoUpdateFullyRelevant;
                            discardRemaining = chooser.DiscardRemaining;
                        }
                    }
                }));

                taskContext.ReportItemCompleted();
            }

            return new ChooseFuzzyMatchesResult(state.Updated, state.AlreadyUpToDate, state.UpdateFailed, state.Errors.ToArray());
        }

        class ChooseFuzzyMatchesState
        {
            public readonly IEnumerable<FuzzyMatch> FuzzyMatches;
            public readonly Form HostForm;
            public readonly ConcurrentQueue<QualifiedError> Errors = new ConcurrentQueue<QualifiedError>();

            public int Updated;
            public int AlreadyUpToDate;
            public int UpdateFailed;

            public ChooseFuzzyMatchesState(IEnumerable<FuzzyMatch> fuzzyMatches, Form hostForm)
            {
                FuzzyMatches = fuzzyMatches;
                HostForm = hostForm;
            }
        }

        #endregion
    }

    class ChooseFuzzyMatchesResult
    {
        public readonly IEnumerable<QualifiedError> Errors;

        public readonly int Updated;
        public readonly int AlreadyUpToDate;
        public readonly int UpdateFailed;

        public ChooseFuzzyMatchesResult(int updated, int alreadyUpToDate, int updateFailed, QualifiedError[] errors)
        {
            Updated = updated;
            AlreadyUpToDate = alreadyUpToDate;
            UpdateFailed = updateFailed;
            Errors = errors;
        }
    }

    class MapResult
    {
        public readonly IEnumerable<FuzzyMatch> FuzzyMatches;
        public readonly IEnumerable<QualifiedError> Errors;

        public readonly int Updated;
        public readonly int AlreadyUpToDate;
        public readonly int NotFound;
        public readonly int UpdateFailed;

        public MapResult(FuzzyMatch[] fuzzyMatches, int updated, int alreadyUpToDate, int notFound, int updateFailed, QualifiedError[] errors)
        {
            FuzzyMatches = fuzzyMatches;
            Updated = updated;
            AlreadyUpToDate = alreadyUpToDate;
            NotFound = notFound;
            UpdateFailed = updateFailed;
            Errors = errors;
        }
    }
}