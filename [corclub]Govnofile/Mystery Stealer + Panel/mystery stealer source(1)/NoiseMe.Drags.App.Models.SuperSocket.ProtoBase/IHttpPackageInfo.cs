namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IHttpPackageInfo : IPackageInfo<string>, IPackageInfo
	{
		HttpHeaderInfo Header
		{
			get;
		}
	}
}
