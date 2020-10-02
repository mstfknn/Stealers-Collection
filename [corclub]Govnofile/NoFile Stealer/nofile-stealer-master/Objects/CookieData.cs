using System;
using System.Collections.Generic;
using System.Text;

namespace NoFile
{
    public class CookieData
    {
        public string host_key { get; set; }

        public string name { get; set; }

        public string value { get; set; }

        public string path { get; set; }

        public string expires_utc { get; set; }

        public string secure { get; set; }

        public override string ToString()
        {
            //return (string)null;
            return string.Format("{0}\tFALSE\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", 
                (object)this.host_key, (object)this.path, (object)this.secure.ToUpper(), (object)this.expires_utc, (object)this.name, (object)this.value);
        }
    }
}
