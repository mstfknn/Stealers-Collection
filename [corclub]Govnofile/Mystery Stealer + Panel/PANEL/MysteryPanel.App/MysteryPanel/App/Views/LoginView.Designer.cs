namespace MysteryPanel.App.Views
{
	// Token: 0x02000035 RID: 53
	public partial class LoginView : global::MetroSet_UI.Forms.MetroSetForm, global::ReactiveUI.IViewFor<global::MysteryPanel.ViewModels.AuthViewModel>, global::ReactiveUI.IViewFor, global::ReactiveUI.IActivatable
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000035E6 File Offset: 0x000017E6
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000084F4 File Offset: 0x000066F4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MysteryPanel.App.Views.LoginView));
			this.loginControlBox = new global::MetroSet_UI.Controls.MetroSetControlBox();
			this.loginStyle = new global::MetroSet_UI.StyleManager();
			this.loginLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.passwordLbl = new global::MetroSet_UI.Controls.MetroSetLabel();
			this.loginTb = new global::System.Windows.Forms.TextBox();
			this.passwordTb = new global::System.Windows.Forms.TextBox();
			this.signInBtn = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.loginControlBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.loginControlBox.CloseHoverBackColor = global::System.Drawing.Color.FromArgb(183, 40, 40);
			this.loginControlBox.CloseHoverForeColor = global::System.Drawing.Color.White;
			this.loginControlBox.CloseNormalForeColor = global::System.Drawing.Color.Gray;
			this.loginControlBox.CloseParent = true;
			this.loginControlBox.DisabledForeColor = global::System.Drawing.Color.Silver;
			this.loginControlBox.Location = new global::System.Drawing.Point(459, 8);
			this.loginControlBox.MaximizeBox = false;
			this.loginControlBox.MaximizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.loginControlBox.MaximizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.loginControlBox.MaximizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.loginControlBox.MinimizeBox = false;
			this.loginControlBox.MinimizeHoverBackColor = global::System.Drawing.Color.FromArgb(238, 238, 238);
			this.loginControlBox.MinimizeHoverForeColor = global::System.Drawing.Color.Gray;
			this.loginControlBox.MinimizeNormalForeColor = global::System.Drawing.Color.Gray;
			this.loginControlBox.Name = "loginControlBox";
			this.loginControlBox.Size = new global::System.Drawing.Size(100, 25);
			this.loginControlBox.Style = global::MetroSet_UI.Design.Style.Dark;
			this.loginControlBox.StyleManager = this.loginStyle;
			this.loginControlBox.TabIndex = 0;
			this.loginControlBox.ThemeAuthor = "Narwin";
			this.loginControlBox.ThemeName = "MetroDark";
			this.loginStyle.MetroForm = this;
			this.loginStyle.Style = global::MetroSet_UI.Design.Style.Dark;
			this.loginStyle.ThemeAuthor = "Narwin";
			this.loginStyle.ThemeName = "MetroDark";
			this.loginLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.loginLbl.Location = new global::System.Drawing.Point(47, 92);
			this.loginLbl.Name = "loginLbl";
			this.loginLbl.Size = new global::System.Drawing.Size(100, 23);
			this.loginLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.loginLbl.StyleManager = this.loginStyle;
			this.loginLbl.TabIndex = 4;
			this.loginLbl.Text = "Login:";
			this.loginLbl.ThemeAuthor = "Narwin";
			this.loginLbl.ThemeName = "MetroDark";
			this.passwordLbl.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.passwordLbl.Location = new global::System.Drawing.Point(47, 148);
			this.passwordLbl.Name = "passwordLbl";
			this.passwordLbl.Size = new global::System.Drawing.Size(100, 23);
			this.passwordLbl.Style = global::MetroSet_UI.Design.Style.Dark;
			this.passwordLbl.StyleManager = this.loginStyle;
			this.passwordLbl.TabIndex = 5;
			this.passwordLbl.Text = "Password:";
			this.passwordLbl.ThemeAuthor = "Narwin";
			this.passwordLbl.ThemeName = "MetroDark";
			this.loginTb.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.loginTb.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.loginTb.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.loginTb.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.loginTb.Location = new global::System.Drawing.Point(47, 115);
			this.loginTb.Name = "loginTb";
			this.loginTb.Size = new global::System.Drawing.Size(479, 22);
			this.loginTb.TabIndex = 6;
			this.loginTb.Text = "Admin";
			this.loginTb.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.passwordTb.BackColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			this.passwordTb.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.passwordTb.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 204);
			this.passwordTb.ForeColor = global::System.Drawing.Color.FromArgb(170, 170, 170);
			this.passwordTb.Location = new global::System.Drawing.Point(47, 172);
			this.passwordTb.Name = "passwordTb";
			this.passwordTb.Size = new global::System.Drawing.Size(479, 22);
			this.passwordTb.TabIndex = 7;
			this.passwordTb.Text = "1234567890";
			this.passwordTb.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.passwordTb.UseSystemPasswordChar = true;
			this.signInBtn.BackColor = global::System.Drawing.Color.FromArgb(35, 147, 195);
			this.signInBtn.FlatAppearance.BorderSize = 0;
			this.signInBtn.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.signInBtn.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f);
			this.signInBtn.Location = new global::System.Drawing.Point(245, 200);
			this.signInBtn.Name = "signInBtn";
			this.signInBtn.Size = new global::System.Drawing.Size(79, 26);
			this.signInBtn.TabIndex = 8;
			this.signInBtn.Text = "Sign in";
			this.signInBtn.UseVisualStyleBackColor = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.BackgroundColor = global::System.Drawing.Color.FromArgb(30, 30, 30);
			base.ClientSize = new global::System.Drawing.Size(568, 241);
			base.Controls.Add(this.signInBtn);
			base.Controls.Add(this.passwordTb);
			base.Controls.Add(this.loginTb);
			base.Controls.Add(this.passwordLbl);
			base.Controls.Add(this.loginLbl);
			base.Controls.Add(this.loginControlBox);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "LoginView";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Style = global::MetroSet_UI.Design.Style.Dark;
			base.StyleManager = this.loginStyle;
			this.Text = "Authorization";
			base.TextColor = global::System.Drawing.Color.White;
			base.ThemeName = "MetroDark";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000EF RID: 239
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000F0 RID: 240
		private global::MetroSet_UI.Controls.MetroSetControlBox loginControlBox;

		// Token: 0x040000F1 RID: 241
		private global::MetroSet_UI.Controls.MetroSetLabel loginLbl;

		// Token: 0x040000F2 RID: 242
		private global::MetroSet_UI.Controls.MetroSetLabel passwordLbl;

		// Token: 0x040000F3 RID: 243
		private global::System.Windows.Forms.TextBox loginTb;

		// Token: 0x040000F4 RID: 244
		private global::System.Windows.Forms.TextBox passwordTb;

		// Token: 0x040000F5 RID: 245
		private global::System.Windows.Forms.Button signInBtn;

		// Token: 0x040000F6 RID: 246
		private global::MetroSet_UI.StyleManager loginStyle;
	}
}
