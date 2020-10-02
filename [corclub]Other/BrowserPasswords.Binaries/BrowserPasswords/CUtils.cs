using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords
{
	// Token: 0x02000047 RID: 71
	public class CUtils
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00006BC8 File Offset: 0x00004DC8
		protected string CopyString(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006BE0 File Offset: 0x00004DE0
		protected int getStrLengthA(byte[] str)
		{
			int num = 0;
			checked
			{
				foreach (byte b in str)
				{
					num++;
					if (b == 0)
					{
						return num;
					}
				}
				return num;
			}
		}
	}
}
