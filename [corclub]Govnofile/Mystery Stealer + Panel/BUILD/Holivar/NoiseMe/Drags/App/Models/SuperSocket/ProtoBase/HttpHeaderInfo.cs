using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000DD RID: 221
	public class HttpHeaderInfo : Dictionary<string, string>
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x000061B4 File Offset: 0x000043B4
		public HttpHeaderInfo() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000061C1 File Offset: 0x000043C1
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x000061C9 File Offset: 0x000043C9
		public string Method { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000061D2 File Offset: 0x000043D2
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x000061DA File Offset: 0x000043DA
		public string Path { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000061E3 File Offset: 0x000043E3
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x000061EB File Offset: 0x000043EB
		public string Version { get; set; }

		// Token: 0x0600071F RID: 1823 RVA: 0x0001B97C File Offset: 0x00019B7C
		public string Get(string key)
		{
			string empty = string.Empty;
			base.TryGetValue(key, out empty);
			return empty;
		}
	}
}
