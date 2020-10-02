#region

using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using KoiVM.Confuser.Internal;

#endregion

namespace KoiVM.Confuser
{
    [Obfuscation(Exclude = false, Feature = "+ref proxy")]
    internal partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            this.InitializeComponent();
            this.timer.Start();

            this.txtId.Text = KoiInfo.settings.KoiID;

            this.cbUI.Checked = KoiInfo.settings.NoUI;
            this.cbUpd.Checked = KoiInfo.settings.NoCheck;

            this.verCurrent.Text = KoiInfo.settings.Version ?? "<< None >>";
        }

        private void txtChanged(object sender, EventArgs e) => KoiInfo.settings.KoiID = this.txtId.Text;

        private void cbChanged(object sender, EventArgs e)
        {
            if(sender == this.cbUI)
                KoiInfo.settings.NoUI = this.cbUI.Checked;
            if(sender == this.cbUpd)
                KoiInfo.settings.NoCheck = this.cbUpd.Checked;
        }

        private void TimerSave(object sender, EventArgs e) => KoiInfo.settings.Save();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.timer.Stop();
            KoiInfo.settings.Save();
        }

        protected override void OnShown(EventArgs e)
        {
            this.CheckVersion();
            base.OnShown(e);
        }

        private void CheckVersion()
        {
            this.verServer.Text = "<< Loading... >>";
            this.btnRefr.Enabled = this.btnDl.Enabled = false;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var sys = new KoiSystem();
                string ver;
                if (!string.IsNullOrEmpty(KoiInfo.settings.KoiID))
                {
                    ver = sys.GetVersion(KoiInfo.settings.KoiID);
                    ver = ver ?? "<< Fail to retrieve version >>";
                }
                else
                {
                    ver = "<< Enter your Koi ID >>";
                }
                this.BeginInvoke(new Action(() =>
                {
                    this.verServer.Text = ver;
                    this.btnRefr.Enabled = this.btnDl.Enabled = true;
                }));
            });
        }

        private void btnRefr_Click(object sender, EventArgs e) => this.CheckVersion();

        private void btnDl_Click(object sender, EventArgs e)
        {
            this.txtId.Enabled = this.btnRefr.Enabled = this.btnDl.Enabled = false;

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
                    this.txtId.Enabled = this.btnRefr.Enabled = this.btnDl.Enabled = true;
                    if(success)
                    {
                        this.verCurrent.Text = KoiInfo.settings.Version = ver;
                        MessageBox.Show("Download finished.", "Koi System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.verCurrent.Text = "<< None >>";
                        KoiInfo.settings.Version = "";
                        MessageBox.Show("Login failed.", "Koi System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            };
            sys.Login(this.txtId.Text);
        }

        private void btnDecoder_Click(object sender, EventArgs e)
        {
            try
            {
                if(Assembly.GetExecutingAssembly().ManifestModule.GetType("KoiVM.Confuser.Internal.Fish") == null)
                    KoiInfo.InitKoi(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Update to newest version first!\r\n" + ex, "Koi System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.ShowDecoder();
        }

        private void ShowDecoder() => new DbgDecoder().ShowDialog();

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}