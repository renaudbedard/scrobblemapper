using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ScrobbleMapper.LastFm;
using WMPLib;
using System.Collections.Generic;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// The service provider for the Windows Media Player library
    /// </summary>
    class WmpLibraryManager : LibraryManager
    {
        readonly WindowsMediaPlayer WindowsMediaPlayer = new WindowsMediaPlayer();

        /// <summary>
        /// Starts the asynchronous reporting task for library mapping
        /// </summary>
        /// <param name="scrobbles">The fetched scrobbles</param>
        /// <returns>The associated task</returns>
        public override IReportingTask<MapResult> MapAsync(IEnumerable<ScrobbledTrack> scrobbles)
        {
            var context = new ReportingTask<MapResult>();
            var future = Future.Create(() => Map(context, new MapState(scrobbles)));
            context.Task = future;

            return context;
        }

        MapResult Map(ReportingTask<MapResult> taskContext, MapState state)
        {
            // Locally register all tracks from WMP
            var library = (IWMPMediaCollection2) WindowsMediaPlayer.mediaCollection;

            taskContext.Description = "Fetching WMP library playlist";
            var wmpTracks = library.getAll();
            var trackCount = wmpTracks.count;

            taskContext.TotalItems = trackCount + state.Scrobbles.Count();

            // No concurrent dictionaries in this CTP, so this has to be sequential...
            for (int i = 0; i < trackCount; i++)
            {
                if (taskContext.Task.IsCanceled) 
                    return null;

                var track = new WmpLibraryTrack(wmpTracks.get_Item(i));
                taskContext.Description = "Registering WMP track '" + track.Artist + " - " + track.Title + "'";

                RegisterLibraryTrack(track, state);

                taskContext.ReportItemCompleted();
            }

            // Find and update tracks
            FindAndUpdateTracks(taskContext, state);

            // "Early" out
            if (taskContext.Task.IsCanceled)
                return null;

            // Compose and return
            return new MapResult(state.FuzzyMatches.ToArray(), 
                state.Updated, state.AlreadyUpToDate, state.NotFound, state.UpdateFailed, state.Errors.ToArray());
        }

        /// <summary>
        /// Translates a COM error code (HRESULT) as a text message.
        /// </summary>
        protected override string InterpretErrorCode(int errorCode)
        {
            return new Win32Exception(errorCode).Message;
        }
    }
}