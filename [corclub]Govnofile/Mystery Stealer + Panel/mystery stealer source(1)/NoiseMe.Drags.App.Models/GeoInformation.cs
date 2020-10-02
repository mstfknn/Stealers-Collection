using ProtoBuf;

namespace NoiseMe.Drags.App.Models
{
	[ProtoContract(Name = "GeoInformation")]
	public class GeoInformation
	{
		[ProtoMember(1, Name = "as")]
		public string As
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "city")]
		public string City
		{
			get;
			set;
		}

		[ProtoMember(3, Name = "country")]
		public string Country
		{
			get;
			set;
		}

		[ProtoMember(4, Name = "countryCode")]
		public string CountryCode
		{
			get;
			set;
		}

		[ProtoMember(5, Name = "isp")]
		public string Isp
		{
			get;
			set;
		}

		[ProtoMember(6, Name = "lat")]
		public double Lat
		{
			get;
			set;
		}

		[ProtoMember(7, Name = "lon")]
		public double Lon
		{
			get;
			set;
		}

		[ProtoMember(8, Name = "org")]
		public string Org
		{
			get;
			set;
		}

		[ProtoMember(9, Name = "query")]
		public string Query
		{
			get;
			set;
		}

		[ProtoMember(10, Name = "region")]
		public string Region
		{
			get;
			set;
		}

		[ProtoMember(11, Name = "regionName")]
		public string RegionName
		{
			get;
			set;
		}

		[ProtoMember(12, Name = "status")]
		public string Status
		{
			get;
			set;
		}

		[ProtoMember(13, Name = "timezone")]
		public string Timezone
		{
			get;
			set;
		}

		[ProtoMember(14, Name = "zip")]
		public string Zip
		{
			get;
			set;
		}
	}
}
