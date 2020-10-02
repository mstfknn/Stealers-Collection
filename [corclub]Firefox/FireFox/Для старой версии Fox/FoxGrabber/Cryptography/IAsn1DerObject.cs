using System.Collections.Generic;

namespace FoxGrabber.Cryptography
{
    public interface IAsn1DerObject
    {
        byte[] Data { get; set; }
        int Lenght { get; set; }
        List<Asn1DerObject> objects { get; set; }
        Asn1Der.Type Type { get; set; }

        string ToString();
    }
}