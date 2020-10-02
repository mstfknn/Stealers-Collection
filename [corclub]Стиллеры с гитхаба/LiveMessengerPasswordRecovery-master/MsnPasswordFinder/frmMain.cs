using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MsnPasswordFinder
{
    public partial class frmMain : Form
    {
        LanguageSupport LangMgr = new LanguageSupport();
        LiveMessegerPasswordFinder MsnAccountFinder = null;

        public frmMain()
        {
            var LangDlg = new frmLanguageSelection();
            LangDlg.ShowDialog();
            LanguageSupport.Language = LangDlg.UseDefault ? LanguageSupport.GetNativeLanguage() : LangDlg.SelectedLanguage == 0 ? "Turkish" : "English";
            this.InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.MsnAccountFinder = new LiveMessegerPasswordFinder();

            if (this.MsnAccountFinder.GetAccountInformations())
            {
                while (this.MsnAccountFinder.Read(out LiveIdInformation Account))
                {
                    this.lstAccounts.Items.Add(new ListViewItem(new string[] { Account.LiveId, Account.Password }));
                }

                this.MsnAccountFinder.Release();
            }
            else
            {
                MessageBox.Show(this.LangMgr["Error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkGoTb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.oguzkartal.net");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kopyalasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this.LangMgr["SelectItemWarn"], "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem Selected = this.lstAccounts.SelectedItems[0];
            string SelectedData = string.Format("{0} = {1}", Selected.SubItems[0].Text, Selected.SubItems[1].Text);
            Clipboard.SetText(SelectedData);
        }

        private void tümünüKopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem Selected = null;
            string SelectedData = "";

            for (int i = 0; i < this.lstAccounts.Items.Count; i++)
            {
                Selected = this.lstAccounts.Items[i];
                SelectedData += string.Format("{0} = {1}\r\n", Selected.SubItems[0].Text, Selected.SubItems[1].Text);
            }

            Clipboard.SetText(SelectedData);
        }
    }
}
