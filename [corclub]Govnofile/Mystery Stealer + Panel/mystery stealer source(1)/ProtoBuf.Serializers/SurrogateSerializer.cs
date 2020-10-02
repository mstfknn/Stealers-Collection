using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class SurrogateSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		private readonly Type forType;

		private readonly Type declaredType;

		private readonly MethodInfo toTail;

		private readonly MethodInfo fromTail;

		private IProtoTypeSerializer rootTail;

		public bool ReturnsValue => false;

		public bool RequiresOldValue => true;

		public Type ExpectedType => forType;

		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		public SurrogateSerializer(TypeModel model, Type forType, Type declaredType, IProtoTypeSerializer rootTail)
		{
			this.forType = forType;
			this.declaredType = declaredType;
			this.rootTail = rootTail;
			toTail = GetConversion(model, toTail: true);
			fromTail = GetConversion(model, toTail: false);
		}

		private static bool HasCast(TypeModel model, Type type, Type from, Type to, out MethodInfo op)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			Type type2 = null;
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.ReturnType != to)
				{
					continue;
				}
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length != 1 || parameters[0].ParameterType != from)
				{
					continue;
				}
				if (type2 == null)
				{
					type2 = model.MapType(typeof(ProtoConverterAttribute), demand: false);
					if (type2 == null)
					{
						break;
					}
				}
				if (methodInfo.IsDefined(type2, inherit: true))
				{
					op = methodInfo;
					return true;
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

		public MethodInfo GetConversion(TypeModel model, bool toTail)
		{
			Type to = toTail ? declaredType : forType;
			Type from = toTail ? forType : declaredType;
			if (HasCast(model, declaredType, from, to, out MethodInfo op) || HasCast(model, forType, from, to, out op))
			{
				return op;
			}
			throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + forType.FullName + " / " + declaredType.FullName);
		}

		public void Write(object value, ProtoWriter writer)
		{
			rootTail.Write(toTail.Invoke(null, new object[1]
			{
				value
			}), writer);
		}

		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[1]
			{
				value
			};
			value = toTail.Invoke(null, array);
			array[0] = rootTail.Read(value, source);
			return fromTail.Invoke(null, array);
		}
	}
}
