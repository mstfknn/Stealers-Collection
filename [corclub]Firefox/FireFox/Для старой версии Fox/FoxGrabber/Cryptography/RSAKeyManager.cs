namespace FoxGrabber.Cryptography
{
    using System;
    using System.Security.Cryptography;

    public class RSAKeyManager : IRSAKeyManager
    {
        private readonly string containerName = string.Empty;

        public RSAKeyManager() => this.containerName = Environment.GetCommandLineArgs()[0];
        public RSAKeyManager(string containerName) => this.containerName = containerName;

        private RSACryptoServiceProvider GenKey_SaveInContainer
        {
            get
            {
                var cp = new CspParameters
                {
                    KeyContainerName = this.containerName
                };
                using (var rsa = new RSACryptoServiceProvider(cp))
                {
                    rsa.KeySize = 512;
                    rsa.PersistKeyInCsp = true;
                    return rsa;
                }
            }
        }

        public RSACryptoServiceProvider KeyFromContainer
        {
            get
            {
                var cp = new CspParameters
                {
                    KeyContainerName = this.containerName
                };
                using (var rsa = new RSACryptoServiceProvider(cp))
                {
                    return rsa ?? this.GenKey_SaveInContainer;
                }
            }
        }
        public void DeleteKeyFromContainer()
        {
            var cp = new CspParameters
            {
                KeyContainerName = this.containerName
            };
            using (var rsa = new RSACryptoServiceProvider(cp))
            {
                rsa.PersistKeyInCsp = false;
                rsa.Clear();
            }
        }
    }
}
