namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class StringPackageInfo : IPackageInfo<string>, IPackageInfo
	{
		public string Key
		{
			get;
			protected set;
		}

		public string Body
		{
			get;
			protected set;
		}

		public string[] Parameters
		{
			get;
			protected set;
		}

		public string this[int index] => Parameters[index];

		protected StringPackageInfo()
		{
		}

		public StringPackageInfo(string key, string body, string[] parameters)
		{
			Key = key;
			Body = body;
			Parameters = parameters;
		}

		public StringPackageInfo(string source, IStringParser sourceParser)
		{
			InitializeData(source, sourceParser);
		}

		protected void InitializeData(string source, IStringParser sourceParser)
		{
			sourceParser.Parse(source, out string key, out string body, out string[] parameters);
			Key = key;
			Body = body;
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
