using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000008 RID: 8
[StandardModule]
internal sealed class Class3
{
	// Token: 0x06000020 RID: 32
	[DllImport("Crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool CryptUnprotectData(ref Class3.Struct1 struct1_0, string string_0, ref Class3.Struct1 struct1_1, IntPtr intptr_0, ref Class3.Struct0 struct0_0, int int_0, ref Class3.Struct1 struct1_2);

	// Token: 0x06000021 RID: 33 RVA: 0x000024BC File Offset: 0x000006BC
	public static string smethod_0(byte[] byte_0)
	{
		Class3.Struct1 @struct = default(Class3.Struct1);
		Class3.Struct1 struct2 = default(Class3.Struct1);
		GCHandle gchandle = GCHandle.Alloc(byte_0, GCHandleType.Pinned);
		@struct.intptr_0 = gchandle.AddrOfPinnedObject();
		@struct.int_0 = byte_0.Length;
		gchandle.Free();
		string string_ = null;
		Class3.Struct1 struct4;
		Class3.Struct1 struct3 = struct4;
		IntPtr intPtr;
		IntPtr intptr_ = intPtr;
		Class3.Struct0 struct6;
		Class3.Struct0 struct5 = struct6;
		Class3.CryptUnprotectData(ref @struct, string_, ref struct3, intptr_, ref struct5, 0, ref struct2);
		checked
		{
			byte[] array = new byte[struct2.int_0 + 1];
			Marshal.Copy(struct2.intptr_0, array, 0, struct2.int_0);
			string @string = Encoding.Default.GetString(array);
			return @string.Substring(0, @string.Length - 1);
		}
	}

	// Token: 0x02000009 RID: 9
	[Flags]
	public enum Enum0
	{
		// Token: 0x0400000A RID: 10
		CRYPTPROTECT_PROMPT_ON_UNPROTECT = 1,
		// Token: 0x0400000B RID: 11
		CRYPTPROTECT_PROMPT_ON_PROTECT = 2
	}

	// Token: 0x0200000A RID: 10
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct Struct0
	{
		// Token: 0x0400000C RID: 12
		public int int_0;

		// Token: 0x0400000D RID: 13
		public Class3.Enum0 enum0_0;

		// Token: 0x0400000E RID: 14
		public IntPtr intptr_0;

		// Token: 0x0400000F RID: 15
		public string string_0;
	}

	// Token: 0x0200000B RID: 11
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct Struct1
	{
		// Token: 0x04000010 RID: 16
		public int int_0;

		// Token: 0x04000011 RID: 17
		public IntPtr intptr_0;
	}
}
