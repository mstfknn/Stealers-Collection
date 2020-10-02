namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public abstract class CommandInfo<TCommandData> : ICommandInfo<TCommandData>, ICommandInfo
	{
		public TCommandData Data
		{
			get;
			private set;
		}

		public string Key
		{
			get;
			private set;
		}

		public CommandInfo(string key, TCommandData data)
		{
			Key = key;
			Data = data;
		}
	}
}
