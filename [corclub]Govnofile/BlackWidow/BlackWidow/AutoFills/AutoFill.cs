using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.AutoFills
{
    class AutoFill
    {
        public string Application { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("Program: {0}\r\nName: {1}\r\nValue: {2}\r\n\r\n", new object[]
            {
                Application,
                Name,
                Password
            });
        }
    }
}
