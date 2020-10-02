namespace KeyboardMouseHooks
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.mouseGroup = new System.Windows.Forms.GroupBox();
            this.mouseMiddleButton = new System.Windows.Forms.Button();
            this.mouseRightButton = new System.Windows.Forms.Button();
            this.mouseLeftButton = new System.Windows.Forms.Button();
            this.mouseLog = new System.Windows.Forms.TextBox();
            this.yPosLabel = new System.Windows.Forms.Label();
            this.xPosLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.keyboardGroup = new System.Windows.Forms.GroupBox();
            this.keyboardLog = new System.Windows.Forms.TextBox();
            this.keyboardKeyPress = new System.Windows.Forms.Button();
            this.mouseHookBox = new System.Windows.Forms.CheckBox();
            this.keyboardHookBox = new System.Windows.Forms.CheckBox();
            this.aboutRichBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mouseGroup.SuspendLayout();
            this.keyboardGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mouseGroup
            // 
            this.mouseGroup.Controls.Add(this.mouseMiddleButton);
            this.mouseGroup.Controls.Add(this.mouseRightButton);
            this.mouseGroup.Controls.Add(this.mouseLeftButton);
            this.mouseGroup.Controls.Add(this.mouseLog);
            this.mouseGroup.Controls.Add(this.yPosLabel);
            this.mouseGroup.Controls.Add(this.xPosLabel);
            this.mouseGroup.Controls.Add(this.label2);
            this.mouseGroup.Controls.Add(this.label1);
            this.mouseGroup.Enabled = false;
            this.mouseGroup.Location = new System.Drawing.Point(12, 35);
            this.mouseGroup.Name = "mouseGroup";
            this.mouseGroup.Size = new System.Drawing.Size(396, 129);
            this.mouseGroup.TabIndex = 0;
            this.mouseGroup.TabStop = false;
            this.mouseGroup.Text = "Mouse Hook";
            // 
            // mouseMiddleButton
            // 
            this.mouseMiddleButton.BackColor = System.Drawing.Color.White;
            this.mouseMiddleButton.Enabled = false;
            this.mouseMiddleButton.Location = new System.Drawing.Point(33, 97);
            this.mouseMiddleButton.Name = "mouseMiddleButton";
            this.mouseMiddleButton.Size = new System.Drawing.Size(49, 23);
            this.mouseMiddleButton.TabIndex = 7;
            this.mouseMiddleButton.Text = "Middle";
            this.mouseMiddleButton.UseVisualStyleBackColor = false;
            // 
            // mouseRightButton
            // 
            this.mouseRightButton.BackColor = System.Drawing.Color.White;
            this.mouseRightButton.Enabled = false;
            this.mouseRightButton.Location = new System.Drawing.Point(64, 68);
            this.mouseRightButton.Name = "mouseRightButton";
            this.mouseRightButton.Size = new System.Drawing.Size(49, 23);
            this.mouseRightButton.TabIndex = 6;
            this.mouseRightButton.Text = "Right";
            this.mouseRightButton.UseVisualStyleBackColor = false;
            // 
            // mouseLeftButton
            // 
            this.mouseLeftButton.BackColor = System.Drawing.Color.White;
            this.mouseLeftButton.Enabled = false;
            this.mouseLeftButton.Location = new System.Drawing.Point(9, 68);
            this.mouseLeftButton.Name = "mouseLeftButton";
            this.mouseLeftButton.Size = new System.Drawing.Size(49, 23);
            this.mouseLeftButton.TabIndex = 5;
            this.mouseLeftButton.Text = "Left";
            this.mouseLeftButton.UseVisualStyleBackColor = false;
            // 
            // mouseLog
            // 
            this.mouseLog.Location = new System.Drawing.Point(136, 16);
            this.mouseLog.Multiline = true;
            this.mouseLog.Name = "mouseLog";
            this.mouseLog.ReadOnly = true;
            this.mouseLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mouseLog.Size = new System.Drawing.Size(254, 107);
            this.mouseLog.TabIndex = 4;
            // 
            // yPosLabel
            // 
            this.yPosLabel.AutoSize = true;
            this.yPosLabel.Location = new System.Drawing.Point(69, 38);
            this.yPosLabel.Name = "yPosLabel";
            this.yPosLabel.Size = new System.Drawing.Size(13, 13);
            this.yPosLabel.TabIndex = 3;
            this.yPosLabel.Text = "0";
            // 
            // xPosLabel
            // 
            this.xPosLabel.AutoSize = true;
            this.xPosLabel.Location = new System.Drawing.Point(69, 16);
            this.xPosLabel.Name = "xPosLabel";
            this.xPosLabel.Size = new System.Drawing.Size(13, 13);
            this.xPosLabel.TabIndex = 2;
            this.xPosLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Position Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Position X:";
            // 
            // keyboardGroup
            // 
            this.keyboardGroup.Controls.Add(this.keyboardLog);
            this.keyboardGroup.Controls.Add(this.keyboardKeyPress);
            this.keyboardGroup.Location = new System.Drawing.Point(12, 193);
            this.keyboardGroup.Name = "keyboardGroup";
            this.keyboardGroup.Size = new System.Drawing.Size(396, 129);
            this.keyboardGroup.TabIndex = 1;
            this.keyboardGroup.TabStop = false;
            this.keyboardGroup.Text = "Keyboard Hook";
            // 
            // keyboardLog
            // 
            this.keyboardLog.Location = new System.Drawing.Point(136, 16);
            this.keyboardLog.Multiline = true;
            this.keyboardLog.Name = "keyboardLog";
            this.keyboardLog.ReadOnly = true;
            this.keyboardLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keyboardLog.Size = new System.Drawing.Size(254, 107);
            this.keyboardLog.TabIndex = 7;
            // 
            // keyboardKeyPress
            // 
            this.keyboardKeyPress.BackColor = System.Drawing.Color.White;
            this.keyboardKeyPress.Enabled = false;
            this.keyboardKeyPress.Location = new System.Drawing.Point(9, 19);
            this.keyboardKeyPress.Name = "keyboardKeyPress";
            this.keyboardKeyPress.Size = new System.Drawing.Size(104, 43);
            this.keyboardKeyPress.TabIndex = 6;
            this.keyboardKeyPress.Text = "Key Press";
            this.keyboardKeyPress.UseVisualStyleBackColor = false;
            // 
            // mouseHookBox
            // 
            this.mouseHookBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mouseHookBox.Location = new System.Drawing.Point(12, 12);
            this.mouseHookBox.Name = "mouseHookBox";
            this.mouseHookBox.Size = new System.Drawing.Size(390, 17);
            this.mouseHookBox.TabIndex = 2;
            this.mouseHookBox.Text = "Install Mouse Hook";
            this.mouseHookBox.UseVisualStyleBackColor = true;
            this.mouseHookBox.CheckedChanged += new System.EventHandler(this.mouseHookBox_CheckedChanged);
            // 
            // keyboardHookBox
            // 
            this.keyboardHookBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.keyboardHookBox.Location = new System.Drawing.Point(12, 170);
            this.keyboardHookBox.Name = "keyboardHookBox";
            this.keyboardHookBox.Size = new System.Drawing.Size(390, 17);
            this.keyboardHookBox.TabIndex = 3;
            this.keyboardHookBox.Text = "Install Keyboard Hook";
            this.keyboardHookBox.UseVisualStyleBackColor = true;
            this.keyboardHookBox.CheckedChanged += new System.EventHandler(this.keyboardHookBox_CheckedChanged);
            // 
            // aboutRichBox
            // 
            this.aboutRichBox.BackColor = System.Drawing.Color.Beige;
            this.aboutRichBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aboutRichBox.Location = new System.Drawing.Point(12, 335);
            this.aboutRichBox.Name = "aboutRichBox";
            this.aboutRichBox.ReadOnly = true;
            this.aboutRichBox.Size = new System.Drawing.Size(396, 90);
            this.aboutRichBox.TabIndex = 4;
            this.aboutRichBox.TabStop = false;
            this.aboutRichBox.Text = resources.GetString("aboutRichBox.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 463);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.aboutRichBox);
            this.Controls.Add(this.keyboardHookBox);
            this.Controls.Add(this.mouseHookBox);
            this.Controls.Add(this.keyboardGroup);
            this.Controls.Add(this.mouseGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.Text = "Windows Hook Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.mouseGroup.ResumeLayout(false);
            this.mouseGroup.PerformLayout();
            this.keyboardGroup.ResumeLayout(false);
            this.keyboardGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mouseGroup;
        private System.Windows.Forms.Label yPosLabel;
        private System.Windows.Forms.Label xPosLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox keyboardGroup;
        private System.Windows.Forms.CheckBox mouseHookBox;
        private System.Windows.Forms.CheckBox keyboardHookBox;
        private System.Windows.Forms.Button mouseMiddleButton;
        private System.Windows.Forms.Button mouseRightButton;
        private System.Windows.Forms.Button mouseLeftButton;
        private System.Windows.Forms.TextBox mouseLog;
        private System.Windows.Forms.TextBox keyboardLog;
        private System.Windows.Forms.Button keyboardKeyPress;
        private System.Windows.Forms.RichTextBox aboutRichBox;
        private System.Windows.Forms.Button button1;
    }
}

