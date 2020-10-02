using System;
using System.Collections.Generic;
using System.Text;
using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;
using NoiseMe.Drags.App.Models.WebSocket4Net.Protocol;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000098 RID: 152
	public class WebSocketCommandInfo : ICommandInfo
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x000022E5 File Offset: 0x000004E5
		public WebSocketCommandInfo()
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00005613 File Offset: 0x00003813
		public WebSocketCommandInfo(string key)
		{
			this.Key = key;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00005622 File Offset: 0x00003822
		public WebSocketCommandInfo(string key, string text)
		{
			this.Key = key;
			this.Text = text;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00018584 File Offset: 0x00016784
		public WebSocketCommandInfo(IList<WebSocketDataFrame> frames)
		{
			sbyte opCode = frames[0].OpCode;
			this.Key = opCode.ToString();
			if (opCode == 8)
			{
				WebSocketDataFrame webSocketDataFrame = frames[0];
				int num = (int)webSocketDataFrame.ActualPayloadLength;
				int num2 = webSocketDataFrame.InnerData.Count - num;
				StringBuilder stringBuilder = new StringBuilder();
				if (num >= 2)
				{
					num2 = webSocketDataFrame.InnerData.Count - num;
					byte[] array = webSocketDataFrame.InnerData.ToArrayData(num2, 2);
					this.CloseStatusCode = (short)((int)array[0] * 256 + (int)array[1]);
					if (num > 2)
					{
						stringBuilder.Append(webSocketDataFrame.InnerData.Decode(Encoding.UTF8, num2 + 2, num - 2));
					}
				}
				else if (num > 0)
				{
					stringBuilder.Append(webSocketDataFrame.InnerData.Decode(Encoding.UTF8, num2, num));
				}
				if (frames.Count > 1)
				{
					for (int i = 1; i < frames.Count; i++)
					{
						WebSocketDataFrame webSocketDataFrame2 = frames[i];
						num2 = webSocketDataFrame2.InnerData.Count - (int)webSocketDataFrame2.ActualPayloadLength;
						num = (int)webSocketDataFrame2.ActualPayloadLength;
						if (webSocketDataFrame2.HasMask)
						{
							webSocketDataFrame2.InnerData.DecodeMask(webSocketDataFrame2.MaskKey, num2, num);
						}
						stringBuilder.Append(webSocketDataFrame2.InnerData.Decode(Encoding.UTF8, num2, num));
					}
				}
				this.Text = stringBuilder.ToString();
				return;
			}
			if (opCode != 2)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int j = 0; j < frames.Count; j++)
				{
					WebSocketDataFrame webSocketDataFrame3 = frames[j];
					int num2 = webSocketDataFrame3.InnerData.Count - (int)webSocketDataFrame3.ActualPayloadLength;
					int num = (int)webSocketDataFrame3.ActualPayloadLength;
					if (webSocketDataFrame3.HasMask)
					{
						webSocketDataFrame3.InnerData.DecodeMask(webSocketDataFrame3.MaskKey, num2, num);
					}
					stringBuilder2.Append(webSocketDataFrame3.InnerData.Decode(Encoding.UTF8, num2, num));
				}
				this.Text = stringBuilder2.ToString();
				return;
			}
			byte[] array2 = new byte[frames.Sum((WebSocketDataFrame f) => (int)f.ActualPayloadLength)];
			int num3 = 0;
			for (int k = 0; k < frames.Count; k++)
			{
				WebSocketDataFrame webSocketDataFrame4 = frames[k];
				int num2 = webSocketDataFrame4.InnerData.Count - (int)webSocketDataFrame4.ActualPayloadLength;
				int num = (int)webSocketDataFrame4.ActualPayloadLength;
				if (webSocketDataFrame4.HasMask)
				{
					webSocketDataFrame4.InnerData.DecodeMask(webSocketDataFrame4.MaskKey, num2, num);
				}
				webSocketDataFrame4.InnerData.CopyTo(array2, num2, num3, num);
				num3 += num;
			}
			this.Data = array2;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00018824 File Offset: 0x00016A24
		public WebSocketCommandInfo(WebSocketDataFrame frame)
		{
			this.Key = frame.OpCode.ToString();
			int num = (int)frame.ActualPayloadLength;
			int num2 = frame.InnerData.Count - (int)frame.ActualPayloadLength;
			if (frame.HasMask && num > 0)
			{
				frame.InnerData.DecodeMask(frame.MaskKey, num2, num);
			}
			if (frame.OpCode == 8 && num >= 2)
			{
				byte[] array = frame.InnerData.ToArrayData(num2, 2);
				this.CloseStatusCode = (short)((int)array[0] * 256 + (int)array[1]);
				if (num > 2)
				{
					this.Text = frame.InnerData.Decode(Encoding.UTF8, num2 + 2, num - 2);
					return;
				}
				this.Text = string.Empty;
				return;
			}
			else if (frame.OpCode != 2)
			{
				if (num > 0)
				{
					this.Text = frame.InnerData.Decode(Encoding.UTF8, num2, num);
					return;
				}
				this.Text = string.Empty;
				return;
			}
			else
			{
				if (num > 0)
				{
					this.Data = frame.InnerData.ToArrayData(num2, num);
					return;
				}
				this.Data = new byte[0];
				return;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00005638 File Offset: 0x00003838
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00005640 File Offset: 0x00003840
		public string Key { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00005649 File Offset: 0x00003849
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00005651 File Offset: 0x00003851
		public byte[] Data { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000565A File Offset: 0x0000385A
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00005662 File Offset: 0x00003862
		public string Text { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000566B File Offset: 0x0000386B
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00005673 File Offset: 0x00003873
		public short CloseStatusCode { get; private set; }
	}
}
