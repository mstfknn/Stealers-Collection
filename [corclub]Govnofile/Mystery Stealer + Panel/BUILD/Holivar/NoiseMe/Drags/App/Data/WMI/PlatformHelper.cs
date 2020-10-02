using System;
using System.Text.RegularExpressions;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000173 RID: 371
	public static class PlatformHelper
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00023F1C File Offset: 0x0002211C
		static PlatformHelper()
		{
			PlatformHelper.RunningOnMono = (Type.GetType("Mono.Runtime") != null);
			PlatformHelper.Name = "Unknown OS";
			try
			{
				PlatformHelper.Name = OsGatherer.GetCaption();
			}
			catch
			{
			}
			PlatformHelper.Name = Regex.Replace(PlatformHelper.Name, "^.*(?=Windows)", "").TrimEnd(new char[0]).TrimStart(new char[0]);
			string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
			PlatformHelper.Is64Bit = (!string.IsNullOrEmpty(environmentVariable) && string.Compare(environmentVariable, 0, "x86", 0, 3, true) != 0);
			PlatformHelper.FullName = string.Format("{0} {1} Bit", PlatformHelper.Name, PlatformHelper.Is64Bit ? 64 : 32);
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0000946B File Offset: 0x0000766B
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00009472 File Offset: 0x00007672
		public static string FullName { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0000947A File Offset: 0x0000767A
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00009481 File Offset: 0x00007681
		public static string Name { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00009489 File Offset: 0x00007689
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00009490 File Offset: 0x00007690
		public static bool Is64Bit { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00009498 File Offset: 0x00007698
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0000949F File Offset: 0x0000769F
		public static bool RunningOnMono { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000094A7 File Offset: 0x000076A7
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000094AE File Offset: 0x000076AE
		public static bool Win32NT { get; private set; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000094B6 File Offset: 0x000076B6
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x000094BD File Offset: 0x000076BD
		public static bool XpOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 5;

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000094C5 File Offset: 0x000076C5
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x000094CC File Offset: 0x000076CC
		public static bool VistaOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 6;

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000094D4 File Offset: 0x000076D4
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x000094DB File Offset: 0x000076DB
		public static bool SevenOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 1);

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000094E3 File Offset: 0x000076E3
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x000094EA File Offset: 0x000076EA
		public static bool EightOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 2, 9200);

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x000094F2 File Offset: 0x000076F2
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x000094F9 File Offset: 0x000076F9
		public static bool EightPointOneOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 3);

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00009501 File Offset: 0x00007701
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00009508 File Offset: 0x00007708
		public static bool TenOrHigher { get; private set; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(10, 0);
	}
}
