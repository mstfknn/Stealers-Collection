namespace MsnPasswordFinder
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LanguageSupport LanguageMgr = new LanguageSupport();


            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstAccounts = new System.Windows.Forms.ListView();
            this.LiveId = new System.Windows.Forms.ColumnHeader();
            this.Password = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kopyalasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tümünüKopyalaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkGoTb = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAccounts
            // 
            this.lstAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LiveId,
            this.Password});
            this.lstAccounts.ContextMenuStrip = this.contextMenuStrip1;
            this.lstAccounts.FullRowSelect = true;
            this.lstAccounts.Location = new System.Drawing.Point(12, 12);
            this.lstAccounts.Name = "lstAccounts";
            this.lstAccounts.Size = new System.Drawing.Size(398, 119);
            this.lstAccounts.TabIndex = 0;
            this.lstAccounts.UseCompatibleStateImageBehavior = false;
            this.lstAccounts.View = System.Windows.Forms.View.Details;
            // 
            // LiveId
            // 
            this.LiveId.Text = LanguageMgr["LiveIdColumn"]; //"Live ID";
            this.LiveId.Width = 222;
            // 
            // Password
            // 
            this.Password.Text = LanguageMgr["PasswordColumn"]; //"Password";
            this.Password.Width = 169;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kopyalasToolStripMenuItem,
            this.tümünüKopyalaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 48);
            // 
            // kopyalasToolStripMenuItem
            // 
            this.kopyalasToolStripMenuItem.Name = "kopyalasToolStripMenuItem";
            this.kopyalasToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.kopyalasToolStripMenuItem.Text = LanguageMgr["Copy"]; //"Kopyala";
            this.kopyalasToolStripMenuItem.Click += new System.EventHandler(this.kopyalasToolStripMenuItem_Click);
            // 
            // tümünüKopyalaToolStripMenuItem
            // 
            this.tümünüKopyalaToolStripMenuItem.Name = "tümünüKopyalaToolStripMenuItem";
            this.tümünüKopyalaToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.tümünüKopyalaToolStripMenuItem.Text = LanguageMgr["CopyAll"]; //"Tümünü kopyala";
            this.tümünüKopyalaToolStripMenuItem.Click += new System.EventHandler(this.tümünüKopyalaToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(334, 137);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = LanguageMgr["Ok"]; //"Ok";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "2009 © ";
            // 
            // lnkGoTb
            // 
            this.lnkGoTb.AutoSize = true;
            this.lnkGoTb.Location = new System.Drawing.Point(55, 142);
            this.lnkGoTb.Name = "lnkGoTb";
            this.lnkGoTb.Size = new System.Drawing.Size(62, 13);
            this.lnkGoTb.TabIndex = 4;
            this.lnkGoTb.TabStop = true;
            this.lnkGoTb.Text = "Oguz Kartal";
            this.lnkGoTb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGoTb_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 168);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lnkGoTb);
            this.Controls.Add(this.lstAccounts);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = LanguageMgr["Title"]; //"Windows Live Messenger Stored Password Recover";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstAccounts;
        private System.Windows.Forms.ColumnHeader LiveId;
        private System.Windows.Forms.ColumnHeader Password;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkGoTb;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kopyalasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tümünüKopyalaToolStripMenuItem;
    }
}

