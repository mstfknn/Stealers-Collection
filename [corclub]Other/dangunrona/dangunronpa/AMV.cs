using System;
using System.IO;
using System.Net;

// Token: 0x02000002 RID: 2
public static class AMV
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static string DontKillMe(string efwrgfwrgfrwg848)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(efwrgfwrgfrwg848);
		httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		return new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
	}
}
