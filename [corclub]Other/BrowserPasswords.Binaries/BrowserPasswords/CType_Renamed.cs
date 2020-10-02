using System;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;

namespace BrowserPasswords
{
	// Token: 0x02000045 RID: 69
	internal class CType_Renamed : CUtils
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000682C File Offset: 0x00004A2C
		public CType_Renamed()
		{
			this.m_Guid = default(Guid);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006840 File Offset: 0x00004A40
		internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType)
		{
			this.m_IPStore = PStore;
			this.m_Guid = guidType;
			int num;
			this.m_IPStore.GetTypeInfo(KeyType, ref guidType, ref num, 0);
			if (num != 0)
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
			this.m_SubTypes = new CSubTypes();
			this.m_SubTypes.Init(PStore, KeyType, ref guidType);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000068DC File Offset: 0x00004ADC
		public void Delete()
		{
			this.m_IPStore.DeleteType(this.m_KeyType, ref this.m_Guid, 0);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000068F8 File Offset: 0x00004AF8
		public string TypeGuid
		{
			get
			{
				return this.m_Guid.ToString();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000691C File Offset: 0x00004B1C
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006934 File Offset: 0x00004B34
		public CSubTypes SubTypes
		{
			get
			{
				return this.m_SubTypes;
			}
		}

		// Token: 0x040001A1 RID: 417
		private IPStore m_IPStore;

		// Token: 0x040001A2 RID: 418
		private Guid m_Guid;

		// Token: 0x040001A3 RID: 419
		private string m_DisplayName;

		// Token: 0x040001A4 RID: 420
		private PST_KEY m_KeyType;

		// Token: 0x040001A5 RID: 421
		private CSubTypes m_SubTypes;
	}
}
