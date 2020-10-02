using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000AF RID: 175
	internal class Rfc6455Processor : DraftHybi10Processor
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00005AEB File Offset: 0x00003CEB
		public Rfc6455Processor() : base(WebSocketVersion.Rfc6455, new CloseStatusCodeRfc6455(), "Origin")
		{
		}
	}
}
