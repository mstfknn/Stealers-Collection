namespace MsnPasswordFinder
{
    partial class frmLanguageSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.cbLangs = new System.Windows.Forms.ComboBox();
            this.chkUseOsLang = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbLangs
            // 
            this.cbLangs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLangs.FormattingEnabled = true;
            this.cbLangs.Items.AddRange(new object[] {
            "Türkçe",
            "English"});
            this.cbLangs.Location = new System.Drawing.Point(12, 12);
            this.cbLangs.Name = "cbLangs";
            this.cbLangs.Size = new System.Drawing.Size(154, 21);
            this.cbLangs.TabIndex = 0;
            // 
            // chkUseOsLang
            // 
            this.chkUseOsLang.AutoSize = true;
            this.chkUseOsLang.Location = new System.Drawing.Point(12, 39);
            this.chkUseOsLang.Name = "chkUseOsLang";
            this.chkUseOsLang.Size = new System.Drawing.Size(115, 17);
            this.chkUseOsLang.TabIndex = 1;
            this.chkUseOsLang.Text = "Use system default";
            this.chkUseOsLang.UseVisualStyleBackColor = true;
            this.chkUseOsLang.CheckedChanged += new System.EventHandler(this.chkUseOsLang_CheckedChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(105, 72);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(68, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Ok";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmLanguageSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 103);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkUseOsLang);
            this.Controls.Add(this.cbLangs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLanguageSelection";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Language";
            this.Load += new System.EventHandler(this.frmLanguageSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLangs;
        private System.Windows.Forms.CheckBox chkUseOsLang;
        private System.Windows.Forms.Button btnSelect;

    }
}
