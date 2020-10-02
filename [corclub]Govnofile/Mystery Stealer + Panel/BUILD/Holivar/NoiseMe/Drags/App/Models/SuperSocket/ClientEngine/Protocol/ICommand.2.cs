using System;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Protocol
{
	// Token: 0x0200012E RID: 302
	public interface ICommand<TSession, TPackageInfo> : ICommand where TSession : class where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000959 RID: 2393
		void ExecuteCommand(TSession session, TPackageInfo packageInfo);
	}
}
