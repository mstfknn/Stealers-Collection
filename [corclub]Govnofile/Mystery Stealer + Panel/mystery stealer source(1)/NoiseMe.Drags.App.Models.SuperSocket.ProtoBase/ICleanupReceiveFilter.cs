namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface ICleanupReceiveFilter<out TPackageInfo>
	{
		TPackageInfo ResolvePackage(BufferList data);
	}
}
