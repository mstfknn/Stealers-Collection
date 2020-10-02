using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CredentialStealer.Entities
{
    public class CredentialData
    {
        public String uid { get; set; }
        public string operatingSystem 
        { 
            get
            {
                return "Windows";
            }
        }
        public string other { get; set; }
        public string teamName 
        {
            get 
            {
                return "StealerCredential";
            }
        }

    }
}
