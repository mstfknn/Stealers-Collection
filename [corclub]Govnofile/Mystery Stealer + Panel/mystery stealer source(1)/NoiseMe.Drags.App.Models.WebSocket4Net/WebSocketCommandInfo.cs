using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;
using NoiseMe.Drags.App.Models.WebSocket4Net.Protocol;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	public class WebSocketCommandInfo : ICommandInfo
	{
		public string Key
		{
			get;
			set;
		}

		public byte[] Data
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public short CloseStatusCode
		{
			get;
			private set;
		}

		public WebSocketCommandInfo()
		{
		}

		public WebSocketCommandInfo(string key)
		{
			Key = key;
		}

		public WebSocketCommandInfo(string key, string text)
		{
			Key = key;
			Text = text;
		}

		public WebSocketCommandInfo(IList<WebSocketDataFrame> frames)
		{
			sbyte opCode = frames[0].OpCode;
			Key = opCode.ToString();
			switch (opCode)
			{
			case 8:
			{
				WebSocketDataFrame webSocketDataFrame2 = frames[0];
				int num3 = (int)webSocketDataFrame2.ActualPayloadLength;
				int num2 = webSocketDataFrame2.InnerData.Count - num3;
				StringBuilder stringBuilder = new StringBuilder();
				if (num3 >= 2)
				{
					num2 = webSocketDataFrame2.InnerData.Count - num3;
					byte[] array2 = webSocketDataFrame2.InnerData.ToArrayData(num2, 2);
					CloseStatusCode = (short)(array2[0] * 256 + array2[1]);
					if (num3 > 2)
					{
						stringBuilder.Append(webSocketDataFrame2.InnerData.Decode(Encoding.UTF8, num2 + 2, num3 - 2));
					}
				}
				else if (num3 > 0)
				{
					stringBuilder.Append(webSocketDataFrame2.InnerData.Decode(Encoding.UTF8, num2, num3));
				}
				if (frames.Count > 1)
				{
					for (int j = 1; j < frames.Count; j++)
					{
						WebSocketDataFrame webSocketDataFrame3 = frames[j];
						num2 = webSocketDataFrame3.InnerData.Count - (int)webSocketDataFrame3.ActualPayloadLength;
						num3 = (int)webSocketDataFrame3.ActualPayloadLength;
						if (webSocketDataFrame3.HasMask)
						{
							webSocketDataFrame3.InnerData.DecodeMask(webSocketDataFrame3.MaskKey, num2, num3);
						}
						stringBuilder.Append(webSocketDataFrame3.InnerData.Decode(Encoding.UTF8, num2, num3));
					}
				}
				Text = stringBuilder.ToString();
				return;
			}
			case 2:
			{
				byte[] array = new byte[frames.Sum((WebSocketDataFrame f) => (int)f.ActualPayloadLength)];
				int num = 0;
				for (int i = 0; i < frames.Count; i++)
				{
					WebSocketDataFrame webSocketDataFrame = frames[i];
					int num2 = webSocketDataFrame.InnerData.Count - (int)webSocketDataFrame.ActualPayloadLength;
					int num3 = (int)webSocketDataFrame.ActualPayloadLength;
					if (webSocketDataFrame.HasMask)
					{
						webSocketDataFrame.InnerData.DecodeMask(webSocketDataFrame.MaskKey, num2, num3);
					}
					webSocketDataFrame.InnerData.CopyTo(array, num2, num, num3);
					num += num3;
				}
				Data = array;
				return;
			}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int k = 0; k < frames.Count; k++)
			{
				WebSocketDataFrame webSocketDataFrame4 = frames[k];
				int num2 = webSocketDataFrame4.InnerData.Count - (int)webSocketDataFrame4.ActualPayloadLength;
				int num3 = (int)webSocketDataFrame4.ActualPayloadLength;
				if (webSocketDataFrame4.HasMask)
				{
					webSocketDataFrame4.InnerData.DecodeMask(webSocketDataFrame4.MaskKey, num2, num3);
				}
				stringBuilder2.Append(webSocketDataFrame4.InnerData.Decode(Encoding.UTF8, num2, num3));
			}
			Text = stringBuilder2.ToString();
		}

		public WebSocketCommandInfo(WebSocketDataFrame frame)
		{
			Key = frame.OpCode.ToString();
			int num = (int)frame.ActualPayloadLength;
			int num2 = frame.InnerData.Count - (int)frame.ActualPayloadLength;
			if (frame.HasMask && num > 0)
			{
				frame.InnerData.DecodeMask(frame.MaskKey, num2, num);
			}
			if (frame.OpCode == 8 && num >= 2)
			{
				byte[] array = frame.InnerData.ToArrayData(num2, 2);
				CloseStatusCode = (short)(array[0] * 256 + array[1]);
				if (num > 2)
				{
					Text = frame.InnerData.Decode(Encoding.UTF8, num2 + 2, num - 2);
				}
				else
				{
					Text = string.Empty;
				}
			}
			else if (frame.OpCode != 2)
			{
				if (num > 0)
				{
					Text = frame.InnerData.Decode(Encoding.UTF8, num2, num);
				}
				else
				{
					Text = string.Empty;
				}
			}
			else if (num > 0)
			{
				Data = frame.InnerData.ToArrayData(num2, num);
			}
			else
			{
				Data = new byte[0];
			}
		}
	}
}
