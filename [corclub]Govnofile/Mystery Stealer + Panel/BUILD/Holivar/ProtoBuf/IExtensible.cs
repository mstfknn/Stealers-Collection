using System;

namespace ProtoBuf
{
	// Token: 0x0200001C RID: 28
	public interface IExtensible
	{
		// Token: 0x06000095 RID: 149
		IExtension GetExtensionObject(bool createIfMissing);
	}
}
