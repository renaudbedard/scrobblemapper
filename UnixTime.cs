using System;

namespace ScrobbleMapper
{
    static class UnixTime
    {
        // Unix times are defined as the number of seconds since the epoch (January 1st, 1970)
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static DateTime ToDate(long unixTime)
        {
            return Epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            TimeSpan diff = date - Epoch;
            return (long)Math.Floor(diff.TotalSeconds);
        }
    }
}