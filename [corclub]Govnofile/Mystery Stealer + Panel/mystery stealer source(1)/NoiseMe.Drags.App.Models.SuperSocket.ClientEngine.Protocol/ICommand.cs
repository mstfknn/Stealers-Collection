using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Protocol
{
	public interface ICommand
	{
		string Name
		{
			get;
		}
	}
	public interface ICommand<TSession, TPackageInfo> : ICommand where TSession : class where TPackageInfo : IPackageInfo
	{
		void ExecuteCommand(TSession session, TPackageInfo packageInfo);
	}
}
