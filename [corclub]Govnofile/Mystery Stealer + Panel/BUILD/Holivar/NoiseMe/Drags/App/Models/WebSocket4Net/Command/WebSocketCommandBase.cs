using System;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Command
{
	// Token: 0x020000CB RID: 203
	public abstract class WebSocketCommandBase : ICommand<WebSocket, WebSocketCommandInfo>, ICommand
	{
		// Token: 0x0600069B RID: 1691
		public abstract void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo);

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600069C RID: 1692
		public abstract string Name { get; }
	}
}
