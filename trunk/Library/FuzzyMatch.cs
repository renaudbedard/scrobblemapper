using System.Collections.Generic;
using ScrobbleMapper.LastFm;

namespace ScrobbleMapper.Library
{
    /// <summary>
    /// A fuzzy match associates a Last.fm scrobble with a series of possible matching library tracks.
    /// </summary>
    class FuzzyMatch
    {
        public FuzzyMatch(ScrobbledTrack scrobble, IEnumerable<RelevantLibraryTrack> matches)
        {
            Scrobble = scrobble;
            Matches = matches;
        }

        public ScrobbledTrack Scrobble { get; private set; }
        public IEnumerable<RelevantLibraryTrack> Matches { get; private set; }
    }

    /// <summary>
    /// A relevant library track is a library track with a relevance factor (in %) in relation to a scrobble.
    /// </summary>
    class RelevantLibraryTrack
    {
        public ILibraryTrack Track { get; private set; }
        public float Relevance { get; private set; }

        public RelevantLibraryTrack(ILibraryTrack track) : this(track, 1) { }
        public RelevantLibraryTrack(ILibraryTrack track, float relevance)
        {
            Track = track;
            Relevance = relevance;
        }
    }
}