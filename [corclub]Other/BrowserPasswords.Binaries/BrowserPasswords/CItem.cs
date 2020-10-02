using System;
using System.Runtime.InteropServices;
using System.Text;
using BrowserPasswords.PStoreLib;
using Microsoft.VisualBasic;

namespace BrowserPasswords
{
	// Token: 0x02000040 RID: 64
	internal class CItem : CUtils
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public CItem()
		{
			this.m_Guid = default(Guid);
			this.m_SubGuid = default(Guid);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005CD8 File Offset: 0x00003ED8
		private void FreeStruct(IntPtr ptr)
		{
			Marshal.FreeCoTaskMem(ptr);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005CE0 File Offset: 0x00003EE0
		internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType, string szItemName)
		{
			this.m_IPStore = PStore;
			this.m_Guid = guidType;
			this.m_SubGuid = guidSubType;
			this.m_Name = szItemName;
			this.m_KeyType = KeyType;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005D14 File Offset: 0x00003F14
		public void Delete()
		{
			this.m_IPStore.DeleteItem(this.m_KeyType, ref this.m_Guid, ref this.m_SubGuid, this.m_Name, 0, 0);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005D3C File Offset: 0x00003F3C
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005D54 File Offset: 0x00003F54
		public byte[] ReadBinary()
		{
			byte[] array = null;
			int num;
			IntPtr intPtr;
			this.m_IPStore.ReadItem(this.m_KeyType, ref this.m_Guid, ref this.m_SubGuid, this.m_Name, ref num, ref intPtr, 0, 0);
			if (intPtr != IntPtr.Zero)
			{
				array = new byte[checked(num - 1 + 1)];
				Marshal.Copy(intPtr, array, 0, num);
				this.FreeStruct(intPtr);
			}
			return array;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005DBC File Offset: 0x00003FBC
		public string ReadBinaryString()
		{
			byte[] array = this.ReadBinary();
			StringBuilder stringBuilder = new StringBuilder();
			checked
			{
				int num = array.Length - 1;
				int num2 = 0;
				int num3 = num;
				for (int i = num2; i <= num3; i++)
				{
					stringBuilder.Append(Conversion.Hex(array[i]) + " ");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0400018A RID: 394
		private IPStore m_IPStore;

		// Token: 0x0400018B RID: 395
		private Guid m_Guid;

		// Token: 0x0400018C RID: 396
		private Guid m_SubGuid;

		// Token: 0x0400018D RID: 397
		private string m_Name;

		// Token: 0x0400018E RID: 398
		private PST_KEY m_KeyType;
	}
}
