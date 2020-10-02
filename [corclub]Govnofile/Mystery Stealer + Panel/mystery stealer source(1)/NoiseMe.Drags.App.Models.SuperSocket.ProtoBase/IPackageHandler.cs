namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IPackageHandler<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		void Handle(TPackageInfo package);
	}
}
