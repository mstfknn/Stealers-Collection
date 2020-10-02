namespace NoFile
{
    using System;
    using System.Runtime.CompilerServices;

    public class FormData
    {
        public override string ToString()
        {
            return string.Format("Name: {0}\r\nValue: {1}\r\n----------------------------\r\n", this.Name, this.Value);
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

