using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;
using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class EasyClient : EasyClientBase
	{
		private Action<IPackageInfo> m_Handler;

		public void Initialize<TPackageInfo>(IReceiveFilter<TPackageInfo> receiveFilter, Action<TPackageInfo> handler) where TPackageInfo : IPackageInfo
		{
			base.PipeLineProcessor = new DefaultPipelineProcessor<TPackageInfo>(receiveFilter);
			m_Handler = delegate(IPackageInfo p)
			{
				handler((TPackageInfo)p);
			};
		}

		protected override void HandlePackage(IPackageInfo package)
		{
			m_Handler?.Invoke(package);
		}
	}
	public class EasyClient<TPackageInfo> : EasyClientBase where TPackageInfo : IPackageInfo
	{
		public event EventHandler<PackageEventArgs<TPackageInfo>> NewPackageReceived;

		public virtual void Initialize(IReceiveFilter<TPackageInfo> receiveFilter)
		{
			base.PipeLineProcessor = new DefaultPipelineProcessor<TPackageInfo>(receiveFilter);
		}

		protected override void HandlePackage(IPackageInfo package)
		{
			this.NewPackageReceived?.Invoke(this, new PackageEventArgs<TPackageInfo>((TPackageInfo)package));
		}
	}
}
