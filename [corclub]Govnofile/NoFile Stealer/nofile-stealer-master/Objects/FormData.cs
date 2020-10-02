using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace NoFile
{
    public class FormData
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}\r\nValue: {1}\r\n----------------------------\r\n", (object)this.Name, (object)this.Value);
        }
    }
}

