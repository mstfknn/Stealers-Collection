using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F1 RID: 241
	public interface IStringParser
	{
		// Token: 0x0600074D RID: 1869
		void Parse(string source, out string key, out string body, out string[] parameters);
	}
}
