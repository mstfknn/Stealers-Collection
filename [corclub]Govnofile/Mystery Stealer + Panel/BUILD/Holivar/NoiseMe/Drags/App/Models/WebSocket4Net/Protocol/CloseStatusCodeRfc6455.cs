using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x0200009F RID: 159
	public class CloseStatusCodeRfc6455 : ICloseStatusCode
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x000189C4 File Offset: 0x00016BC4
		public CloseStatusCodeRfc6455()
		{
			this.NormalClosure = 1000;
			this.GoingAway = 1001;
			this.ProtocolError = 1002;
			this.NotAcceptableData = 1003;
			this.TooLargeFrame = 1009;
			this.InvalidUTF8 = 1007;
			this.ViolatePolicy = 1008;
			this.ExtensionNotMatch = 1010;
			this.UnexpectedCondition = 1011;
			this.NoStatusCode = 1005;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000574C File Offset: 0x0000394C
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00005754 File Offset: 0x00003954
		public short NormalClosure { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000575D File Offset: 0x0000395D
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00005765 File Offset: 0x00003965
		public short GoingAway { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000576E File Offset: 0x0000396E
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00005776 File Offset: 0x00003976
		public short ProtocolError { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000577F File Offset: 0x0000397F
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00005787 File Offset: 0x00003987
		public short NotAcceptableData { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00005790 File Offset: 0x00003990
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00005798 File Offset: 0x00003998
		public short TooLargeFrame { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x000057A1 File Offset: 0x000039A1
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x000057A9 File Offset: 0x000039A9
		public short InvalidUTF8 { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000057B2 File Offset: 0x000039B2
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000057BA File Offset: 0x000039BA
		public short ViolatePolicy { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000057C3 File Offset: 0x000039C3
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000057CB File Offset: 0x000039CB
		public short ExtensionNotMatch { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000057D4 File Offset: 0x000039D4
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x000057DC File Offset: 0x000039DC
		public short UnexpectedCondition { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x000057E5 File Offset: 0x000039E5
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x000057ED File Offset: 0x000039ED
		public short TLSHandshakeFailure { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000057F6 File Offset: 0x000039F6
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x000057FE File Offset: 0x000039FE
		public short NoStatusCode { get; private set; }
	}
}
