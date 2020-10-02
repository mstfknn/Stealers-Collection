using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MsnPasswordFinder
{
    partial class frmLanguageSelection : Form
    {
        public int SelectedLanguage
        {
            get;
            private set;
        }

        public bool UseDefault
        {
            get;
            private set;
        }

        public frmLanguageSelection()
        {
            this.InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.SelectedLanguage = this.cbLangs.SelectedIndex;
            this.Close();
        }

        private void frmLanguageSelection_Load(object sender, EventArgs e)
        {
            this.cbLangs.SelectedIndex = 0;
        }

        private void chkUseOsLang_CheckedChanged(object sender, EventArgs e)
        {
            this.UseDefault = this.chkUseOsLang.Checked;
        }
    }
}
