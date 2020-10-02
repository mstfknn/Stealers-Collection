using System;
using System.IO;

namespace Strange.Additions
{
	// Token: 0x02000006 RID: 6
	internal class NameR
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002070 File Offset: 0x00000270
		public static string GetName()
		{
			return Path.GetRandomFileName().Replace(".", "");
		}
	}
}
