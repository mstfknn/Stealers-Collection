using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A1 RID: 161
	internal class DraftHybi00HandshakeReader : HandshakeReader
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0000583F File Offset: 0x00003A3F
		public DraftHybi00HandshakeReader(WebSocket websocket) : base(websocket)
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00005864 File Offset: 0x00003A64
		private void SetDataReader()
		{
			base.NextCommandReader = new DraftHybi00DataReader(this);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018C68 File Offset: 0x00016E68
		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			if (this.m_ReceivedChallengeLength < 0)
			{
				WebSocketCommandInfo commandInfo = base.GetCommandInfo(readBuffer, offset, length, out left);
				if (commandInfo == null)
				{
					return null;
				}
				if (HandshakeReader.BadRequestCode.Equals(commandInfo.Key))
				{
					return commandInfo;
				}
				this.m_ReceivedChallengeLength = 0;
				this.m_HandshakeCommand = commandInfo;
				int srcOffset = offset + length - left;
				if (left < this.m_ExpectedChallengeLength)
				{
					if (left > 0)
					{
						Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, left);
						this.m_ReceivedChallengeLength = left;
						left = 0;
					}
					return null;
				}
				if (left == this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, left);
					this.SetDataReader();
					this.m_HandshakeCommand.Data = this.m_Challenges;
					left = 0;
					return this.m_HandshakeCommand;
				}
				Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, this.m_ExpectedChallengeLength);
				left -= this.m_ExpectedChallengeLength;
				this.SetDataReader();
				this.m_HandshakeCommand.Data = this.m_Challenges;
				return this.m_HandshakeCommand;
			}
			else
			{
				int num = this.m_ReceivedChallengeLength + length;
				if (num < this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, length);
					left = 0;
					this.m_ReceivedChallengeLength = num;
					return null;
				}
				if (num == this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, length);
					left = 0;
					this.SetDataReader();
					this.m_HandshakeCommand.Data = this.m_Challenges;
					return this.m_HandshakeCommand;
				}
				int num2 = this.m_ExpectedChallengeLength - this.m_ReceivedChallengeLength;
				Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, num2);
				left = length - num2;
				this.SetDataReader();
				this.m_HandshakeCommand.Data = this.m_Challenges;
				return this.m_HandshakeCommand;
			}
		}

		// Token: 0x0400027E RID: 638
		private int m_ReceivedChallengeLength = -1;

		// Token: 0x0400027F RID: 639
		private int m_ExpectedChallengeLength = 16;

		// Token: 0x04000280 RID: 640
		private WebSocketCommandInfo m_HandshakeCommand;

		// Token: 0x04000281 RID: 641
		private byte[] m_Challenges = new byte[16];
	}
}
