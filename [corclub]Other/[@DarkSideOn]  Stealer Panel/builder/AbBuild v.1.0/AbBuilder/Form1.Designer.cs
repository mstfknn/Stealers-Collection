namespace AbBuilder
{
	// Token: 0x02000008 RID: 8
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000ECC0 File Offset: 0x0000D0C0
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000ED10 File Offset: 0x0000D110
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.TextBox1 = new global::System.Windows.Forms.TextBox();
			this.Button1 = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Button2 = new global::System.Windows.Forms.Button();
			this.SuspendLayout();
			global::System.Windows.Forms.Control textBox = this.TextBox1;
			global::System.Drawing.Point location = new global::System.Drawing.Point(12, 12);
			textBox.Location = location;
			this.TextBox1.Name = "TextBox1";
			global::System.Windows.Forms.Control textBox2 = this.TextBox1;
			global::System.Drawing.Size size = new global::System.Drawing.Size(368, 20);
			textBox2.Size = size;
			this.TextBox1.TabIndex = 0;
			this.TextBox1.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			global::System.Windows.Forms.Control button = this.Button1;
			location = new global::System.Drawing.Point(12, 40);
			button.Location = location;
			this.Button1.Name = "Button1";
			global::System.Windows.Forms.Control button2 = this.Button1;
			size = new global::System.Drawing.Size(104, 33);
			button2.Size = size;
			this.Button1.TabIndex = 1;
			this.Button1.Text = "Build";
			this.Button1.UseVisualStyleBackColor = true;
			this.Label1.AutoSize = true;
			this.Label1.BackColor = global::System.Drawing.Color.White;
			global::System.Windows.Forms.Control label = this.Label1;
			location = new global::System.Drawing.Point(15, 15);
			label.Location = location;
			this.Label1.Name = "Label1";
			global::System.Windows.Forms.Control label2 = this.Label1;
			size = new global::System.Drawing.Size(27, 13);
			label2.Size = size;
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Url :";
			global::System.Windows.Forms.Control button3 = this.Button2;
			location = new global::System.Drawing.Point(122, 40);
			button3.Location = location;
			this.Button2.Name = "Button2";
			global::System.Windows.Forms.Control button4 = this.Button2;
			size = new global::System.Drawing.Size(104, 33);
			button4.Size = size;
			this.Button2.TabIndex = 3;
			this.Button2.Text = "About";
			this.Button2.UseVisualStyleBackColor = true;
			global::System.Drawing.SizeF autoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			this.AutoScaleDimensions = autoScaleDimensions;
			this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			size = new global::System.Drawing.Size(392, 85);
			this.ClientSize = size;
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.TextBox1);
			this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ab Builder v.1.0";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		// Token: 0x0400000B RID: 11
		private global::System.ComponentModel.IContainer components;
	}
}
