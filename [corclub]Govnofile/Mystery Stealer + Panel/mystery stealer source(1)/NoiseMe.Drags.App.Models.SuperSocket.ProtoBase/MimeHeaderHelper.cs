using System;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public static class MimeHeaderHelper
	{
		private const string m_Tab = "\t";

		private const char m_Colon = ':';

		private const string m_Space = " ";

		private const char m_SpaceChar = ' ';

		private const string m_ValueSeparator = ", ";

		public static void ParseHttpHeader(string headerData, HttpHeaderInfo header)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string str = string.Empty;
			StringReader stringReader = new StringReader(headerData);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string text3;
			while (!string.IsNullOrEmpty(text3 = stringReader.ReadLine()))
			{
				if (string.IsNullOrEmpty(text))
				{
					text = text3;
					continue;
				}
				if (text3.StartsWith("\t") && !string.IsNullOrEmpty(text2))
				{
					str = (header[text2] = str + text3.Trim());
					continue;
				}
				int num = text3.IndexOf(':');
				if (num <= 0)
				{
					continue;
				}
				string text5 = text3.Substring(0, num);
				if (!string.IsNullOrEmpty(text5))
				{
					text5 = text5.Trim();
				}
				int num2 = num + 1;
				if (text3.Length <= num2)
				{
					continue;
				}
				string text6 = text3.Substring(num2);
				if (!string.IsNullOrEmpty(text6) && text6.StartsWith(" ") && text6.Length > 1)
				{
					text6 = text6.Substring(1);
				}
				if (!string.IsNullOrEmpty(text5))
				{
					if (dictionary.ContainsKey(text5))
					{
						str = (header[text5] = header[text5] + ", " + text6);
					}
					else
					{
						str = (header[text5] = text6);
						dictionary.Add(text5, text5);
					}
					text2 = text5;
				}
			}
			string[] array = text.Split(' ');
			header.Method = array[0];
			header.Path = array[1];
			header.Version = array[2];
		}
	}
}
