using ProtoBuf;
using ProtoBuf.Meta;
using System.IO;

namespace NoiseMe.Drags.App.Models.LocalModels.Extensions
{
	public static class ProtoExtensions
	{
		public static byte[] SerializeProto<T>(this T instance)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				RuntimeTypeModel.Default.SerializeWithLengthPrefix(memoryStream, instance, typeof(T), PrefixStyle.Base128, 1);
				return memoryStream.GetBuffer();
			}
		}

		public static T DeSerializeProto<T>(this byte[] data)
		{
			using (MemoryStream source = new MemoryStream(data))
			{
				return (T)RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, default(T), typeof(T), PrefixStyle.Base128, 1);
			}
		}
	}
}
