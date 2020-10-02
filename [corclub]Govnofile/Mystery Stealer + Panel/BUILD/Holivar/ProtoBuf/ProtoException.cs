using System;

namespace ProtoBuf
{
	// Token: 0x02000025 RID: 37
	public class ProtoException : Exception
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000027EF File Offset: 0x000009EF
		public ProtoException()
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000027F7 File Offset: 0x000009F7
		public ProtoException(string message) : base(message)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002800 File Offset: 0x00000A00
		public ProtoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
