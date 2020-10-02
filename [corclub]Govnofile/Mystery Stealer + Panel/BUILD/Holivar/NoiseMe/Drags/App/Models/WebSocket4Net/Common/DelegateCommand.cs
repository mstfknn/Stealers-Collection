using System;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000BD RID: 189
	internal class DelegateCommand<TClientSession, TCommandInfo> : ICommand<TClientSession, TCommandInfo>, ICommand where TClientSession : IClientSession where TCommandInfo : ICommandInfo
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x00005E0B File Offset: 0x0000400B
		public DelegateCommand(string name, CommandDelegate<TClientSession, TCommandInfo> execution)
		{
			this.Name = name;
			this.m_Execution = execution;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00005E21 File Offset: 0x00004021
		public void ExecuteCommand(TClientSession session, TCommandInfo commandInfo)
		{
			this.m_Execution(session, commandInfo);
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00005E30 File Offset: 0x00004030
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00005E38 File Offset: 0x00004038
		public string Name { get; private set; }

		// Token: 0x040002C0 RID: 704
		private CommandDelegate<TClientSession, TCommandInfo> m_Execution;
	}
}
