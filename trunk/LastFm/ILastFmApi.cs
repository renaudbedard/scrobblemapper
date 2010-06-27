using System.ServiceModel;
using System.ServiceModel.Web;

namespace ScrobbleMapper.LastFm
{
    // Uses the Windows Communication Foundation attributes to access the Last.fm RESTful API
    [ServiceContract]
    [XmlSerializerFormat]
    interface ILastFmApi
    {
        [OperationContract]
        [WebGet(UriTemplate = "?method=user.getweeklychartlist&user={user}&api_key={apiKey}")]
        LastFmResponse<WeeklyChartList> UserGetWeeklyChartList(string user, string apiKey);

        [OperationContract]
        [WebGet(UriTemplate = "?method=user.getweeklytrackchart&user={user}&from={from}&to={to}&api_key={apiKey}")]
        LastFmResponse<WeeklyTrackChart> UserGetWeeklyTrackChart(string user, long from, long to, string apiKey);
    }
}