#region

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace KoiVM.Confuser
{
    internal partial class LoginPrompt : Form
    {
        public LoginPrompt()
        {
            this.InitializeComponent();
            this.txtId.Text = KoiInfo.settings.KoiID ?? "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = false;
            this.txtId.Enabled = false;
            KoiInfo.settings.KoiID = this.txtId.Text;
            KoiInfo.settings.Save();

            var sys = new KoiSystem();
            string ver = sys.GetVersion(KoiInfo.settings.KoiID);
            sys.Progress += value => this.BeginInvoke(new Action(() =>
            {
                if (value != 0)
                {
                    this.progress.Style = ProgressBarStyle.Continuous;
                    this.progress.Value = (int)(value * 1000);
                }
                else
                {
                    this.progress.Value = 0;
                    this.progress.Style = ProgressBarStyle.Marquee;
                }
            }));
            sys.Finish += success =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.btnLogin.Enabled = true;
                    this.txtId.Enabled = true;
                    if (!success)
                    {
                        KoiInfo.settings.Version = "";
                        KoiInfo.settings.Save();
                        MessageBox.Show("Login failed.", "Koi System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        KoiInfo.settings.Version = ver;
                        KoiInfo.settings.Save();
                        this.DialogResult = DialogResult.OK;
                    }
                }));
            };
            sys.Login(this.txtId.Text);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BringToFront();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!this.btnLogin.Enabled)
                e.Cancel = true;
            base.OnClosing(e);
        }
    }
}