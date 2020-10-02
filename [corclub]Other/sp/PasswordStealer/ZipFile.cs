using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Anime
{
    class ZipFile
    {
        public static void ZipFilses(string ZIP_PATH, string FOLDER_TO_ZIP)
        {
                FastZip zip = new FastZip();
                zip.CreateZip(ZIP_PATH, FOLDER_TO_ZIP, true, null);
        }
    }
}