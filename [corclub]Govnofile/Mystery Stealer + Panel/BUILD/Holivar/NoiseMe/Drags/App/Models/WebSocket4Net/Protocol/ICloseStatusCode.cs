using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A8 RID: 168
	public interface ICloseStatusCode
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005D7 RID: 1495
		short ExtensionNotMatch { get; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005D8 RID: 1496
		short GoingAway { get; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005D9 RID: 1497
		short InvalidUTF8 { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005DA RID: 1498
		short NormalClosure { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005DB RID: 1499
		short NotAcceptableData { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005DC RID: 1500
		short ProtocolError { get; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005DD RID: 1501
		short TLSHandshakeFailure { get; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005DE RID: 1502
		short TooLargeFrame { get; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005DF RID: 1503
		short UnexpectedCondition { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005E0 RID: 1504
		short ViolatePolicy { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005E1 RID: 1505
		short NoStatusCode { get; }
	}
}
