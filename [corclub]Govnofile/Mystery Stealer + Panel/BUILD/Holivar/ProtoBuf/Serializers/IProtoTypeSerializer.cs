using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000047 RID: 71
	internal interface IProtoTypeSerializer : IProtoSerializer
	{
		// Token: 0x06000204 RID: 516
		bool HasCallbacks(TypeModel.CallbackType callbackType);

		// Token: 0x06000205 RID: 517
		bool CanCreateInstance();

		// Token: 0x06000206 RID: 518
		object CreateInstance(ProtoReader source);

		// Token: 0x06000207 RID: 519
		void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context);
	}
}
