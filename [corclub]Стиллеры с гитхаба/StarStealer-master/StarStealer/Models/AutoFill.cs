using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class AutoFill
    {
        [DataMember]
        public string TextBox { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
