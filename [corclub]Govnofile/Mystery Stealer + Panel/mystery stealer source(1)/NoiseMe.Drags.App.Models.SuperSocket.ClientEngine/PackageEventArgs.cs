using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;
using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class PackageEventArgs<TPackageInfo> : EventArgs where TPackageInfo : IPackageInfo
	{
		public TPackageInfo Package
		{
			get;
			private set;
		}

		public PackageEventArgs(TPackageInfo package)
		{
			Package = package;
		}
	}
}
