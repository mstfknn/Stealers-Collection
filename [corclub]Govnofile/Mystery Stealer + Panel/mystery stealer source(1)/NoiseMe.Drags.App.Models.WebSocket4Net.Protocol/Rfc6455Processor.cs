namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class Rfc6455Processor : DraftHybi10Processor
	{
		public Rfc6455Processor()
			: base(WebSocketVersion.Rfc6455, new CloseStatusCodeRfc6455(), "Origin")
		{
		}
	}
}
