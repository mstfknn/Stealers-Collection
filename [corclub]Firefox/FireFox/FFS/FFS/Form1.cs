using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace FFS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            var general = new List<string[]>();
            FFDecrypter.DecryptFirefox(general);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
