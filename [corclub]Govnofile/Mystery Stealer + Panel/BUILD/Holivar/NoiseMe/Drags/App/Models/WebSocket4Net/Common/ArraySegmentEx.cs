using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000B7 RID: 183
	public class ArraySegmentEx<T>
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public ArraySegmentEx(T[] array, int offset, int count)
		{
			this.Array = array;
			this.Offset = offset;
			this.Count = count;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00005CC1 File Offset: 0x00003EC1
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00005CC9 File Offset: 0x00003EC9
		public T[] Array { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00005CD2 File Offset: 0x00003ED2
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00005CDA File Offset: 0x00003EDA
		public int Count { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00005CE3 File Offset: 0x00003EE3
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00005CEB File Offset: 0x00003EEB
		public int Offset { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00005CF4 File Offset: 0x00003EF4
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00005CFC File Offset: 0x00003EFC
		public int From { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00005D05 File Offset: 0x00003F05
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00005D0D File Offset: 0x00003F0D
		public int To { get; set; }
	}
}
