namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class HttpPackageInfoBase : IHttpPackageInfo, IPackageInfo<string>, IPackageInfo
	{
		public string Key
		{
			get;
			private set;
		}

		public HttpHeaderInfo Header
		{
			get;
			private set;
		}

		protected HttpPackageInfoBase(string key, HttpHeaderInfo header)
		{
			Key = key;
			Header = header;
		}
	}
	public abstract class HttpPackageInfoBase<TRequestBody> : HttpPackageInfoBase
	{
		public TRequestBody Body
		{
			get;
			private set;
		}

		protected HttpPackageInfoBase(string key, HttpHeaderInfo header, TRequestBody body)
			: base(key, header)
		{
			Body = body;
		}
	}
}
