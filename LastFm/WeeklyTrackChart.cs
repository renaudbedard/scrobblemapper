using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using System;
using System.Xml.Schema;

namespace ScrobbleMapper.LastFm
{
    [XmlRoot("weeklytrackchart")]
    public struct WeeklyTrackChart : IXmlSerializable
    {
        public Track[] Tracks;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XElement root = (XElement)XNode.ReadFrom(reader);
            Tracks = root.Elements().Select(x => new Track
            {
                Artist = x.Element("artist").Value,
                Title = x.Element("name").Value,
                PlayCount = int.Parse(x.Element("playcount").Value)
            }).ToArray();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public struct Track
    {
        public string Artist;
        public string Title;
        public int PlayCount;
    }
}