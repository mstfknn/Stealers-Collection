using System;
using System.IO;

namespace Reborn.Helper
{
	// Token: 0x0200001C RID: 28
	internal class Misc
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00002D70 File Offset: 0x00000F70
		public static string GetRandomString()
		{
			return Path.GetRandomFileName().Replace(".", "");
		}
	}
}
