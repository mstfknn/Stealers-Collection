namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class DefaultProtoHandler : ProtoHandlerBase
	{
		public override bool CanSend()
		{
			return true;
		}

		public override void Close(ICommunicationChannel channel, CloseReason reason)
		{
			channel.Close(reason);
		}
	}
}
