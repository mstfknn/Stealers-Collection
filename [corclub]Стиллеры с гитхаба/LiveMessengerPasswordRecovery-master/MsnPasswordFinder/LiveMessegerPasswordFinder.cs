using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MsnPasswordFinder
{
    public struct LiveIdInformation
    {
        public string LiveId;
        public string Password;
    }

    internal class LiveMessegerPasswordFinder
    {
        private LanguageSupport                 LangMgr;
        private List<LiveIdInformation>         LiveAccounts; 
        private IntPtr                          CredentialPtr;
        private int                             ReaderIndex;
        private bool                            IsSuccess;

        public LiveMessegerPasswordFinder()
        {
            this.CredentialPtr = IntPtr.Zero;
            this.LiveAccounts = null;
            this.IsSuccess = false;
            this.ReaderIndex = 0;
        }

        public bool GetAccountInformations()
        {
            bool Result;
            var Info = new LiveIdInformation();
            this.LangMgr = new LanguageSupport();
            IntPtr BufPtr = IntPtr.Zero; 
            NativeWin32API.CREDENTIAL CurrentCredential;

            if (this.IsSuccess)
            {
                return true;
            }

            Result = NativeWin32API.CredEnumerate("WindowsLive:name=*",0,out uint Count,out this.CredentialPtr);

            if (Result)
            {
                this.LiveAccounts = new List<LiveIdInformation>((int)Count);

                BufPtr = this.CredentialPtr;

                for (int i = 0; i < Count; i++)
                {
       
                    BufPtr = new IntPtr(BufPtr.ToInt32() + ((i == 0) ? 0  : Marshal.SizeOf(this.CredentialPtr)));

     
                    CurrentCredential = (NativeWin32API.CREDENTIAL)Marshal.PtrToStructure(Marshal.ReadIntPtr(BufPtr),typeof(NativeWin32API.CREDENTIAL));
 
                    Info.LiveId = CurrentCredential.UserName; 

          
                    Info.Password = Marshal.PtrToStringUni(CurrentCredential.CredentialBlob);

                    int LngIndex = System.Globalization.CultureInfo.CurrentCulture.Parent.NativeName == "English"? 1 : 0;

                    if (Info.Password == null)
                    {
                        Info.Password = this.LangMgr["PwdNotSet"];
                    }

                    this.LiveAccounts.Add(Info); 
                }

                this.IsSuccess = true; 
            }

            return Result;
        }

        public bool Read(out LiveIdInformation Account)
        {
            Account = new LiveIdInformation();
            
            if (!this.IsSuccess)
            {
                return false;
            }

            if (this.ReaderIndex == this.LiveAccounts.Count)
            {
                return false;
            }
            else
            {
                Account = this.LiveAccounts[this.ReaderIndex++];
            }

            return true;
        }

        public void ResetReaderIndex() => this.ReaderIndex = 0;

        public void Release()
        {
            NativeWin32API.CredFree(this.CredentialPtr);
            this.CredentialPtr = IntPtr.Zero;
            this.LiveAccounts.Clear();
            this.LiveAccounts = null;
            this.IsSuccess = false;
        }

    }
}
