using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader
{
	// Token: 0x020000B1 RID: 177
	internal abstract class DataFramePartReader : IDataFramePartReader
	{
		// Token: 0x0600062B RID: 1579
		public abstract int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader);

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00005C59 File Offset: 0x00003E59
		public static IDataFramePartReader NewReader
		{
			get
			{
				return DataFramePartReader.FixPartReader;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00005C60 File Offset: 0x00003E60
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00005C67 File Offset: 0x00003E67
		private protected static IDataFramePartReader FixPartReader { protected get; private set; } = new FixPartReader();

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00005C6F File Offset: 0x00003E6F
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00005C76 File Offset: 0x00003E76
		private protected static IDataFramePartReader ExtendedLenghtReader { protected get; private set; } = new ExtendedLenghtReader();

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00005C7E File Offset: 0x00003E7E
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00005C85 File Offset: 0x00003E85
		private protected static IDataFramePartReader MaskKeyReader { protected get; private set; } = new MaskKeyReader();

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00005C8D File Offset: 0x00003E8D
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00005C94 File Offset: 0x00003E94
		private protected static IDataFramePartReader PayloadDataReader { protected get; private set; } = new PayloadDataReader();
	}
}
