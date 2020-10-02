using System;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

namespace GrandSteal.Client.Models.Extensions
{
	// Token: 0x0200000F RID: 15
	public static class ProtoExtensions
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003D08 File Offset: 0x00001F08
		public static byte[] SerializeProto<T>(this T instance)
		{
			byte[] buffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				RuntimeTypeModel.Default.SerializeWithLengthPrefix(memoryStream, instance, typeof(T), PrefixStyle.Base128, 1);
				buffer = memoryStream.GetBuffer();
			}
			return buffer;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D5C File Offset: 0x00001F5C
		public static T DeSerializeProto<T>(this byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				TypeModel @default = RuntimeTypeModel.Default;
				Stream source = memoryStream;
				t = default(T);
				t = (T)((object)@default.DeserializeWithLengthPrefix(source, t, typeof(T), PrefixStyle.Base128, 1));
			}
			return t;
		}
	}
}
