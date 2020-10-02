using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E1 RID: 225
	public abstract class HttpPackageInfoBase<TRequestBody> : HttpPackageInfoBase
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000628E File Offset: 0x0000448E
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00006296 File Offset: 0x00004496
		public TRequestBody Body { get; private set; }

		// Token: 0x06000731 RID: 1841 RVA: 0x0000629F File Offset: 0x0000449F
		protected HttpPackageInfoBase(string key, HttpHeaderInfo header, TRequestBody body) : base(key, header)
		{
			this.Body = body;
		}
	}
}
