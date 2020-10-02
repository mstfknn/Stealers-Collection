using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProtoBuf.Meta
{
	// Token: 0x02000073 RID: 115
	public abstract class TypeModel
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00004504 File Offset: 0x00002704
		protected internal Type MapType(Type type)
		{
			return this.MapType(type, true);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000450E File Offset: 0x0000270E
		protected internal virtual Type MapType(Type type, bool demand)
		{
			return type;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00013718 File Offset: 0x00011918
		private WireType GetWireType(ProtoTypeCode code, DataFormat format, ref Type type, out int modelKey)
		{
			modelKey = -1;
			if (Helpers.IsEnum(type))
			{
				modelKey = this.GetKey(ref type);
				return WireType.Variant;
			}
			switch (code)
			{
			case ProtoTypeCode.Boolean:
			case ProtoTypeCode.Char:
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.UInt16:
			case ProtoTypeCode.Int32:
			case ProtoTypeCode.UInt32:
				if (format != DataFormat.FixedSize)
				{
					return WireType.Variant;
				}
				return WireType.Fixed32;
			case ProtoTypeCode.Int64:
			case ProtoTypeCode.UInt64:
				if (format != DataFormat.FixedSize)
				{
					return WireType.Variant;
				}
				return WireType.Fixed64;
			case ProtoTypeCode.Single:
				return WireType.Fixed32;
			case ProtoTypeCode.Double:
				return WireType.Fixed64;
			case ProtoTypeCode.Decimal:
			case ProtoTypeCode.DateTime:
			case ProtoTypeCode.String:
				break;
			case (ProtoTypeCode)17:
				goto IL_80;
			default:
				if (code - ProtoTypeCode.TimeSpan > 3)
				{
					goto IL_80;
				}
				break;
			}
			return WireType.String;
			IL_80:
			if ((modelKey = this.GetKey(ref type)) >= 0)
			{
				return WireType.String;
			}
			return WireType.None;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000137B8 File Offset: 0x000119B8
		internal bool TrySerializeAuxiliaryType(ProtoWriter writer, Type type, DataFormat format, int tag, object value, bool isInsideList)
		{
			if (type == null)
			{
				type = value.GetType();
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			if (num >= 0)
			{
				if (Helpers.IsEnum(type))
				{
					this.Serialize(num, value, writer);
					return true;
				}
				ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				if (wireType == WireType.None)
				{
					throw ProtoWriter.CreateException(writer);
				}
				if (wireType - WireType.String > 1)
				{
					this.Serialize(num, value, writer);
					return true;
				}
				SubItemToken token = ProtoWriter.StartSubItem(value, writer);
				this.Serialize(num, value, writer);
				ProtoWriter.EndSubItem(token, writer);
				return true;
			}
			else
			{
				if (wireType != WireType.None)
				{
					ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				}
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					ProtoWriter.WriteBoolean((bool)value, writer);
					return true;
				case ProtoTypeCode.Char:
					ProtoWriter.WriteUInt16((ushort)((char)value), writer);
					return true;
				case ProtoTypeCode.SByte:
					ProtoWriter.WriteSByte((sbyte)value, writer);
					return true;
				case ProtoTypeCode.Byte:
					ProtoWriter.WriteByte((byte)value, writer);
					return true;
				case ProtoTypeCode.Int16:
					ProtoWriter.WriteInt16((short)value, writer);
					return true;
				case ProtoTypeCode.UInt16:
					ProtoWriter.WriteUInt16((ushort)value, writer);
					return true;
				case ProtoTypeCode.Int32:
					ProtoWriter.WriteInt32((int)value, writer);
					return true;
				case ProtoTypeCode.UInt32:
					ProtoWriter.WriteUInt32((uint)value, writer);
					return true;
				case ProtoTypeCode.Int64:
					ProtoWriter.WriteInt64((long)value, writer);
					return true;
				case ProtoTypeCode.UInt64:
					ProtoWriter.WriteUInt64((ulong)value, writer);
					return true;
				case ProtoTypeCode.Single:
					ProtoWriter.WriteSingle((float)value, writer);
					return true;
				case ProtoTypeCode.Double:
					ProtoWriter.WriteDouble((double)value, writer);
					return true;
				case ProtoTypeCode.Decimal:
					BclHelpers.WriteDecimal((decimal)value, writer);
					return true;
				case ProtoTypeCode.DateTime:
					BclHelpers.WriteDateTime((DateTime)value, writer);
					return true;
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					ProtoWriter.WriteString((string)value, writer);
					return true;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						BclHelpers.WriteTimeSpan((TimeSpan)value, writer);
						return true;
					case ProtoTypeCode.ByteArray:
						ProtoWriter.WriteBytes((byte[])value, writer);
						return true;
					case ProtoTypeCode.Guid:
						BclHelpers.WriteGuid((Guid)value, writer);
						return true;
					case ProtoTypeCode.Uri:
						ProtoWriter.WriteString(((Uri)value).AbsoluteUri, writer);
						return true;
					}
					break;
				}
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable == null)
				{
					return false;
				}
				if (isInsideList)
				{
					throw TypeModel.CreateNestedListsNotSupported();
				}
				foreach (object obj in enumerable)
				{
					if (obj == null)
					{
						throw new NullReferenceException();
					}
					if (!this.TrySerializeAuxiliaryType(writer, null, format, tag, obj, true))
					{
						TypeModel.ThrowUnexpectedType(obj.GetType());
					}
				}
				return true;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00013A58 File Offset: 0x00011C58
		private void SerializeCore(ProtoWriter writer, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				this.Serialize(key, value, writer);
				return;
			}
			if (!this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00004511 File Offset: 0x00002711
		public void Serialize(Stream dest, object value)
		{
			this.Serialize(dest, value, null);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public void Serialize(Stream dest, object value, SerializationContext context)
		{
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				protoWriter.SetRootObject(value);
				this.SerializeCore(protoWriter, value);
				protoWriter.Close();
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000451C File Offset: 0x0000271C
		public void Serialize(ProtoWriter dest, object value)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest.CheckDepthFlushlock();
			dest.SetRootObject(value);
			this.SerializeCore(dest, value);
			dest.CheckDepthFlushlock();
			ProtoWriter.Flush(dest);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, fieldNumber, null, out num);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00013B10 File Offset: 0x00011D10
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out num);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013B30 File Offset: 0x00011D30
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead)
		{
			bool flag;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead, out flag, null);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013B54 File Offset: 0x00011D54
		private object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead, out bool haveObject, SerializationContext context)
		{
			haveObject = false;
			bytesRead = 0;
			if (type == null && (style != PrefixStyle.Base128 || resolver == null))
			{
				throw new InvalidOperationException("A type must be provided unless base-128 prefixing is being used in combination with a resolver");
			}
			for (;;)
			{
				bool flag = expectedField > 0 || resolver != null;
				int num2;
				int num3;
				int num = ProtoReader.ReadLengthPrefix(source, flag, style, out num2, out num3);
				if (num3 == 0)
				{
					break;
				}
				bytesRead += num3;
				if (num < 0)
				{
					return value;
				}
				bool flag2;
				if (style == PrefixStyle.Base128)
				{
					if (flag && expectedField == 0 && type == null && resolver != null)
					{
						type = resolver(num2);
						flag2 = (type == null);
					}
					else
					{
						flag2 = (expectedField != num2);
					}
				}
				else
				{
					flag2 = false;
				}
				if (flag2)
				{
					if (num == 2147483647)
					{
						goto Block_12;
					}
					ProtoReader.Seek(source, num, null);
					bytesRead += num;
				}
				if (!flag2)
				{
					goto Block_13;
				}
			}
			return value;
			Block_12:
			throw new InvalidOperationException();
			Block_13:
			ProtoReader protoReader = null;
			object result;
			try
			{
				int num;
				protoReader = ProtoReader.Create(source, this, context, num);
				int key = this.GetKey(ref type);
				if (key >= 0 && !Helpers.IsEnum(type))
				{
					value = this.Deserialize(key, value, protoReader);
				}
				else if (!this.TryDeserializeAuxiliaryType(protoReader, DataFormat.Default, 1, type, ref value, true, false, true, false) && num != 0)
				{
					TypeModel.ThrowUnexpectedType(type);
				}
				bytesRead += protoReader.Position;
				haveObject = true;
				result = value;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000454D File Offset: 0x0000274D
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			return this.DeserializeItems(source, type, style, expectedField, resolver, null);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000455D File Offset: 0x0000275D
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator(this, source, type, style, expectedField, resolver, context);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000456E File Offset: 0x0000276E
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField)
		{
			return this.DeserializeItems<T>(source, style, expectedField, null);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000457A File Offset: 0x0000277A
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator<T>(this, source, style, expectedField, context);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00004587 File Offset: 0x00002787
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			this.SerializeWithLengthPrefix(dest, value, type, style, fieldNumber, null);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00013C8C File Offset: 0x00011E8C
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber, SerializationContext context)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				type = this.MapType(value.GetType());
			}
			int key = this.GetKey(ref type);
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				if (style != PrefixStyle.None)
				{
					if (style - PrefixStyle.Base128 > 2)
					{
						throw new ArgumentOutOfRangeException("style");
					}
					ProtoWriter.WriteObject(value, key, protoWriter, style, fieldNumber);
				}
				else
				{
					this.Serialize(key, value, protoWriter);
				}
				protoWriter.Close();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00004597 File Offset: 0x00002797
		public object Deserialize(Stream source, object value, Type type)
		{
			return this.Deserialize(source, value, type, null);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013D20 File Offset: 0x00011F20
		public object Deserialize(Stream source, object value, Type type, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, -1);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013D7C File Offset: 0x00011F7C
		private bool PrepareDeserialize(object value, ref Type type)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("type");
				}
				type = this.MapType(value.GetType());
			}
			bool result = true;
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
				result = false;
			}
			return result;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000045A3 File Offset: 0x000027A3
		public object Deserialize(Stream source, object value, Type type, int length)
		{
			return this.Deserialize(source, value, type, length, null);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00013DBC File Offset: 0x00011FBC
		public object Deserialize(Stream source, object value, Type type, int length, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, length);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013E18 File Offset: 0x00012018
		public object Deserialize(ProtoReader source, object value, Type type)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			if (value != null)
			{
				source.SetRootObject(value);
			}
			object result = this.DeserializeCore(source, type, value, noAutoCreate);
			source.CheckFullyConsumed();
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013E58 File Offset: 0x00012058
		private object DeserializeCore(ProtoReader reader, Type type, object value, bool noAutoCreate)
		{
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				return this.Deserialize(key, value, reader);
			}
			this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, noAutoCreate, false);
			return value;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013E98 File Offset: 0x00012098
		internal static MethodInfo ResolveListAdd(TypeModel model, Type listType, Type itemType, out bool isList)
		{
			isList = model.MapType(TypeModel.ilist).IsAssignableFrom(listType);
			Type[] array = new Type[]
			{
				itemType
			};
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			if (instanceMethod == null)
			{
				bool flag = listType.IsInterface && listType == model.MapType(typeof(IEnumerable<>)).MakeGenericType(array);
				Type type = model.MapType(typeof(ICollection<>)).MakeGenericType(array);
				if (flag || type.IsAssignableFrom(listType))
				{
					instanceMethod = Helpers.GetInstanceMethod(type, "Add", array);
				}
			}
			if (instanceMethod == null)
			{
				foreach (Type type2 in listType.GetInterfaces())
				{
					if (type2.Name == "IProducerConsumerCollection`1" && type2.IsGenericType && type2.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
					{
						instanceMethod = Helpers.GetInstanceMethod(type2, "TryAdd", array);
						if (instanceMethod != null)
						{
							break;
						}
					}
				}
			}
			if (instanceMethod == null)
			{
				array[0] = model.MapType(typeof(object));
				instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			}
			if (instanceMethod == null & isList)
			{
				instanceMethod = Helpers.GetInstanceMethod(model.MapType(TypeModel.ilist), "Add", array);
			}
			return instanceMethod;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013FD8 File Offset: 0x000121D8
		internal static Type GetListItemType(TypeModel model, Type listType)
		{
			if (listType == model.MapType(typeof(string)) || listType.IsArray || !model.MapType(typeof(IEnumerable)).IsAssignableFrom(listType))
			{
				return null;
			}
			BasicList basicList = new BasicList();
			foreach (MethodInfo methodInfo in listType.GetMethods())
			{
				if (!methodInfo.IsStatic && !(methodInfo.Name != "Add"))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Type parameterType;
					if (parameters.Length == 1 && !basicList.Contains(parameterType = parameters[0].ParameterType))
					{
						basicList.Add(parameterType);
					}
				}
			}
			string name = listType.Name;
			if (name == null || (name.IndexOf("Queue") < 0 && name.IndexOf("Stack") < 0))
			{
				TypeModel.TestEnumerableListPatterns(model, basicList, listType);
				foreach (Type iType in listType.GetInterfaces())
				{
					TypeModel.TestEnumerableListPatterns(model, basicList, iType);
				}
			}
			foreach (PropertyInfo propertyInfo in listType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.Name != "Item") && !basicList.Contains(propertyInfo.PropertyType))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && indexParameters[0].ParameterType == model.MapType(typeof(int)))
					{
						basicList.Add(propertyInfo.PropertyType);
					}
				}
			}
			switch (basicList.Count)
			{
			case 0:
				return null;
			case 1:
				return (Type)basicList[0];
			case 2:
				if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[0], (Type)basicList[1]))
				{
					return (Type)basicList[0];
				}
				if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[1], (Type)basicList[0]))
				{
					return (Type)basicList[1];
				}
				break;
			}
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000141E4 File Offset: 0x000123E4
		private static void TestEnumerableListPatterns(TypeModel model, BasicList candidates, Type iType)
		{
			if (iType.IsGenericType)
			{
				Type genericTypeDefinition = iType.GetGenericTypeDefinition();
				if (genericTypeDefinition == model.MapType(typeof(IEnumerable<>)) || genericTypeDefinition == model.MapType(typeof(ICollection<>)) || genericTypeDefinition.FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
				{
					Type[] genericArguments = iType.GetGenericArguments();
					if (!candidates.Contains(genericArguments[0]))
					{
						candidates.Add(genericArguments[0]);
					}
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000045B1 File Offset: 0x000027B1
		private static bool CheckDictionaryAccessors(TypeModel model, Type pair, Type value)
		{
			return pair.IsGenericType && pair.GetGenericTypeDefinition() == model.MapType(typeof(KeyValuePair<, >)) && pair.GetGenericArguments()[1] == value;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00014254 File Offset: 0x00012454
		private bool TryDeserializeList(TypeModel model, ProtoReader reader, DataFormat format, int tag, Type listType, Type itemType, ref object value)
		{
			bool flag;
			MethodInfo methodInfo = TypeModel.ResolveListAdd(model, listType, itemType, out flag);
			if (methodInfo == null)
			{
				throw new NotSupportedException("Unknown list variant: " + listType.FullName);
			}
			bool result = false;
			object obj = null;
			IList list = value as IList;
			object[] array = flag ? null : new object[1];
			BasicList basicList = listType.IsArray ? new BasicList() : null;
			while (this.TryDeserializeAuxiliaryType(reader, format, tag, itemType, ref obj, true, true, true, true))
			{
				result = true;
				if (value == null && basicList == null)
				{
					value = TypeModel.CreateListInstance(listType, itemType);
					list = (value as IList);
				}
				if (list != null)
				{
					list.Add(obj);
				}
				else if (basicList != null)
				{
					basicList.Add(obj);
				}
				else
				{
					array[0] = obj;
					methodInfo.Invoke(value, array);
				}
				obj = null;
			}
			if (basicList != null)
			{
				if (value != null)
				{
					if (basicList.Count != 0)
					{
						Array array2 = (Array)value;
						Array array3 = Array.CreateInstance(itemType, array2.Length + basicList.Count);
						Array.Copy(array2, array3, array2.Length);
						basicList.CopyTo(array3, array2.Length);
						value = array3;
					}
				}
				else
				{
					Array array3 = Array.CreateInstance(itemType, basicList.Count);
					basicList.CopyTo(array3, 0);
					value = array3;
				}
			}
			return result;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001439C File Offset: 0x0001259C
		private static object CreateListInstance(Type listType, Type itemType)
		{
			Type type = listType;
			if (listType.IsArray)
			{
				return Array.CreateInstance(itemType, 0);
			}
			if (!listType.IsClass || listType.IsAbstract || Helpers.GetConstructor(listType, Helpers.EmptyTypes, true) == null)
			{
				bool flag = false;
				string fullName;
				if (listType.IsInterface && (fullName = listType.FullName) != null && fullName.IndexOf("Dictionary") >= 0)
				{
					if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(IDictionary<, >))
					{
						Type[] genericArguments = listType.GetGenericArguments();
						type = typeof(Dictionary<, >).MakeGenericType(genericArguments);
						flag = true;
					}
					if (!flag && listType == typeof(IDictionary))
					{
						type = typeof(Hashtable);
						flag = true;
					}
				}
				if (!flag)
				{
					type = typeof(List<>).MakeGenericType(new Type[]
					{
						itemType
					});
					flag = true;
				}
				if (!flag)
				{
					type = typeof(ArrayList);
				}
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00014484 File Offset: 0x00012684
		internal bool TryDeserializeAuxiliaryType(ProtoReader reader, DataFormat format, int tag, Type type, ref object value, bool skipOtherFields, bool asListItem, bool autoCreate, bool insideList)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			bool flag = false;
			if (wireType == WireType.None)
			{
				Type type2 = TypeModel.GetListItemType(this, type);
				if (type2 == null && type.IsArray && type.GetArrayRank() == 1 && type != typeof(byte[]))
				{
					type2 = type.GetElementType();
				}
				if (type2 != null)
				{
					if (insideList)
					{
						throw TypeModel.CreateNestedListsNotSupported();
					}
					flag = this.TryDeserializeList(this, reader, format, tag, type, type2, ref value);
					if (!flag && autoCreate)
					{
						value = TypeModel.CreateListInstance(type, type2);
					}
					return flag;
				}
				else
				{
					TypeModel.ThrowUnexpectedType(type);
				}
			}
			while (!flag || !asListItem)
			{
				int num2 = reader.ReadFieldHeader();
				if (num2 <= 0)
				{
					break;
				}
				if (num2 != tag)
				{
					if (!skipOtherFields)
					{
						throw ProtoReader.AddErrorData(new InvalidOperationException("Expected field " + tag.ToString() + ", but found " + num2.ToString()), reader);
					}
					reader.SkipField();
				}
				else
				{
					flag = true;
					reader.Hint(wireType);
					if (num >= 0)
					{
						if (wireType - WireType.String <= 1)
						{
							SubItemToken token = ProtoReader.StartSubItem(reader);
							value = this.Deserialize(num, value, reader);
							ProtoReader.EndSubItem(token, reader);
						}
						else
						{
							value = this.Deserialize(num, value, reader);
						}
					}
					else
					{
						switch (typeCode)
						{
						case ProtoTypeCode.Boolean:
							value = reader.ReadBoolean();
							break;
						case ProtoTypeCode.Char:
							value = (char)reader.ReadUInt16();
							break;
						case ProtoTypeCode.SByte:
							value = reader.ReadSByte();
							break;
						case ProtoTypeCode.Byte:
							value = reader.ReadByte();
							break;
						case ProtoTypeCode.Int16:
							value = reader.ReadInt16();
							break;
						case ProtoTypeCode.UInt16:
							value = reader.ReadUInt16();
							break;
						case ProtoTypeCode.Int32:
							value = reader.ReadInt32();
							break;
						case ProtoTypeCode.UInt32:
							value = reader.ReadUInt32();
							break;
						case ProtoTypeCode.Int64:
							value = reader.ReadInt64();
							break;
						case ProtoTypeCode.UInt64:
							value = reader.ReadUInt64();
							break;
						case ProtoTypeCode.Single:
							value = reader.ReadSingle();
							break;
						case ProtoTypeCode.Double:
							value = reader.ReadDouble();
							break;
						case ProtoTypeCode.Decimal:
							value = BclHelpers.ReadDecimal(reader);
							break;
						case ProtoTypeCode.DateTime:
							value = BclHelpers.ReadDateTime(reader);
							break;
						case (ProtoTypeCode)17:
							break;
						case ProtoTypeCode.String:
							value = reader.ReadString();
							break;
						default:
							switch (typeCode)
							{
							case ProtoTypeCode.TimeSpan:
								value = BclHelpers.ReadTimeSpan(reader);
								break;
							case ProtoTypeCode.ByteArray:
								value = ProtoReader.AppendBytes((byte[])value, reader);
								break;
							case ProtoTypeCode.Guid:
								value = BclHelpers.ReadGuid(reader);
								break;
							case ProtoTypeCode.Uri:
								value = new Uri(reader.ReadString());
								break;
							}
							break;
						}
					}
				}
			}
			if (!flag && !asListItem && autoCreate && type != typeof(string))
			{
				value = Activator.CreateInstance(type);
			}
			return flag;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000045E0 File Offset: 0x000027E0
		public static RuntimeTypeModel Create()
		{
			return new RuntimeTypeModel(false);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000147C4 File Offset: 0x000129C4
		protected internal static Type ResolveProxies(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.IsGenericParameter)
			{
				return null;
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return underlyingType;
			}
			string fullName = type.FullName;
			if (fullName != null && fullName.StartsWith("System.Data.Entity.DynamicProxies."))
			{
				return type.BaseType;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				string fullName2 = interfaces[i].FullName;
				if (fullName2 == "NHibernate.Proxy.INHibernateProxy" || fullName2 == "NHibernate.Proxy.DynamicProxy.IProxy" || fullName2 == "NHibernate.Intercept.IFieldInterceptorAccessor")
				{
					return type.BaseType;
				}
			}
			return null;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000045E8 File Offset: 0x000027E8
		public bool IsDefined(Type type)
		{
			return this.GetKey(ref type) >= 0;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001485C File Offset: 0x00012A5C
		protected internal int GetKey(ref Type type)
		{
			if (type == null)
			{
				return -1;
			}
			int keyImpl = this.GetKeyImpl(type);
			if (keyImpl < 0)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					type = type2;
					keyImpl = this.GetKeyImpl(type);
				}
			}
			return keyImpl;
		}

		// Token: 0x060003CF RID: 975
		protected abstract int GetKeyImpl(Type type);

		// Token: 0x060003D0 RID: 976
		protected internal abstract void Serialize(int key, object value, ProtoWriter dest);

		// Token: 0x060003D1 RID: 977
		protected internal abstract object Deserialize(int key, object value, ProtoReader source);

		// Token: 0x060003D2 RID: 978 RVA: 0x00014894 File Offset: 0x00012A94
		public object DeepClone(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ProtoWriter protoWriter = new ProtoWriter(memoryStream, this, null))
					{
						protoWriter.SetRootObject(value);
						this.Serialize(key, value, protoWriter);
						protoWriter.Close();
					}
					memoryStream.Position = 0L;
					ProtoReader protoReader = null;
					try
					{
						protoReader = ProtoReader.Create(memoryStream, this, null, -1);
						return this.Deserialize(key, null, protoReader);
					}
					finally
					{
						ProtoReader.Recycle(protoReader);
					}
				}
			}
			if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				byte[] array2 = new byte[array.Length];
				Helpers.BlockCopy(array, 0, array2, 0, array.Length);
				return array2;
			}
			int num;
			if (this.GetWireType(Helpers.GetTypeCode(type), DataFormat.Default, ref type, out num) != WireType.None && num < 0)
			{
				return value;
			}
			object result;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (ProtoWriter protoWriter2 = new ProtoWriter(memoryStream2, this, null))
				{
					if (!this.TrySerializeAuxiliaryType(protoWriter2, type, DataFormat.Default, 1, value, false))
					{
						TypeModel.ThrowUnexpectedType(type);
					}
					protoWriter2.Close();
				}
				memoryStream2.Position = 0L;
				ProtoReader reader = null;
				try
				{
					reader = ProtoReader.Create(memoryStream2, this, null, -1);
					value = null;
					this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false);
					result = value;
				}
				finally
				{
					ProtoReader.Recycle(reader);
				}
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000045F8 File Offset: 0x000027F8
		protected internal static void ThrowUnexpectedSubtype(Type expected, Type actual)
		{
			if (expected != TypeModel.ResolveProxies(actual))
			{
				throw new InvalidOperationException("Unexpected sub-type: " + actual.FullName);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014A50 File Offset: 0x00012C50
		protected internal static void ThrowUnexpectedType(Type type)
		{
			string str = (type == null) ? "(unknown)" : type.FullName;
			if (type != null)
			{
				Type baseType = type.BaseType;
				if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition().Name == "GeneratedMessage`2")
				{
					throw new InvalidOperationException("Are you mixing protobuf-net and protobuf-csharp-port? See http://stackoverflow.com/q/11564914; type: " + str);
				}
			}
			throw new InvalidOperationException("Type is not expected, and no contract can be inferred: " + str);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00004619 File Offset: 0x00002819
		internal static Exception CreateNestedListsNotSupported()
		{
			return new NotSupportedException("Nested or jagged lists and arrays are not supported");
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00004625 File Offset: 0x00002825
		public static void ThrowCannotCreateInstance(Type type)
		{
			throw new ProtoException("No parameterless constructor found for " + ((type == null) ? "(null)" : type.Name));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014ABC File Offset: 0x00012CBC
		internal static string SerializeType(TypeModel model, Type type)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(type);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (!Helpers.IsNullOrEmpty(typeFormatEventArgs.FormattedName))
					{
						return typeFormatEventArgs.FormattedName;
					}
				}
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014B00 File Offset: 0x00012D00
		internal static Type DeserializeType(TypeModel model, string value)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(value);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (typeFormatEventArgs.Type != null)
					{
						return typeFormatEventArgs.Type;
					}
				}
			}
			return Type.GetType(value);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00004646 File Offset: 0x00002846
		public bool CanSerializeContractType(Type type)
		{
			return this.CanSerialize(type, false, true, true);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00004652 File Offset: 0x00002852
		public bool CanSerialize(Type type)
		{
			return this.CanSerialize(type, true, true, true);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000465E File Offset: 0x0000285E
		public bool CanSerializeBasicType(Type type)
		{
			return this.CanSerialize(type, true, false, true);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014B40 File Offset: 0x00012D40
		private bool CanSerialize(Type type, bool allowBasic, bool allowContract, bool allowLists)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			if (typeCode > ProtoTypeCode.Unknown)
			{
				return allowBasic;
			}
			if (this.GetKey(ref type) >= 0)
			{
				return allowContract;
			}
			if (allowLists)
			{
				Type type2 = null;
				if (type.IsArray)
				{
					if (type.GetArrayRank() == 1)
					{
						type2 = type.GetElementType();
					}
				}
				else
				{
					type2 = TypeModel.GetListItemType(this, type);
				}
				if (type2 != null)
				{
					return this.CanSerialize(type2, allowBasic, allowContract, false);
				}
			}
			return false;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000250E File Offset: 0x0000070E
		public virtual string GetSchema(Type type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003DE RID: 990 RVA: 0x00014BBC File Offset: 0x00012DBC
		// (remove) Token: 0x060003DF RID: 991 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public event TypeFormatEventHandler DynamicTypeFormatting;

		// Token: 0x060003E0 RID: 992 RVA: 0x0000466A File Offset: 0x0000286A
		internal virtual Type GetType(string fullName, Assembly context)
		{
			return TypeModel.ResolveKnownType(fullName, this, context);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00014C2C File Offset: 0x00012E2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Type ResolveKnownType(string name, TypeModel model, Assembly assembly)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			try
			{
				Type type = Type.GetType(name);
				if (type != null)
				{
					return type;
				}
			}
			catch
			{
			}
			try
			{
				int num = name.IndexOf(',');
				string name2 = ((num > 0) ? name.Substring(0, num) : name).Trim();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				Type type2 = (assembly == null) ? null : assembly.GetType(name2);
				if (type2 != null)
				{
					return type2;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0400017D RID: 381
		private static readonly Type ilist = typeof(IList);

		// Token: 0x02000074 RID: 116
		private sealed class DeserializeItemsIterator<T> : TypeModel.DeserializeItemsIterator, IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060003E4 RID: 996 RVA: 0x00004685 File Offset: 0x00002885
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this;
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x00004688 File Offset: 0x00002888
			public new T Current
			{
				get
				{
					return (T)((object)base.Current);
				}
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x00002596 File Offset: 0x00000796
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x00004695 File Offset: 0x00002895
			public DeserializeItemsIterator(TypeModel model, Stream source, PrefixStyle style, int expectedField, SerializationContext context) : base(model, source, model.MapType(typeof(T)), style, expectedField, null, context)
			{
			}
		}

		// Token: 0x02000075 RID: 117
		private class DeserializeItemsIterator : IEnumerator, IEnumerable
		{
			// Token: 0x060003E8 RID: 1000 RVA: 0x00004685 File Offset: 0x00002885
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x00014CBC File Offset: 0x00012EBC
			public bool MoveNext()
			{
				if (this.haveObject)
				{
					int num;
					this.current = this.model.DeserializeWithLengthPrefix(this.source, null, this.type, this.style, this.expectedField, this.resolver, out num, out this.haveObject, this.context);
				}
				return this.haveObject;
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0000250E File Offset: 0x0000070E
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x000046B5 File Offset: 0x000028B5
			public object Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00014D18 File Offset: 0x00012F18
			public DeserializeItemsIterator(TypeModel model, Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
			{
				this.haveObject = true;
				this.source = source;
				this.type = type;
				this.style = style;
				this.expectedField = expectedField;
				this.resolver = resolver;
				this.model = model;
				this.context = context;
			}

			// Token: 0x0400017F RID: 383
			private bool haveObject;

			// Token: 0x04000180 RID: 384
			private object current;

			// Token: 0x04000181 RID: 385
			private readonly Stream source;

			// Token: 0x04000182 RID: 386
			private readonly Type type;

			// Token: 0x04000183 RID: 387
			private readonly PrefixStyle style;

			// Token: 0x04000184 RID: 388
			private readonly int expectedField;

			// Token: 0x04000185 RID: 389
			private readonly Serializer.TypeResolver resolver;

			// Token: 0x04000186 RID: 390
			private readonly TypeModel model;

			// Token: 0x04000187 RID: 391
			private readonly SerializationContext context;
		}

		// Token: 0x02000076 RID: 118
		protected internal enum CallbackType
		{
			// Token: 0x04000189 RID: 393
			BeforeSerialize,
			// Token: 0x0400018A RID: 394
			AfterSerialize,
			// Token: 0x0400018B RID: 395
			BeforeDeserialize,
			// Token: 0x0400018C RID: 396
			AfterDeserialize
		}
	}
}
