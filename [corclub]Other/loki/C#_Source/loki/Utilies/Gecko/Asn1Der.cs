namespace Loki.Gecko
{
	using System;

	public static class Asn1Der
	{
		public static Asn1DerObject Parse(byte[] dataToParse)
		{
			var Asn = new Asn1DerObject();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				var Gecko2 = (Type)dataToParse[i];
				int num;
				switch (Gecko2)
				{
					case Type.Sequence:
						{
							byte[] array;
							switch (Asn.ObjectLength)
							{
								case 0:
									Asn.Type = Type.Sequence;
									Asn.ObjectLength = dataToParse.Length - (i + 2);
									array = new byte[Asn.ObjectLength];
									break;
								default:
									Asn.Objects.Add(new Asn1DerObject
									{
										Type = Type.Sequence,
										ObjectLength = dataToParse[i + 1]
									});
									array = new byte[dataToParse[i + 1]];
									break;
							}
							int unused = ((array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length);
							Array.Copy(dataToParse, i + 2, array, 0, array.Length);
							Asn.Objects.Add(Parse(array));
							i = i + 1 + dataToParse[i + 1];
							break;
						}
					case Type.Integer:
						{
							Asn.Objects.Add(new Asn1DerObject
							{
								Type = Type.Integer,
								ObjectLength = dataToParse[i + 1]
							});
							byte[] array = new byte[dataToParse[i + 1]];
							num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
							Array.Copy(dataToParse, i + 2, array, 0, num);
							Asn.Objects[Asn.Objects.Count - 1].SetObjectData(array);
							i = i + 1 + Asn.Objects[Asn.Objects.Count - 1].ObjectLength;
							break;
						}
					case Type.OctetString:
						{
							Asn.Objects.Add(new Asn1DerObject
							{
								Type = Type.OctetString,
								ObjectLength = dataToParse[i + 1]
							});
							byte[] array = new byte[dataToParse[i + 1]];
							num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
							Array.Copy(dataToParse, i + 2, array, 0, num);
							Asn.Objects[Asn.Objects.Count - 1].SetObjectData(array);
							i = i + 1 + Asn.Objects[Asn.Objects.Count - 1].ObjectLength;
							break;
						}
					case Type.ObjectIdentifier:
						{
							Asn.Objects.Add(new Asn1DerObject
							{
								Type = Type.ObjectIdentifier,
								ObjectLength = dataToParse[i + 1]
							});
							byte[] array = new byte[dataToParse[i + 1]];
							num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
							Array.Copy(dataToParse, i + 2, array, 0, num);
							Asn.Objects[Asn.Objects.Count - 1].SetObjectData(array);
							i = i + 1 + Asn.Objects[Asn.Objects.Count - 1].ObjectLength;
							break;
						}
				}
			}
			return Asn;
		}
	}
}