using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000096 RID: 150
	public class OpCode
	{
		// Token: 0x0400021A RID: 538
		public const int Handshake = -1;

		// Token: 0x0400021B RID: 539
		public const int Text = 1;

		// Token: 0x0400021C RID: 540
		public const int Binary = 2;

		// Token: 0x0400021D RID: 541
		public const int Close = 8;

		// Token: 0x0400021E RID: 542
		public const int Ping = 9;

		// Token: 0x0400021F RID: 543
		public const int Pong = 10;

		// Token: 0x04000220 RID: 544
		public const int BadRequest = 400;
	}
}
