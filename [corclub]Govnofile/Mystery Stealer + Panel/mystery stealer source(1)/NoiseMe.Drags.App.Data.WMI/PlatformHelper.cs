using System;
using System.Text.RegularExpressions;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class PlatformHelper
	{
		public static string FullName
		{
			get;
			private set;
		}

		public static string Name
		{
			get;
			private set;
		}

		public static bool Is64Bit
		{
			get;
			private set;
		}

		public static bool RunningOnMono
		{
			get;
			private set;
		}

		public static bool Win32NT
		{
			get;
			private set;
		}

		public static bool XpOrHigher
		{
			get;
			private set;
		}

		public static bool VistaOrHigher
		{
			get;
			private set;
		}

		public static bool SevenOrHigher
		{
			get;
			private set;
		}

		public static bool EightOrHigher
		{
			get;
			private set;
		}

		public static bool EightPointOneOrHigher
		{
			get;
			private set;
		}

		public static bool TenOrHigher
		{
			get;
			private set;
		}

		static PlatformHelper()
		{
			Win32NT = (Environment.OSVersion.Platform == PlatformID.Win32NT);
			XpOrHigher = (Win32NT && Environment.OSVersion.Version.Major >= 5);
			VistaOrHigher = (Win32NT && Environment.OSVersion.Version.Major >= 6);
			SevenOrHigher = (Win32NT && Environment.OSVersion.Version >= new Version(6, 1));
			EightOrHigher = (Win32NT && Environment.OSVersion.Version >= new Version(6, 2, 9200));
			EightPointOneOrHigher = (Win32NT && Environment.OSVersion.Version >= new Version(6, 3));
			TenOrHigher = (Win32NT && Environment.OSVersion.Version >= new Version(10, 0));
			RunningOnMono = (Type.GetType("Mono.Runtime") != null);
			Name = "Unknown OS";
			try
			{
				Name = OsGatherer.GetCaption();
			}
			catch
			{
			}
			Name = Regex.Replace(Name, "^.*(?=Windows)", "").TrimEnd().TrimStart();
			string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
			Is64Bit = ((!string.IsNullOrEmpty(environmentVariable) && string.Compare(environmentVariable, 0, "x86", 0, 3, ignoreCase: true) != 0) ? true : false);
			FullName = $"{Name} {(Is64Bit ? 64 : 32)} Bit";
		}
	}
}
