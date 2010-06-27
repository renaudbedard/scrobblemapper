using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System;

namespace ScrobbleMapper.LastFm
{
    // This attribute with IsAny is essential! 
    // It's the only way to have a generic reponse container object
    [XmlSchemaProvider(null, IsAny = true)]
    public class LastFmResponse<T> : IXmlSerializable 
    {
        static readonly XmlSerializer ErrorSerializer = new XmlSerializer(typeof(Error));
        readonly XmlSerializer ContentSerializer = new XmlSerializer(typeof(T));

        public T Content { get; private set; }
        public StatusCode StatusCode { get; internal set; }
        public Error Error { get; internal set; }

        public LastFmResponse()
        {
            // Nothing done yet...
            StatusCode = StatusCode.None;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            // Parse the status code
            StatusCode = (StatusCode)Enum.Parse(typeof(StatusCode), reader.GetAttribute("status"), true);

            // Skip the "lfm" node
            reader.Read();  

            // If there was no problem,
            if (StatusCode == StatusCode.Ok)
            {
                // Deserialize the expected result
                if (reader.MoveToContent() == XmlNodeType.Element)
                    try
                    {
                        Content = (T) ContentSerializer.Deserialize(reader);
                    } 
                    catch (Exception)
                    {
                        reader.Skip();
                    }
            }
            else
            {
                // Deserialize the error
                if (reader.MoveToContent() == XmlNodeType.Element)
                    Error = (Error)ErrorSerializer.Deserialize(reader);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            // We never send that element
            throw new NotSupportedException();
        }
    }

    public enum StatusCode
    {
        None,
        Ok,
        Failed
    }

    [XmlRoot("error")]
    public struct Error
    {
        [XmlElement("code")]
        public int Code { get; set; }

        [XmlText]
        public string Message { get; set; }
    }
}