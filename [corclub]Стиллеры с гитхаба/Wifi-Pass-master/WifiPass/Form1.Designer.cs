namespace WifiPass
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1_pass = new System.Windows.Forms.TextBox();
            this.button_getPasswords = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2_exportCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1_pass
            // 
            this.textBox1_pass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1_pass.Location = new System.Drawing.Point(18, 308);
            this.textBox1_pass.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1_pass.Multiline = true;
            this.textBox1_pass.Name = "textBox1_pass";
            this.textBox1_pass.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1_pass.Size = new System.Drawing.Size(823, 162);
            this.textBox1_pass.TabIndex = 0;
            this.textBox1_pass.Text = "Green = Open Network\r\nYellow = Insecure Wifi (Password less than 8 chars)\r\n";
            // 
            // button_getPasswords
            // 
            this.button_getPasswords.Location = new System.Drawing.Point(18, 18);
            this.button_getPasswords.Margin = new System.Windows.Forms.Padding(4);
            this.button_getPasswords.Name = "button_getPasswords";
            this.button_getPasswords.Size = new System.Drawing.Size(246, 34);
            this.button_getPasswords.TabIndex = 1;
            this.button_getPasswords.Text = "Get Wifi Passwords";
            this.button_getPasswords.UseVisualStyleBackColor = true;
            this.button_getPasswords.Click += new System.EventHandler(this.button_getPasswords_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 60);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(825, 240);
            this.dataGridView1.TabIndex = 2;
            // 
            // button2_exportCSV
            // 
            this.button2_exportCSV.Location = new System.Drawing.Point(272, 18);
            this.button2_exportCSV.Margin = new System.Windows.Forms.Padding(4);
            this.button2_exportCSV.Name = "button2_exportCSV";
            this.button2_exportCSV.Size = new System.Drawing.Size(246, 34);
            this.button2_exportCSV.TabIndex = 3;
            this.button2_exportCSV.Text = "Export as CSV";
            this.button2_exportCSV.UseVisualStyleBackColor = true;
            this.button2_exportCSV.Click += new System.EventHandler(this.button2_exportCSV_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 490);
            this.Controls.Add(this.button2_exportCSV);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_getPasswords);
            this.Controls.Add(this.textBox1_pass);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WifiPass";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1_pass;
        private System.Windows.Forms.Button button_getPasswords;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2_exportCSV;
    }
}

