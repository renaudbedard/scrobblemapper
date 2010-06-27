using System.Xml.Serialization;

namespace ScrobbleMapper.LastFm
{
    [XmlRoot("weeklytrackchart")]
    public struct WeeklyTrackChart
    {
        [XmlElement("track")] 
        public Track[] Tracks;
    }

    public struct Track
    {
        [XmlElement("artist")] 
        public Artist Artist;
        [XmlElement("name")] 
        public string Title;
        [XmlElement("playcount")] 
        public int PlayCount;

        // These elements are available in the API but useless and almost always null...

        //[XmlElement("mbid")] 
        //public string MusicBrainzId;
        //[XmlElement("url")]
        //public string Url;
    }

    public struct Artist
    {
        [XmlText] 
        public string Name;

        // Ditto here

        //[XmlAttribute("mbid")] 
        //public string MusicBrainzId;
    }
}