using System;
using System.IO;

namespace ProtoBuf
{
	// Token: 0x0200000F RID: 15
	public sealed class BufferExtension : IExtension
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002398 File Offset: 0x00000598
		int IExtension.GetLength()
		{
			if (this.buffer != null)
			{
				return this.buffer.Length;
			}
			return 0;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000023AC File Offset: 0x000005AC
		Stream IExtension.BeginAppend()
		{
			return new MemoryStream();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000A930 File Offset: 0x00008B30
		void IExtension.EndAppend(Stream stream, bool commit)
		{
			try
			{
				int num;
				if (commit && (num = (int)stream.Length) > 0)
				{
					using (MemoryStream memoryStream = (MemoryStream)stream)
					{
						if (this.buffer == null)
						{
							this.buffer = memoryStream.ToArray();
						}
						else
						{
							int num2 = this.buffer.Length;
							byte[] to = new byte[num2 + num];
							Helpers.BlockCopy(this.buffer, 0, to, 0, num2);
							Helpers.BlockCopy(memoryStream.GetBuffer(), 0, to, num2, num);
							this.buffer = to;
						}
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000023B3 File Offset: 0x000005B3
		Stream IExtension.BeginQuery()
		{
			if (this.buffer != null)
			{
				return new MemoryStream(this.buffer);
			}
			return Stream.Null;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000023CE File Offset: 0x000005CE
		void IExtension.EndQuery(Stream stream)
		{
			if (stream != null)
			{
				stream.Dispose();
			}
		}

		// Token: 0x0400002D RID: 45
		private byte[] buffer;
	}
}
