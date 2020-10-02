using System;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Protocol
{
	// Token: 0x0200012C RID: 300
	internal class DelegateCommand<TClientSession, TPackageInfo> : ICommand<TClientSession, TPackageInfo>, ICommand where TClientSession : class where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x0000724C File Offset: 0x0000544C
		public DelegateCommand(string name, CommandDelegate<TClientSession, TPackageInfo> execution)
		{
			this.Name = name;
			this.m_Execution = execution;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00007262 File Offset: 0x00005462
		public void ExecuteCommand(TClientSession session, TPackageInfo packageInfo)
		{
			this.m_Execution(session, packageInfo);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00007271 File Offset: 0x00005471
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00007279 File Offset: 0x00005479
		public string Name { get; private set; }

		// Token: 0x04000395 RID: 917
		private CommandDelegate<TClientSession, TPackageInfo> m_Execution;
	}
}
