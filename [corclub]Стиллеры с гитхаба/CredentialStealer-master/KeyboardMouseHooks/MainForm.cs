using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyboardMouseHooks
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Class declarations
        /// </summary>
        RamGecTools.MouseHook mouseHook = new RamGecTools.MouseHook();
        RamGecTools.KeyboardHook keyboardHook = new RamGecTools.KeyboardHook();


        private void mouseHookBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseHookBox.Checked)
            {
                mouseGroup.Enabled = true;

                // register events
                mouseHook.MouseMove += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_MouseMove);
                mouseHook.LeftButtonDown += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_LeftButtonDown);
                mouseHook.LeftButtonUp += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_LeftButtonUp);
                mouseHook.RightButtonDown += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_RightButtonDown);
                mouseHook.RightButtonUp += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_RightButtonUp);
                mouseHook.MiddleButtonDown += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_MiddleButtonDown);
                mouseHook.MiddleButtonUp += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_MiddleButtonUp);
                mouseHook.MouseWheel += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_MouseWheel);

                mouseHook.Install();
            }
            else
            {
                mouseGroup.Enabled = false;
                mouseHook.Uninstall();
            }
        }

        void mouseHook_MouseWheel(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] MouseWheel Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_MiddleButtonUp(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseMiddleButton.BackColor = Color.White;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] MiddleButtonUp Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_MiddleButtonDown(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseMiddleButton.BackColor = Color.IndianRed;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] MiddleButtonDown Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_RightButtonUp(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseRightButton.BackColor = Color.White;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] RightButtonUp Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_RightButtonDown(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseRightButton.BackColor = Color.IndianRed;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] RightButtonDown Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_LeftButtonUp(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseLeftButton.BackColor = Color.White;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] LeftButtonUp Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_LeftButtonDown(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            mouseLeftButton.BackColor = Color.IndianRed;
            mouseLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] LeftButtonDown Event" + Environment.NewLine + mouseLog.Text;
        }

        void mouseHook_MouseMove(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            xPosLabel.Text = mouseStruct.pt.x.ToString();
            yPosLabel.Text = mouseStruct.pt.y.ToString();
        }

        private void keyboardHookBox_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardHookBox.Checked)
            {
                keyboardGroup.Enabled = true;

                // register evens
                keyboardHook.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
                keyboardHook.KeyUp += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);

                keyboardHook.Install();
            }
            else
            {
                keyboardGroup.Enabled = false;
                keyboardHook.Uninstall();
            }
        }

        void keyboardHook_KeyUp(RamGecTools.KeyboardHook.VKeys key)
        {   
            keyboardKeyPress.BackColor = Color.White;
            keyboardLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] KeyUp Event {" + key.ToString() + "}" + Environment.NewLine + keyboardLog.Text;
        }

        void keyboardHook_KeyDown(RamGecTools.KeyboardHook.VKeys key)
        {
            
            keyboardKeyPress.BackColor = Color.IndianRed;
            keyboardLog.Text = "[" + DateTime.Now.ToLongTimeString() + "] KeyDown Event {" + key.ToString() + "}" + Environment.NewLine + keyboardLog.Text;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // there's no harm to call Uninstall method repeatedly even if hooks aren't installed
            mouseHook.Uninstall();
            keyboardHook.Uninstall();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
