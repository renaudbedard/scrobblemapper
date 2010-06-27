using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;

namespace ScrobbleMapper.LastFm
{
    [XmlRoot("weeklychartlist")]
    public struct WeeklyChartList
    {
        [XmlElement("chart")] 
        public Chart[] Charts;
    }

    public struct Chart : IXmlSerializable
    {
        public DateTime From;
        public DateTime To;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            // Convert the response's Unix times to .NET DateTime's
            From = UnixTime.ToDate(long.Parse(reader.GetAttribute("from")));
            To = UnixTime.ToDate(long.Parse(reader.GetAttribute("to")));
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}