using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;
using System.Xml.Linq;
using System.Linq;

namespace ScrobbleMapper.LastFm
{
    [XmlRoot("weeklychartlist")]
    public struct WeeklyChartList : IXmlSerializable
    {
        public Chart[] Charts;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XElement root = (XElement) XNode.ReadFrom(reader);
            Charts = root.Elements().Select(x => new Chart
            {
                From = UnixTime.ToDate(long.Parse(x.Attribute("from").Value)),
                To = UnixTime.ToDate(long.Parse(x.Attribute("to").Value)),
            }).ToArray();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public struct Chart
    {
        public DateTime From;
        public DateTime To;
    }
}