using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Protocol
{
	internal class DelegateCommand<TClientSession, TPackageInfo> : ICommand<TClientSession, TPackageInfo>, ICommand where TClientSession : class where TPackageInfo : IPackageInfo
	{
		private CommandDelegate<TClientSession, TPackageInfo> m_Execution;

		public string Name
		{
			get;
			private set;
		}

		public DelegateCommand(string name, CommandDelegate<TClientSession, TPackageInfo> execution)
		{
			Name = name;
			m_Execution = execution;
		}

		public void ExecuteCommand(TClientSession session, TPackageInfo packageInfo)
		{
			m_Execution(session, packageInfo);
		}
	}
}
