using System;
using System.IO.Compression;

namespace Strange.Additions
{
	// Token: 0x02000007 RID: 7
	internal class Packer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000023E8 File Offset: 0x000005E8
		public static void Zip(string dir, string zipPath)
		{
			try
			{
				ZipFile.CreateFromDirectory(dir, zipPath, CompressionLevel.Fastest, false);
			}
			catch (Exception)
			{
			}
		}
	}
}
