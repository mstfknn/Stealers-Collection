using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004D RID: 77
	internal sealed class ParseableSerializer : IProtoSerializer
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		public static ParseableSerializer TryCreate(Type type, TypeModel model)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MethodInfo method = type.GetMethod("Parse", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				model.MapType(typeof(string))
			}, null);
			if (method != null && method.ReturnType == type)
			{
				if (Helpers.IsValueType(type))
				{
					MethodInfo customToString = ParseableSerializer.GetCustomToString(type);
					if (customToString == null || customToString.ReturnType != model.MapType(typeof(string)))
					{
						return null;
					}
				}
				return new ParseableSerializer(method);
			}
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000035DE File Offset: 0x000017DE
		private static MethodInfo GetCustomToString(Type type)
		{
			return type.GetMethod("ToString", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public, null, Helpers.EmptyTypes, null);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000035F4 File Offset: 0x000017F4
		private ParseableSerializer(MethodInfo parse)
		{
			this.parse = parse;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00003603 File Offset: 0x00001803
		public Type ExpectedType
		{
			get
			{
				return this.parse.DeclaringType;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00003610 File Offset: 0x00001810
		public object Read(object value, ProtoReader source)
		{
			return this.parse.Invoke(null, new object[]
			{
				source.ReadString()
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000362D File Offset: 0x0000182D
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString(value.ToString(), dest);
		}

		// Token: 0x0400010D RID: 269
		private readonly MethodInfo parse;
	}
}
