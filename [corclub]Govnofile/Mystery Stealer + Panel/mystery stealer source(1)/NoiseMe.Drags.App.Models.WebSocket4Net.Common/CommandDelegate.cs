namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public delegate void CommandDelegate<TClientSession, TCommandInfo>(TClientSession session, TCommandInfo commandInfo);
}
