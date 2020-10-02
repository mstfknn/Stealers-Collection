using System;
using System.Threading;

namespace ProtoBuf
{
	// Token: 0x02000010 RID: 16
	internal sealed class BufferPool
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000A9DC File Offset: 0x00008BDC
		internal static void Flush()
		{
			for (int i = 0; i < BufferPool.pool.Length; i++)
			{
				Interlocked.Exchange(ref BufferPool.pool[i], null);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000022E5 File Offset: 0x000004E5
		private BufferPool()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000AA10 File Offset: 0x00008C10
		internal static byte[] GetBuffer()
		{
			for (int i = 0; i < BufferPool.pool.Length; i++)
			{
				object obj;
				if ((obj = Interlocked.Exchange(ref BufferPool.pool[i], null)) != null)
				{
					return (byte[])obj;
				}
			}
			return new byte[1024];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000AA58 File Offset: 0x00008C58
		internal static void ResizeAndFlushLeft(ref byte[] buffer, int toFitAtLeastBytes, int copyFromIndex, int copyBytes)
		{
			int num = buffer.Length * 2;
			if (num < toFitAtLeastBytes)
			{
				num = toFitAtLeastBytes;
			}
			byte[] array = new byte[num];
			if (copyBytes > 0)
			{
				Helpers.BlockCopy(buffer, copyFromIndex, array, 0, copyBytes);
			}
			if (buffer.Length == 1024)
			{
				BufferPool.ReleaseBufferToPool(ref buffer);
			}
			buffer = array;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000AA9C File Offset: 0x00008C9C
		internal static void ReleaseBufferToPool(ref byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			if (buffer.Length == 1024)
			{
				int num = 0;
				while (num < BufferPool.pool.Length && Interlocked.CompareExchange(ref BufferPool.pool[num], buffer, null) != null)
				{
					num++;
				}
			}
			buffer = null;
		}

		// Token: 0x0400002E RID: 46
		private const int PoolSize = 20;

		// Token: 0x0400002F RID: 47
		internal const int BufferLength = 1024;

		// Token: 0x04000030 RID: 48
		private static readonly object[] pool = new object[20];
	}
}
