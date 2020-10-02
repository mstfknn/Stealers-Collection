using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200002F RID: 47
	public static class Serializer
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00002FD4 File Offset: 0x000011D4
		public static string GetProto<T>()
		{
			return RuntimeTypeModel.Default.GetSchema(RuntimeTypeModel.Default.MapType(typeof(T)));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static T DeepClone<T>(T instance)
		{
			if (instance != null)
			{
				return (T)((object)RuntimeTypeModel.Default.DeepClone(instance));
			}
			return instance;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003015 File Offset: 0x00001215
		public static T Merge<T>(Stream source, T instance)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, instance, typeof(T)));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003037 File Offset: 0x00001237
		public static T Deserialize<T>(Stream source)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, null, typeof(T)));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003054 File Offset: 0x00001254
		public static void Serialize<T>(Stream destination, T instance)
		{
			if (instance != null)
			{
				RuntimeTypeModel.Default.Serialize(destination, instance);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public static TTo ChangeType<TFrom, TTo>(TFrom instance)
		{
			TTo result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize<TFrom>(memoryStream, instance);
				memoryStream.Position = 0L;
				result = Serializer.Deserialize<TTo>(memoryStream);
			}
			return result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00002596 File Offset: 0x00000796
		public static void PrepareSerializer<T>()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000306F File Offset: 0x0000126F
		public static IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			return RuntimeTypeModel.Default.DeserializeItems<T>(source, style, fieldNumber);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000307E File Offset: 0x0000127E
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style)
		{
			return Serializer.DeserializeWithLengthPrefix<T>(source, style, 0);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, null, @default.MapType(typeof(T)), style, fieldNumber));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000DE20 File Offset: 0x0000C020
		public static T MergeWithLengthPrefix<T>(Stream source, T instance, PrefixStyle style)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, instance, @default.MapType(typeof(T)), style, 0));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003088 File Offset: 0x00001288
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style)
		{
			Serializer.SerializeWithLengthPrefix<T>(destination, instance, style, 0);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000DE58 File Offset: 0x0000C058
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(typeof(T)), style, fieldNumber);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public static bool TryReadLengthPrefix(Stream source, PrefixStyle style, out int length)
		{
			int num;
			int num2;
			length = ProtoReader.ReadLengthPrefix(source, false, style, out num, out num2);
			return num2 > 0;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		public static bool TryReadLengthPrefix(byte[] buffer, int index, int count, PrefixStyle style, out int length)
		{
			bool result;
			using (Stream stream = new MemoryStream(buffer, index, count))
			{
				result = Serializer.TryReadLengthPrefix(stream, style, out length);
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003093 File Offset: 0x00001293
		public static void FlushPool()
		{
			BufferPool.Flush();
		}

		// Token: 0x040000CC RID: 204
		private const string ProtoBinaryField = "proto";

		// Token: 0x040000CD RID: 205
		public const int ListItemTag = 1;

		// Token: 0x02000030 RID: 48
		public static class NonGeneric
		{
			// Token: 0x06000183 RID: 387 RVA: 0x0000309A File Offset: 0x0000129A
			public static object DeepClone(object instance)
			{
				if (instance != null)
				{
					return RuntimeTypeModel.Default.DeepClone(instance);
				}
				return null;
			}

			// Token: 0x06000184 RID: 388 RVA: 0x000030AC File Offset: 0x000012AC
			public static void Serialize(Stream dest, object instance)
			{
				if (instance != null)
				{
					RuntimeTypeModel.Default.Serialize(dest, instance);
				}
			}

			// Token: 0x06000185 RID: 389 RVA: 0x000030BD File Offset: 0x000012BD
			public static object Deserialize(Type type, Stream source)
			{
				return RuntimeTypeModel.Default.Deserialize(source, null, type);
			}

			// Token: 0x06000186 RID: 390 RVA: 0x000030CC File Offset: 0x000012CC
			public static object Merge(Stream source, object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return RuntimeTypeModel.Default.Deserialize(source, instance, instance.GetType(), null);
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000DEEC File Offset: 0x0000C0EC
			public static void SerializeWithLengthPrefix(Stream destination, object instance, PrefixStyle style, int fieldNumber)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(instance.GetType()), style, fieldNumber);
			}

			// Token: 0x06000188 RID: 392 RVA: 0x000030EF File Offset: 0x000012EF
			public static bool TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, Serializer.TypeResolver resolver, out object value)
			{
				value = RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, null, null, style, 0, resolver);
				return value != null;
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00003108 File Offset: 0x00001308
			public static bool CanSerialize(Type type)
			{
				return RuntimeTypeModel.Default.IsDefined(type);
			}
		}

		// Token: 0x02000031 RID: 49
		public static class GlobalOptions
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600018A RID: 394 RVA: 0x00003115 File Offset: 0x00001315
			// (set) Token: 0x0600018B RID: 395 RVA: 0x00003121 File Offset: 0x00001321
			[Obsolete("Please use RuntimeTypeModel.Default.InferTagFromNameDefault instead (or on a per-model basis)", false)]
			public static bool InferTagFromName
			{
				get
				{
					return RuntimeTypeModel.Default.InferTagFromNameDefault;
				}
				set
				{
					RuntimeTypeModel.Default.InferTagFromNameDefault = value;
				}
			}
		}

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x0600018D RID: 397
		public delegate Type TypeResolver(int fieldNumber);
	}
}
