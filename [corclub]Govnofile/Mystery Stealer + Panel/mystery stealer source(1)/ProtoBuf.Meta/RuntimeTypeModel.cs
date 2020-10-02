using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ProtoBuf.Meta
{
	public sealed class RuntimeTypeModel : TypeModel
	{
		private sealed class Singleton
		{
			internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(isDefault: true);

			private Singleton()
			{
			}
		}

		private sealed class BasicType
		{
			private readonly Type type;

			private readonly IProtoSerializer serializer;

			public Type Type => type;

			public IProtoSerializer Serializer => serializer;

			public BasicType(Type type, IProtoSerializer serializer)
			{
				this.type = type;
				this.serializer = serializer;
			}
		}

		private byte options;

		private const byte OPTIONS_InferTagFromNameDefault = 1;

		private const byte OPTIONS_IsDefaultModel = 2;

		private const byte OPTIONS_Frozen = 4;

		private const byte OPTIONS_AutoAddMissingTypes = 8;

		private const byte OPTIONS_UseImplicitZeroDefaults = 32;

		private const byte OPTIONS_AllowParseableTypes = 64;

		private const byte OPTIONS_AutoAddProtoContractTypesOnly = 128;

		private static readonly BasicList.MatchPredicate MetaTypeFinder = MetaTypeFinderImpl;

		private static readonly BasicList.MatchPredicate BasicTypeFinder = BasicTypeFinderImpl;

		private BasicList basicTypes = new BasicList();

		private readonly BasicList types = new BasicList();

		private int metadataTimeoutMilliseconds = 5000;

		private int contentionCounter = 1;

		private MethodInfo defaultFactory;

		public bool InferTagFromNameDefault
		{
			get
			{
				return GetOption(1);
			}
			set
			{
				SetOption(1, value);
			}
		}

		public bool AutoAddProtoContractTypesOnly
		{
			get
			{
				return GetOption(128);
			}
			set
			{
				SetOption(128, value);
			}
		}

		public bool UseImplicitZeroDefaults
		{
			get
			{
				return GetOption(32);
			}
			set
			{
				if (!value && GetOption(2))
				{
					throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
				}
				SetOption(32, value);
			}
		}

		public bool AllowParseableTypes
		{
			get
			{
				return GetOption(64);
			}
			set
			{
				SetOption(64, value);
			}
		}

		public static RuntimeTypeModel Default => Singleton.Value;

		public MetaType this[Type type] => (MetaType)types[FindOrAddAuto(type, demand: true, addWithContractOnly: false, addEvenIfAutoDisabled: false)];

		public bool AutoAddMissingTypes
		{
			get
			{
				return GetOption(8);
			}
			set
			{
				if (!value && GetOption(2))
				{
					throw new InvalidOperationException("The default model must allow missing types");
				}
				ThrowIfFrozen();
				SetOption(8, value);
			}
		}

		public int MetadataTimeoutMilliseconds
		{
			get
			{
				return metadataTimeoutMilliseconds;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
				}
				metadataTimeoutMilliseconds = value;
			}
		}

		public event LockContentedEventHandler LockContended;

		private bool GetOption(byte option)
		{
			return (options & option) == option;
		}

		private void SetOption(byte option, bool value)
		{
			if (value)
			{
				options |= option;
			}
			else
			{
				options &= (byte)(~option);
			}
		}

		public IEnumerable GetTypes()
		{
			return types;
		}

		public override string GetSchema(Type type)
		{
			BasicList basicList = new BasicList();
			MetaType metaType = null;
			bool flag = false;
			if (type == null)
			{
				BasicList.NodeEnumerator enumerator = types.GetEnumerator();
				while (enumerator.MoveNext())
				{
					MetaType surrogateOrBaseOrSelf = ((MetaType)enumerator.Current).GetSurrogateOrBaseOrSelf(deep: false);
					if (!basicList.Contains(surrogateOrBaseOrSelf))
					{
						basicList.Add(surrogateOrBaseOrSelf);
						CascadeDependents(basicList, surrogateOrBaseOrSelf);
					}
				}
			}
			else
			{
				Type underlyingType = Helpers.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				flag = (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out WireType _, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false) != null);
				if (!flag)
				{
					int num = FindOrAddAuto(type, demand: false, addWithContractOnly: false, addEvenIfAutoDisabled: false);
					if (num < 0)
					{
						throw new ArgumentException("The type specified is not a contract-type", "type");
					}
					metaType = ((MetaType)types[num]).GetSurrogateOrBaseOrSelf(deep: false);
					basicList.Add(metaType);
					CascadeDependents(basicList, metaType);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (!flag)
			{
				foreach (MetaType item in (IEnumerable)((metaType == null) ? types : basicList))
				{
					if (!item.IsList)
					{
						string @namespace = item.Type.Namespace;
						if (!Helpers.IsNullOrEmpty(@namespace) && !@namespace.StartsWith("System."))
						{
							if (text == null)
							{
								text = @namespace;
							}
							else if (!(text == @namespace))
							{
								text = null;
								break;
							}
						}
					}
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				stringBuilder.Append("package ").Append(text).Append(';');
				Helpers.AppendLine(stringBuilder);
			}
			bool requiresBclImport = false;
			StringBuilder stringBuilder2 = new StringBuilder();
			MetaType[] array = new MetaType[basicList.Count];
			basicList.CopyTo(array, 0);
			Array.Sort(array, MetaType.Comparer.Default);
			if (flag)
			{
				Helpers.AppendLine(stringBuilder2).Append("message ").Append(type.Name)
					.Append(" {");
				MetaType.NewLine(stringBuilder2, 1).Append("optional ").Append(GetSchemaTypeName(type, DataFormat.Default, asReference: false, dynamicType: false, ref requiresBclImport))
					.Append(" value = 1;");
				Helpers.AppendLine(stringBuilder2).Append('}');
			}
			else
			{
				foreach (MetaType metaType3 in array)
				{
					if (!metaType3.IsList || metaType3 == metaType)
					{
						metaType3.WriteSchema(stringBuilder2, 0, ref requiresBclImport);
					}
				}
			}
			if (requiresBclImport)
			{
				stringBuilder.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
				Helpers.AppendLine(stringBuilder);
			}
			return Helpers.AppendLine(stringBuilder.Append(stringBuilder2)).ToString();
		}

		private void CascadeDependents(BasicList list, MetaType metaType)
		{
			MetaType surrogateOrBaseOrSelf;
			if (metaType.IsList)
			{
				Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
				if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out WireType _, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false) != null)
				{
					return;
				}
				int num = FindOrAddAuto(listItemType, demand: false, addWithContractOnly: false, addEvenIfAutoDisabled: false);
				if (num >= 0)
				{
					surrogateOrBaseOrSelf = ((MetaType)types[num]).GetSurrogateOrBaseOrSelf(deep: false);
					if (!list.Contains(surrogateOrBaseOrSelf))
					{
						list.Add(surrogateOrBaseOrSelf);
						CascadeDependents(list, surrogateOrBaseOrSelf);
					}
				}
				return;
			}
			if (metaType.IsAutoTuple)
			{
				if (MetaType.ResolveTupleConstructor(metaType.Type, out MemberInfo[] mappedMembers) != null)
				{
					for (int i = 0; i < mappedMembers.Length; i++)
					{
						Type type = null;
						if (mappedMembers[i] is PropertyInfo)
						{
							type = ((PropertyInfo)mappedMembers[i]).PropertyType;
						}
						else if (mappedMembers[i] is FieldInfo)
						{
							type = ((FieldInfo)mappedMembers[i]).FieldType;
						}
						if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out WireType _, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false) != null)
						{
							continue;
						}
						int num2 = FindOrAddAuto(type, demand: false, addWithContractOnly: false, addEvenIfAutoDisabled: false);
						if (num2 >= 0)
						{
							surrogateOrBaseOrSelf = ((MetaType)types[num2]).GetSurrogateOrBaseOrSelf(deep: false);
							if (!list.Contains(surrogateOrBaseOrSelf))
							{
								list.Add(surrogateOrBaseOrSelf);
								CascadeDependents(list, surrogateOrBaseOrSelf);
							}
						}
					}
				}
			}
			else
			{
				foreach (ValueMember field in metaType.Fields)
				{
					Type type2 = field.ItemType;
					if (type2 == null)
					{
						type2 = field.MemberType;
					}
					if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type2, out WireType _, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false) == null)
					{
						int num3 = FindOrAddAuto(type2, demand: false, addWithContractOnly: false, addEvenIfAutoDisabled: false);
						if (num3 >= 0)
						{
							surrogateOrBaseOrSelf = ((MetaType)types[num3]).GetSurrogateOrBaseOrSelf(deep: false);
							if (!list.Contains(surrogateOrBaseOrSelf))
							{
								list.Add(surrogateOrBaseOrSelf);
								CascadeDependents(list, surrogateOrBaseOrSelf);
							}
						}
					}
				}
			}
			if (metaType.HasSubtypes)
			{
				SubType[] subtypes = metaType.GetSubtypes();
				for (int j = 0; j < subtypes.Length; j++)
				{
					surrogateOrBaseOrSelf = subtypes[j].DerivedType.GetSurrogateOrSelf();
					if (!list.Contains(surrogateOrBaseOrSelf))
					{
						list.Add(surrogateOrBaseOrSelf);
						CascadeDependents(list, surrogateOrBaseOrSelf);
					}
				}
			}
			surrogateOrBaseOrSelf = metaType.BaseType;
			if (surrogateOrBaseOrSelf != null)
			{
				surrogateOrBaseOrSelf = surrogateOrBaseOrSelf.GetSurrogateOrSelf();
			}
			if (surrogateOrBaseOrSelf != null && !list.Contains(surrogateOrBaseOrSelf))
			{
				list.Add(surrogateOrBaseOrSelf);
				CascadeDependents(list, surrogateOrBaseOrSelf);
			}
		}

		internal RuntimeTypeModel(bool isDefault)
		{
			AutoAddMissingTypes = true;
			UseImplicitZeroDefaults = true;
			SetOption(2, isDefault);
		}

		internal MetaType FindWithoutAdd(Type type)
		{
			BasicList.NodeEnumerator enumerator = types.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MetaType metaType = (MetaType)enumerator.Current;
				if (metaType.Type == type)
				{
					if (metaType.Pending)
					{
						WaitOnLock(metaType);
					}
					return metaType;
				}
			}
			Type type2 = TypeModel.ResolveProxies(type);
			if (type2 != null)
			{
				return FindWithoutAdd(type2);
			}
			return null;
		}

		private static bool MetaTypeFinderImpl(object value, object ctx)
		{
			return ((MetaType)value).Type == (Type)ctx;
		}

		private static bool BasicTypeFinderImpl(object value, object ctx)
		{
			return ((BasicType)value).Type == (Type)ctx;
		}

		private void WaitOnLock(MetaType type)
		{
			int opaqueToken = 0;
			try
			{
				TakeLock(ref opaqueToken);
			}
			finally
			{
				ReleaseLock(opaqueToken);
			}
		}

		internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
		{
			int num = basicTypes.IndexOf(BasicTypeFinder, type);
			if (num >= 0)
			{
				return ((BasicType)basicTypes[num]).Serializer;
			}
			lock (basicTypes)
			{
				num = basicTypes.IndexOf(BasicTypeFinder, type);
				if (num >= 0)
				{
					return ((BasicType)basicTypes[num]).Serializer;
				}
				WireType defaultWireType;
				IProtoSerializer protoSerializer = (MetaType.GetContractFamily(this, type, null) == MetaType.AttributeFamily.None) ? ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out defaultWireType, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false) : null;
				if (protoSerializer != null)
				{
					basicTypes.Add(new BasicType(type, protoSerializer));
				}
				return protoSerializer;
			}
		}

		internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
		{
			int num = types.IndexOf(MetaTypeFinder, type);
			if (num >= 0)
			{
				MetaType metaType = (MetaType)types[num];
				if (metaType.Pending)
				{
					WaitOnLock(metaType);
				}
				return num;
			}
			bool flag = AutoAddMissingTypes | addEvenIfAutoDisabled;
			if (!Helpers.IsEnum(type) && TryGetBasicTypeSerializer(type) != null)
			{
				if (flag && !addWithContractOnly)
				{
					throw MetaType.InbuiltType(type);
				}
				return -1;
			}
			Type type2 = TypeModel.ResolveProxies(type);
			if (type2 != null)
			{
				num = types.IndexOf(MetaTypeFinder, type2);
				type = type2;
			}
			if (num < 0)
			{
				int opaqueToken = 0;
				try
				{
					TakeLock(ref opaqueToken);
					MetaType metaType;
					if ((metaType = RecogniseCommonTypes(type)) == null)
					{
						MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
						if (contractFamily == MetaType.AttributeFamily.AutoTuple)
						{
							flag = (addEvenIfAutoDisabled = true);
						}
						if (!flag || (!Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None))
						{
							if (demand)
							{
								TypeModel.ThrowUnexpectedType(type);
							}
							return num;
						}
						metaType = Create(type);
					}
					metaType.Pending = true;
					bool flag2 = false;
					int num2 = types.IndexOf(MetaTypeFinder, type);
					if (num2 < 0)
					{
						ThrowIfFrozen();
						num = types.Add(metaType);
						flag2 = true;
					}
					else
					{
						num = num2;
					}
					if (!flag2)
					{
						return num;
					}
					metaType.ApplyDefaultBehaviour();
					metaType.Pending = false;
					return num;
				}
				finally
				{
					ReleaseLock(opaqueToken);
				}
			}
			return num;
		}

		private MetaType RecogniseCommonTypes(Type type)
		{
			return null;
		}

		private MetaType Create(Type type)
		{
			ThrowIfFrozen();
			return new MetaType(this, type, defaultFactory);
		}

		public MetaType Add(Type type, bool applyDefaultBehaviour)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MetaType metaType = FindWithoutAdd(type);
			if (metaType != null)
			{
				return metaType;
			}
			int opaqueToken = 0;
			if (type.IsInterface && MapType(MetaType.ienumerable).IsAssignableFrom(type) && TypeModel.GetListItemType(this, type) == null)
			{
				throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
			}
			try
			{
				metaType = RecogniseCommonTypes(type);
				if (metaType != null)
				{
					if (!applyDefaultBehaviour)
					{
						throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.FullName, "applyDefaultBehaviour");
					}
					applyDefaultBehaviour = false;
				}
				if (metaType == null)
				{
					metaType = Create(type);
				}
				metaType.Pending = true;
				TakeLock(ref opaqueToken);
				if (FindWithoutAdd(type) != null)
				{
					throw new ArgumentException("Duplicate type", "type");
				}
				ThrowIfFrozen();
				types.Add(metaType);
				if (applyDefaultBehaviour)
				{
					metaType.ApplyDefaultBehaviour();
				}
				metaType.Pending = false;
				return metaType;
			}
			finally
			{
				ReleaseLock(opaqueToken);
			}
		}

		private void ThrowIfFrozen()
		{
			if (GetOption(4))
			{
				throw new InvalidOperationException("The model cannot be changed once frozen");
			}
		}

		public void Freeze()
		{
			if (GetOption(2))
			{
				throw new InvalidOperationException("The default model cannot be frozen");
			}
			SetOption(4, value: true);
		}

		protected override int GetKeyImpl(Type type)
		{
			return GetKey(type, demand: false, getBaseKey: true);
		}

		internal int GetKey(Type type, bool demand, bool getBaseKey)
		{
			try
			{
				int num = FindOrAddAuto(type, demand, addWithContractOnly: true, addEvenIfAutoDisabled: false);
				if (num >= 0)
				{
					MetaType source = (MetaType)types[num];
					if (getBaseKey)
					{
						source = MetaType.GetRootType(source);
						num = FindOrAddAuto(source.Type, demand: true, addWithContractOnly: true, addEvenIfAutoDisabled: false);
					}
				}
				return num;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (ex2.Message.IndexOf(type.FullName) >= 0)
				{
					throw;
				}
				throw new ProtoException(ex2.Message + " (" + type.FullName + ")", ex2);
			}
		}

		protected internal override void Serialize(int key, object value, ProtoWriter dest)
		{
			((MetaType)types[key]).Serializer.Write(value, dest);
		}

		protected internal override object Deserialize(int key, object value, ProtoReader source)
		{
			IProtoSerializer serializer = ((MetaType)types[key]).Serializer;
			if (value == null && Helpers.IsValueType(serializer.ExpectedType))
			{
				if (serializer.RequiresOldValue)
				{
					value = Activator.CreateInstance(serializer.ExpectedType);
				}
				return serializer.Read(value, source);
			}
			return serializer.Read(value, source);
		}

		internal bool IsPrepared(Type type)
		{
			return FindWithoutAdd(type)?.IsPrepared() ?? false;
		}

		internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
		{
			int num = FindOrAddAuto(type, demand: false, addWithContractOnly: false, addEvenIfAutoDisabled: false);
			if (num >= 0)
			{
				return ((MetaType)types[num]).GetEnumMap();
			}
			return null;
		}

		internal void TakeLock(ref int opaqueToken)
		{
			opaqueToken = 0;
			if (Monitor.TryEnter(types, metadataTimeoutMilliseconds))
			{
				opaqueToken = GetContention();
				return;
			}
			AddContention();
			throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
		}

		private int GetContention()
		{
			return Interlocked.CompareExchange(ref contentionCounter, 0, 0);
		}

		private void AddContention()
		{
			Interlocked.Increment(ref contentionCounter);
		}

		internal void ReleaseLock(int opaqueToken)
		{
			if (opaqueToken == 0)
			{
				return;
			}
			Monitor.Exit(types);
			if (opaqueToken != GetContention())
			{
				LockContentedEventHandler lockContended = this.LockContended;
				if (lockContended != null)
				{
					string stackTrace;
					try
					{
						throw new ProtoException();
					}
					catch (Exception ex)
					{
						stackTrace = ex.StackTrace;
					}
					lockContended(this, new LockContentedEventArgs(stackTrace));
				}
			}
		}

		internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null || Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown || this[type].IgnoreListHandling)
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
				if (itemType == MapType(typeof(byte)))
				{
					defaultType = (itemType = null);
				}
				else
				{
					defaultType = type;
				}
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(this, type);
			}
			if (itemType != null)
			{
				Type itemType2 = null;
				Type defaultType2 = null;
				ResolveListTypes(itemType, ref itemType2, ref defaultType2);
				if (itemType2 != null)
				{
					throw TypeModel.CreateNestedListsNotSupported();
				}
			}
			if (itemType == null || defaultType != null)
			{
				return;
			}
			if (type.IsClass && !type.IsAbstract && Helpers.GetConstructor(type, Helpers.EmptyTypes, nonPublic: true) != null)
			{
				defaultType = type;
			}
			if (defaultType == null && type.IsInterface)
			{
				Type[] genericArguments;
				if (type.IsGenericType && type.GetGenericTypeDefinition() == MapType(typeof(IDictionary<, >)) && itemType == MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
				{
					defaultType = MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
				}
				else
				{
					defaultType = MapType(typeof(List<>)).MakeGenericType(itemType);
				}
			}
			if (defaultType != null && !Helpers.IsAssignableFrom(type, defaultType))
			{
				defaultType = null;
			}
		}

		internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
		{
			Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
			if (underlyingType != null)
			{
				effectiveType = underlyingType;
			}
			if (effectiveType == MapType(typeof(byte[])))
			{
				return "bytes";
			}
			WireType defaultWireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out defaultWireType, asReference: false, dynamicType: false, overwriteList: false, allowComplexTypes: false);
			if (protoSerializer == null)
			{
				if (asReference | dynamicType)
				{
					requiresBclImport = true;
					return "bcl.NetObjectProxy";
				}
				return this[effectiveType].GetSurrogateOrBaseOrSelf(deep: true).GetSchemaTypeName();
			}
			if (protoSerializer is ParseableSerializer)
			{
				if (asReference)
				{
					requiresBclImport = true;
				}
				if (!asReference)
				{
					return "string";
				}
				return "bcl.NetObjectProxy";
			}
			switch (Helpers.GetTypeCode(effectiveType))
			{
			case ProtoTypeCode.Boolean:
				return "bool";
			case ProtoTypeCode.Single:
				return "float";
			case ProtoTypeCode.Double:
				return "double";
			case ProtoTypeCode.String:
				if (asReference)
				{
					requiresBclImport = true;
				}
				if (!asReference)
				{
					return "string";
				}
				return "bcl.NetObjectProxy";
			case ProtoTypeCode.Char:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.UInt16:
			case ProtoTypeCode.UInt32:
				if (dataFormat == DataFormat.FixedSize)
				{
					return "fixed32";
				}
				return "uint32";
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.Int32:
				switch (dataFormat)
				{
				case DataFormat.ZigZag:
					return "sint32";
				case DataFormat.FixedSize:
					return "sfixed32";
				default:
					return "int32";
				}
			case ProtoTypeCode.UInt64:
				if (dataFormat == DataFormat.FixedSize)
				{
					return "fixed64";
				}
				return "uint64";
			case ProtoTypeCode.Int64:
				switch (dataFormat)
				{
				case DataFormat.ZigZag:
					return "sint64";
				case DataFormat.FixedSize:
					return "sfixed64";
				default:
					return "int64";
				}
			case ProtoTypeCode.DateTime:
				requiresBclImport = true;
				return "bcl.DateTime";
			case ProtoTypeCode.TimeSpan:
				requiresBclImport = true;
				return "bcl.TimeSpan";
			case ProtoTypeCode.Decimal:
				requiresBclImport = true;
				return "bcl.Decimal";
			case ProtoTypeCode.Guid:
				requiresBclImport = true;
				return "bcl.Guid";
			default:
				throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
			}
		}

		public void SetDefaultFactory(MethodInfo methodInfo)
		{
			VerifyFactory(methodInfo, null);
			defaultFactory = methodInfo;
		}

		internal void VerifyFactory(MethodInfo factory, Type type)
		{
			if (factory != null)
			{
				if (type != null && Helpers.IsValueType(type))
				{
					throw new InvalidOperationException();
				}
				if (!factory.IsStatic)
				{
					throw new ArgumentException("A factory-method must be static", "factory");
				}
				if (type != null && factory.ReturnType != type && factory.ReturnType != MapType(typeof(object)))
				{
					throw new ArgumentException("The factory-method must return object" + ((type == null) ? "" : (" or " + type.FullName)), "factory");
				}
				if (!CallbackSet.CheckCallbackParameters(this, factory))
				{
					throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
				}
			}
		}
	}
}
