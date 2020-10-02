using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x0200006A RID: 106
	public sealed class RuntimeTypeModel : TypeModel
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000414F File Offset: 0x0000234F
		private bool GetOption(byte option)
		{
			return (this.options & option) == option;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000415C File Offset: 0x0000235C
		private void SetOption(byte option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00004182 File Offset: 0x00002382
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000418B File Offset: 0x0000238B
		public bool InferTagFromNameDefault
		{
			get
			{
				return this.GetOption(1);
			}
			set
			{
				this.SetOption(1, value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00004195 File Offset: 0x00002395
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000041A2 File Offset: 0x000023A2
		public bool AutoAddProtoContractTypesOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000041B0 File Offset: 0x000023B0
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000041BA File Offset: 0x000023BA
		public bool UseImplicitZeroDefaults
		{
			get
			{
				return this.GetOption(32);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
				}
				this.SetOption(32, value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000041DC File Offset: 0x000023DC
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000041E6 File Offset: 0x000023E6
		public bool AllowParseableTypes
		{
			get
			{
				return this.GetOption(64);
			}
			set
			{
				this.SetOption(64, value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000041F1 File Offset: 0x000023F1
		public static RuntimeTypeModel Default
		{
			get
			{
				return RuntimeTypeModel.Singleton.Value;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000041F8 File Offset: 0x000023F8
		public IEnumerable GetTypes()
		{
			return this.types;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012780 File Offset: 0x00010980
		public override string GetSchema(Type type)
		{
			BasicList basicList = new BasicList();
			MetaType metaType = null;
			bool flag = false;
			if (type == null)
			{
				foreach (object obj in this.types)
				{
					MetaType surrogateOrBaseOrSelf = ((MetaType)obj).GetSurrogateOrBaseOrSelf(false);
					if (!basicList.Contains(surrogateOrBaseOrSelf))
					{
						basicList.Add(surrogateOrBaseOrSelf);
						this.CascadeDependents(basicList, surrogateOrBaseOrSelf);
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
				WireType wireType;
				flag = (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) != null);
				if (!flag)
				{
					int num = this.FindOrAddAuto(type, false, false, false);
					if (num < 0)
					{
						throw new ArgumentException("The type specified is not a contract-type", "type");
					}
					metaType = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
					basicList.Add(metaType);
					this.CascadeDependents(basicList, metaType);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (!flag)
			{
				foreach (object obj2 in ((IEnumerable)((metaType == null) ? this.types : basicList)))
				{
					MetaType metaType2 = (MetaType)obj2;
					if (!metaType2.IsList)
					{
						string @namespace = metaType2.Type.Namespace;
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
			bool flag2 = false;
			StringBuilder stringBuilder2 = new StringBuilder();
			MetaType[] array = new MetaType[basicList.Count];
			basicList.CopyTo(array, 0);
			Array.Sort<MetaType>(array, MetaType.Comparer.Default);
			if (flag)
			{
				Helpers.AppendLine(stringBuilder2).Append("message ").Append(type.Name).Append(" {");
				MetaType.NewLine(stringBuilder2, 1).Append("optional ").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref flag2)).Append(" value = 1;");
				Helpers.AppendLine(stringBuilder2).Append('}');
			}
			else
			{
				foreach (MetaType metaType3 in array)
				{
					if (!metaType3.IsList || metaType3 == metaType)
					{
						metaType3.WriteSchema(stringBuilder2, 0, ref flag2);
					}
				}
			}
			if (flag2)
			{
				stringBuilder.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
				Helpers.AppendLine(stringBuilder);
			}
			return Helpers.AppendLine(stringBuilder.Append(stringBuilder2)).ToString();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012A1C File Offset: 0x00010C1C
		private void CascadeDependents(BasicList list, MetaType metaType)
		{
			if (metaType.IsList)
			{
				Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
				WireType wireType;
				if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out wireType, false, false, false, false) == null)
				{
					int num = this.FindOrAddAuto(listItemType, false, false, false);
					if (num >= 0)
					{
						MetaType metaType2 = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
							return;
						}
					}
				}
			}
			else
			{
				MetaType metaType2;
				if (metaType.IsAutoTuple)
				{
					MemberInfo[] array;
					if (MetaType.ResolveTupleConstructor(metaType.Type, out array) != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Type type = null;
							if (array[i] is PropertyInfo)
							{
								type = ((PropertyInfo)array[i]).PropertyType;
							}
							else if (array[i] is FieldInfo)
							{
								type = ((FieldInfo)array[i]).FieldType;
							}
							WireType wireType2;
							if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType2, false, false, false, false) == null)
							{
								int num2 = this.FindOrAddAuto(type, false, false, false);
								if (num2 >= 0)
								{
									metaType2 = ((MetaType)this.types[num2]).GetSurrogateOrBaseOrSelf(false);
									if (!list.Contains(metaType2))
									{
										list.Add(metaType2);
										this.CascadeDependents(list, metaType2);
									}
								}
							}
						}
					}
				}
				else
				{
					foreach (object obj in metaType.Fields)
					{
						ValueMember valueMember = (ValueMember)obj;
						Type type2 = valueMember.ItemType;
						if (type2 == null)
						{
							type2 = valueMember.MemberType;
						}
						WireType wireType3;
						if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type2, out wireType3, false, false, false, false) == null)
						{
							int num3 = this.FindOrAddAuto(type2, false, false, false);
							if (num3 >= 0)
							{
								metaType2 = ((MetaType)this.types[num3]).GetSurrogateOrBaseOrSelf(false);
								if (!list.Contains(metaType2))
								{
									list.Add(metaType2);
									this.CascadeDependents(list, metaType2);
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
						metaType2 = subtypes[j].DerivedType.GetSurrogateOrSelf();
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
						}
					}
				}
				metaType2 = metaType.BaseType;
				if (metaType2 != null)
				{
					metaType2 = metaType2.GetSurrogateOrSelf();
				}
				if (metaType2 != null && !list.Contains(metaType2))
				{
					list.Add(metaType2);
					this.CascadeDependents(list, metaType2);
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012C9C File Offset: 0x00010E9C
		internal RuntimeTypeModel(bool isDefault)
		{
			this.AutoAddMissingTypes = true;
			this.UseImplicitZeroDefaults = true;
			this.SetOption(2, isDefault);
		}

		// Token: 0x170000E4 RID: 228
		public MetaType this[Type type]
		{
			get
			{
				return (MetaType)this.types[this.FindOrAddAuto(type, true, false, false)];
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012CF0 File Offset: 0x00010EF0
		internal MetaType FindWithoutAdd(Type type)
		{
			foreach (object obj in this.types)
			{
				MetaType metaType = (MetaType)obj;
				if (metaType.Type == type)
				{
					if (metaType.Pending)
					{
						this.WaitOnLock(metaType);
					}
					return metaType;
				}
			}
			Type type2 = TypeModel.ResolveProxies(type);
			if (type2 != null)
			{
				return this.FindWithoutAdd(type2);
			}
			return null;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000421C File Offset: 0x0000241C
		private static bool MetaTypeFinderImpl(object value, object ctx)
		{
			return ((MetaType)value).Type == (Type)ctx;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00004231 File Offset: 0x00002431
		private static bool BasicTypeFinderImpl(object value, object ctx)
		{
			return ((RuntimeTypeModel.BasicType)value).Type == (Type)ctx;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012D50 File Offset: 0x00010F50
		private void WaitOnLock(MetaType type)
		{
			int opaqueToken = 0;
			try
			{
				this.TakeLock(ref opaqueToken);
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012D84 File Offset: 0x00010F84
		internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
		{
			int num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
			if (num >= 0)
			{
				return ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
			}
			BasicList obj = this.basicTypes;
			IProtoSerializer result;
			lock (obj)
			{
				num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
				if (num >= 0)
				{
					result = ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
				}
				else
				{
					WireType wireType;
					IProtoSerializer protoSerializer = (MetaType.GetContractFamily(this, type, null) == MetaType.AttributeFamily.None) ? ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) : null;
					if (protoSerializer != null)
					{
						this.basicTypes.Add(new RuntimeTypeModel.BasicType(type, protoSerializer));
					}
					result = protoSerializer;
				}
			}
			return result;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012E4C File Offset: 0x0001104C
		internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
		{
			int num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
			if (num >= 0)
			{
				MetaType metaType = (MetaType)this.types[num];
				if (metaType.Pending)
				{
					this.WaitOnLock(metaType);
				}
				return num;
			}
			bool flag = this.AutoAddMissingTypes || addEvenIfAutoDisabled;
			if (Helpers.IsEnum(type) || this.TryGetBasicTypeSerializer(type) == null)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type2);
					type = type2;
				}
				if (num < 0)
				{
					int opaqueToken = 0;
					try
					{
						this.TakeLock(ref opaqueToken);
						MetaType metaType;
						if ((metaType = this.RecogniseCommonTypes(type)) == null)
						{
							MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
							if (contractFamily == MetaType.AttributeFamily.AutoTuple)
							{
								addEvenIfAutoDisabled = (flag = true);
							}
							if (!flag || (!Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None))
							{
								if (demand)
								{
									TypeModel.ThrowUnexpectedType(type);
								}
								return num;
							}
							metaType = this.Create(type);
						}
						metaType.Pending = true;
						bool flag2 = false;
						int num2 = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
						if (num2 < 0)
						{
							this.ThrowIfFrozen();
							num = this.types.Add(metaType);
							flag2 = true;
						}
						else
						{
							num = num2;
						}
						if (flag2)
						{
							metaType.ApplyDefaultBehaviour();
							metaType.Pending = false;
						}
					}
					finally
					{
						this.ReleaseLock(opaqueToken);
					}
					return num;
				}
				return num;
			}
			if (flag && !addWithContractOnly)
			{
				throw MetaType.InbuiltType(type);
			}
			return -1;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00004246 File Offset: 0x00002446
		private MetaType RecogniseCommonTypes(Type type)
		{
			return null;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00004249 File Offset: 0x00002449
		private MetaType Create(Type type)
		{
			this.ThrowIfFrozen();
			return new MetaType(this, type, this.defaultFactory);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00012FA4 File Offset: 0x000111A4
		public MetaType Add(Type type, bool applyDefaultBehaviour)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MetaType metaType = this.FindWithoutAdd(type);
			if (metaType != null)
			{
				return metaType;
			}
			int opaqueToken = 0;
			if (type.IsInterface && base.MapType(MetaType.ienumerable).IsAssignableFrom(type) && TypeModel.GetListItemType(this, type) == null)
			{
				throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
			}
			try
			{
				metaType = this.RecogniseCommonTypes(type);
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
					metaType = this.Create(type);
				}
				metaType.Pending = true;
				this.TakeLock(ref opaqueToken);
				if (this.FindWithoutAdd(type) != null)
				{
					throw new ArgumentException("Duplicate type", "type");
				}
				this.ThrowIfFrozen();
				this.types.Add(metaType);
				if (applyDefaultBehaviour)
				{
					metaType.ApplyDefaultBehaviour();
				}
				metaType.Pending = false;
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
			return metaType;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000425E File Offset: 0x0000245E
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00004267 File Offset: 0x00002467
		public bool AutoAddMissingTypes
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("The default model must allow missing types");
				}
				this.ThrowIfFrozen();
				this.SetOption(8, value);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000428E File Offset: 0x0000248E
		private void ThrowIfFrozen()
		{
			if (this.GetOption(4))
			{
				throw new InvalidOperationException("The model cannot be changed once frozen");
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000042A4 File Offset: 0x000024A4
		public void Freeze()
		{
			if (this.GetOption(2))
			{
				throw new InvalidOperationException("The default model cannot be frozen");
			}
			this.SetOption(4, true);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000042C2 File Offset: 0x000024C2
		protected override int GetKeyImpl(Type type)
		{
			return this.GetKey(type, false, true);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001309C File Offset: 0x0001129C
		internal int GetKey(Type type, bool demand, bool getBaseKey)
		{
			int result;
			try
			{
				int num = this.FindOrAddAuto(type, demand, true, false);
				if (num >= 0)
				{
					MetaType metaType = (MetaType)this.types[num];
					if (getBaseKey)
					{
						metaType = MetaType.GetRootType(metaType);
						num = this.FindOrAddAuto(metaType.Type, true, true, false);
					}
				}
				result = num;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.Message.IndexOf(type.FullName) >= 0)
				{
					throw;
				}
				throw new ProtoException(ex.Message + " (" + type.FullName + ")", ex);
			}
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000042CD File Offset: 0x000024CD
		protected internal override void Serialize(int key, object value, ProtoWriter dest)
		{
			((MetaType)this.types[key]).Serializer.Write(value, dest);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013144 File Offset: 0x00011344
		protected internal override object Deserialize(int key, object value, ProtoReader source)
		{
			IProtoSerializer serializer = ((MetaType)this.types[key]).Serializer;
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

		// Token: 0x0600037E RID: 894 RVA: 0x000131A0 File Offset: 0x000113A0
		internal bool IsPrepared(Type type)
		{
			MetaType metaType = this.FindWithoutAdd(type);
			return metaType != null && metaType.IsPrepared();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000131C0 File Offset: 0x000113C0
		internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
		{
			int num = this.FindOrAddAuto(type, false, false, false);
			if (num >= 0)
			{
				return ((MetaType)this.types[num]).GetEnumMap();
			}
			return null;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000042EC File Offset: 0x000024EC
		// (set) Token: 0x06000381 RID: 897 RVA: 0x000042F4 File Offset: 0x000024F4
		public int MetadataTimeoutMilliseconds
		{
			get
			{
				return this.metadataTimeoutMilliseconds;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
				}
				this.metadataTimeoutMilliseconds = value;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000430C File Offset: 0x0000250C
		internal void TakeLock(ref int opaqueToken)
		{
			opaqueToken = 0;
			if (Monitor.TryEnter(this.types, this.metadataTimeoutMilliseconds))
			{
				opaqueToken = this.GetContention();
				return;
			}
			this.AddContention();
			throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000433D File Offset: 0x0000253D
		private int GetContention()
		{
			return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000434C File Offset: 0x0000254C
		private void AddContention()
		{
			Interlocked.Increment(ref this.contentionCounter);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000131F4 File Offset: 0x000113F4
		internal void ReleaseLock(int opaqueToken)
		{
			if (opaqueToken != 0)
			{
				Monitor.Exit(this.types);
				if (opaqueToken != this.GetContention())
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
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000386 RID: 902 RVA: 0x00013250 File Offset: 0x00011450
		// (remove) Token: 0x06000387 RID: 903 RVA: 0x00013288 File Offset: 0x00011488
		public event LockContentedEventHandler LockContended;

		// Token: 0x06000388 RID: 904 RVA: 0x000132C0 File Offset: 0x000114C0
		internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown)
			{
				return;
			}
			if (this[type].IgnoreListHandling)
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
				if (itemType == base.MapType(typeof(byte)))
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
				itemType = TypeModel.GetListItemType(this, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				this.ResolveListTypes(itemType, ref type3, ref type4);
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
					if (type.IsGenericType && type.GetGenericTypeDefinition() == base.MapType(typeof(IDictionary<, >)) && itemType == base.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = base.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = base.MapType(typeof(List<>)).MakeGenericType(new Type[]
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

		// Token: 0x06000389 RID: 905 RVA: 0x00013428 File Offset: 0x00011628
		internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
		{
			Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
			if (underlyingType != null)
			{
				effectiveType = underlyingType;
			}
			if (effectiveType == base.MapType(typeof(byte[])))
			{
				return "bytes";
			}
			WireType wireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out wireType, false, false, false, false);
			if (protoSerializer == null)
			{
				if (asReference || dynamicType)
				{
					requiresBclImport = true;
					return "bcl.NetObjectProxy";
				}
				return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
			}
			else
			{
				if (!(protoSerializer is ParseableSerializer))
				{
					ProtoTypeCode typeCode = Helpers.GetTypeCode(effectiveType);
					switch (typeCode)
					{
					case ProtoTypeCode.Boolean:
						return "bool";
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
						if (dataFormat == DataFormat.ZigZag)
						{
							return "sint32";
						}
						if (dataFormat != DataFormat.FixedSize)
						{
							return "int32";
						}
						return "sfixed32";
					case ProtoTypeCode.Int64:
						if (dataFormat == DataFormat.ZigZag)
						{
							return "sint64";
						}
						if (dataFormat != DataFormat.FixedSize)
						{
							return "int64";
						}
						return "sfixed64";
					case ProtoTypeCode.UInt64:
						if (dataFormat == DataFormat.FixedSize)
						{
							return "fixed64";
						}
						return "uint64";
					case ProtoTypeCode.Single:
						return "float";
					case ProtoTypeCode.Double:
						return "double";
					case ProtoTypeCode.Decimal:
						requiresBclImport = true;
						return "bcl.Decimal";
					case ProtoTypeCode.DateTime:
						requiresBclImport = true;
						return "bcl.DateTime";
					case (ProtoTypeCode)17:
						break;
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
					default:
						if (typeCode == ProtoTypeCode.TimeSpan)
						{
							requiresBclImport = true;
							return "bcl.TimeSpan";
						}
						if (typeCode == ProtoTypeCode.Guid)
						{
							requiresBclImport = true;
							return "bcl.Guid";
						}
						break;
					}
					throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
				}
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
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000435A File Offset: 0x0000255A
		public void SetDefaultFactory(MethodInfo methodInfo)
		{
			this.VerifyFactory(methodInfo, null);
			this.defaultFactory = methodInfo;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000135D0 File Offset: 0x000117D0
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
				if (type != null && factory.ReturnType != type && factory.ReturnType != base.MapType(typeof(object)))
				{
					throw new ArgumentException("The factory-method must return object" + ((type == null) ? "" : (" or " + type.FullName)), "factory");
				}
				if (!CallbackSet.CheckCallbackParameters(this, factory))
				{
					throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
				}
			}
		}

		// Token: 0x04000161 RID: 353
		private byte options;

		// Token: 0x04000162 RID: 354
		private const byte OPTIONS_InferTagFromNameDefault = 1;

		// Token: 0x04000163 RID: 355
		private const byte OPTIONS_IsDefaultModel = 2;

		// Token: 0x04000164 RID: 356
		private const byte OPTIONS_Frozen = 4;

		// Token: 0x04000165 RID: 357
		private const byte OPTIONS_AutoAddMissingTypes = 8;

		// Token: 0x04000166 RID: 358
		private const byte OPTIONS_UseImplicitZeroDefaults = 32;

		// Token: 0x04000167 RID: 359
		private const byte OPTIONS_AllowParseableTypes = 64;

		// Token: 0x04000168 RID: 360
		private const byte OPTIONS_AutoAddProtoContractTypesOnly = 128;

		// Token: 0x04000169 RID: 361
		private static readonly BasicList.MatchPredicate MetaTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.MetaTypeFinderImpl);

		// Token: 0x0400016A RID: 362
		private static readonly BasicList.MatchPredicate BasicTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.BasicTypeFinderImpl);

		// Token: 0x0400016B RID: 363
		private BasicList basicTypes = new BasicList();

		// Token: 0x0400016C RID: 364
		private readonly BasicList types = new BasicList();

		// Token: 0x0400016D RID: 365
		private int metadataTimeoutMilliseconds = 5000;

		// Token: 0x0400016E RID: 366
		private int contentionCounter = 1;

		// Token: 0x04000170 RID: 368
		private MethodInfo defaultFactory;

		// Token: 0x0200006B RID: 107
		private sealed class Singleton
		{
			// Token: 0x0600038D RID: 909 RVA: 0x000022E5 File Offset: 0x000004E5
			private Singleton()
			{
			}

			// Token: 0x04000171 RID: 369
			internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);
		}

		// Token: 0x0200006C RID: 108
		private sealed class BasicType
		{
			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x0600038F RID: 911 RVA: 0x0000439C File Offset: 0x0000259C
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000390 RID: 912 RVA: 0x000043A4 File Offset: 0x000025A4
			public IProtoSerializer Serializer
			{
				get
				{
					return this.serializer;
				}
			}

			// Token: 0x06000391 RID: 913 RVA: 0x000043AC File Offset: 0x000025AC
			public BasicType(Type type, IProtoSerializer serializer)
			{
				this.type = type;
				this.serializer = serializer;
			}

			// Token: 0x04000172 RID: 370
			private readonly Type type;

			// Token: 0x04000173 RID: 371
			private readonly IProtoSerializer serializer;
		}
	}
}
