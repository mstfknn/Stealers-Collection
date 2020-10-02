using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000C0 RID: 192
	public interface ICommand<TSession, TCommandInfo> : ICommand where TCommandInfo : ICommandInfo
	{
		// Token: 0x0600067D RID: 1661
		void ExecuteCommand(TSession session, TCommandInfo commandInfo);
	}
}
