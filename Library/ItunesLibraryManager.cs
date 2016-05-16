using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTunesLib;
using ScrobbleMapper.LastFm;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// The service provider for the iTunes library
    /// </summary>
    class ItunesLibraryManager : LibraryManager
    {
        readonly iTunesAppClass iTunes = new iTunesAppClass();

        /// <summary>
        /// Starts the asynchronous reporting task for library mapping
        /// </summary>
        /// <param name="scrobbles">The fetched scrobbles</param>
        /// <returns>The associated task</returns>
        public override IReportingTask<MapResult> MapAsync(IEnumerable<ScrobbledTrack> scrobbles)
        {
            var context = new ReportingTask<MapResult>();
            context.Task = Task.Factory.StartNew(() => Map(context, new MapState(scrobbles)), context.CancellationTokenSource.Token);
            return context;
        }

        MapResult Map(ReportingTask<MapResult> taskContext, MapState state)
        {
            // Locally register all tracks from iTunes
            taskContext.Description = "Fetching iTunes library playlist";
            var iTunesTracks = iTunes.LibraryPlaylist.Tracks;
            var trackCount = iTunesTracks.Count;

            taskContext.TotalItems = trackCount + state.Scrobbles.Count();

            for (int i = 1; i <= trackCount; i++)
            {
                if (taskContext.CancellationTokenSource.Token.IsCancellationRequested)
                    taskContext.CancellationTokenSource.Token.ThrowIfCancellationRequested();

                var track = new ItunesLibraryTrack(iTunesTracks.get_ItemByPlayOrder(i));
                taskContext.Description = "Registering iTunes track '" + track.Artist + " - " + track.Title + "'";

                RegisterLibraryTrack(track, state);

                taskContext.ReportItemCompleted();
            }

            // Find and update tracks
            FindAndUpdateTracks(taskContext, state);

            // "Early" out
            if (taskContext.CancellationTokenSource.Token.IsCancellationRequested)
                taskContext.CancellationTokenSource.Token.ThrowIfCancellationRequested();

            // Compose and return
            return new MapResult(state.FuzzyMatches.ToArray(),
                state.Updated, state.AlreadyUpToDate, state.NotFound, state.UpdateFailed, state.Errors.ToArray());
        }

        /// <summary>
        /// Translates a COM error code (HRESULT) as a text message.
        /// </summary>
        protected override string InterpretErrorCode(int errorCode)
        {
            switch ((uint) errorCode)
            {
                case 0xa0040201: 
                    return "User canceled the operation.";
                case 0xa0040202: 
                    return "The entity referenced by this COM object has been deleted.";
                case 0xa0040203: 
                    return "Attempt to modify a locked property. (the file or containing folder may be read-only)";
                default:
                    return "Unknown error.";
            }
        }
    }
}