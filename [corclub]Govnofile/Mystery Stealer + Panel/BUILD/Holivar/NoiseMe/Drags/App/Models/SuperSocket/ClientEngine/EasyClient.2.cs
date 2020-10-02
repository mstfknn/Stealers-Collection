using System;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200011F RID: 287
	public class EasyClient<TPackageInfo> : EasyClientBase where TPackageInfo : IPackageInfo
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060008DC RID: 2268 RVA: 0x0001E238 File Offset: 0x0001C438
		// (remove) Token: 0x060008DD RID: 2269 RVA: 0x0001E270 File Offset: 0x0001C470
		public event EventHandler<PackageEventArgs<TPackageInfo>> NewPackageReceived;

		// Token: 0x060008DF RID: 2271 RVA: 0x00006F11 File Offset: 0x00005111
		public virtual void Initialize(IReceiveFilter<TPackageInfo> receiveFilter)
		{
			base.PipeLineProcessor = new DefaultPipelineProcessor<TPackageInfo>(receiveFilter, 0);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		protected override void HandlePackage(IPackageInfo package)
		{
			EventHandler<PackageEventArgs<TPackageInfo>> newPackageReceived = this.NewPackageReceived;
			if (newPackageReceived == null)
			{
				return;
			}
			newPackageReceived(this, new PackageEventArgs<TPackageInfo>((TPackageInfo)((object)package)));
		}
	}
}
