using System;
using System.ServiceModel;

namespace ScrobbleMapper.LastFm
{
    class LastFmClient : ClientBase<ILastFmApi>
    {
        public readonly string ApiKey;

        public LastFmClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        public LastFmResponse<WeeklyChartList> UserGetWeeklyChartList(string user)
        {
            try
            {
                return Channel.UserGetWeeklyChartList(user, ApiKey);
            } 
            catch (ProtocolException ex)
            {
                return new LastFmResponse<WeeklyChartList>
                {
                    StatusCode = StatusCode.Failed,
                    Error = new Error { Message = ex.Message }
                };
            }
        }

        public LastFmResponse<WeeklyTrackChart> UserGetWeeklyTrackChart(string user, DateTime from, DateTime to)
        {
            try
            {
                return Channel.UserGetWeeklyTrackChart(user, from.ToUnixTime(), to.ToUnixTime(), ApiKey);
            }
            catch (Exception ex)
            {
                return new LastFmResponse<WeeklyTrackChart>
                {
                    StatusCode = StatusCode.Failed,
                    Error = new Error { Message = ex.Message }
                };
            }
        }
    }
}
