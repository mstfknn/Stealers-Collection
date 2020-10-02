using System;
using System.IO;

namespace ProtoBuf
{
	// Token: 0x0200001D RID: 29
	public interface IExtension
	{
		// Token: 0x06000096 RID: 150
		Stream BeginAppend();

		// Token: 0x06000097 RID: 151
		void EndAppend(Stream stream, bool commit);

		// Token: 0x06000098 RID: 152
		Stream BeginQuery();

		// Token: 0x06000099 RID: 153
		void EndQuery(Stream stream);

		// Token: 0x0600009A RID: 154
		int GetLength();
	}
}
