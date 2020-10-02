using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Cookie
{
    class CookieChromium
    {
        public string HostKey { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public string ExpiresUTC { get; set; }
        public string LastAccessUTC { get; set; }
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }
        public bool Expired { get; set; }
        public bool Persistent { get; set; }
        public bool Priority { get; set; }
        public string Browser { get; set; }

        public override string ToString()
        {
            return string.Format("{12}=================== {0} ==================={12} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12}", new object[]
            {
                    Browser,
                    HostKey,
                    Name,
                    Value,
                    Path,
                    ExpiresUTC,
                    LastAccessUTC,
                    Secure,
                    HttpOnly,
                    Expired,
                    Persistent,
                    Priority,
                    Environment.NewLine,
            });
        }

        //public override string ToString()
        //{
        //    return string.Format("Domain: {1}{0}Cookie Name: {2}{0}Value: {3}{0}Path: {4}{0}Expired: {5}{0}HttpOnly: {6}{0}Secure: {7}", new object[]
        //    {
        //            Environment.NewLine,
        //            this.HostKey,
        //            this.Name,
        //            this.Value,
        //            this.Path,
        //            this.Expired,
        //            this.HttpOnly,
        //            this.Secure
        //    });
        //}
    }
}
