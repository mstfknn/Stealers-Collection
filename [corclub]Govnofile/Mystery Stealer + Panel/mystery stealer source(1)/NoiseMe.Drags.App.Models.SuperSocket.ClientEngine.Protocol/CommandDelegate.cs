namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Protocol
{
	public delegate void CommandDelegate<TClientSession, TPackageInfo>(TClientSession session, TPackageInfo packageInfo);
}
