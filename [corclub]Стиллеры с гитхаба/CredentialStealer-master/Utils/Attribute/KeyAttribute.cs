using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CredentialStealer.Utils.Attribute
{
    public interface ICustomAttribute<U>
    {
        U Key { get; }
    }

    public class KeyAttribute : System.Attribute, ICustomAttribute<String>
    {
        public KeyAttribute()
        {
            this.Key = string.Empty;
        }

        public KeyAttribute(string keyValue)
            : this()
        {
            this.Key = keyValue;
        }

        public string Key { get; set; }

    }

}
