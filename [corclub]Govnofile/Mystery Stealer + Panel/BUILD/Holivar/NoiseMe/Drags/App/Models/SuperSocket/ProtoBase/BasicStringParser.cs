using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000CC RID: 204
	public class BasicStringParser : IStringParser
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00005EF7 File Offset: 0x000040F7
		public BasicStringParser() : this(" ", " ")
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00005F09 File Offset: 0x00004109
		public BasicStringParser(string spliter, string parameterSpliter)
		{
			this.m_Spliter = spliter;
			this.m_ParameterSpliters = new string[]
			{
				parameterSpliter
			};
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001AC54 File Offset: 0x00018E54
		public void Parse(string source, out string key, out string body, out string[] parameters)
		{
			int num = source.IndexOf(this.m_Spliter);
			if (num > 0)
			{
				key = source.Substring(0, num);
				body = source.Substring(num + 1);
				parameters = body.Split(this.m_ParameterSpliters, StringSplitOptions.RemoveEmptyEntries);
				return;
			}
			body = source;
			key = source;
			parameters = null;
		}

		// Token: 0x040002C5 RID: 709
		private const string SPACE = " ";

		// Token: 0x040002C6 RID: 710
		private readonly string m_Spliter;

		// Token: 0x040002C7 RID: 711
		private readonly string[] m_ParameterSpliters;

		// Token: 0x040002C8 RID: 712
		public static readonly BasicStringParser DefaultInstance = new BasicStringParser();
	}
}
