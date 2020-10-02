namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IPackageInfo
	{
	}
	public interface IPackageInfo<out TKey> : IPackageInfo
	{
		TKey Key
		{
			get;
		}
	}
}
