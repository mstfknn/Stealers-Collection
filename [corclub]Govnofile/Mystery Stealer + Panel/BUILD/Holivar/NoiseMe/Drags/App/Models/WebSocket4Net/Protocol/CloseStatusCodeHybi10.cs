using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x0200009E RID: 158
	public class CloseStatusCodeHybi10 : ICloseStatusCode
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00018938 File Offset: 0x00016B38
		public CloseStatusCodeHybi10()
		{
			this.NormalClosure = 1000;
			this.GoingAway = 1001;
			this.ProtocolError = 1002;
			this.NotAcceptableData = 1003;
			this.TooLargeFrame = 1004;
			this.InvalidUTF8 = 1007;
			this.ViolatePolicy = 1000;
			this.ExtensionNotMatch = 1000;
			this.UnexpectedCondition = 1000;
			this.TLSHandshakeFailure = 1000;
			this.NoStatusCode = 1005;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00005691 File Offset: 0x00003891
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00005699 File Offset: 0x00003899
		public short NormalClosure { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000056A2 File Offset: 0x000038A2
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000056AA File Offset: 0x000038AA
		public short GoingAway { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000056B3 File Offset: 0x000038B3
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000056BB File Offset: 0x000038BB
		public short ProtocolError { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000056C4 File Offset: 0x000038C4
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000056CC File Offset: 0x000038CC
		public short NotAcceptableData { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000056D5 File Offset: 0x000038D5
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000056DD File Offset: 0x000038DD
		public short TooLargeFrame { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000056E6 File Offset: 0x000038E6
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x000056EE File Offset: 0x000038EE
		public short InvalidUTF8 { get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000056F7 File Offset: 0x000038F7
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x000056FF File Offset: 0x000038FF
		public short ViolatePolicy { get; private set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00005708 File Offset: 0x00003908
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00005710 File Offset: 0x00003910
		public short ExtensionNotMatch { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00005719 File Offset: 0x00003919
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00005721 File Offset: 0x00003921
		public short UnexpectedCondition { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000572A File Offset: 0x0000392A
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00005732 File Offset: 0x00003932
		public short TLSHandshakeFailure { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000573B File Offset: 0x0000393B
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00005743 File Offset: 0x00003943
		public short NoStatusCode { get; private set; }
	}
}
