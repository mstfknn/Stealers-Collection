using System;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200011D RID: 285
	public class EasyClient : EasyClientBase
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001E1DC File Offset: 0x0001C3DC
		public void Initialize<TPackageInfo>(IReceiveFilter<TPackageInfo> receiveFilter, Action<TPackageInfo> handler) where TPackageInfo : IPackageInfo
		{
			base.PipeLineProcessor = new DefaultPipelineProcessor<TPackageInfo>(receiveFilter, 0);
			this.m_Handler = delegate(IPackageInfo p)
			{
				handler((TPackageInfo)((object)p));
			};
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001E218 File Offset: 0x0001C418
		protected override void HandlePackage(IPackageInfo package)
		{
			Action<IPackageInfo> handler = this.m_Handler;
			if (handler == null)
			{
				return;
			}
			handler(package);
		}

		// Token: 0x04000367 RID: 871
		private Action<IPackageInfo> m_Handler;
	}
}
