using System;

namespace MozillaDecrypt
{
    class DataHelper
    {
        private static DateTime FromUnixTime(long unixTime) => new DateTime(0x7B2, 0x1, 0x1, 0x0, 0x0, 0x0, DateTimeKind.Utc).AddSeconds(unixTime);

        private static long ToUnixTime(DateTime value) => (long)(value - new DateTime(0x7B2, 0x1, 0x1, 0x0, 0x0, 0x0).ToLocalTime()).TotalSeconds;
    }
}