namespace PEngine.Engine.Applications.Steam
{
    using PEngine.Helpers;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SteamProfiles
    {
        private static readonly string LoginFile = CombineEx.Combination(SteamPath.GetLocationSteam(), @"config\loginusers.vdf");
        private static StringBuilder Sb = new StringBuilder();

        public static string GetSteamID()
        {
            foreach (Match match in new Regex("(\\d{17})|\"\"\\s+\"(\\S+)\"").Matches(File.ReadAllText(LoginFile)))
            {
                try
                {
                    if (match.Success)
                    {
                        if (Regex.IsMatch(match.Groups[1].Value, SteamConverter.STEAM64))
                        {
                            string ConvertID = SteamConverter.FromSteam64ToSteam2(Convert.ToInt64(match.Groups[1].Value));
                            string ConvertSteam3 = $"{SteamConverter.STEAMPREFIX}{SteamConverter.FromSteam64ToSteam32(Convert.ToInt64(match.Groups[1].Value)).ToString(CultureInfo.InvariantCulture)}";

                            Sb.AppendLine($"Steam2ID: {ConvertID}");
                            Sb.AppendLine($"Steam3_x32: {ConvertSteam3}");
                            Sb.AppendLine($"Steam3_x64: {match.Groups[1].Value}");
                            Sb.AppendLine($"User Profile: {SteamConverter.HTTPS}{match.Groups[1].Value}{Environment.NewLine}");
                        }
                    }
                }
                catch { }
            }
            return Sb?.ToString();
        }
    }
}