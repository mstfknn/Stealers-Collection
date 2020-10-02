using System;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

namespace NoiseMe.Drags.App.Models.LocalModels.Extensions
{
	// Token: 0x02000138 RID: 312
	public static class ProtoExtensions
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x0002024C File Offset: 0x0001E44C
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

		// Token: 0x060009A1 RID: 2465 RVA: 0x000202A0 File Offset: 0x0001E4A0
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
