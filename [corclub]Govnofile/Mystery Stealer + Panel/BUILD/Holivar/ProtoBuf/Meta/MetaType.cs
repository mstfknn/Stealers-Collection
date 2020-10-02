using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000067 RID: 103
	public class MetaType : ISerializerProxy
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00003D86 File Offset: 0x00001F86
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003D93 File Offset: 0x00001F93
		IProtoSerializer ISerializerProxy.Serializer
		{
			get
			{
				return this.Serializer;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00003D9B File Offset: 0x00001F9B
		public MetaType BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003DA3 File Offset: 0x00001FA3
		internal TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00003DAB File Offset: 0x00001FAB
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public bool IncludeSerializerMethod
		{
			get
			{
				return !this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, !value, true);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003DC5 File Offset: 0x00001FC5
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00003DCF File Offset: 0x00001FCF
		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value, true);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00003DDB File Offset: 0x00001FDB
		private bool IsValidSubType(Type subType)
		{
			return this.type.IsAssignableFrom(subType);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00003DE9 File Offset: 0x00001FE9
		public MetaType AddSubType(int fieldNumber, Type derivedType)
		{
			return this.AddSubType(fieldNumber, derivedType, DataFormat.Default);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001021C File Offset: 0x0000E41C
		public MetaType AddSubType(int fieldNumber, Type derivedType, DataFormat dataFormat)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber < 1)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if ((!this.type.IsClass && !this.type.IsInterface) || this.type.IsSealed)
			{
				throw new InvalidOperationException("Sub-types can only be added to non-sealed classes");
			}
			if (!this.IsValidSubType(derivedType))
			{
				throw new ArgumentException(derivedType.Name + " is not a valid sub-type of " + this.type.Name, "derivedType");
			}
			MetaType metaType = this.model[derivedType];
			this.ThrowIfFrozen();
			metaType.ThrowIfFrozen();
			SubType value = new SubType(fieldNumber, metaType, dataFormat);
			this.ThrowIfFrozen();
			metaType.SetBaseType(this);
			if (this.subTypes == null)
			{
				this.subTypes = new BasicList();
			}
			this.subTypes.Add(value);
			return this;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000102F8 File Offset: 0x0000E4F8
		private void SetBaseType(MetaType baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (this.baseType == baseType)
			{
				return;
			}
			if (this.baseType != null)
			{
				throw new InvalidOperationException("A type can only participate in one inheritance hierarchy");
			}
			for (MetaType metaType = baseType; metaType != null; metaType = metaType.baseType)
			{
				if (metaType == this)
				{
					throw new InvalidOperationException("Cyclic inheritance is not allowed");
				}
			}
			this.baseType = baseType;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public bool HasCallbacks
		{
			get
			{
				return this.callbacks != null && this.callbacks.NonTrivial;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003E0B File Offset: 0x0000200B
		public bool HasSubtypes
		{
			get
			{
				return this.subTypes != null && this.subTypes.Count != 0;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00003E25 File Offset: 0x00002025
		public CallbackSet Callbacks
		{
			get
			{
				if (this.callbacks == null)
				{
					this.callbacks = new CallbackSet(this);
				}
				return this.callbacks;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00003E41 File Offset: 0x00002041
		private bool IsValueType
		{
			get
			{
				return this.type.IsValueType;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00003E4E File Offset: 0x0000204E
		public MetaType SetCallbacks(MethodInfo beforeSerialize, MethodInfo afterSerialize, MethodInfo beforeDeserialize, MethodInfo afterDeserialize)
		{
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = beforeSerialize;
			callbackSet.AfterSerialize = afterSerialize;
			callbackSet.BeforeDeserialize = beforeDeserialize;
			callbackSet.AfterDeserialize = afterDeserialize;
			return this;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010354 File Offset: 0x0000E554
		public MetaType SetCallbacks(string beforeSerialize, string afterSerialize, string beforeDeserialize, string afterDeserialize)
		{
			if (this.IsValueType)
			{
				throw new InvalidOperationException();
			}
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = this.ResolveMethod(beforeSerialize, true);
			callbackSet.AfterSerialize = this.ResolveMethod(afterSerialize, true);
			callbackSet.BeforeDeserialize = this.ResolveMethod(beforeDeserialize, true);
			callbackSet.AfterDeserialize = this.ResolveMethod(afterDeserialize, true);
			return this;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000103B0 File Offset: 0x0000E5B0
		internal string GetSchemaTypeName()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate].GetSchemaTypeName();
			}
			if (!Helpers.IsNullOrEmpty(this.name))
			{
				return this.name;
			}
			string text = this.type.Name;
			if (this.type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				int num = text.IndexOf('`');
				if (num >= 0)
				{
					stringBuilder.Length = num;
				}
				foreach (Type type in this.type.GetGenericArguments())
				{
					stringBuilder.Append('_');
					Type type2 = type;
					MetaType metaType;
					if (this.model.GetKey(ref type2) >= 0 && (metaType = this.model[type2]) != null && metaType.surrogate == null)
					{
						stringBuilder.Append(metaType.GetSchemaTypeName());
					}
					else
					{
						stringBuilder.Append(type2.Name);
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00003E73 File Offset: 0x00002073
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00003E7B File Offset: 0x0000207B
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.ThrowIfFrozen();
				this.name = value;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00003E8A File Offset: 0x0000208A
		public MetaType SetFactory(MethodInfo factory)
		{
			this.model.VerifyFactory(factory, this.type);
			this.ThrowIfFrozen();
			this.factory = factory;
			return this;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00003EAC File Offset: 0x000020AC
		public MetaType SetFactory(string factory)
		{
			return this.SetFactory(this.ResolveMethod(factory, false));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00003EBC File Offset: 0x000020BC
		private MethodInfo ResolveMethod(string name, bool instance)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!instance)
			{
				return Helpers.GetStaticMethod(this.type, name);
			}
			return Helpers.GetInstanceMethod(this.type, name);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00003EE4 File Offset: 0x000020E4
		internal static Exception InbuiltType(Type type)
		{
			return new ArgumentException("Data of this type has inbuilt behaviour, and cannot be added to a model in this way: " + type.FullName);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000104A8 File Offset: 0x0000E6A8
		internal MetaType(RuntimeTypeModel model, Type type, MethodInfo factory)
		{
			this.factory = factory;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (model.TryGetBasicTypeSerializer(type) != null)
			{
				throw MetaType.InbuiltType(type);
			}
			this.type = type;
			this.model = model;
			if (Helpers.IsEnum(type))
			{
				this.EnumPassthru = type.IsDefined(model.MapType(typeof(FlagsAttribute)), false);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00003EFB File Offset: 0x000020FB
		protected internal void ThrowIfFrozen()
		{
			if ((this.flags & 4) != 0)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated for " + this.type.FullName);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00003F24 File Offset: 0x00002124
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001052C File Offset: 0x0000E72C
		internal IProtoTypeSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					int opaqueToken = 0;
					try
					{
						this.model.TakeLock(ref opaqueToken);
						if (this.serializer == null)
						{
							this.SetFlag(4, true, false);
							this.serializer = this.BuildSerializer();
						}
					}
					finally
					{
						this.model.ReleaseLock(opaqueToken);
					}
				}
				return this.serializer;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00003F2C File Offset: 0x0000212C
		internal bool IsList
		{
			get
			{
				return (this.IgnoreListHandling ? null : TypeModel.GetListItemType(this.model, this.type)) != null;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010594 File Offset: 0x0000E794
		private IProtoTypeSerializer BuildSerializer()
		{
			if (Helpers.IsEnum(this.type))
			{
				return new TagDecorator(1, WireType.Variant, false, new EnumSerializer(this.type, this.GetEnumMap()));
			}
			Type type = this.IgnoreListHandling ? null : TypeModel.GetListItemType(this.model, this.type);
			if (type != null)
			{
				if (this.surrogate != null)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot use a surrogate");
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be subclassed");
				}
				Type defaultType = null;
				MetaType.ResolveListTypes(this.model, this.type, ref type, ref defaultType);
				ValueMember valueMember = new ValueMember(this.model, 1, this.type, type, defaultType, DataFormat.Default);
				return new TypeSerializer(this.model, this.type, new int[]
				{
					1
				}, new IProtoSerializer[]
				{
					valueMember.Serializer
				}, null, true, true, null, this.constructType, this.factory);
			}
			else
			{
				if (this.surrogate != null)
				{
					MetaType metaType = this.model[this.surrogate];
					MetaType metaType2;
					while ((metaType2 = metaType.baseType) != null)
					{
						metaType = metaType2;
					}
					return new SurrogateSerializer(this.model, this.type, this.surrogate, metaType.Serializer);
				}
				if (!this.IsAutoTuple)
				{
					this.fields.Trim();
					int count = this.fields.Count;
					int num = (this.subTypes == null) ? 0 : this.subTypes.Count;
					int[] array = new int[count + num];
					IProtoSerializer[] array2 = new IProtoSerializer[count + num];
					int num2 = 0;
					if (num != 0)
					{
						foreach (object obj in this.subTypes)
						{
							SubType subType = (SubType)obj;
							if (!subType.DerivedType.IgnoreListHandling && this.model.MapType(MetaType.ienumerable).IsAssignableFrom(subType.DerivedType.Type))
							{
								throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a subclass");
							}
							array[num2] = subType.FieldNumber;
							array2[num2++] = subType.Serializer;
						}
					}
					if (count != 0)
					{
						foreach (object obj2 in this.fields)
						{
							ValueMember valueMember2 = (ValueMember)obj2;
							array[num2] = valueMember2.FieldNumber;
							array2[num2++] = valueMember2.Serializer;
						}
					}
					BasicList basicList = null;
					for (MetaType metaType3 = this.BaseType; metaType3 != null; metaType3 = metaType3.BaseType)
					{
						MethodInfo methodInfo = metaType3.HasCallbacks ? metaType3.Callbacks.BeforeDeserialize : null;
						if (methodInfo != null)
						{
							if (basicList == null)
							{
								basicList = new BasicList();
							}
							basicList.Add(methodInfo);
						}
					}
					MethodInfo[] array3 = null;
					if (basicList != null)
					{
						array3 = new MethodInfo[basicList.Count];
						basicList.CopyTo(array3, 0);
						Array.Reverse(array3);
					}
					return new TypeSerializer(this.model, this.type, array, array2, array3, this.baseType == null, this.UseConstructor, this.callbacks, this.constructType, this.factory);
				}
				MemberInfo[] members;
				ConstructorInfo constructorInfo = MetaType.ResolveTupleConstructor(this.type, out members);
				if (constructorInfo == null)
				{
					throw new InvalidOperationException();
				}
				return new TupleSerializer(this.model, constructorInfo, members);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00003F4D File Offset: 0x0000214D
		private static Type GetBaseType(MetaType type)
		{
			return type.type.BaseType;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000108BC File Offset: 0x0000EABC
		internal static bool GetAsReferenceDefault(RuntimeTypeModel model, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (Helpers.IsEnum(type))
			{
				return false;
			}
			AttributeMap[] array = AttributeMap.Create(model, type, false);
			for (int i = 0; i < array.Length; i++)
			{
				object obj;
				if (array[i].AttributeType.FullName == "ProtoBuf.ProtoContractAttribute" && array[i].TryGet("AsReferenceDefault", out obj))
				{
					return (bool)obj;
				}
			}
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001092C File Offset: 0x0000EB2C
		internal void ApplyDefaultBehaviour()
		{
			Type type = MetaType.GetBaseType(this);
			if (type != null && this.model.FindWithoutAdd(type) == null && MetaType.GetContractFamily(this.model, type, null) != MetaType.AttributeFamily.None)
			{
				this.model.FindOrAddAuto(type, true, false, false);
			}
			AttributeMap[] array = AttributeMap.Create(this.model, this.type, false);
			MetaType.AttributeFamily attributeFamily = MetaType.GetContractFamily(this.model, this.type, array);
			if (attributeFamily == MetaType.AttributeFamily.AutoTuple)
			{
				this.SetFlag(64, true, true);
			}
			bool flag = !this.EnumPassthru && Helpers.IsEnum(this.type);
			if (attributeFamily == MetaType.AttributeFamily.None && !flag)
			{
				return;
			}
			BasicList basicList = null;
			BasicList basicList2 = null;
			int dataMemberOffset = 0;
			int num = 1;
			bool flag2 = this.model.InferTagFromNameDefault;
			ImplicitFields implicitFields = ImplicitFields.None;
			string text = null;
			foreach (AttributeMap attributeMap in array)
			{
				string fullName = attributeMap.AttributeType.FullName;
				object obj;
				if (!flag && fullName == "ProtoBuf.ProtoIncludeAttribute")
				{
					int fieldNumber = 0;
					if (attributeMap.TryGet("tag", out obj))
					{
						fieldNumber = (int)obj;
					}
					DataFormat dataFormat = DataFormat.Default;
					if (attributeMap.TryGet("DataFormat", out obj))
					{
						dataFormat = (DataFormat)((int)obj);
					}
					Type type2 = null;
					try
					{
						if (attributeMap.TryGet("knownTypeName", out obj))
						{
							type2 = this.model.GetType((string)obj, this.type.Assembly);
						}
						else if (attributeMap.TryGet("knownType", out obj))
						{
							type2 = (Type)obj;
						}
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName, innerException);
					}
					if (type2 == null)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName);
					}
					if (this.IsValidSubType(type2))
					{
						this.AddSubType(fieldNumber, type2, dataFormat);
					}
				}
				if (fullName == "ProtoBuf.ProtoPartialIgnoreAttribute" && attributeMap.TryGet("MemberName", out obj) && obj != null)
				{
					if (basicList == null)
					{
						basicList = new BasicList();
					}
					basicList.Add((string)obj);
				}
				if (!flag && fullName == "ProtoBuf.ProtoPartialMemberAttribute")
				{
					if (basicList2 == null)
					{
						basicList2 = new BasicList();
					}
					basicList2.Add(attributeMap);
				}
				if (fullName == "ProtoBuf.ProtoContractAttribute")
				{
					if (attributeMap.TryGet("Name", out obj))
					{
						text = (string)obj;
					}
					if (Helpers.IsEnum(this.type))
					{
						if (attributeMap.TryGet("EnumPassthruHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("EnumPassthru", out obj))
						{
							this.EnumPassthru = (bool)obj;
							if (this.EnumPassthru)
							{
								flag = false;
							}
						}
					}
					else
					{
						if (attributeMap.TryGet("DataMemberOffset", out obj))
						{
							dataMemberOffset = (int)obj;
						}
						if (attributeMap.TryGet("InferTagFromNameHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("InferTagFromName", out obj))
						{
							flag2 = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFields", out obj) && obj != null)
						{
							implicitFields = (ImplicitFields)((int)obj);
						}
						if (attributeMap.TryGet("SkipConstructor", out obj))
						{
							this.UseConstructor = !(bool)obj;
						}
						if (attributeMap.TryGet("IgnoreListHandling", out obj))
						{
							this.IgnoreListHandling = (bool)obj;
						}
						if (attributeMap.TryGet("AsReferenceDefault", out obj))
						{
							this.AsReferenceDefault = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFirstTag", out obj) && (int)obj > 0)
						{
							num = (int)obj;
						}
					}
				}
				if (fullName == "System.Runtime.Serialization.DataContractAttribute" && text == null && attributeMap.TryGet("Name", out obj))
				{
					text = (string)obj;
				}
				if (fullName == "System.Xml.Serialization.XmlTypeAttribute" && text == null && attributeMap.TryGet("TypeName", out obj))
				{
					text = (string)obj;
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				this.Name = text;
			}
			if (implicitFields != ImplicitFields.None)
			{
				attributeFamily &= MetaType.AttributeFamily.ProtoBuf;
			}
			MethodInfo[] array2 = null;
			BasicList basicList3 = new BasicList();
			foreach (MemberInfo memberInfo in this.type.GetMembers(flag ? (BindingFlags.Static | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)))
			{
				if (memberInfo.DeclaringType == this.type && !memberInfo.IsDefined(this.model.MapType(typeof(ProtoIgnoreAttribute)), true) && (basicList == null || !basicList.Contains(memberInfo.Name)))
				{
					bool flag3 = false;
					PropertyInfo propertyInfo;
					FieldInfo fieldInfo;
					MethodInfo methodInfo;
					if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
					{
						if (!flag)
						{
							Type type3 = propertyInfo.PropertyType;
							bool isPublic = Helpers.GetGetMethod(propertyInfo, false, false) != null;
							bool isField = false;
							MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
						}
					}
					else if ((fieldInfo = (memberInfo as FieldInfo)) != null)
					{
						Type type3 = fieldInfo.FieldType;
						bool isPublic = fieldInfo.IsPublic;
						bool isField = true;
						if (!flag || fieldInfo.IsStatic)
						{
							MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
						}
					}
					else if ((methodInfo = (memberInfo as MethodInfo)) != null && !flag)
					{
						AttributeMap[] array3 = AttributeMap.Create(this.model, methodInfo, false);
						if (array3 != null && array3.Length != 0)
						{
							MetaType.CheckForCallback(methodInfo, array3, "ProtoBuf.ProtoBeforeSerializationAttribute", ref array2, 0);
							MetaType.CheckForCallback(methodInfo, array3, "ProtoBuf.ProtoAfterSerializationAttribute", ref array2, 1);
							MetaType.CheckForCallback(methodInfo, array3, "ProtoBuf.ProtoBeforeDeserializationAttribute", ref array2, 2);
							MetaType.CheckForCallback(methodInfo, array3, "ProtoBuf.ProtoAfterDeserializationAttribute", ref array2, 3);
							MetaType.CheckForCallback(methodInfo, array3, "System.Runtime.Serialization.OnSerializingAttribute", ref array2, 4);
							MetaType.CheckForCallback(methodInfo, array3, "System.Runtime.Serialization.OnSerializedAttribute", ref array2, 5);
							MetaType.CheckForCallback(methodInfo, array3, "System.Runtime.Serialization.OnDeserializingAttribute", ref array2, 6);
							MetaType.CheckForCallback(methodInfo, array3, "System.Runtime.Serialization.OnDeserializedAttribute", ref array2, 7);
						}
					}
				}
			}
			ProtoMemberAttribute[] array4 = new ProtoMemberAttribute[basicList3.Count];
			basicList3.CopyTo(array4, 0);
			if (flag2 || implicitFields != ImplicitFields.None)
			{
				Array.Sort<ProtoMemberAttribute>(array4);
				int num2 = num;
				foreach (ProtoMemberAttribute protoMemberAttribute in array4)
				{
					if (!protoMemberAttribute.TagIsPinned)
					{
						protoMemberAttribute.Rebase(num2++);
					}
				}
			}
			foreach (ProtoMemberAttribute normalizedAttribute in array4)
			{
				ValueMember valueMember = this.ApplyDefaultBehaviour(flag, normalizedAttribute);
				if (valueMember != null)
				{
					this.Add(valueMember);
				}
			}
			if (array2 != null)
			{
				this.SetCallbacks(MetaType.Coalesce(array2, 0, 4), MetaType.Coalesce(array2, 1, 5), MetaType.Coalesce(array2, 2, 6), MetaType.Coalesce(array2, 3, 7));
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011008 File Offset: 0x0000F208
		private static void ApplyDefaultBehaviour_AddMembers(TypeModel model, MetaType.AttributeFamily family, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferTagByName, ImplicitFields implicitMode, BasicList members, MemberInfo member, ref bool forced, bool isPublic, bool isField, ref Type effectiveType)
		{
			if (implicitMode != ImplicitFields.AllPublic)
			{
				if (implicitMode == ImplicitFields.AllFields && isField)
				{
					forced = true;
				}
			}
			else if (isPublic)
			{
				forced = true;
			}
			if (effectiveType.IsSubclassOf(model.MapType(typeof(Delegate))))
			{
				effectiveType = null;
			}
			if (effectiveType != null)
			{
				ProtoMemberAttribute protoMemberAttribute = MetaType.NormalizeProtoMember(model, member, family, forced, isEnum, partialMembers, dataMemberOffset, inferTagByName);
				if (protoMemberAttribute != null)
				{
					members.Add(protoMemberAttribute);
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00011074 File Offset: 0x0000F274
		private static MethodInfo Coalesce(MethodInfo[] arr, int x, int y)
		{
			MethodInfo methodInfo = arr[x];
			if (methodInfo == null)
			{
				methodInfo = arr[y];
			}
			return methodInfo;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00011090 File Offset: 0x0000F290
		internal static MetaType.AttributeFamily GetContractFamily(RuntimeTypeModel model, Type type, AttributeMap[] attributes)
		{
			MetaType.AttributeFamily attributeFamily = MetaType.AttributeFamily.None;
			if (attributes == null)
			{
				attributes = AttributeMap.Create(model, type, false);
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				string fullName = attributes[i].AttributeType.FullName;
				if (!(fullName == "ProtoBuf.ProtoContractAttribute"))
				{
					if (!(fullName == "System.Xml.Serialization.XmlTypeAttribute"))
					{
						if (fullName == "System.Runtime.Serialization.DataContractAttribute")
						{
							if (!model.AutoAddProtoContractTypesOnly)
							{
								attributeFamily |= MetaType.AttributeFamily.DataContractSerialier;
							}
						}
					}
					else if (!model.AutoAddProtoContractTypesOnly)
					{
						attributeFamily |= MetaType.AttributeFamily.XmlSerializer;
					}
				}
				else
				{
					bool flag = false;
					MetaType.GetFieldBoolean(ref flag, attributes[i], "UseProtoMembersOnly");
					if (flag)
					{
						return MetaType.AttributeFamily.ProtoBuf;
					}
					attributeFamily |= MetaType.AttributeFamily.ProtoBuf;
				}
			}
			MemberInfo[] array;
			if (attributeFamily == MetaType.AttributeFamily.None && MetaType.ResolveTupleConstructor(type, out array) != null)
			{
				attributeFamily |= MetaType.AttributeFamily.AutoTuple;
			}
			return attributeFamily;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001113C File Offset: 0x0000F33C
		internal static ConstructorInfo ResolveTupleConstructor(Type type, out MemberInfo[] mappedMembers)
		{
			mappedMembers = null;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsAbstract)
			{
				return null;
			}
			ConstructorInfo[] constructors = Helpers.GetConstructors(type, false);
			if (constructors.Length == 0 || (constructors.Length == 1 && constructors[0].GetParameters().Length == 0))
			{
				return null;
			}
			MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(type, true);
			BasicList basicList = new BasicList();
			for (int i = 0; i < instanceFieldsAndProperties.Length; i++)
			{
				PropertyInfo propertyInfo = instanceFieldsAndProperties[i] as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						return null;
					}
					if (propertyInfo.CanWrite && Helpers.GetSetMethod(propertyInfo, false, false) != null)
					{
						return null;
					}
					basicList.Add(propertyInfo);
				}
				else
				{
					FieldInfo fieldInfo = instanceFieldsAndProperties[i] as FieldInfo;
					if (fieldInfo != null)
					{
						if (!fieldInfo.IsInitOnly)
						{
							return null;
						}
						basicList.Add(fieldInfo);
					}
				}
			}
			if (basicList.Count == 0)
			{
				return null;
			}
			MemberInfo[] array = new MemberInfo[basicList.Count];
			basicList.CopyTo(array, 0);
			int[] array2 = new int[array.Length];
			int num = 0;
			ConstructorInfo result = null;
			mappedMembers = new MemberInfo[array2.Length];
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				if (parameters.Length == array.Length)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] = -1;
					}
					for (int l = 0; l < parameters.Length; l++)
					{
						string b = parameters[l].Name.ToLower();
						for (int m = 0; m < array.Length; m++)
						{
							if (!(array[m].Name.ToLower() != b) && Helpers.GetMemberType(array[m]) == parameters[l].ParameterType)
							{
								array2[l] = m;
							}
						}
					}
					bool flag = false;
					for (int n = 0; n < array2.Length; n++)
					{
						if (array2[n] < 0)
						{
							flag = true;
							break;
						}
						mappedMembers[n] = array[array2[n]];
					}
					if (!flag)
					{
						num++;
						result = constructors[j];
					}
				}
			}
			if (num != 1)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00011334 File Offset: 0x0000F534
		private static void CheckForCallback(MethodInfo method, AttributeMap[] attributes, string callbackTypeName, ref MethodInfo[] callbacks, int index)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].AttributeType.FullName == callbackTypeName)
				{
					if (callbacks == null)
					{
						callbacks = new MethodInfo[8];
					}
					else if (callbacks[index] != null)
					{
						Type reflectedType = method.ReflectedType;
						throw new ProtoException("Duplicate " + callbackTypeName + " callbacks on " + reflectedType.FullName);
					}
					callbacks[index] = method;
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00003F5A File Offset: 0x0000215A
		private static bool HasFamily(MetaType.AttributeFamily value, MetaType.AttributeFamily required)
		{
			return (value & required) == required;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000113A4 File Offset: 0x0000F5A4
		private static ProtoMemberAttribute NormalizeProtoMember(TypeModel model, MemberInfo member, MetaType.AttributeFamily family, bool forced, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferByTagName)
		{
			if (member == null || (family == MetaType.AttributeFamily.None && !isEnum))
			{
				return null;
			}
			int num = int.MinValue;
			int num2 = inferByTagName ? -1 : 1;
			string text = null;
			bool isPacked = false;
			bool flag = false;
			bool flag2 = false;
			bool isRequired = false;
			bool asReference = false;
			bool flag3 = false;
			bool dynamicType = false;
			bool tagIsPinned = false;
			bool overwriteList = false;
			DataFormat dataFormat = DataFormat.Default;
			if (isEnum)
			{
				forced = true;
			}
			AttributeMap[] attribs = AttributeMap.Create(model, member, true);
			if (isEnum)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (attribute != null)
				{
					flag = true;
				}
				else
				{
					attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoEnumAttribute");
					num = Convert.ToInt32(((FieldInfo)member).GetRawConstantValue());
					if (attribute != null)
					{
						MetaType.GetFieldName(ref text, attribute, "Name");
						object obj;
						if ((bool)Helpers.GetInstanceMethod(attribute.AttributeType, "HasValue").Invoke(attribute.Target, null) && attribute.TryGet("Value", out obj))
						{
							num = (int)obj;
						}
					}
				}
				flag2 = true;
			}
			if (!flag && !flag2)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMemberAttribute");
				MetaType.GetIgnore(ref flag, attribute, attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (!flag && attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Tag");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					MetaType.GetFieldBoolean(ref isPacked, attribute, "IsPacked");
					MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
					MetaType.GetDataFormat(ref dataFormat, attribute, "DataFormat");
					MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
					if (flag3)
					{
						flag3 = MetaType.GetFieldBoolean(ref asReference, attribute, "AsReference", true);
					}
					MetaType.GetFieldBoolean(ref dynamicType, attribute, "DynamicType");
					tagIsPinned = (flag2 = (num > 0));
				}
				if (!flag2 && partialMembers != null)
				{
					foreach (object obj2 in partialMembers)
					{
						AttributeMap attributeMap = (AttributeMap)obj2;
						object obj3;
						if (attributeMap.TryGet("MemberName", out obj3) && (string)obj3 == member.Name)
						{
							MetaType.GetFieldNumber(ref num, attributeMap, "Tag");
							MetaType.GetFieldName(ref text, attributeMap, "Name");
							MetaType.GetFieldBoolean(ref isRequired, attributeMap, "IsRequired");
							MetaType.GetFieldBoolean(ref isPacked, attributeMap, "IsPacked");
							MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
							MetaType.GetDataFormat(ref dataFormat, attributeMap, "DataFormat");
							MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
							if (flag3)
							{
								flag3 = MetaType.GetFieldBoolean(ref asReference, attributeMap, "AsReference", true);
							}
							MetaType.GetFieldBoolean(ref dynamicType, attributeMap, "DynamicType");
							if (flag2 = (tagIsPinned = (num > 0)))
							{
								break;
							}
						}
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.DataContractSerialier))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Runtime.Serialization.DataMemberAttribute");
				if (attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					flag2 = (num >= num2);
					if (flag2)
					{
						num += dataMemberOffset;
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.XmlSerializer))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlElementAttribute");
				if (attribute == null)
				{
					attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlArrayAttribute");
				}
				MetaType.GetIgnore(ref flag, attribute, attribs, "System.Xml.Serialization.XmlIgnoreAttribute");
				if (attribute != null && !flag)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "ElementName");
					flag2 = (num >= num2);
				}
			}
			if (!flag && !flag2 && MetaType.GetAttribute(attribs, "System.NonSerializedAttribute") != null)
			{
				flag = true;
			}
			if (flag || (num < num2 && !forced))
			{
				return null;
			}
			return new ProtoMemberAttribute(num, forced || inferByTagName)
			{
				AsReference = asReference,
				AsReferenceHasValue = flag3,
				DataFormat = dataFormat,
				DynamicType = dynamicType,
				IsPacked = isPacked,
				OverwriteList = overwriteList,
				IsRequired = isRequired,
				Name = (Helpers.IsNullOrEmpty(text) ? member.Name : text),
				Member = member,
				TagIsPinned = tagIsPinned
			};
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000117A4 File Offset: 0x0000F9A4
		private ValueMember ApplyDefaultBehaviour(bool isEnum, ProtoMemberAttribute normalizedAttribute)
		{
			MemberInfo member;
			if (normalizedAttribute == null || (member = normalizedAttribute.Member) == null)
			{
				return null;
			}
			Type memberType = Helpers.GetMemberType(member);
			Type type = null;
			Type defaultType = null;
			MetaType.ResolveListTypes(this.model, memberType, ref type, ref defaultType);
			if (type != null && this.model.FindOrAddAuto(memberType, false, true, false) >= 0 && this.model[memberType].IgnoreListHandling)
			{
				type = null;
				defaultType = null;
			}
			AttributeMap[] attribs = AttributeMap.Create(this.model, member, true);
			object defaultValue = null;
			if (this.model.UseImplicitZeroDefaults)
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(memberType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultValue = false;
					break;
				case ProtoTypeCode.Char:
					defaultValue = '\0';
					break;
				case ProtoTypeCode.SByte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Byte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int32:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt32:
					defaultValue = 0u;
					break;
				case ProtoTypeCode.Int64:
					defaultValue = 0L;
					break;
				case ProtoTypeCode.UInt64:
					defaultValue = 0UL;
					break;
				case ProtoTypeCode.Single:
					defaultValue = 0f;
					break;
				case ProtoTypeCode.Double:
					defaultValue = 0.0;
					break;
				case ProtoTypeCode.Decimal:
					defaultValue = 0m;
					break;
				default:
					if (typeCode != ProtoTypeCode.TimeSpan)
					{
						if (typeCode == ProtoTypeCode.Guid)
						{
							defaultValue = Guid.Empty;
						}
					}
					else
					{
						defaultValue = TimeSpan.Zero;
					}
					break;
				}
			}
			AttributeMap attribute;
			object obj;
			if ((attribute = MetaType.GetAttribute(attribs, "System.ComponentModel.DefaultValueAttribute")) != null && attribute.TryGet("Value", out obj))
			{
				defaultValue = obj;
			}
			ValueMember valueMember = (isEnum || normalizedAttribute.Tag > 0) ? new ValueMember(this.model, this.type, normalizedAttribute.Tag, member, memberType, type, defaultType, normalizedAttribute.DataFormat, defaultValue) : null;
			if (valueMember != null)
			{
				Type declaringType = this.type;
				PropertyInfo propertyInfo = Helpers.GetProperty(declaringType, member.Name + "Specified", true);
				MethodInfo getMethod = Helpers.GetGetMethod(propertyInfo, true, true);
				if (getMethod == null || getMethod.IsStatic)
				{
					propertyInfo = null;
				}
				if (propertyInfo != null)
				{
					valueMember.SetSpecified(getMethod, Helpers.GetSetMethod(propertyInfo, true, true));
				}
				else
				{
					MethodInfo instanceMethod = Helpers.GetInstanceMethod(declaringType, "ShouldSerialize" + member.Name, Helpers.EmptyTypes);
					if (instanceMethod != null && instanceMethod.ReturnType == this.model.MapType(typeof(bool)))
					{
						valueMember.SetSpecified(instanceMethod, null);
					}
				}
				if (!Helpers.IsNullOrEmpty(normalizedAttribute.Name))
				{
					valueMember.SetName(normalizedAttribute.Name);
				}
				valueMember.IsPacked = normalizedAttribute.IsPacked;
				valueMember.IsRequired = normalizedAttribute.IsRequired;
				valueMember.OverwriteList = normalizedAttribute.OverwriteList;
				if (normalizedAttribute.AsReferenceHasValue)
				{
					valueMember.AsReference = normalizedAttribute.AsReference;
				}
				valueMember.DynamicType = normalizedAttribute.DynamicType;
			}
			return valueMember;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		private static void GetDataFormat(ref DataFormat value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value != DataFormat.Default)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (DataFormat)obj;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00003F62 File Offset: 0x00002162
		private static void GetIgnore(ref bool ignore, AttributeMap attrib, AttributeMap[] attribs, string fullName)
		{
			if (ignore || attrib == null)
			{
				return;
			}
			ignore = (MetaType.GetAttribute(attribs, fullName) != null);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00003F78 File Offset: 0x00002178
		private static void GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName)
		{
			MetaType.GetFieldBoolean(ref value, attrib, memberName, true);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00011ACC File Offset: 0x0000FCCC
		private static bool GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName, bool publicOnly)
		{
			if (attrib == null)
			{
				return false;
			}
			if (value)
			{
				return true;
			}
			object obj;
			if (attrib.TryGet(memberName, publicOnly, out obj) && obj != null)
			{
				value = (bool)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00011B00 File Offset: 0x0000FD00
		private static void GetFieldNumber(ref int value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value > 0)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (int)obj;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00011B2C File Offset: 0x0000FD2C
		private static void GetFieldName(ref string name, AttributeMap attrib, string memberName)
		{
			if (attrib == null || !Helpers.IsNullOrEmpty(name))
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				name = (string)obj;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00011B5C File Offset: 0x0000FD5C
		private static AttributeMap GetAttribute(AttributeMap[] attribs, string fullName)
		{
			foreach (AttributeMap attributeMap in attribs)
			{
				if (attributeMap != null && attributeMap.AttributeType.FullName == fullName)
				{
					return attributeMap;
				}
			}
			return null;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00003F84 File Offset: 0x00002184
		public MetaType Add(int fieldNumber, string memberName)
		{
			this.AddField(fieldNumber, memberName, null, null, null);
			return this;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00003F93 File Offset: 0x00002193
		public ValueMember AddField(int fieldNumber, string memberName)
		{
			return this.AddField(fieldNumber, memberName, null, null, null);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00003FA0 File Offset: 0x000021A0
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00003FAD File Offset: 0x000021AD
		public bool UseConstructor
		{
			get
			{
				return !this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, !value, true);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00003FBC File Offset: 0x000021BC
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00003FC4 File Offset: 0x000021C4
		public Type ConstructType
		{
			get
			{
				return this.constructType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.constructType = value;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00003FD3 File Offset: 0x000021D3
		public MetaType Add(string memberName)
		{
			this.Add(this.GetNextFieldNumber(), memberName);
			return this;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011B94 File Offset: 0x0000FD94
		public void SetSurrogate(Type surrogateType)
		{
			if (surrogateType == this.type)
			{
				surrogateType = null;
			}
			if (surrogateType != null && surrogateType != null && Helpers.IsAssignableFrom(this.model.MapType(typeof(IEnumerable)), surrogateType))
			{
				throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a surrogate");
			}
			this.ThrowIfFrozen();
			this.surrogate = surrogateType;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00003FE4 File Offset: 0x000021E4
		internal MetaType GetSurrogateOrSelf()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			return this;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		internal MetaType GetSurrogateOrBaseOrSelf(bool deep)
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			MetaType metaType = this.baseType;
			if (metaType == null)
			{
				return this;
			}
			if (deep)
			{
				MetaType result;
				do
				{
					result = metaType;
					metaType = metaType.baseType;
				}
				while (metaType != null);
				return result;
			}
			return metaType;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011C30 File Offset: 0x0000FE30
		private int GetNextFieldNumber()
		{
			int num = 0;
			foreach (object obj in this.fields)
			{
				ValueMember valueMember = (ValueMember)obj;
				if (valueMember.FieldNumber > num)
				{
					num = valueMember.FieldNumber;
				}
			}
			if (this.subTypes != null)
			{
				foreach (object obj2 in this.subTypes)
				{
					SubType subType = (SubType)obj2;
					if (subType.FieldNumber > num)
					{
						num = subType.FieldNumber;
					}
				}
			}
			return num + 1;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public MetaType Add(params string[] memberNames)
		{
			if (memberNames == null)
			{
				throw new ArgumentNullException("memberNames");
			}
			int nextFieldNumber = this.GetNextFieldNumber();
			for (int i = 0; i < memberNames.Length; i++)
			{
				this.Add(nextFieldNumber++, memberNames[i]);
			}
			return this;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00004001 File Offset: 0x00002201
		public MetaType Add(int fieldNumber, string memberName, object defaultValue)
		{
			this.AddField(fieldNumber, memberName, null, null, defaultValue);
			return this;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00004010 File Offset: 0x00002210
		public MetaType Add(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			this.AddField(fieldNumber, memberName, itemType, defaultType, null);
			return this;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00004020 File Offset: 0x00002220
		public ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			return this.AddField(fieldNumber, memberName, itemType, defaultType, null);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		private ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType, object defaultValue)
		{
			MemberInfo memberInfo = null;
			MemberInfo[] member = this.type.GetMember(memberName, Helpers.IsEnum(this.type) ? (BindingFlags.Static | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
			if (member != null && member.Length == 1)
			{
				memberInfo = member[0];
			}
			if (memberInfo == null)
			{
				throw new ArgumentException("Unable to determine member: " + memberName, "memberName");
			}
			MemberTypes memberType = memberInfo.MemberType;
			Type memberType2;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new NotSupportedException(memberInfo.MemberType.ToString());
				}
				memberType2 = ((PropertyInfo)memberInfo).PropertyType;
			}
			else
			{
				memberType2 = ((FieldInfo)memberInfo).FieldType;
			}
			MetaType.ResolveListTypes(this.model, memberType2, ref itemType, ref defaultType);
			ValueMember valueMember = new ValueMember(this.model, this.type, fieldNumber, memberInfo, memberType2, itemType, defaultType, DataFormat.Default, defaultValue);
			this.Add(valueMember);
			return valueMember;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		internal static void ResolveListTypes(TypeModel model, Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (type.IsArray)
			{
				if (type.GetArrayRank() != 1)
				{
					throw new NotSupportedException("Multi-dimension arrays are supported");
				}
				itemType = type.GetElementType();
				if (itemType == model.MapType(typeof(byte)))
				{
					Type type2;
					itemType = (type2 = null);
					defaultType = type2;
				}
				else
				{
					defaultType = type;
				}
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(model, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				MetaType.ResolveListTypes(model, itemType, ref type3, ref type4);
				if (type3 != null)
				{
					throw TypeModel.CreateNestedListsNotSupported();
				}
			}
			if (itemType != null && defaultType == null)
			{
				if (type.IsClass && !type.IsAbstract && Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != null)
				{
					defaultType = type;
				}
				if (defaultType == null && type.IsInterface)
				{
					Type[] genericArguments;
					if (type.IsGenericType && type.GetGenericTypeDefinition() == model.MapType(typeof(IDictionary<, >)) && itemType == model.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = model.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = model.MapType(typeof(List<>)).MakeGenericType(new Type[]
						{
							itemType
						});
					}
				}
				if (defaultType != null && !Helpers.IsAssignableFrom(type, defaultType))
				{
					defaultType = null;
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011F14 File Offset: 0x00010114
		private void Add(ValueMember member)
		{
			int opaqueToken = 0;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				this.ThrowIfFrozen();
				this.fields.Add(member);
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x170000D8 RID: 216
		public ValueMember this[int fieldNumber]
		{
			get
			{
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.FieldNumber == fieldNumber)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x170000D9 RID: 217
		public ValueMember this[MemberInfo member]
		{
			get
			{
				if (member == null)
				{
					return null;
				}
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.Member == member)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00011FE8 File Offset: 0x000101E8
		public ValueMember[] GetFields()
		{
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			return array;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00012020 File Offset: 0x00010220
		public SubType[] GetSubtypes()
		{
			if (this.subTypes == null || this.subTypes.Count == 0)
			{
				return new SubType[0];
			}
			SubType[] array = new SubType[this.subTypes.Count];
			this.subTypes.CopyTo(array, 0);
			Array.Sort<SubType>(array, SubType.Comparer.Default);
			return array;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012074 File Offset: 0x00010274
		internal bool IsDefined(int fieldNumber)
		{
			BasicList.NodeEnumerator enumerator = this.fields.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((ValueMember)enumerator.Current).FieldNumber == fieldNumber)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000402E File Offset: 0x0000222E
		internal int GetKey(bool demand, bool getBaseKey)
		{
			return this.model.GetKey(this.type, demand, getBaseKey);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000120B0 File Offset: 0x000102B0
		internal EnumSerializer.EnumPair[] GetEnumMap()
		{
			if (this.HasFlag(2))
			{
				return null;
			}
			EnumSerializer.EnumPair[] array = new EnumSerializer.EnumPair[this.fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ValueMember valueMember = (ValueMember)this.fields[i];
				int fieldNumber = valueMember.FieldNumber;
				object rawEnumValue = valueMember.GetRawEnumValue();
				array[i] = new EnumSerializer.EnumPair(fieldNumber, rawEnumValue, valueMember.MemberType);
			}
			return array;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00004043 File Offset: 0x00002243
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000404C File Offset: 0x0000224C
		public bool EnumPassthru
		{
			get
			{
				return this.HasFlag(2);
			}
			set
			{
				this.SetFlag(2, value, true);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00004057 File Offset: 0x00002257
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00004064 File Offset: 0x00002264
		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(128);
			}
			set
			{
				this.SetFlag(128, value, true);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00004073 File Offset: 0x00002273
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000407C File Offset: 0x0000227C
		internal bool Pending
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, false);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00004087 File Offset: 0x00002287
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00004096 File Offset: 0x00002296
		private void SetFlag(byte flag, bool value, bool throwIfFrozen)
		{
			if (throwIfFrozen && this.HasFlag(flag) != value)
			{
				this.ThrowIfFrozen();
			}
			if (value)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012120 File Offset: 0x00010320
		internal static MetaType GetRootType(MetaType source)
		{
			while (source.serializer != null)
			{
				MetaType metaType = source.baseType;
				if (metaType == null)
				{
					return source;
				}
				source = metaType;
			}
			RuntimeTypeModel runtimeTypeModel = source.model;
			int opaqueToken = 0;
			MetaType result;
			try
			{
				runtimeTypeModel.TakeLock(ref opaqueToken);
				MetaType metaType2;
				while ((metaType2 = source.baseType) != null)
				{
					source = metaType2;
				}
				result = source;
			}
			finally
			{
				runtimeTypeModel.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000031DF File Offset: 0x000013DF
		internal bool IsPrepared()
		{
			return false;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000040D6 File Offset: 0x000022D6
		internal IEnumerable Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000040DE File Offset: 0x000022DE
		internal static StringBuilder NewLine(StringBuilder builder, int indent)
		{
			return Helpers.AppendLine(builder).Append(' ', indent * 3);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000040F0 File Offset: 0x000022F0
		internal bool IsAutoTuple
		{
			get
			{
				return this.HasFlag(64);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00012188 File Offset: 0x00010388
		internal void WriteSchema(StringBuilder builder, int indent, ref bool requiresBclImport)
		{
			if (this.surrogate != null)
			{
				return;
			}
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			if (this.IsList)
			{
				string schemaTypeName = this.model.GetSchemaTypeName(TypeModel.GetListItemType(this.model, this.type), DataFormat.Default, false, false, ref requiresBclImport);
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				MetaType.NewLine(builder, indent + 1).Append("repeated ").Append(schemaTypeName).Append(" items = 1;");
				MetaType.NewLine(builder, indent).Append('}');
				return;
			}
			if (this.IsAutoTuple)
			{
				MemberInfo[] array2;
				if (MetaType.ResolveTupleConstructor(this.type, out array2) != null)
				{
					MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
					for (int i = 0; i < array2.Length; i++)
					{
						Type effectiveType;
						if (array2[i] is PropertyInfo)
						{
							effectiveType = ((PropertyInfo)array2[i]).PropertyType;
						}
						else
						{
							if (!(array2[i] is FieldInfo))
							{
								throw new NotSupportedException("Unknown member type: " + array2[i].GetType().Name);
							}
							effectiveType = ((FieldInfo)array2[i]).FieldType;
						}
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(this.model.GetSchemaTypeName(effectiveType, DataFormat.Default, false, false, ref requiresBclImport).Replace('.', '_')).Append(' ').Append(array2[i].Name).Append(" = ").Append(i + 1).Append(';');
					}
					MetaType.NewLine(builder, indent).Append('}');
					return;
				}
			}
			else
			{
				if (Helpers.IsEnum(this.type))
				{
					MetaType.NewLine(builder, indent).Append("enum ").Append(this.GetSchemaTypeName()).Append(" {");
					if (array.Length == 0 && this.EnumPassthru)
					{
						if (this.type.IsDefined(this.model.MapType(typeof(FlagsAttribute)), false))
						{
							MetaType.NewLine(builder, indent + 1).Append("// this is a composite/flags enumeration");
						}
						else
						{
							MetaType.NewLine(builder, indent + 1).Append("// this enumeration will be passed as a raw value");
						}
						foreach (FieldInfo fieldInfo in this.type.GetFields())
						{
							if (fieldInfo.IsStatic && fieldInfo.IsLiteral)
							{
								object rawConstantValue = fieldInfo.GetRawConstantValue();
								MetaType.NewLine(builder, indent + 1).Append(fieldInfo.Name).Append(" = ").Append(rawConstantValue).Append(";");
							}
						}
					}
					else
					{
						foreach (ValueMember valueMember in array)
						{
							MetaType.NewLine(builder, indent + 1).Append(valueMember.Name).Append(" = ").Append(valueMember.FieldNumber).Append(';');
						}
					}
					MetaType.NewLine(builder, indent).Append('}');
					return;
				}
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				foreach (ValueMember valueMember2 in array)
				{
					string value = (valueMember2.ItemType != null) ? "repeated" : (valueMember2.IsRequired ? "required" : "optional");
					MetaType.NewLine(builder, indent + 1).Append(value).Append(' ');
					if (valueMember2.DataFormat == DataFormat.Group)
					{
						builder.Append("group ");
					}
					string schemaTypeName2 = valueMember2.GetSchemaTypeName(true, ref requiresBclImport);
					builder.Append(schemaTypeName2).Append(" ").Append(valueMember2.Name).Append(" = ").Append(valueMember2.FieldNumber);
					if (valueMember2.DefaultValue != null)
					{
						if (valueMember2.DefaultValue is string)
						{
							builder.Append(" [default = \"").Append(valueMember2.DefaultValue).Append("\"]");
						}
						else if (valueMember2.DefaultValue is bool)
						{
							builder.Append(((bool)valueMember2.DefaultValue) ? " [default = true]" : " [default = false]");
						}
						else
						{
							builder.Append(" [default = ").Append(valueMember2.DefaultValue).Append(']');
						}
					}
					if (valueMember2.ItemType != null && valueMember2.IsPacked)
					{
						builder.Append(" [packed=true]");
					}
					builder.Append(';');
					if (schemaTypeName2 == "bcl.NetObjectProxy" && valueMember2.AsReference && !valueMember2.DynamicType)
					{
						builder.Append(" // reference-tracked ").Append(valueMember2.GetSchemaTypeName(false, ref requiresBclImport));
					}
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					MetaType.NewLine(builder, indent + 1).Append("// the following represent sub-types; at most 1 should have a value");
					SubType[] array5 = new SubType[this.subTypes.Count];
					this.subTypes.CopyTo(array5, 0);
					Array.Sort<SubType>(array5, SubType.Comparer.Default);
					foreach (SubType subType in array5)
					{
						string schemaTypeName3 = subType.DerivedType.GetSchemaTypeName();
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(schemaTypeName3).Append(" ").Append(schemaTypeName3).Append(" = ").Append(subType.FieldNumber).Append(';');
					}
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
		}

		// Token: 0x04000145 RID: 325
		private MetaType baseType;

		// Token: 0x04000146 RID: 326
		private BasicList subTypes;

		// Token: 0x04000147 RID: 327
		internal static readonly Type ienumerable = typeof(IEnumerable);

		// Token: 0x04000148 RID: 328
		private CallbackSet callbacks;

		// Token: 0x04000149 RID: 329
		private string name;

		// Token: 0x0400014A RID: 330
		private MethodInfo factory;

		// Token: 0x0400014B RID: 331
		private readonly RuntimeTypeModel model;

		// Token: 0x0400014C RID: 332
		private readonly Type type;

		// Token: 0x0400014D RID: 333
		private IProtoTypeSerializer serializer;

		// Token: 0x0400014E RID: 334
		private Type constructType;

		// Token: 0x0400014F RID: 335
		private Type surrogate;

		// Token: 0x04000150 RID: 336
		private readonly BasicList fields = new BasicList();

		// Token: 0x04000151 RID: 337
		private const byte OPTIONS_Pending = 1;

		// Token: 0x04000152 RID: 338
		private const byte OPTIONS_EnumPassThru = 2;

		// Token: 0x04000153 RID: 339
		private const byte OPTIONS_Frozen = 4;

		// Token: 0x04000154 RID: 340
		private const byte OPTIONS_PrivateOnApi = 8;

		// Token: 0x04000155 RID: 341
		private const byte OPTIONS_SkipConstructor = 16;

		// Token: 0x04000156 RID: 342
		private const byte OPTIONS_AsReferenceDefault = 32;

		// Token: 0x04000157 RID: 343
		private const byte OPTIONS_AutoTuple = 64;

		// Token: 0x04000158 RID: 344
		private const byte OPTIONS_IgnoreListHandling = 128;

		// Token: 0x04000159 RID: 345
		private volatile byte flags;

		// Token: 0x02000068 RID: 104
		internal sealed class Comparer : IComparer, IComparer<MetaType>
		{
			// Token: 0x06000359 RID: 857 RVA: 0x0000410B File Offset: 0x0000230B
			public int Compare(object x, object y)
			{
				return this.Compare(x as MetaType, y as MetaType);
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000411F File Offset: 0x0000231F
			public int Compare(MetaType x, MetaType y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return string.Compare(x.GetSchemaTypeName(), y.GetSchemaTypeName(), StringComparison.Ordinal);
			}

			// Token: 0x0400015A RID: 346
			public static readonly MetaType.Comparer Default = new MetaType.Comparer();
		}

		// Token: 0x02000069 RID: 105
		[Flags]
		internal enum AttributeFamily
		{
			// Token: 0x0400015C RID: 348
			None = 0,
			// Token: 0x0400015D RID: 349
			ProtoBuf = 1,
			// Token: 0x0400015E RID: 350
			DataContractSerialier = 2,
			// Token: 0x0400015F RID: 351
			XmlSerializer = 4,
			// Token: 0x04000160 RID: 352
			AutoTuple = 8
		}
	}
}
