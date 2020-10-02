using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000049 RID: 73
	internal class ListDecorator : ProtoDecoratorBase
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00003483 File Offset: 0x00001683
		internal static bool CanPack(WireType wireType)
		{
			return wireType <= WireType.Fixed64 || wireType == WireType.Fixed32 || wireType == WireType.SignedVariant;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00003494 File Offset: 0x00001694
		private bool IsList
		{
			get
			{
				return (this.options & 1) > 0;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000034A1 File Offset: 0x000016A1
		private bool SuppressIList
		{
			get
			{
				return (this.options & 2) > 0;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000034AE File Offset: 0x000016AE
		private bool WritePacked
		{
			get
			{
				return (this.options & 4) > 0;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000034BB File Offset: 0x000016BB
		private bool SupportNull
		{
			get
			{
				return (this.options & 32) > 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000034C9 File Offset: 0x000016C9
		private bool ReturnList
		{
			get
			{
				return (this.options & 8) > 0;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E99C File Offset: 0x0000CB9C
		internal static ListDecorator Create(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull)
		{
			MethodInfo builderFactory;
			MethodInfo methodInfo;
			MethodInfo addRange;
			MethodInfo finish;
			if (returnList && ImmutableCollectionDecorator.IdentifyImmutable(model, declaredType, out builderFactory, out methodInfo, out addRange, out finish))
			{
				return new ImmutableCollectionDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull, builderFactory, methodInfo, addRange, finish);
			}
			return new ListDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		protected ListDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull) : base(tail)
		{
			if (returnList)
			{
				this.options |= 8;
			}
			if (overwriteList)
			{
				this.options |= 16;
			}
			if (supportNull)
			{
				this.options |= 32;
			}
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			if (writePacked)
			{
				this.options |= 4;
			}
			this.packedWireType = packedWireType;
			if (declaredType == null)
			{
				throw new ArgumentNullException("declaredType");
			}
			if (declaredType.IsArray)
			{
				throw new ArgumentException("Cannot treat arrays as lists", "declaredType");
			}
			this.declaredType = declaredType;
			this.concreteType = concreteType;
			if (this.RequireAdd)
			{
				bool flag;
				this.add = TypeModel.ResolveListAdd(model, declaredType, tail.ExpectedType, out flag);
				if (flag)
				{
					this.options |= 1;
					string fullName = declaredType.FullName;
					if (fullName != null && fullName.StartsWith("System.Data.Linq.EntitySet`1[["))
					{
						this.options |= 2;
					}
				}
				if (this.add == null)
				{
					throw new InvalidOperationException("Unable to resolve a suitable Add method for " + declaredType.FullName);
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00003147 File Offset: 0x00001347
		protected virtual bool RequireAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000034D6 File Offset: 0x000016D6
		public override Type ExpectedType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000034DE File Offset: 0x000016DE
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000034E6 File Offset: 0x000016E6
		public override bool ReturnsValue
		{
			get
			{
				return this.ReturnList;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000034EE File Offset: 0x000016EE
		protected bool AppendToCollection
		{
			get
			{
				return (this.options & 16) == 0;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000EB40 File Offset: 0x0000CD40
		protected MethodInfo GetEnumeratorInfo(TypeModel model, out MethodInfo moveNext, out MethodInfo current)
		{
			Type type = null;
			Type expectedType = this.ExpectedType;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(expectedType, "GetEnumerator", null);
			Type expectedType2 = this.Tail.ExpectedType;
			Type returnType;
			if (instanceMethod != null)
			{
				returnType = instanceMethod.ReturnType;
				moveNext = Helpers.GetInstanceMethod(returnType, "MoveNext", null);
				PropertyInfo property = Helpers.GetProperty(returnType, "Current", false);
				current = ((property == null) ? null : Helpers.GetGetMethod(property, false, false));
				if (moveNext == null && model.MapType(ListDecorator.ienumeratorType).IsAssignableFrom(returnType))
				{
					moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext", null);
				}
				if (moveNext != null && moveNext.ReturnType == model.MapType(typeof(bool)) && current != null && current.ReturnType == expectedType2)
				{
					return instanceMethod;
				}
				MethodInfo methodInfo;
				current = (methodInfo = null);
				moveNext = methodInfo;
			}
			Type type2 = model.MapType(typeof(IEnumerable<>), false);
			if (type2 != null)
			{
				type2 = type2.MakeGenericType(new Type[]
				{
					expectedType2
				});
				type = type2;
			}
			if (type != null && type.IsAssignableFrom(expectedType))
			{
				instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
				returnType = instanceMethod.ReturnType;
				moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext");
				current = Helpers.GetGetMethod(Helpers.GetProperty(returnType, "Current", false), false, false);
				return instanceMethod;
			}
			type = model.MapType(ListDecorator.ienumerableType);
			instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
			returnType = instanceMethod.ReturnType;
			moveNext = Helpers.GetInstanceMethod(returnType, "MoveNext");
			current = Helpers.GetGetMethod(Helpers.GetProperty(returnType, "Current", false), false, false);
			return instanceMethod;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public override void Write(object value, ProtoWriter dest)
		{
			bool writePacked = this.WritePacked;
			SubItemToken token;
			if (writePacked)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag = !this.SupportNull;
			foreach (object obj in ((IEnumerable)value))
			{
				if (flag && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (writePacked)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			object obj = value;
			if (value == null)
			{
				value = Activator.CreateInstance(this.concreteType);
			}
			bool flag = this.IsList && !this.SuppressIList;
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				if (flag)
				{
					IList list = (IList)value;
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						list.Add(this.Tail.Read(null, source));
					}
				}
				else
				{
					object[] array = new object[1];
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						array[0] = this.Tail.Read(null, source);
						this.add.Invoke(value, array);
					}
				}
				ProtoReader.EndSubItem(token, source);
			}
			else if (flag)
			{
				IList list2 = (IList)value;
				do
				{
					list2.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			else
			{
				object[] array2 = new object[1];
				do
				{
					array2[0] = this.Tail.Read(null, source);
					this.add.Invoke(value, array2);
				}
				while (source.TryReadFieldHeader(field));
			}
			if (obj != value)
			{
				return value;
			}
			return null;
		}

		// Token: 0x040000F8 RID: 248
		private readonly byte options;

		// Token: 0x040000F9 RID: 249
		private const byte OPTIONS_IsList = 1;

		// Token: 0x040000FA RID: 250
		private const byte OPTIONS_SuppressIList = 2;

		// Token: 0x040000FB RID: 251
		private const byte OPTIONS_WritePacked = 4;

		// Token: 0x040000FC RID: 252
		private const byte OPTIONS_ReturnList = 8;

		// Token: 0x040000FD RID: 253
		private const byte OPTIONS_OverwriteList = 16;

		// Token: 0x040000FE RID: 254
		private const byte OPTIONS_SupportNull = 32;

		// Token: 0x040000FF RID: 255
		private readonly Type declaredType;

		// Token: 0x04000100 RID: 256
		private readonly Type concreteType;

		// Token: 0x04000101 RID: 257
		private readonly MethodInfo add;

		// Token: 0x04000102 RID: 258
		private readonly int fieldNumber;

		// Token: 0x04000103 RID: 259
		protected readonly WireType packedWireType;

		// Token: 0x04000104 RID: 260
		private static readonly Type ienumeratorType = typeof(IEnumerator);

		// Token: 0x04000105 RID: 261
		private static readonly Type ienumerableType = typeof(IEnumerable);
	}
}
