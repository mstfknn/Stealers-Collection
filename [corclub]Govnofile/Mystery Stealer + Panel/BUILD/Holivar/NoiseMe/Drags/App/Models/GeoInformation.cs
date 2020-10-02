using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models
{
	// Token: 0x02000086 RID: 134
	[ProtoContract(Name = "GeoInformation")]
	public class GeoInformation
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00004C94 File Offset: 0x00002E94
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00004C9C File Offset: 0x00002E9C
		[ProtoMember(1, Name = "as")]
		public string As { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00004CA5 File Offset: 0x00002EA5
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00004CAD File Offset: 0x00002EAD
		[ProtoMember(2, Name = "city")]
		public string City { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00004CB6 File Offset: 0x00002EB6
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00004CBE File Offset: 0x00002EBE
		[ProtoMember(3, Name = "country")]
		public string Country { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00004CC7 File Offset: 0x00002EC7
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00004CCF File Offset: 0x00002ECF
		[ProtoMember(4, Name = "countryCode")]
		public string CountryCode { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00004CD8 File Offset: 0x00002ED8
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00004CE0 File Offset: 0x00002EE0
		[ProtoMember(5, Name = "isp")]
		public string Isp { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00004CE9 File Offset: 0x00002EE9
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00004CF1 File Offset: 0x00002EF1
		[ProtoMember(6, Name = "lat")]
		public double Lat { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00004CFA File Offset: 0x00002EFA
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00004D02 File Offset: 0x00002F02
		[ProtoMember(7, Name = "lon")]
		public double Lon { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00004D0B File Offset: 0x00002F0B
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00004D13 File Offset: 0x00002F13
		[ProtoMember(8, Name = "org")]
		public string Org { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00004D1C File Offset: 0x00002F1C
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00004D24 File Offset: 0x00002F24
		[ProtoMember(9, Name = "query")]
		public string Query { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00004D2D File Offset: 0x00002F2D
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00004D35 File Offset: 0x00002F35
		[ProtoMember(10, Name = "region")]
		public string Region { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004D3E File Offset: 0x00002F3E
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00004D46 File Offset: 0x00002F46
		[ProtoMember(11, Name = "regionName")]
		public string RegionName { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00004D4F File Offset: 0x00002F4F
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00004D57 File Offset: 0x00002F57
		[ProtoMember(12, Name = "status")]
		public string Status { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00004D68 File Offset: 0x00002F68
		[ProtoMember(13, Name = "timezone")]
		public string Timezone { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00004D71 File Offset: 0x00002F71
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00004D79 File Offset: 0x00002F79
		[ProtoMember(14, Name = "zip")]
		public string Zip { get; set; }
	}
}
