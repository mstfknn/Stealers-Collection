namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IReceiveFilter<out TPackageInfo> where TPackageInfo : IPackageInfo
	{
		IReceiveFilter<TPackageInfo> NextReceiveFilter
		{
			get;
		}

		FilterState State
		{
			get;
		}

		TPackageInfo Filter(BufferList data, out int rest);

		void Reset();
	}
}
