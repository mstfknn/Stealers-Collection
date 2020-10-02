using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	internal class DelegateCommand<TClientSession, TCommandInfo> : ICommand<TClientSession, TCommandInfo>, ICommand where TClientSession : IClientSession where TCommandInfo : ICommandInfo
	{
		private CommandDelegate<TClientSession, TCommandInfo> m_Execution;

		public string Name
		{
			get;
			private set;
		}

		public DelegateCommand(string name, CommandDelegate<TClientSession, TCommandInfo> execution)
		{
			Name = name;
			m_Execution = execution;
		}

		public void ExecuteCommand(TClientSession session, TCommandInfo commandInfo)
		{
			m_Execution(session, commandInfo);
		}
	}
}
