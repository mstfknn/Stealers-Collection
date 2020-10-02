using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace RdpPwd
{
    public partial class Form1 : Form
    {
        static byte[] s_aditionalEntropy = null;         //附加的加密因子，自定义

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] secret = Encoding.Unicode.GetBytes(textBox1.Text);
                byte[] encryptedSecret = ProtectedData.Protect(secret, s_aditionalEntropy, DataProtectionScope.LocalMachine);
                StringBuilder res = new StringBuilder();
                foreach (byte b in encryptedSecret)
                {
                    res.Append(b.ToString("X2"));
                }

                textBox2.Text = res.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<byte> encryptedSecret = new List<byte>();
                for (int i = 0; i < textBox2.Text.Length / 2; i++)
                {
                    encryptedSecret.Add(Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16));
                }

                byte[] originalData = ProtectedData.Unprotect(encryptedSecret.ToArray(), s_aditionalEntropy, DataProtectionScope.LocalMachine);
                string str = Encoding.Unicode.GetString(originalData);
                textBox1.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
