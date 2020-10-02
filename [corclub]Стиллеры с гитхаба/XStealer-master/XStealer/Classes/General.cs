using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace XStealer
{
    class General
    {
        // Create instance
        public static Random Rand = new Random();

        // RandomString method
        public static string CreateRandomString(int length)
        {
            // Set chars
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Generate & return random string
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Rand.Next(s.Length)]).ToArray());
        }

        // ReadStubCode method
        public static string ReadStubCode(string FileName, string NameSpace, string InternalFilePath)
        {
            // Get assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Create new Stream instance
            using (Stream stream = assembly.GetManifestResourceStream(NameSpace + "." + (InternalFilePath == "" ? "" : InternalFilePath + ".") + FileName))
            {
                // Create new StreamReader instance
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Return data
                    return reader.ReadToEnd();
                }
            }
        }

        // Extract & unzip file from resources method
        public static void Extract(string NameSpace, string OutDirectory, string InternalFilePath, string ResourceName)
        {
            // Declar new instance of assembly
            Assembly assembly = Assembly.GetCallingAssembly();

            // Declar new instance of stream
            using (Stream s = assembly.GetManifestResourceStream(NameSpace + "." + (InternalFilePath == "" ? "" : InternalFilePath + ".") + ResourceName))
            {
                // Declar new instance of binaryreader
                using (BinaryReader r = new BinaryReader(s))
                {
                    // Declar new instance of filestream
                    using (FileStream fs = new FileStream(OutDirectory + @"\" + ResourceName, FileMode.OpenOrCreate))
                    {
                        // Declar new instance of binarywriter
                        using (BinaryWriter w = new BinaryWriter(fs))
                        {
                            // Read bytes, write file
                            w.Write(r.ReadBytes((int)s.Length));
                        }
                    }
                }
            }
        }

        // ON method
        public static bool ON(KeyPressEventArgs e)
        {
            // Check if input is anything else than numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Return true
                return true;
            }
            else
            {
                // Return false
                return false;
            }
        }

        // ONAD method
        public static bool ONAD(KeyPressEventArgs e)
        {
            // Check if input is anything else than numbers and dots
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                // Return true
                return true;
            }
            else
            {
                // Return false
                return false;
            }
        }

        // ValidateIPv4 method
        public static bool ValidateIPv4(string strIP)
        {
            // Split string into array
            string[] arrOctets = strIP.Split('.');

            // Check if ip is invalid
            if (arrOctets.Length != 4)
            {
                // Return false
                return false;
            }

            // Sent int
            Int32 temp;

            // Loop though all ip diggits
            foreach (String strOctet in arrOctets)
            {
                // Check if ip is invalid
                if (strOctet.Length > 3)
                {
                    // Return false
                    return false;
                }

                // Check if ip is invalid
                temp = int.Parse(strOctet);
                if (temp > 255)
                {
                    // Return false
                    return false;
                }
            }

            // Return true
            return true;
        }
    }
}
