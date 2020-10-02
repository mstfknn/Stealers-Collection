using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoAfterSerializationAttribute : Attribute
	{
	}
}
