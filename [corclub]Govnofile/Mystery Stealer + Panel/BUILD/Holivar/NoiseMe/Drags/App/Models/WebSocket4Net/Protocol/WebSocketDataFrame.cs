using System;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000B0 RID: 176
	public class WebSocketDataFrame
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00005AFF File Offset: 0x00003CFF
		public ArraySegmentList InnerData
		{
			get
			{
				return this.m_InnerData;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00005B07 File Offset: 0x00003D07
		public WebSocketDataFrame(ArraySegmentList data)
		{
			this.m_InnerData = data;
			this.m_InnerData.ClearSegements();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00019DF0 File Offset: 0x00017FF0
		public bool IsControlFrame
		{
			get
			{
				sbyte opCode = this.OpCode;
				return opCode - 8 <= 2;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00005B29 File Offset: 0x00003D29
		public bool FIN
		{
			get
			{
				return (this.m_InnerData[0] & 128) == 128;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00005B44 File Offset: 0x00003D44
		public bool RSV1
		{
			get
			{
				return (this.m_InnerData[0] & 64) == 64;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00005B59 File Offset: 0x00003D59
		public bool RSV2
		{
			get
			{
				return (this.m_InnerData[0] & 32) == 32;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00005B6E File Offset: 0x00003D6E
		public bool RSV3
		{
			get
			{
				return (this.m_InnerData[0] & 16) == 16;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00005B83 File Offset: 0x00003D83
		public sbyte OpCode
		{
			get
			{
				return (sbyte)(this.m_InnerData[0] & 15);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00005B95 File Offset: 0x00003D95
		public bool HasMask
		{
			get
			{
				return (this.m_InnerData[1] & 128) == 128;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public sbyte PayloadLenght
		{
			get
			{
				return (sbyte)(this.m_InnerData[1] & 127);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00019E10 File Offset: 0x00018010
		public long ActualPayloadLength
		{
			get
			{
				if (this.m_ActualPayloadLength >= 0L)
				{
					return this.m_ActualPayloadLength;
				}
				sbyte payloadLenght = this.PayloadLenght;
				if (payloadLenght < 126)
				{
					this.m_ActualPayloadLength = (long)payloadLenght;
				}
				else if (payloadLenght == 126)
				{
					this.m_ActualPayloadLength = (long)((int)this.m_InnerData[2] * 256 + (int)this.m_InnerData[3]);
				}
				else
				{
					long num = 0L;
					int num2 = 1;
					for (int i = 7; i >= 0; i--)
					{
						num += (long)((int)this.m_InnerData[i + 2] * num2);
						num2 *= 256;
					}
					this.m_ActualPayloadLength = num;
				}
				return this.m_ActualPayloadLength;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00005BC2 File Offset: 0x00003DC2
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00005BCA File Offset: 0x00003DCA
		public byte[] MaskKey { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00005BD3 File Offset: 0x00003DD3
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00005BDB File Offset: 0x00003DDB
		public byte[] ExtensionData { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00005BE4 File Offset: 0x00003DE4
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00005BEC File Offset: 0x00003DEC
		public byte[] ApplicationData { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00005BF5 File Offset: 0x00003DF5
		public int Length
		{
			get
			{
				return this.m_InnerData.Count;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00005C02 File Offset: 0x00003E02
		public void Clear()
		{
			this.m_InnerData.ClearSegements();
			this.ExtensionData = new byte[0];
			this.ApplicationData = new byte[0];
			this.m_ActualPayloadLength = -1L;
		}

		// Token: 0x040002AC RID: 684
		private ArraySegmentList m_InnerData;

		// Token: 0x040002AD RID: 685
		private long m_ActualPayloadLength = -1L;
	}
}
