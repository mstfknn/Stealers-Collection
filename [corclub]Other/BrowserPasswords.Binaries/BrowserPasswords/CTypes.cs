using System;
using System.Collections;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x02000046 RID: 70
	internal class CTypes : CUtils, IEnumerator, IEnumerable
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000694C File Offset: 0x00004B4C
		internal CTypes(IPStore PStore, PST_KEY KeyType)
		{
			this.m_IPStore = PStore;
			this.m_KeyType = KeyType;
			this.m_IEnumType = PStore.EnumTypes(KeyType, 0);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006970 File Offset: 0x00004B70
		public CType_Renamed Item
		{
			get
			{
				int num;
				CType_Renamed ctype_Renamed;
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
					ctype_Renamed = new CType_Renamed();
					ctype_Renamed.Init(this.m_IPStore, this.m_KeyType, ref guid);
					IL_51:
					goto IL_96;
					IL_53:
					num3 = -1;
					@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
					IL_68:;
				}
				catch when (endfilter(obj is Exception & num != 0 & num3 == 0))
				{
					Exception ex = (Exception)obj2;
					goto IL_53;
				}
				throw ProjectData.CreateProjectError(-2146828237);
				IL_96:
				CType_Renamed result = ctype_Renamed;
				if (num3 != 0)
				{
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006A30 File Offset: 0x00004C30
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

		// Token: 0x060001CC RID: 460 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public void Delete(Guid guidType)
		{
			this.m_IPStore.DeleteType(this.m_KeyType, ref guidType, 0);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public void Add(Guid guidType, string szDisplayName)
		{
			PST_TYPEINFO pst_TYPEINFO;
			pst_TYPEINFO.cbSize = Strings.Len(pst_TYPEINFO);
			pst_TYPEINFO.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
			this.m_IPStore.CreateType(this.m_KeyType, ref guidType, ref pst_TYPEINFO, 0);
			Marshal.FreeHGlobal(pst_TYPEINFO.szDisplayName);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006B28 File Offset: 0x00004D28
		public IEnumerator GetEnumerator()
		{
			this.Reset();
			return this;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006B40 File Offset: 0x00004D40
		public void Reset()
		{
			this.m_IEnumType.Reset();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006B50 File Offset: 0x00004D50
		public object Current
		{
			get
			{
				return this.m_Current;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006B68 File Offset: 0x00004D68
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
				this.m_Current = new CType_Renamed();
				this.m_Current.Init(this.m_IPStore, this.m_KeyType, ref guid);
				result = true;
			}
			return result;
		}

		// Token: 0x040001A6 RID: 422
		private IPStore m_IPStore;

		// Token: 0x040001A7 RID: 423
		private PST_KEY m_KeyType;

		// Token: 0x040001A8 RID: 424
		private IEnumPStoreTypes m_IEnumType;

		// Token: 0x040001A9 RID: 425
		private CType_Renamed m_Current;
	}
}
