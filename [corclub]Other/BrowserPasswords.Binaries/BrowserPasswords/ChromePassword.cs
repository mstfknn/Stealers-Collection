using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords
{
	// Token: 0x02000008 RID: 8
	public class ChromePassword
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002108 File Offset: 0x00000308
		public ChromePassword(string origin_url, string action_url, string user_element, string user_value, string pass_element, object pass_value, string submit_element, string signon_realm, int ssl_valid, int creation_date, int blacklisted)
		{
			this.m_ActionURL = action_url;
			this.m_Blacklisted = (blacklisted != 0);
			checked
			{
				long num = unchecked((long)creation_date) * 10000000L;
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
				DateTime dateTime2 = dateTime;
				this.m_CreationDate = new DateTime(num + dateTime2.Ticks);
				this.m_OriginURL = origin_url;
				this.m_PasswordElement = pass_element;
				this.m_PasswordValue = (byte[])pass_value;
				this.m_SignonRealm = signon_realm;
				this.m_SSLValid = (ssl_valid != 0);
				this.m_SubmitElement = submit_element;
				this.m_UsernameElement = user_element;
				this.m_UsernameValue = user_value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021AC File Offset: 0x000003AC
		public string OriginURL
		{
			get
			{
				return this.m_OriginURL;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000021C4 File Offset: 0x000003C4
		public string ActionURL
		{
			get
			{
				return this.m_ActionURL;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021DC File Offset: 0x000003DC
		public string UsernameElement
		{
			get
			{
				return this.m_UsernameElement;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021F4 File Offset: 0x000003F4
		public string UsernameValue
		{
			get
			{
				return this.m_UsernameValue;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000220C File Offset: 0x0000040C
		public string PasswordElement
		{
			get
			{
				return this.m_PasswordElement;
			}
		}

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int LocalFree(IntPtr hMem);

		// Token: 0x06000024 RID: 36
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref ChromePassword.DATA_BLOB pDataIn, IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref ChromePassword.DATA_BLOB pDataOut);

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002224 File Offset: 0x00000424
		public string PasswordValue
		{
			get
			{
				IntPtr intPtr = Marshal.AllocHGlobal(this.m_PasswordValue.Length);
				Marshal.Copy(this.m_PasswordValue, 0, intPtr, this.m_PasswordValue.Length);
				ChromePassword.DATA_BLOB data_BLOB = default(ChromePassword.DATA_BLOB);
				data_BLOB.cbData = this.m_PasswordValue.Length;
				data_BLOB.pbData = intPtr;
				ChromePassword.DATA_BLOB data_BLOB2 = default(ChromePassword.DATA_BLOB);
				ChromePassword.CryptUnprotectData(ref data_BLOB, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, ref data_BLOB2);
				string result = Marshal.PtrToStringAnsi(data_BLOB2.pbData, data_BLOB2.cbData);
				Marshal.FreeHGlobal(intPtr);
				return result;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022BC File Offset: 0x000004BC
		public string SubmitElement
		{
			get
			{
				return this.m_SubmitElement;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022D4 File Offset: 0x000004D4
		public string SignonRealm
		{
			get
			{
				return this.m_SignonRealm;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022EC File Offset: 0x000004EC
		public bool SSLValid
		{
			get
			{
				return this.m_SSLValid;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002304 File Offset: 0x00000504
		public DateTime CreationDate
		{
			get
			{
				return this.m_CreationDate;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000231C File Offset: 0x0000051C
		public bool Blacklisted
		{
			get
			{
				return this.m_Blacklisted;
			}
		}

		// Token: 0x04000009 RID: 9
		private string m_OriginURL;

		// Token: 0x0400000A RID: 10
		private string m_ActionURL;

		// Token: 0x0400000B RID: 11
		private string m_UsernameElement;

		// Token: 0x0400000C RID: 12
		private string m_UsernameValue;

		// Token: 0x0400000D RID: 13
		private string m_PasswordElement;

		// Token: 0x0400000E RID: 14
		private byte[] m_PasswordValue;

		// Token: 0x0400000F RID: 15
		private string m_SubmitElement;

		// Token: 0x04000010 RID: 16
		private string m_SignonRealm;

		// Token: 0x04000011 RID: 17
		private bool m_SSLValid;

		// Token: 0x04000012 RID: 18
		private DateTime m_CreationDate;

		// Token: 0x04000013 RID: 19
		private bool m_Blacklisted;

		// Token: 0x02000009 RID: 9
		private struct DATA_BLOB
		{
			// Token: 0x04000014 RID: 20
			public int cbData;

			// Token: 0x04000015 RID: 21
			public IntPtr pbData;
		}
	}
}
