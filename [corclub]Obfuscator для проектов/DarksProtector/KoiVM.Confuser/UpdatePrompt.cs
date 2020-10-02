#region

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace KoiVM.Confuser
{
    internal partial class UpdatePrompt : Form
    {
        private bool failed;
        private readonly string newVersion;

        public UpdatePrompt(string newVersion)
        {
            this.InitializeComponent();

            this.newVersion = newVersion;
            this.verCurrent.Text = KoiInfo.settings.Version ?? "<< None >>";
            this.verServer.Text = newVersion;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Enabled = false;

            var sys = new KoiSystem();
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
                    this.btnUpdate.Enabled = true;

                    if (!success)
                    {
                        this.failed = true;
                        KoiInfo.settings.Version = "";
                        KoiInfo.settings.Save();
                        MessageBox.Show("Login failed.", "Koi System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        KoiInfo.settings.Version = this.newVersion;
                        KoiInfo.settings.Save();
                        this.DialogResult = DialogResult.OK;
                    }
                }));
            };
            sys.Login(KoiInfo.settings.KoiID);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BringToFront();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!this.btnUpdate.Enabled)
                e.Cancel = true;
            if(!this.failed)
                this.DialogResult = DialogResult.OK;
            base.OnClosing(e);
        }

        private void UpdatePrompt_Load(object sender, EventArgs e)
        {

        }
    }
}