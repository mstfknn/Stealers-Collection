using System;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;

namespace BrowserPasswords
{
	// Token: 0x02000043 RID: 67
	internal class CSubType : CUtils
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00006458 File Offset: 0x00004658
		public CSubType()
		{
			this.m_Guid = default(Guid);
			this.m_GuidSub = default(Guid);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006478 File Offset: 0x00004678
		internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType)
		{
			this.m_IPStore = PStore;
			this.m_Guid = guidType;
			this.m_GuidSub = guidSubType;
			int num;
			this.m_IPStore.GetSubtypeInfo(KeyType, ref guidType, ref guidSubType, ref num, 0);
			if (num > 0)
			{
				IntPtr ptr = new IntPtr(num);
				PST_TYPEINFO pst_TYPEINFO;
				object obj = Marshal.PtrToStructure(ptr, pst_TYPEINFO.GetType());
				PST_TYPEINFO pst_TYPEINFO2;
				pst_TYPEINFO = ((obj != null) ? ((PST_TYPEINFO)obj) : pst_TYPEINFO2);
				this.m_DisplayName = this.CopyString(pst_TYPEINFO.szDisplayName);
				ptr = new IntPtr(num);
				Marshal.FreeCoTaskMem(ptr);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006508 File Offset: 0x00004708
		public void Delete()
		{
			this.m_IPStore.DeleteSubtype(this.m_KeyType, ref this.m_Guid, ref this.m_GuidSub, 0);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006528 File Offset: 0x00004728
		public string TypeGuid
		{
			get
			{
				return this.m_GuidSub.ToString();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000654C File Offset: 0x0000474C
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x04000197 RID: 407
		private IPStore m_IPStore;

		// Token: 0x04000198 RID: 408
		private Guid m_Guid;

		// Token: 0x04000199 RID: 409
		private Guid m_GuidSub;

		// Token: 0x0400019A RID: 410
		private string m_DisplayName;

		// Token: 0x0400019B RID: 411
		private PST_KEY m_KeyType;
	}
}
