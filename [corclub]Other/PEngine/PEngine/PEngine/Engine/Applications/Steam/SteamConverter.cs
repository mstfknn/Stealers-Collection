namespace PEngine.Engine.Applications.Steam
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    internal class SteamConverter
    {
        public const string STEAM2 = "^STEAM_0:[0-1]:([0-9]{1,10})$",
                            STEAM32 = "^U:1:([0-9]{1,10})$",
                            STEAM64 = "^7656119([0-9]{10})$",
                            STEAMPREFIX = "U:1:",
                            STEAMPREFIX2 = "STEAM_0:",
                            HTTPS = "https://steamcommunity.com/profiles/";

        private static readonly long Num64 = 0x110000100000000, Num32 = 0x110000100000001;
        private static readonly int Number0 = 0;

        public static long FromSteam2ToSteam64(string accountId) => !Regex.IsMatch(accountId, STEAM2) ? Number0 : Num64 + (Convert.ToInt64(accountId.Substring(0xA)) * 0x2) + Convert.ToInt64(accountId.Substring(0x8, 0x1));
        public static long FromSteam32ToSteam64(long steam32) => steam32 < 0x1 || !Regex.IsMatch($"{STEAMPREFIX}{steam32.ToString(CultureInfo.InvariantCulture)}", STEAM32) ? Number0 : steam32 + Num64;
        public static long FromSteam64ToSteam32(long communityId) => communityId < Num32 || !Regex.IsMatch(communityId.ToString(CultureInfo.InvariantCulture), STEAM64) ? Number0 : communityId - Num64;
        public static string FromSteam64ToSteam2(long communityId)
        {
            if (communityId < Num32 || !Regex.IsMatch(communityId.ToString(CultureInfo.InvariantCulture), STEAM64))
            {
                return string.Empty;
            }
            communityId -= Num64;
            communityId -= communityId % 0x2;
            string text = $"{STEAMPREFIX2}{communityId % 2}:{communityId / 0x2}";
            return !Regex.IsMatch(text, STEAM2) ? string.Empty : text;
        }
    }
}