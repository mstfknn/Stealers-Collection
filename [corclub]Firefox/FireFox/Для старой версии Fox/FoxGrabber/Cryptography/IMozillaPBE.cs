namespace FoxGrabber.Cryptography
{
    public interface IMozillaPBE
    {
        byte[] IV { get; }
        byte[] Key { get; }

        void Compute();
    }
}