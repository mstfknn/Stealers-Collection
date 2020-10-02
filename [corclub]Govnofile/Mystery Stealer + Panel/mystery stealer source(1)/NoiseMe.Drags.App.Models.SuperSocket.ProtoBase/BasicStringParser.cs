using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class BasicStringParser : IStringParser
	{
		private const string SPACE = " ";

		private readonly string m_Spliter;

		private readonly string[] m_ParameterSpliters;

		public static readonly BasicStringParser DefaultInstance = new BasicStringParser();

		public BasicStringParser()
			: this(" ", " ")
		{
		}

		public BasicStringParser(string spliter, string parameterSpliter)
		{
			m_Spliter = spliter;
			m_ParameterSpliters = new string[1]
			{
				parameterSpliter
			};
		}

		public void Parse(string source, out string key, out string body, out string[] parameters)
		{
			int num = source.IndexOf(m_Spliter);
			if (num > 0)
			{
				key = source.Substring(0, num);
				body = source.Substring(num + 1);
				parameters = body.Split(m_ParameterSpliters, StringSplitOptions.RemoveEmptyEntries);
			}
			else
			{
				key = (body = source);
				parameters = null;
			}
		}
	}
}
