using System;
using System.Collections;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x02000044 RID: 68
	internal class CSubTypes : CUtils, IEnumerable, IEnumerator
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00006564 File Offset: 0x00004764
		public CSubTypes()
		{
			this.m_TypeGuid = default(Guid);
		}

		// Token: 0x060001B8 RID: 440
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(int lpString);

		// Token: 0x060001B9 RID: 441
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenW(int lpString);

		// Token: 0x060001BA RID: 442 RVA: 0x00006578 File Offset: 0x00004778
		internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType)
		{
			this.m_IPStore = PStore;
			this.m_KeyType = KeyType;
			this.m_TypeGuid = guidType;
			this.m_IEnumType = PStore.EnumSubtypes(KeyType, ref guidType, 0);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000065A4 File Offset: 0x000047A4
		public CSubType Item
		{
			get
			{
				int num;
				CSubType csubType;
				int num3;
				object obj;
				try
				{
					Guid guid = default(Guid);
					ProjectData.ClearProjectError();
					num = 2;
					this.m_IEnumType.Reset();
					this.m_IEnumType.Skip(Index);
					int num2;
					this.m_IEnumType.Next(1, ref guid, ref num2);
					csubType = new CSubType();
					csubType.Init(this.m_IPStore, this.m_KeyType, ref this.m_TypeGuid, ref guid);
					IL_57:
					goto IL_9C;
					IL_59:
					num3 = -1;
					@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
					IL_6E:;
				}
				catch when (endfilter(obj is Exception & num != 0 & num3 == 0))
				{
					Exception ex = (Exception)obj2;
					goto IL_59;
				}
				throw ProjectData.CreateProjectError(-2146828237);
				IL_9C:
				CSubType result = csubType;
				if (num3 != 0)
				{
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006668 File Offset: 0x00004868
		public int Count
		{
			get
			{
				checked
				{
					int num;
					int num2;
					int num3;
					object obj;
					try
					{
						ProjectData.ClearProjectError();
						num = 2;
						this.m_IEnumType.Reset();
						for (;;)
						{
							this.m_IEnumType.Skip(1);
							num2++;
						}
						IL_27:
						goto IL_6A;
						IL_29:
						num3 = -1;
						@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
						IL_3D:;
					}
					catch when (endfilter(obj is Exception & num != 0 & num3 == 0))
					{
						Exception ex = (Exception)obj2;
						goto IL_29;
					}
					throw ProjectData.CreateProjectError(-2146828237);
					IL_6A:
					int result = num2;
					if (num3 != 0)
					{
						ProjectData.ClearProjectError();
					}
					return result;
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000066F8 File Offset: 0x000048F8
		public void Delete(Guid guidSubType)
		{
			this.m_IPStore.DeleteSubtype(this.m_KeyType, ref this.m_TypeGuid, ref guidSubType, 0);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006714 File Offset: 0x00004914
		public void Add(Guid guidSubType, string szDisplayName)
		{
			PST_TYPEINFO pst_TYPEINFO;
			pst_TYPEINFO.cbSize = Strings.Len(pst_TYPEINFO);
			pst_TYPEINFO.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
			PST_ACCESSRULESET pst_ACCESSRULESET;
			pst_ACCESSRULESET.cbSize = Strings.Len(pst_ACCESSRULESET);
			pst_ACCESSRULESET.cRules = 0;
			pst_ACCESSRULESET.rgRules = 0;
			this.m_IPStore.CreateSubtype(this.m_KeyType, ref this.m_TypeGuid, ref guidSubType, ref pst_TYPEINFO, ref pst_ACCESSRULESET, 0);
			Marshal.FreeHGlobal(pst_TYPEINFO.szDisplayName);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000678C File Offset: 0x0000498C
		public IEnumerator GetEnumerator()
		{
			this.Reset();
			return this;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000067A4 File Offset: 0x000049A4
		public void Reset()
		{
			this.m_IEnumType.Reset();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000067B4 File Offset: 0x000049B4
		public object Current
		{
			get
			{
				return this.m_Current;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000067CC File Offset: 0x000049CC
		public bool MoveNext()
		{
			Guid guid = default(Guid);
			int num2;
			int num = this.m_IEnumType.Next(1, ref guid, ref num2);
			bool result;
			if (num != 0)
			{
				result = false;
			}
			else
			{
				this.m_Current = new CSubType();
				this.m_Current.Init(this.m_IPStore, this.m_KeyType, ref this.m_TypeGuid, ref guid);
				result = true;
			}
			return result;
		}

		// Token: 0x0400019C RID: 412
		private IPStore m_IPStore;

		// Token: 0x0400019D RID: 413
		private PST_KEY m_KeyType;

		// Token: 0x0400019E RID: 414
		private Guid m_TypeGuid;

		// Token: 0x0400019F RID: 415
		private IEnumPStoreTypes m_IEnumType;

		// Token: 0x040001A0 RID: 416
		private CSubType m_Current;
	}
}
