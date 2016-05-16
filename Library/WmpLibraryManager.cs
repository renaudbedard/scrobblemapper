using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ScrobbleMapper.LastFm;
using WMPLib;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// The service provider for the Windows Media Player library
    /// </summary>
    class WmpLibraryManager : LibraryManager
    {
        readonly WindowsMediaPlayer WindowsMediaPlayer = new WindowsMediaPlayer();

        const string MediaTypeAttribute = "MediaType";
        const string AudioMediaType = "audio";

        /// <summary>
        /// Starts the asynchronous reporting task for library mapping
        /// </summary>
        /// <param name="scrobbles">The fetched scrobbles</param>
        /// <returns>The associated task</returns>
        public override IReportingTask<MapResult> MapAsync(IEnumerable<ScrobbledTrack> scrobbles)
        {
            var context = new ReportingTask<MapResult>();
            var future = Task.Factory.StartNew(() => Map(context, new MapState(scrobbles)), context.CancellationTokenSource.Token);
            context.Task = future;

            return context;
        }

        MapResult Map(ReportingTask<MapResult> taskContext, MapState state)
        {
            // Locally register all tracks from WMP
            var library = (IWMPMediaCollection2) WindowsMediaPlayer.mediaCollection;

            taskContext.Description = "Fetching WMP library items";
            var wmpTracks = library.getByAttribute(MediaTypeAttribute, AudioMediaType);

            var itemCount = wmpTracks.count;
            taskContext.TotalItems = itemCount + state.Scrobbles.Count();

            for (int i = 0; i < itemCount; i++)
            {
                if (taskContext.CancellationTokenSource.Token.IsCancellationRequested)
                    taskContext.CancellationTokenSource.Token.ThrowIfCancellationRequested();

                var track = new WmpLibraryTrack(wmpTracks.get_Item(i));
                taskContext.Description = "Registering WMP track '" + track.Artist + " - " + track.Title + "'";

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
            return new Win32Exception(errorCode).Message;
        }

        public override void Dispose()
        {
            Marshal.FinalReleaseComObject(WindowsMediaPlayer);
        }
    }
}