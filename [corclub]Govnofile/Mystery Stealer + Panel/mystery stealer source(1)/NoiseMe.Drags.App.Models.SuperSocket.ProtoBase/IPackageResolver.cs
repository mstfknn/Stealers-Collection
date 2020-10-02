namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IPackageResolver<out TPackageInfo> where TPackageInfo : IPackageInfo
	{
		TPackageInfo ResolvePackage(IBufferStream bufferStream);
	}
}
