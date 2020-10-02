namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IProtoTextEncoder
	{
		void EncodeText(IOutputBuffer output, string message);
	}
}
