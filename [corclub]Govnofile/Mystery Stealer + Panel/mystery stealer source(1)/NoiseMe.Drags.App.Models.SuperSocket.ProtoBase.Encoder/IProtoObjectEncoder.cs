namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase.Encoder
{
	public interface IProtoObjectEncoder
	{
		void EncodeObject(IOutputBuffer output, object target);
	}
}
