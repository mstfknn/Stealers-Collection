using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000077 RID: 119
	public class ValueMember
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000046BD File Offset: 0x000028BD
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000046C5 File Offset: 0x000028C5
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000046CD File Offset: 0x000028CD
		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000046D5 File Offset: 0x000028D5
		public Type MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000046DD File Offset: 0x000028DD
		public Type DefaultType
		{
			get
			{
				return this.defaultType;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000046E5 File Offset: 0x000028E5
		public Type ParentType
		{
			get
			{
				return this.parentType;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000046ED File Offset: 0x000028ED
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x000046F5 File Offset: 0x000028F5
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.ThrowIfFrozen();
				this.defaultValue = value;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014D68 File Offset: 0x00012F68
		public ValueMember(RuntimeTypeModel model, Type parentType, int fieldNumber, MemberInfo member, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat, object defaultValue) : this(model, fieldNumber, memberType, itemType, defaultType, dataFormat)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (parentType == null)
			{
				throw new ArgumentNullException("parentType");
			}
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this.member = member;
			this.parentType = parentType;
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (defaultValue != null && model.MapType(defaultValue.GetType()) != memberType)
			{
				defaultValue = ValueMember.ParseDefaultValue(memberType, defaultValue);
			}
			this.defaultValue = defaultValue;
			MetaType metaType = model.FindWithoutAdd(memberType);
			if (metaType != null)
			{
				this.asReference = metaType.AsReferenceDefault;
				return;
			}
			this.asReference = MetaType.GetAsReferenceDefault(model, memberType);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014E30 File Offset: 0x00013030
		internal ValueMember(RuntimeTypeModel model, int fieldNumber, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat)
		{
			if (memberType == null)
			{
				throw new ArgumentNullException("memberType");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.fieldNumber = fieldNumber;
			this.memberType = memberType;
			this.itemType = itemType;
			this.defaultType = defaultType;
			this.model = model;
			this.dataFormat = dataFormat;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00004704 File Offset: 0x00002904
		internal object GetRawEnumValue()
		{
			return ((FieldInfo)this.member).GetRawConstantValue();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014E8C File Offset: 0x0001308C
		private static object ParseDefaultValue(Type type, object value)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (value is string)
			{
				string text = (string)value;
				if (Helpers.IsEnum(type))
				{
					return Helpers.ParseEnum(type, text);
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return bool.Parse(text);
				case ProtoTypeCode.Char:
					if (text.Length == 1)
					{
						return text[0];
					}
					throw new FormatException("Single character expected: \"" + text + "\"");
				case ProtoTypeCode.SByte:
					return sbyte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Byte:
					return byte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int16:
					return short.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt16:
					return ushort.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int32:
					return int.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt32:
					return uint.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int64:
					return long.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt64:
					return ulong.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Single:
					return float.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Double:
					return double.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Decimal:
					return decimal.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.DateTime:
					return DateTime.Parse(text, CultureInfo.InvariantCulture);
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					return text;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						return TimeSpan.Parse(text);
					case ProtoTypeCode.Guid:
						return new Guid(text);
					case ProtoTypeCode.Uri:
						return text;
					}
					break;
				}
			}
			if (Helpers.IsEnum(type))
			{
				return Enum.ToObject(type, value);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00004716 File Offset: 0x00002916
		internal IProtoSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.BuildSerializer();
				}
				return this.serializer;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00004732 File Offset: 0x00002932
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000473A File Offset: 0x0000293A
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dataFormat = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00004749 File Offset: 0x00002949
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00004752 File Offset: 0x00002952
		public bool IsStrict
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, true);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000475D File Offset: 0x0000295D
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00004766 File Offset: 0x00002966
		public bool IsPacked
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00004771 File Offset: 0x00002971
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000477A File Offset: 0x0000297A
		public bool OverwriteList
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value, true);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00004785 File Offset: 0x00002985
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000478E File Offset: 0x0000298E
		public bool IsRequired
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value, true);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00004799 File Offset: 0x00002999
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x000047A1 File Offset: 0x000029A1
		public bool AsReference
		{
			get
			{
				return this.asReference;
			}
			set
			{
				this.ThrowIfFrozen();
				this.asReference = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000047B0 File Offset: 0x000029B0
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x000047B8 File Offset: 0x000029B8
		public bool DynamicType
		{
			get
			{
				return this.dynamicType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dynamicType = value;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000150A0 File Offset: 0x000132A0
		public void SetSpecified(MethodInfo getSpecified, MethodInfo setSpecified)
		{
			if (getSpecified != null && (getSpecified.ReturnType != this.model.MapType(typeof(bool)) || getSpecified.IsStatic || getSpecified.GetParameters().Length != 0))
			{
				throw new ArgumentException("Invalid pattern for checking member-specified", "getSpecified");
			}
			ParameterInfo[] parameters;
			if (setSpecified != null && (setSpecified.ReturnType != this.model.MapType(typeof(void)) || setSpecified.IsStatic || (parameters = setSpecified.GetParameters()).Length != 1 || parameters[0].ParameterType != this.model.MapType(typeof(bool))))
			{
				throw new ArgumentException("Invalid pattern for setting member-specified", "setSpecified");
			}
			this.ThrowIfFrozen();
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000047C7 File Offset: 0x000029C7
		private void ThrowIfFrozen()
		{
			if (this.serializer != null)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015168 File Offset: 0x00013368
		private IProtoSerializer BuildSerializer()
		{
			int opaqueToken = 0;
			IProtoSerializer result;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				Type type = (this.itemType == null) ? this.memberType : this.itemType;
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this.model, this.dataFormat, type, out wireType, this.asReference, this.dynamicType, this.OverwriteList, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type.FullName);
				}
				if (this.itemType != null && this.SupportNull)
				{
					if (this.IsPacked)
					{
						throw new NotSupportedException("Packed encodings cannot support null values");
					}
					protoSerializer = new TagDecorator(1, wireType, this.IsStrict, protoSerializer);
					protoSerializer = new NullDecorator(this.model, protoSerializer);
					protoSerializer = new TagDecorator(this.fieldNumber, WireType.StartGroup, false, protoSerializer);
				}
				else
				{
					protoSerializer = new TagDecorator(this.fieldNumber, wireType, this.IsStrict, protoSerializer);
				}
				if (this.itemType != null)
				{
					if (!this.SupportNull)
					{
						if (Helpers.GetUnderlyingType(this.itemType) == null)
						{
							Type type2 = this.itemType;
						}
					}
					else
					{
						Type type3 = this.itemType;
					}
					if (this.memberType.IsArray)
					{
						protoSerializer = new ArrayDecorator(this.model, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.memberType, this.OverwriteList, this.SupportNull);
					}
					else
					{
						protoSerializer = ListDecorator.Create(this.model, this.memberType, this.defaultType, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.member != null && PropertyDecorator.CanWrite(this.model, this.member), this.OverwriteList, this.SupportNull);
					}
				}
				else if (this.defaultValue != null && !this.IsRequired && this.getSpecified == null)
				{
					protoSerializer = new DefaultValueDecorator(this.model, this.defaultValue, protoSerializer);
				}
				if (this.memberType == this.model.MapType(typeof(Uri)))
				{
					protoSerializer = new UriDecorator(this.model, protoSerializer);
				}
				if (this.member != null)
				{
					if (this.member is PropertyInfo)
					{
						protoSerializer = new PropertyDecorator(this.model, this.parentType, (PropertyInfo)this.member, protoSerializer);
					}
					else
					{
						if (!(this.member is FieldInfo))
						{
							throw new InvalidOperationException();
						}
						protoSerializer = new FieldDecorator(this.parentType, (FieldInfo)this.member, protoSerializer);
					}
					if (this.getSpecified != null || this.setSpecified != null)
					{
						protoSerializer = new MemberSpecifiedDecorator(this.getSpecified, this.setSpecified, protoSerializer);
					}
				}
				result = protoSerializer;
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000047DC File Offset: 0x000029DC
		private static WireType GetIntWireType(DataFormat format, int width)
		{
			switch (format)
			{
			case DataFormat.Default:
			case DataFormat.TwosComplement:
				return WireType.Variant;
			case DataFormat.ZigZag:
				return WireType.SignedVariant;
			case DataFormat.FixedSize:
				if (width != 32)
				{
					return WireType.Fixed64;
				}
				return WireType.Fixed32;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00004808 File Offset: 0x00002A08
		private static WireType GetDateTimeWireType(DataFormat format)
		{
			switch (format)
			{
			case DataFormat.Default:
				return WireType.String;
			case DataFormat.FixedSize:
				return WireType.Fixed64;
			case DataFormat.Group:
				return WireType.StartGroup;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015408 File Offset: 0x00013608
		internal static IProtoSerializer TryGetCoreSerializer(RuntimeTypeModel model, DataFormat dataFormat, Type type, out WireType defaultWireType, bool asReference, bool dynamicType, bool overwriteList, bool allowComplexTypes)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (Helpers.IsEnum(type))
			{
				if (allowComplexTypes && model != null)
				{
					defaultWireType = WireType.Variant;
					return new EnumSerializer(type, model.GetEnumMap(type));
				}
				defaultWireType = WireType.None;
				return null;
			}
			else
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultWireType = WireType.Variant;
					return new BooleanSerializer(model);
				case ProtoTypeCode.Char:
					defaultWireType = WireType.Variant;
					return new CharSerializer(model);
				case ProtoTypeCode.SByte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new SByteSerializer(model);
				case ProtoTypeCode.Byte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new ByteSerializer(model);
				case ProtoTypeCode.Int16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int16Serializer(model);
				case ProtoTypeCode.UInt16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt16Serializer(model);
				case ProtoTypeCode.Int32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int32Serializer(model);
				case ProtoTypeCode.UInt32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt32Serializer(model);
				case ProtoTypeCode.Int64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new Int64Serializer(model);
				case ProtoTypeCode.UInt64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new UInt64Serializer(model);
				case ProtoTypeCode.Single:
					defaultWireType = WireType.Fixed32;
					return new SingleSerializer(model);
				case ProtoTypeCode.Double:
					defaultWireType = WireType.Fixed64;
					return new DoubleSerializer(model);
				case ProtoTypeCode.Decimal:
					defaultWireType = WireType.String;
					return new DecimalSerializer(model);
				case ProtoTypeCode.DateTime:
					defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
					return new DateTimeSerializer(model);
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					defaultWireType = WireType.String;
					if (asReference)
					{
						return new NetObjectSerializer(model, model.MapType(typeof(string)), 0, BclHelpers.NetObjectOptions.AsReference);
					}
					return new StringSerializer(model);
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
						return new TimeSpanSerializer(model);
					case ProtoTypeCode.ByteArray:
						defaultWireType = WireType.String;
						return new BlobSerializer(model, overwriteList);
					case ProtoTypeCode.Guid:
						defaultWireType = WireType.String;
						return new GuidSerializer(model);
					case ProtoTypeCode.Uri:
						defaultWireType = WireType.String;
						return new StringSerializer(model);
					case ProtoTypeCode.Type:
						defaultWireType = WireType.String;
						return new SystemTypeSerializer(model);
					}
					break;
				}
				IProtoSerializer protoSerializer = model.AllowParseableTypes ? ParseableSerializer.TryCreate(type, model) : null;
				if (protoSerializer != null)
				{
					defaultWireType = WireType.String;
					return protoSerializer;
				}
				if (allowComplexTypes && model != null)
				{
					int key = model.GetKey(type, false, true);
					if (asReference || dynamicType)
					{
						defaultWireType = ((dataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String);
						BclHelpers.NetObjectOptions netObjectOptions = BclHelpers.NetObjectOptions.None;
						if (asReference)
						{
							netObjectOptions |= BclHelpers.NetObjectOptions.AsReference;
						}
						if (dynamicType)
						{
							netObjectOptions |= BclHelpers.NetObjectOptions.DynamicType;
						}
						if (key >= 0)
						{
							if (asReference && Helpers.IsValueType(type))
							{
								string text = "AsReference cannot be used with value-types";
								if (type.Name == "KeyValuePair`2")
								{
									text += "; please see http://stackoverflow.com/q/14436606/";
								}
								else
								{
									text = text + ": " + type.FullName;
								}
								throw new InvalidOperationException(text);
							}
							MetaType metaType = model[type];
							if (asReference && metaType.IsAutoTuple)
							{
								netObjectOptions |= BclHelpers.NetObjectOptions.LateSet;
							}
							if (metaType.UseConstructor)
							{
								netObjectOptions |= BclHelpers.NetObjectOptions.UseConstructor;
							}
						}
						return new NetObjectSerializer(model, type, key, netObjectOptions);
					}
					if (key >= 0)
					{
						defaultWireType = ((dataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String);
						return new SubItemSerializer(type, key, model[type], true);
					}
				}
				defaultWireType = WireType.None;
				return null;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00004831 File Offset: 0x00002A31
		internal void SetName(string name)
		{
			this.ThrowIfFrozen();
			this.name = name;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00004840 File Offset: 0x00002A40
		public string Name
		{
			get
			{
				if (!Helpers.IsNullOrEmpty(this.name))
				{
					return this.name;
				}
				return this.member.Name;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00004861 File Offset: 0x00002A61
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000486E File Offset: 0x00002A6E
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000048A6 File Offset: 0x00002AA6
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000048B0 File Offset: 0x00002AB0
		public bool SupportNull
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value, true);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000156EC File Offset: 0x000138EC
		internal string GetSchemaTypeName(bool applyNetObjectProxy, ref bool requiresBclImport)
		{
			Type type = this.ItemType;
			if (type == null)
			{
				type = this.MemberType;
			}
			return this.model.GetSchemaTypeName(type, this.DataFormat, applyNetObjectProxy && this.asReference, applyNetObjectProxy && this.dynamicType, ref requiresBclImport);
		}

		// Token: 0x0400018D RID: 397
		private readonly int fieldNumber;

		// Token: 0x0400018E RID: 398
		private readonly MemberInfo member;

		// Token: 0x0400018F RID: 399
		private readonly Type parentType;

		// Token: 0x04000190 RID: 400
		private readonly Type itemType;

		// Token: 0x04000191 RID: 401
		private readonly Type defaultType;

		// Token: 0x04000192 RID: 402
		private readonly Type memberType;

		// Token: 0x04000193 RID: 403
		private object defaultValue;

		// Token: 0x04000194 RID: 404
		private readonly RuntimeTypeModel model;

		// Token: 0x04000195 RID: 405
		private IProtoSerializer serializer;

		// Token: 0x04000196 RID: 406
		private DataFormat dataFormat;

		// Token: 0x04000197 RID: 407
		private bool asReference;

		// Token: 0x04000198 RID: 408
		private bool dynamicType;

		// Token: 0x04000199 RID: 409
		private MethodInfo getSpecified;

		// Token: 0x0400019A RID: 410
		private MethodInfo setSpecified;

		// Token: 0x0400019B RID: 411
		private string name;

		// Token: 0x0400019C RID: 412
		private const byte OPTIONS_IsStrict = 1;

		// Token: 0x0400019D RID: 413
		private const byte OPTIONS_IsPacked = 2;

		// Token: 0x0400019E RID: 414
		private const byte OPTIONS_IsRequired = 4;

		// Token: 0x0400019F RID: 415
		private const byte OPTIONS_OverwriteList = 8;

		// Token: 0x040001A0 RID: 416
		private const byte OPTIONS_SupportNull = 16;

		// Token: 0x040001A1 RID: 417
		private byte flags;

		// Token: 0x02000078 RID: 120
		internal sealed class Comparer : IComparer, IComparer<ValueMember>
		{
			// Token: 0x06000415 RID: 1045 RVA: 0x000048BC File Offset: 0x00002ABC
			public int Compare(object x, object y)
			{
				return this.Compare(x as ValueMember, y as ValueMember);
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x00015738 File Offset: 0x00013938
			public int Compare(ValueMember x, ValueMember y)
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
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x040001A2 RID: 418
			public static readonly ValueMember.Comparer Default = new ValueMember.Comparer();
		}
	}
}
