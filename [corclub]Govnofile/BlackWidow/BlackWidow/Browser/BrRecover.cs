using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Browser
{
    class BrRecover
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string Application { get; set; }

        public override string ToString()
        {
            return string.Format("Programs: {0}\r\nSite: {1}\r\nUsername: {2}\r\nPassword: {3}\r\n\r\n",

                new object[]
                {
                    Application,
                    URL,
                    Username,
                    Password
                });
        }
    }
}
