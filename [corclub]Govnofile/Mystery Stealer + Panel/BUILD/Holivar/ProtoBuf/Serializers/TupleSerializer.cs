using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000058 RID: 88
	internal sealed class TupleSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public TupleSerializer(RuntimeTypeModel model, ConstructorInfo ctor, MemberInfo[] members)
		{
			if (ctor == null)
			{
				throw new ArgumentNullException("ctor");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			this.ctor = ctor;
			this.members = members;
			this.tails = new IProtoSerializer[members.Length];
			ParameterInfo[] parameters = ctor.GetParameters();
			for (int i = 0; i < members.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				Type type = null;
				Type concreteType = null;
				MetaType.ResolveListTypes(model, parameterType, ref type, ref concreteType);
				Type type2 = (type == null) ? parameterType : type;
				bool asReference = false;
				if (model.FindOrAddAuto(type2, false, true, false) >= 0)
				{
					asReference = model[type2].AsReferenceDefault;
				}
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(model, DataFormat.Default, type2, out wireType, asReference, false, false, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type2.FullName);
				}
				protoSerializer = new TagDecorator(i + 1, wireType, false, protoSerializer);
				IProtoSerializer protoSerializer2;
				if (type == null)
				{
					protoSerializer2 = protoSerializer;
				}
				else if (parameterType.IsArray)
				{
					protoSerializer2 = new ArrayDecorator(model, protoSerializer, i + 1, false, wireType, parameterType, false, false);
				}
				else
				{
					protoSerializer2 = ListDecorator.Create(model, parameterType, concreteType, protoSerializer, i + 1, false, wireType, true, false, false);
				}
				this.tails[i] = protoSerializer2;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000031DF File Offset: 0x000013DF
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000393B File Offset: 0x00001B3B
		public Type ExpectedType
		{
			get
			{
				return this.ctor.DeclaringType;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00002596 File Offset: 0x00000796
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000250E File Offset: 0x0000070E
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		private object GetValue(object obj, int index)
		{
			PropertyInfo propertyInfo;
			if ((propertyInfo = (this.members[index] as PropertyInfo)) != null)
			{
				if (obj != null)
				{
					return propertyInfo.GetValue(obj, null);
				}
				if (!Helpers.IsValueType(propertyInfo.PropertyType))
				{
					return null;
				}
				return Activator.CreateInstance(propertyInfo.PropertyType);
			}
			else
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = (this.members[index] as FieldInfo)) == null)
				{
					throw new InvalidOperationException();
				}
				if (obj != null)
				{
					return fieldInfo.GetValue(obj);
				}
				if (!Helpers.IsValueType(fieldInfo.FieldType))
				{
					return null;
				}
				return Activator.CreateInstance(fieldInfo.FieldType);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F658 File Offset: 0x0000D858
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[this.members.Length];
			bool flag = false;
			if (value == null)
			{
				flag = true;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetValue(value, i);
			}
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				flag = true;
				if (num <= this.tails.Length)
				{
					IProtoSerializer protoSerializer = this.tails[num - 1];
					array[num - 1] = this.tails[num - 1].Read(protoSerializer.RequiresOldValue ? array[num - 1] : null, source);
				}
				else
				{
					source.SkipField();
				}
			}
			if (!flag)
			{
				return value;
			}
			return this.ctor.Invoke(array);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public void Write(object value, ProtoWriter dest)
		{
			for (int i = 0; i < this.tails.Length; i++)
			{
				object value2 = this.GetValue(value, i);
				if (value2 != null)
				{
					this.tails[i].Write(value2, dest);
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00003147 File Offset: 0x00001347
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000031DF File Offset: 0x000013DF
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00003948 File Offset: 0x00001B48
		private Type GetMemberType(int index)
		{
			Type memberType = Helpers.GetMemberType(this.members[index]);
			if (memberType == null)
			{
				throw new InvalidOperationException();
			}
			return memberType;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x04000124 RID: 292
		private readonly MemberInfo[] members;

		// Token: 0x04000125 RID: 293
		private readonly ConstructorInfo ctor;

		// Token: 0x04000126 RID: 294
		private IProtoSerializer[] tails;
	}
}
