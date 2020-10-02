namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class PackageInfo<TKey, TBody> : IPackageInfo<TKey>, IPackageInfo
	{
		public TKey Key
		{
			get;
			private set;
		}

		public TBody Body
		{
			get;
			private set;
		}

		public PackageInfo(TKey key, TBody body)
		{
			Key = key;
			Body = body;
		}
	}
}
