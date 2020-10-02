using System;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F2 RID: 242
	public static class MimeHeaderHelper
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public static void ParseHttpHeader(string headerData, HttpHeaderInfo header)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			StringReader stringReader = new StringReader(headerData);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string text4;
			while (!string.IsNullOrEmpty(text4 = stringReader.ReadLine()))
			{
				if (string.IsNullOrEmpty(text))
				{
					text = text4;
				}
				else if (text4.StartsWith("\t") && !string.IsNullOrEmpty(text2))
				{
					text3 += text4.Trim();
					header[text2] = text3;
				}
				else
				{
					int num = text4.IndexOf(':');
					if (num > 0)
					{
						string text5 = text4.Substring(0, num);
						if (!string.IsNullOrEmpty(text5))
						{
							text5 = text5.Trim();
						}
						int num2 = num + 1;
						if (text4.Length > num2)
						{
							string text6 = text4.Substring(num2);
							if (!string.IsNullOrEmpty(text6) && text6.StartsWith(" ") && text6.Length > 1)
							{
								text6 = text6.Substring(1);
							}
							if (!string.IsNullOrEmpty(text5))
							{
								if (dictionary.ContainsKey(text5))
								{
									text3 = header[text5] + ", " + text6;
									header[text5] = text3;
								}
								else
								{
									text3 = (header[text5] = text6);
									dictionary.Add(text5, text5);
								}
								text2 = text5;
							}
						}
					}
				}
			}
			string[] array = text.Split(new char[]
			{
				' '
			});
			header.Method = array[0];
			header.Path = array[1];
			header.Version = array[2];
		}

		// Token: 0x040002F5 RID: 757
		private const string m_Tab = "\t";

		// Token: 0x040002F6 RID: 758
		private const char m_Colon = ':';

		// Token: 0x040002F7 RID: 759
		private const string m_Space = " ";

		// Token: 0x040002F8 RID: 760
		private const char m_SpaceChar = ' ';

		// Token: 0x040002F9 RID: 761
		private const string m_ValueSeparator = ", ";
	}
}
