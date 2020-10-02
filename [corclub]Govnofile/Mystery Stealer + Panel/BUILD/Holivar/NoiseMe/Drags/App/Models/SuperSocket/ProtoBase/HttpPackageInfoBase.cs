using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E0 RID: 224
	public abstract class HttpPackageInfoBase : IHttpPackageInfo, IPackageInfo<string>, IPackageInfo
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00006256 File Offset: 0x00004456
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000625E File Offset: 0x0000445E
		public string Key { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00006267 File Offset: 0x00004467
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0000626F File Offset: 0x0000446F
		public HttpHeaderInfo Header { get; private set; }

		// Token: 0x0600072E RID: 1838 RVA: 0x00006278 File Offset: 0x00004478
		protected HttpPackageInfoBase(string key, HttpHeaderInfo header)
		{
			this.Key = key;
			this.Header = header;
		}
	}
}
