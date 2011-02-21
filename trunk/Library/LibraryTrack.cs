using System;
using System.Threading;
using iTunesLib;
using ScrobbleMapper.LastFm;
using WMPLib;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// A track from the iTunes music library 
    /// </summary>
    class ItunesLibraryTrack : LibraryTrack
    {
        public ItunesLibraryTrack(IITTrack libraryItem)
        {
            LibraryItem = libraryItem;

            // iTunes will give null values and we need to protect ourselves...

            Artist = libraryItem.Artist ?? "";
            ArtistKey = Artist.Neutralize();

            Title = libraryItem.Name ?? "";
            TitleKey = Title.Neutralize();

            // Defer initialization for the following, for performance

            album = new LazyInit<string>(() => libraryItem.Album ?? "");

            playCountCache = new Cacheable<int>(
                () => LibraryItem.PlayedCount,
                value => LibraryItem.PlayedCount = value);

            playDateCache = new Cacheable<DateTime>(
                () => LibraryItem.PlayedDate,
                value => LibraryItem.PlayedDate = value);
        }

        public IITTrack LibraryItem { get; private set; }
    }

    /// <summary>
    /// A track from the Windows Media Player music library
    /// </summary>
    class WmpLibraryTrack : LibraryTrack
    {
        // Metadata attribute names
        const string ArtistAttribute = "Author";
        const string TitleAttribute = "Title";
        const string AlbumTitleAttribute = "WM/AlbumTitle";
        const string PlayCountAttribute = "UserPlayCount";

        public WmpLibraryTrack(IWMPMedia libraryItem)
        {
            LibraryItem = libraryItem;

            Artist = libraryItem.getItemInfo(ArtistAttribute) ?? "";
            ArtistKey = Artist.Neutralize();

            Title = libraryItem.getItemInfo(TitleAttribute) ?? "";
            TitleKey = Title.Neutralize();

            // Defer initialization for the following, for performance

            album = new LazyInit<string>(() => libraryItem.getItemInfo(AlbumTitleAttribute) ?? "");

            playCountCache = new Cacheable<int>(() =>
                {
                    int playCount;
                    if (!int.TryParse(LibraryItem.getItemInfo(PlayCountAttribute), out playCount))
                        return 0;
                    return playCount;
                },
                value => LibraryItem.setItemInfo(PlayCountAttribute, value.ToString()));

            // Not implemented (more like implementable...)
            playDateCache = new Cacheable<DateTime>(() => DateTime.Now, value => { });
        }

        public IWMPMedia LibraryItem { get; private set; }
    }

    abstract class LibraryTrack : ILibraryTrack
    {
        public string Artist { get; protected set; }
        public string ArtistKey { get; protected set; }

        public string Title { get; protected set; }
        public string TitleKey { get; protected set; }

        protected LazyInit<string> album;
        public string Album
        {
            get { return album.Value; }
        }

        protected Cacheable<int> playCountCache;
        public int PlayCount
        {
            get { return playCountCache; }
            set { playCountCache.Value = value; }
        }

        protected Cacheable<DateTime> playDateCache;
        public DateTime PlayDate
        {
            get { return playDateCache; }
            set { playDateCache.Value = value; }
        }

        /// <summary>
        /// Try to update the library track from the scrobble information.
        /// A COMException will be raised on error.
        /// </summary>
        /// <returns>True if the file was updated, false if no update was necessary</returns>
        public bool TryUpdate(ScrobbledTrack scrobble)
        {
            var updated = false;

            if (PlayCount < scrobble.PlayCount)
            {
                PlayCount = scrobble.PlayCount;
                updated = true;
            }

            if (PlayDate < scrobble.WeekLastPlayed)
            {
                PlayDate = scrobble.WeekLastPlayed;
                updated = true;
            }

            return updated;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} (Album : '{2}')", Artist, Title, Album);
        }
    }

    interface ILibraryTrack
    {
        string Artist { get; }
        string Title { get; }
        string Album { get; }

        int PlayCount { get; set; }
        DateTime PlayDate { get; set; }

        /// <summary>
        /// Try to update the library track from the scrobble information.
        /// A COMException will be raised on error.
        /// </summary>
        /// <returns>True if the file was updated, false if no update was necessary</returns>
        bool TryUpdate(ScrobbledTrack scrobble);
    }
}