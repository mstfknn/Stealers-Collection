// Token: 0x0200001C RID: 28
[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
public partial class Form1 : global::System.Windows.Forms.Form
{
	// Token: 0x0600006A RID: 106 RVA: 0x00004384 File Offset: 0x00002584
	[global::System.Diagnostics.DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000043C4 File Offset: 0x000025C4
	[global::System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.TextBox1 = new global::System.Windows.Forms.TextBox();
		this.SuspendLayout();
		global::System.Windows.Forms.Control textBox = this.TextBox1;
		global::System.Drawing.Point location = new global::System.Drawing.Point(-1, 0);
		textBox.Location = location;
		this.TextBox1.Multiline = true;
		this.TextBox1.Name = "TextBox1";
		this.TextBox1.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
		global::System.Windows.Forms.Control textBox2 = this.TextBox1;
		global::System.Drawing.Size size = new global::System.Drawing.Size(10, 10);
		textBox2.Size = size;
		this.TextBox1.TabIndex = 0;
		global::System.Drawing.SizeF autoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = autoScaleDimensions;
		this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
		size = new global::System.Drawing.Size(10, 10);
		this.ClientSize = size;
		this.Controls.Add(this.TextBox1);
		this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
		this.Name = "Form1";
		this.Opacity = 0.0;
		this.ShowIcon = false;
		this.ShowInTaskbar = false;
		this.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Form1";
		this.WindowState = global::System.Windows.Forms.FormWindowState.Minimized;
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	// Token: 0x04000034 RID: 52
	private global::System.ComponentModel.IContainer icontainer_0;
}
