using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000054 RID: 84
	internal sealed class SurrogateSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000250E File Offset: 0x0000070E
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00002596 File Offset: 0x00000796
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000031DF File Offset: 0x000013DF
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00003147 File Offset: 0x00001347
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000037DA File Offset: 0x000019DA
		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000037E2 File Offset: 0x000019E2
		public SurrogateSerializer(TypeModel model, Type forType, Type declaredType, IProtoTypeSerializer rootTail)
		{
			this.forType = forType;
			this.declaredType = declaredType;
			this.rootTail = rootTail;
			this.toTail = this.GetConversion(model, true);
			this.fromTail = this.GetConversion(model, false);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000F284 File Offset: 0x0000D484
		private static bool HasCast(TypeModel model, Type type, Type from, Type to, out MethodInfo op)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			Type type2 = null;
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.ReturnType == to)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						if (type2 == null)
						{
							type2 = model.MapType(typeof(ProtoConverterAttribute), false);
							if (type2 == null)
							{
								break;
							}
						}
						if (methodInfo.IsDefined(type2, true))
						{
							op = methodInfo;
							return true;
						}
					}
				}
			}
			foreach (MethodInfo methodInfo2 in methods)
			{
				if ((!(methodInfo2.Name != "op_Implicit") || !(methodInfo2.Name != "op_Explicit")) && methodInfo2.ReturnType == to)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						op = methodInfo2;
						return true;
					}
				}
			}
			op = null;
			return false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F36C File Offset: 0x0000D56C
		public MethodInfo GetConversion(TypeModel model, bool toTail)
		{
			Type to = toTail ? this.declaredType : this.forType;
			Type from = toTail ? this.forType : this.declaredType;
			MethodInfo result;
			if (SurrogateSerializer.HasCast(model, this.declaredType, from, to, out result) || SurrogateSerializer.HasCast(model, this.forType, from, to, out result))
			{
				return result;
			}
			throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + this.forType.FullName + " / " + this.declaredType.FullName);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000381C File Offset: 0x00001A1C
		public void Write(object value, ProtoWriter writer)
		{
			this.rootTail.Write(this.toTail.Invoke(null, new object[]
			{
				value
			}), writer);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[]
			{
				value
			};
			value = this.toTail.Invoke(null, array);
			array[0] = this.rootTail.Read(value, source);
			return this.fromTail.Invoke(null, array);
		}

		// Token: 0x0400011A RID: 282
		private readonly Type forType;

		// Token: 0x0400011B RID: 283
		private readonly Type declaredType;

		// Token: 0x0400011C RID: 284
		private readonly MethodInfo toTail;

		// Token: 0x0400011D RID: 285
		private readonly MethodInfo fromTail;

		// Token: 0x0400011E RID: 286
		private IProtoTypeSerializer rootTail;
	}
}
