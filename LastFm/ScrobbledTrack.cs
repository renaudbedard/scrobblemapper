using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScrobbleMapper.LastFm
{
    /// <summary>
    /// A track supplied by the Last.fm API
    /// </summary>
    class ScrobbledTrack : IEquatable<ScrobbledTrack>
    {
        public ScrobbledTrack(string artist, string title, int playCount, DateTime weekLastPlayed)
        {
            Artist = artist;
            Title = title;
            PlayCount = playCount;
            WeekLastPlayed = weekLastPlayed;

            // Cache/prepapare the neutralized keys
            ArtistKey = Artist.Neutralize();
            TitleKey = Title.Neutralize();
        }

        public string Artist { get; private set; }
        internal string ArtistKey { get; private set; }

        public string Title { get; private set; }
        internal string TitleKey { get; private set; }

        public int PlayCount { get; set; }
        public DateTime WeekLastPlayed { get; set; }

        public bool Equals(ScrobbledTrack other)
        {
            return other.ArtistKey == ArtistKey && other.TitleKey == TitleKey;
        }

        public override bool Equals(object obj)
        {
            var otherTrack = obj as ScrobbledTrack;
            return obj != null && Equals(otherTrack);
        }

        public override int GetHashCode()
        {
            return ArtistKey.GetHashCode() ^ TitleKey.GetHashCode();
        }
    }

    // The following classes are only to have sorting on the ListView control... :(

    class ScrobbledTrackBindingList : List<ScrobbledTrack>, IBindingList
    {
        bool sorted;
        ListSortDirection sortDirection;
        PropertyDescriptor sortProperty;
        ScrobbledTrackComparer comparer;

#pragma warning disable 0067
        public event ListChangedEventHandler ListChanged;
#pragma warning restore 0067

        public ScrobbledTrackBindingList(IEnumerable<ScrobbledTrack> collection) : base(collection) { } 

        public void AddIndex(PropertyDescriptor property) { throw new NotSupportedException(); }
        public object AddNew() { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public void RemoveIndex(PropertyDescriptor property) { throw new NotSupportedException(); }

        public bool AllowEdit { get { return false; } }
        public bool AllowNew { get { return false; } }
        public bool AllowRemove { get { return false; } }
        public bool SupportsChangeNotification { get { return false; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return true; } }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            sortProperty = property;
            sortDirection = direction;

            comparer = new ScrobbledTrackComparer { Direction = direction };
            if (property.Name == "Artist") comparer.UseArtist = true;
            if (property.Name == "Title") comparer.UseTitle = true;
            if (property.Name == "PlayCount") comparer.UsePlayCount = true;
            if (property.Name == "WeekLastPlayed") comparer.UseLastPlayed = true;

            Sort(comparer);
        }

        public bool IsSorted { get { return sorted; } }
        public void RemoveSort() { sorted = false; }
        public ListSortDirection SortDirection { get { return sortDirection; } }
        public PropertyDescriptor SortProperty { get { return sortProperty; } }
    }

    class ScrobbledTrackComparer : IComparer<ScrobbledTrack>
    {
        public ListSortDirection Direction { get; set; }

        public bool UseArtist { get; set; }
        public bool UseTitle { get; set; }
        public bool UsePlayCount { get; set; }
        public bool UseLastPlayed { get; set; }

        public int Compare(ScrobbledTrack x, ScrobbledTrack y)
        {
            int r = 0;
            if (UseArtist) r = x.Artist.CompareTo(y.Artist);
            if (r == 0 && UseTitle) r = x.Title.CompareTo(y.Title);
            if (r == 0 && UsePlayCount) r = x.PlayCount.CompareTo(y.PlayCount);
            if (r == 0 && UseLastPlayed) r = x.WeekLastPlayed.CompareTo(y.WeekLastPlayed);

            return r * Direction.Sign();
        }
    }

    static class SortDirectionExtensions
    {
        public static int Sign(this ListSortDirection direction)
        {
            return direction == ListSortDirection.Ascending ? 1 : -1;
        }
    }
}