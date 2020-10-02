using System.Security.Cryptography;

namespace FoxGrabber.Cryptography
{
    public interface IRSAKeyManager
    {
        RSACryptoServiceProvider KeyFromContainer { get; }

        void DeleteKeyFromContainer();
    }
}