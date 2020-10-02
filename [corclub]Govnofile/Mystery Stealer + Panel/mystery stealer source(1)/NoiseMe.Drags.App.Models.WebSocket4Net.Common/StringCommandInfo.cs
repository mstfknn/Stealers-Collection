namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public class StringCommandInfo : CommandInfo<string>
	{
		public string[] Parameters
		{
			get;
			private set;
		}

		public string this[int index] => Parameters[index];

		public StringCommandInfo(string key, string data, string[] parameters)
			: base(key, data)
		{
			Parameters = parameters;
		}

		public string GetFirstParam()
		{
			if (Parameters.Length != 0)
			{
				return Parameters[0];
			}
			return string.Empty;
		}
	}
}
