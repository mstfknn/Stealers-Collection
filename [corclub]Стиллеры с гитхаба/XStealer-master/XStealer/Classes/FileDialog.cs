using System;
using System.Windows.Forms;

namespace XStealer
{
    class FileDialog
    {
        // Ofd method
        public static string Ofd(string Filter)
        {
            // Create new instance
            using (OpenFileDialog Ofd = new OpenFileDialog())
            {
                // Set file filter
                Ofd.Filter = Filter;

                // Check if everything is ok
                if (Ofd.ShowDialog() == DialogResult.OK)
                {
                    // Return data
                    return Ofd.FileName;
                }
                else
                {
                    // return data
                    return "";
                }
            }
        }

        // Sfd method
        public static string Sfd(string Filter)
        {
            // Create new SaveFileDialog instance
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // Set file filter
                sfd.Filter = Filter;

                // Check if everything is ok
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Return data
                    return sfd.FileName;
                }
                else
                {
                    // Return data
                    return "";
                }
            }
        }
    }
}
