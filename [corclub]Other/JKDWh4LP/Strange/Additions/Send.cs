using System;
using System.Net;

namespace Strange.Additions
{
	// Token: 0x02000008 RID: 8
	internal class Send
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002414 File Offset: 0x00000614
		public static void File(string url, string filepath)
		{
			try
			{
				new WebClient().UploadFile(url, "POST", filepath);
			}
			catch (Exception)
			{
			}
		}
	}
}
