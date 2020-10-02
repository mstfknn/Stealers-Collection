namespace FoxGrabber.Cryptography
{
    public interface IAsn1Der
    {
        Asn1DerObject Parse(byte[] dataToParse);
    }
}