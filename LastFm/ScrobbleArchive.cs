using System;
using System.Collections.Generic;

namespace ScrobbleMapper.LastFm
{
    class ScrobbleArchive
    {
        public string Account;
        public DateTime LastWeekFetched;
        public List<ScrobbledTrack> Scrobbles;
    }
}
