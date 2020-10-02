namespace Loki.Gecko
{
    using System.Collections.Generic;
    using System.Text;

    public class Asn1DerObject
    {
        public Type Type { get; set; }

        private byte[] Lenght;

        public byte[] GetObjectData() => this.Lenght;

        public void SetObjectData(byte[] value) => this.Lenght = value;

        public int ObjectLength { get; set; }

        public List<Asn1DerObject> Objects { get; set; }

        public Asn1DerObject() => this.Objects = new List<Asn1DerObject>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var stringBuilder2 = new StringBuilder();
            switch (this.Type)
            {
                case Type.Sequence:
                    stringBuilder.AppendLine("SEQUENCE {");
                    break;
                case Type.Integer:
                    {
                        byte[] objectData = GetObjectData();
                        foreach (byte b2 in objectData)
                        {
                            stringBuilder2.AppendFormat("{0:X2}", b2);
                        }
                        stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
                        break;
                    }
                case Type.OctetString:
                    {
                        byte[] objectData = GetObjectData();
                        foreach (byte b3 in objectData)
                        {
                            stringBuilder2.AppendFormat("{0:X2}", b3);
                        }
                        stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
                        break;
                    }
                case Type.ObjectIdentifier:
                    {
                        byte[] objectData = GetObjectData();
                        foreach (byte b in objectData)
                        {
                            stringBuilder2.AppendFormat("{0:X2}", b);
                        }
                        stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
                        break;
                    }
            }
            foreach (Asn1DerObject @object in this.Objects)
            {
                stringBuilder.Append(@object.ToString());
            }
            if (this.Type == Type.Sequence)
            {
                stringBuilder.AppendLine("}");
            }
            stringBuilder2.Remove(0, stringBuilder2.Length - 1);
            return stringBuilder.ToString();
        }
    }
}
