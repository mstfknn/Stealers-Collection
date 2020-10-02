namespace FoxGrabber.Cryptography
{
    public partial class Asn1Der
    {
        public enum Type
        {
            Sequence = 0x30,
            Integer = 0x02,
            BitString = 0x03,
            OctetString = 0x04,
            Null = 0x05,
            ObjectIdentifier = 0x06
        }
    }
}