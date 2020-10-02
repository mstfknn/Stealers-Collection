namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public interface IClientCommandReader<TCommandInfo> where TCommandInfo : ICommandInfo
	{
		IClientCommandReader<TCommandInfo> NextCommandReader
		{
			get;
		}

		TCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left);
	}
}
