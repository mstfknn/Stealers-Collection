using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoBeforeSerializationAttribute : Attribute
	{
	}
}
