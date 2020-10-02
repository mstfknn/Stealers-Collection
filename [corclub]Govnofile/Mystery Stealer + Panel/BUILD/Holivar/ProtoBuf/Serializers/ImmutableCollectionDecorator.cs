using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000042 RID: 66
	internal sealed class ImmutableCollectionDecorator : ListDecorator
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000031DF File Offset: 0x000013DF
		protected override bool RequireAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000E53C File Offset: 0x0000C73C
		private static Type ResolveIReadOnlyCollection(Type declaredType, Type t)
		{
			foreach (Type type in declaredType.GetInterfaces())
			{
				if (type.IsGenericType && type.Name.StartsWith("IReadOnlyCollection`"))
				{
					if (t != null)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments.Length != 1 && genericArguments[0] != t)
						{
							goto IL_41;
						}
					}
					return type;
				}
				IL_41:;
			}
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000E598 File Offset: 0x0000C798
		internal static bool IdentifyImmutable(TypeModel model, Type declaredType, out MethodInfo builderFactory, out MethodInfo add, out MethodInfo addRange, out MethodInfo finish)
		{
			MethodInfo methodInfo;
			finish = (methodInfo = null);
			addRange = (methodInfo = methodInfo);
			add = (methodInfo = methodInfo);
			builderFactory = methodInfo;
			if (model == null || declaredType == null)
			{
				return false;
			}
			if (!declaredType.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = declaredType.GetGenericArguments();
			int i = genericArguments.Length;
			Type[] array;
			if (i != 1)
			{
				if (i != 2)
				{
					return false;
				}
				Type type = model.MapType(typeof(KeyValuePair<, >));
				if (type == null)
				{
					return false;
				}
				type = type.MakeGenericType(genericArguments);
				array = new Type[]
				{
					type
				};
			}
			else
			{
				array = genericArguments;
			}
			if (ImmutableCollectionDecorator.ResolveIReadOnlyCollection(declaredType, null) == null)
			{
				return false;
			}
			string text = declaredType.Name;
			int num = text.IndexOf('`');
			if (num <= 0)
			{
				return false;
			}
			text = (declaredType.IsInterface ? text.Substring(1, num - 1) : text.Substring(0, num));
			Type type2 = model.GetType(declaredType.Namespace + "." + text, declaredType.Assembly);
			if (type2 == null && text == "ImmutableSet")
			{
				type2 = model.GetType(declaredType.Namespace + ".ImmutableHashSet", declaredType.Assembly);
			}
			if (type2 == null)
			{
				return false;
			}
			foreach (MethodInfo methodInfo2 in type2.GetMethods())
			{
				if (methodInfo2.IsStatic && !(methodInfo2.Name != "CreateBuilder") && methodInfo2.IsGenericMethodDefinition && methodInfo2.GetParameters().Length == 0 && methodInfo2.GetGenericArguments().Length == genericArguments.Length)
				{
					builderFactory = methodInfo2.MakeGenericMethod(genericArguments);
					break;
				}
			}
			Type type3 = model.MapType(typeof(void));
			if (builderFactory == null || builderFactory.ReturnType == null || builderFactory.ReturnType == type3)
			{
				return false;
			}
			add = Helpers.GetInstanceMethod(builderFactory.ReturnType, "Add", array);
			if (add == null)
			{
				return false;
			}
			finish = Helpers.GetInstanceMethod(builderFactory.ReturnType, "ToImmutable", Helpers.EmptyTypes);
			if (finish == null || finish.ReturnType == null || finish.ReturnType == type3)
			{
				return false;
			}
			if (finish.ReturnType != declaredType && !Helpers.IsAssignableFrom(declaredType, finish.ReturnType))
			{
				return false;
			}
			addRange = Helpers.GetInstanceMethod(builderFactory.ReturnType, "AddRange", new Type[]
			{
				declaredType
			});
			if (addRange == null)
			{
				Type type4 = model.MapType(typeof(IEnumerable<>), false);
				if (type4 != null)
				{
					addRange = Helpers.GetInstanceMethod(builderFactory.ReturnType, "AddRange", new Type[]
					{
						type4.MakeGenericType(array)
					});
				}
			}
			return true;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E81C File Offset: 0x0000CA1C
		internal ImmutableCollectionDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull, MethodInfo builderFactory, MethodInfo add, MethodInfo addRange, MethodInfo finish) : base(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull)
		{
			this.builderFactory = builderFactory;
			this.add = add;
			this.addRange = addRange;
			this.finish = finish;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E860 File Offset: 0x0000CA60
		public override object Read(object value, ProtoReader source)
		{
			object obj = this.builderFactory.Invoke(null, null);
			int fieldNumber = source.FieldNumber;
			object[] array = new object[1];
			if (base.AppendToCollection && value != null && ((IList)value).Count != 0)
			{
				if (this.addRange != null)
				{
					array[0] = value;
					this.addRange.Invoke(obj, array);
				}
				else
				{
					foreach (object obj2 in ((IList)value))
					{
						array[0] = obj2;
						this.add.Invoke(obj, array);
					}
				}
			}
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				while (source.TryReadFieldHeader(fieldNumber));
			}
			return this.finish.Invoke(obj, null);
		}

		// Token: 0x040000F1 RID: 241
		private readonly MethodInfo builderFactory;

		// Token: 0x040000F2 RID: 242
		private readonly MethodInfo add;

		// Token: 0x040000F3 RID: 243
		private readonly MethodInfo addRange;

		// Token: 0x040000F4 RID: 244
		private readonly MethodInfo finish;
	}
}
