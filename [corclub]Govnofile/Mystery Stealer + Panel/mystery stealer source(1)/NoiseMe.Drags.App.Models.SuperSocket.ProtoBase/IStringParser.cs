namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IStringParser
	{
		void Parse(string source, out string key, out string body, out string[] parameters);
	}
}
