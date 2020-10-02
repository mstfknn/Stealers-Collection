using Confuser.Core;
using Confuser.Core.Project;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Rule = Confuser.Core.Project.Rule;

namespace DarksProtector
{
    public partial class DarksProtectorForm : Form, ILogger
    {
        private readonly DateTime begin;
        private string charset;
        private string rename;

        public DarksProtectorForm()
        {
            this.InitializeComponent();
            this.Text = "DarksProtector v" + ConfuserEngine.Version;
            this.lentghtext.Text = "Length: " + this.lentgh.Value;
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void DarksProtectorForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Message msg = Message.Create(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Message msg = Message.Create(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            base.WndProc(ref msg);
        }
        private void thirteenButton1_Click(object sender, EventArgs e) => Environment.Exit(0);

        private void thirteenButton2_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void thirteenTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (array != null)
                {
                    string path = array.GetValue(0).ToString();
                    int num = path.LastIndexOf(".");
                    if (num != -1)
                    {
                        string extension = path.Substring(num).ToLower();
                        if (extension == ".exe" || extension == ".dll")
                        {
                            this.Activate();
                            this.thirteenTextBox1.Text = path;
                        }
                    }
                }
            }
            catch { }
        }

        private void thirteenButton6_Click(object sender, EventArgs e)
        {

            if (this.thirteenTextBox1.Text == "")
            {
                MessageBox.Show("You need to provide a file !");
            }
            else
            {

                ConfuserProject proj = new ConfuserProject();
                proj.BaseDirectory = Path.GetDirectoryName(this.thirteenTextBox1.Text);
                proj.OutputDirectory = Path.Combine(Path.GetDirectoryName(this.thirteenTextBox1.Text) + @"\Protected");


                ProjectModule module = new ProjectModule();
                module.Path = Path.GetFileName(this.thirteenTextBox1.Text);
                proj.Add(module);
                Rule rule = new Rule("true", ProtectionPreset.None, false);
                if (this.groupBox1.Enabled == true)
                {
                    if (this.antiTamper.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDebug.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti debug", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDump.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti dump", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiILDasm.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti ildasm", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.calli.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Calli Protection", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.constants.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("constants", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.controlFlow.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ctrl flow", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.invalidMetadat.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("invalid metadata", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.renamer.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.refProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.cleanRefProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.moduleFlood.Checked)
                    {
                        SettingItem<Protection> moduleflood = new SettingItem<Protection>("module flood", SettingItemAction.Add);
                        rule.Add(moduleflood);
                    }

                    if (this.fakeNative.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Fake Native", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    SettingItem<Protection> rename1 = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                    rule.Add(rename1);

                    if (this.mutateConst.Checked == true)
                    {
                        SettingItem<Protection> mutateconst = new SettingItem<Protection>("Mutate Constants", SettingItemAction.Add);
                        rule.Add(mutateconst);
                    }

                    if (this.mutations.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Mutations", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.localtofield.Checked)
                    {
                        SettingItem<Protection> field2field = new SettingItem<Protection>("lcltofield", SettingItemAction.Add);
                        rule.Add(field2field);
                    }

                    if (this.hideMethods.Checked)
                    {
                        SettingItem<Protection> entrypoint = new SettingItem<Protection>("Hide Methods", SettingItemAction.Add);
                        rule.Add(entrypoint);
                    }

                    if (this.md5Checksum.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.disConst.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Const disint", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.junk.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Junk", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.stackUn.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("stack underflow", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.virtualization.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                    }

                    if (this.antidefordot.Checked)
                    {
                        SettingItem<Protection> headers = new SettingItem<Protection>("anti de4dot", SettingItemAction.Add);
                        rule.Add(headers);
                    }

                }
                else
                {
                    if (this.virt.Checked)
                    {
                        string extension = Path.GetExtension(this.thirteenTextBox1.Text);
                        if (extension == ".dll")
                        {
                            proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                            SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                            rule.Add(virtualization);
                            SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                            rule.Add(md5);
                        }
                        else
                        {
                            proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                            SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                            rule.Add(virtualization);
                            SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                            rule.Add(rename);
                            SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                            rule.Add(md5);
                            SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                            rule.Add(modulere);
                            SettingItem<Protection> debug = new SettingItem<Protection>("anti debug", SettingItemAction.Add);
                            rule.Add(debug);
                        }
                    }
                    else if (this.virtStrong.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                        SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(rename);
                        SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(md5);
                        SettingItem<Protection> refproxy = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(refproxy);
                        SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                        rule.Add(modulere);
                        SettingItem<Protection> antitamper = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(antitamper);
                    }
                }

                proj.Rules.Add(rule);

                XmlDocument tempdarpr = proj.Save();
                tempdarpr.Save("temp.darkpr");


                Process.Start("Confuser.CLI.exe", "temp.darkpr").WaitForExit();
                File.Delete("temp.darkpr");

            }
        }

        private void thirteenButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog k = new OpenFileDialog();
            DialogResult result = k.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = k.FileName;
                this.thirteenTextBox1.Text = file;
            }
        }

        private void thirteenTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/wHBexMW");
        }

        #region ConfuserEx Shit

        public static void Input(string text)
        {
            Colorful.Console.Write(string.Format("                         [{0}] ", DateTime.Now), Color.DarkRed);
            Colorful.Console.Write(text + "\n", Color.White);
        }

        public void Log(string msg) => Input(msg);

        public void LogFormat(string format, params object[] args) => Input(string.Format(format, args));

        public void Error(string msg) => Input(msg);

        public void ErrorException(string msg, Exception ex)
        {
            Input("ERROR: " + msg);
            Input("Exception: " + ex);
        }

        public void ErrorFormat(string format, params object[] args) => Input("ERROR: " + string.Format(format, args));

        public void Finish(bool successful)
        {
            DateTime now = DateTime.Now;
            string timeString = string.Format(
                "at {0}, {1}:{2:d2} elapsed.",
                now.ToShortTimeString(),
                (int)now.Subtract(this.begin).TotalMinutes,
                now.Subtract(this.begin).Seconds);

            if (successful)
            {
                Input("Finished " + timeString);
            }
            else
            {
                Input("Failed " + timeString);
            }
        }

        public void EndProgress() { }

        public void Progress(int progress, int overall) { }

        #endregion

        #region Checkboxes Changes

        private void best_CheckedChanged(object sender, EventArgs e)
        {
            if (this.best.Checked == true)
            {
                this.junk.Checked = true;
                this.antidefordot.Checked = false;
                this.antiDebug.Checked = true;
                this.antiDump.Checked = true;
                this.antiILDasm.Checked = true;
                this.antiTamper.Checked = true;
                this.calli.Checked = true;
                this.virtualization.Checked = false;
                this.disConst.Checked = true;
                this.constants.Checked = true;
                this.controlFlow.Checked = true;
                this.fakeNative.Checked = false;
                this.hideMethods.Checked = true;
                this.invalidMetadat.Checked = true;
                this.md5Checksum.Checked = true;
                this.cleanRefProxy.Checked = false;
                this.mutations.Checked = false;
                this.moduleFlood.Checked = true;
                this.localtofield.Checked = false;
                this.refProxy.Checked = false;
                this.renamer.Checked = true;
                this.resources1.Checked = false;
                this.mutateConst.Checked = true;
                this.stackUn.Checked = true;
                this.virt.Checked = false;
                this.all.Checked = false;
                this.minimum.Checked = false;
            }
            else
            {
                this.clearProtections();
            }

        }

        private void minimum_CheckedChanged(object sender, EventArgs e)
        {
            if (this.minimum.Checked == true)
            {
                this.junk.Checked = false;
                this.antiDebug.Checked = true;
                this.antidefordot.Checked = false;
                this.antiDump.Checked = false;
                this.antiILDasm.Checked = false;
                this.antiTamper.Checked = true;
                this.calli.Checked = false;
                this.virtualization.Checked = false;
                this.disConst.Checked = false;
                this.constants.Checked = true;
                this.controlFlow.Checked = true;
                this.fakeNative.Checked = false;
                this.hideMethods.Checked = true;
                this.invalidMetadat.Checked = true;
                this.md5Checksum.Checked = false;
                this.cleanRefProxy.Checked = false;
                this.mutations.Checked = false;
                this.moduleFlood.Checked = false;
                this.localtofield.Checked = false;
                this.refProxy.Checked = false;
                this.renamer.Checked = true;
                this.resources1.Checked = false;
                this.mutateConst.Checked = false;
                this.stackUn.Checked = false;
                this.best.Checked = false;
                this.all.Checked = false;
                this.virt.Checked = false;
            }
            else
            {
                this.clearProtections();
            }
        }

        private void all_CheckedChanged(object sender, EventArgs e)
        {
            if (this.all.Checked == true)
            {
                this.junk.Checked = true;
                this.antiDebug.Checked = true;
                this.antiDump.Checked = true;
                this.antiILDasm.Checked = true;
                this.antidefordot.Checked = true;
                this.antiTamper.Checked = true;
                this.calli.Checked = true;
                this.virtualization.Checked = true;
                this.disConst.Checked = true;
                this.constants.Checked = true;
                this.controlFlow.Checked = true;
                this.fakeNative.Checked = true;
                this.hideMethods.Checked = true;
                this.invalidMetadat.Checked = true;
                this.md5Checksum.Checked = true;
                this.cleanRefProxy.Checked = true;
                this.mutations.Checked = true;
                this.moduleFlood.Checked = true;
                this.localtofield.Checked = true;
                this.refProxy.Checked = true;
                this.mutateConst.Checked = true;
                this.renamer.Checked = true;
                this.resources1.Checked = true;
                this.stackUn.Checked = true;
                this.best.Checked = false;
                this.minimum.Checked = false;
            }
            else
            {
                this.clearProtections();
            }
        }
        public void clearProtections()
        {
            this.junk.Checked = false;
            this.antiDebug.Checked = false;
            this.antiDump.Checked = false;
            this.antiILDasm.Checked = false;
            this.antiTamper.Checked = false;
            this.calli.Checked = false;
            this.virtualization.Checked = false;
            this.disConst.Checked = false;
            this.constants.Checked = false;
            this.controlFlow.Checked = false;
            this.fakeNative.Checked = false;
            this.hideMethods.Checked = false;
            this.invalidMetadat.Checked = false;
            this.md5Checksum.Checked = false;
            this.cleanRefProxy.Checked = false;
            this.mutations.Checked = false;
            this.moduleFlood.Checked = false;
            this.localtofield.Checked = false;
            this.refProxy.Checked = false;
            this.renamer.Checked = false;
            this.resources1.Checked = false;
            this.mutateConst.Checked = false;
            this.stackUn.Checked = false;
            this.virt.Checked = false;
            this.virtStrong.Checked = false;
            this.antidefordot.Checked = false;
        }

        private void Virt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.virt.Checked == true)
            {
                this.groupBox1.Enabled = false;
                this.minimum.Enabled = false;
                this.best.Enabled = false;
                this.all.Enabled = false;
                this.minimum.Checked = false;
                this.best.Checked = false;
                this.all.Checked = false;
                this.virtStrong.Checked = false;
            }
            else if (this.virt.Checked == false)
            {
                if (this.virtStrong.Checked != true)
                {
                    this.groupBox1.Enabled = true;
                    this.minimum.Enabled = true;
                    this.best.Enabled = true;
                    this.all.Enabled = true;
                }
            }
        }

        private void VirtStrong_CheckedChanged(object sender, EventArgs e)
        {
            if (this.virtStrong.Checked == true)
            {
                this.groupBox1.Enabled = false;
                this.minimum.Enabled = false;
                this.best.Enabled = false;
                this.all.Enabled = false;
                this.virt.Checked = false;
                this.minimum.Checked = false;
                this.best.Checked = false;
                this.all.Checked = false;
            }
            else if (this.virtStrong.Checked == false)
            {
                if (this.virt.Checked != true)
                {
                    this.groupBox1.Enabled = true;
                    this.minimum.Enabled = true;
                    this.best.Enabled = true;
                    this.all.Enabled = true;
                }
            }
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.renameAll.Checked == true)
            {
                this.renChi.Checked = true;
                this.renGre.Checked = true;
                this.renInv.Checked = true;
                this.renLet.Checked = true;
                this.renNum.Checked = true;
                this.renRus.Checked = true;
            }
            else
            {
                this.renChi.Checked = false;
                this.renGre.Checked = false;
                this.renInv.Checked = false;
                this.renLet.Checked = false;
                this.renNum.Checked = false;
                this.renRus.Checked = false;
            }
        }

        #endregion

        #region Refresh Project

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Projects")))
            {
                this.listView1.Clear();
                string[] files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Projects"), "*.darkpr");
                foreach (string file in files)
                {
                    this.listView1.Items.Add(Path.GetFileName(file));
                }
            }
            else
            {
                MessageBox.Show("Projects folder doesn't exist, creating it...", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Projects"));
                }
                catch
                {
                    MessageBox.Show("Error while trying to create projects folder, contact dark#5000 if this isn't normal", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Create Project

        private void MetroButton5_Click(object sender, EventArgs e)
        {
            if (this.projName.Text == "")
            {
                return;
            }

            if (this.projName.Text.Length > 30)
            {
                return;
            }

            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Projects")))
            {
                ConfuserProject proj = new ConfuserProject();


                ProjectModule module = new ProjectModule();
                module.Path = Path.GetFileName(this.thirteenTextBox1.Text);
                proj.Add(module);
                Rule rule = new Rule("true", ProtectionPreset.None, false);
                if (this.groupBox1.Enabled == true)
                {
                    if (this.antiTamper.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDebug.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti debug", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDump.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti dump", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiILDasm.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti ildasm", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.calli.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Calli Protection", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.constants.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("constants", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.controlFlow.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ctrl flow", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.invalidMetadat.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("invalid metadata", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.renamer.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.refProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.cleanRefProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.moduleFlood.Checked)
                    {
                        SettingItem<Protection> moduleflood = new SettingItem<Protection>("module flood", SettingItemAction.Add);
                        rule.Add(moduleflood);
                    }

                    if (this.fakeNative.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Fake Native", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    SettingItem<Protection> rename = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                    rule.Add(rename);

                    if (this.mutations.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Mutations", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.mutateConst.Checked == true)
                    {
                        SettingItem<Protection> mutateconst = new SettingItem<Protection>("Mutate Constants", SettingItemAction.Add);
                        rule.Add(mutateconst);
                    }

                    if (this.localtofield.Checked)
                    {
                        SettingItem<Protection> field2field = new SettingItem<Protection>("lcltofield", SettingItemAction.Add);
                        rule.Add(field2field);
                    }

                    if (this.hideMethods.Checked)
                    {
                        SettingItem<Protection> entrypoint = new SettingItem<Protection>("Hide Methods", SettingItemAction.Add);
                        rule.Add(entrypoint);
                    }

                    if (this.md5Checksum.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.disConst.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Const disint", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.junk.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Junk", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.stackUn.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("stack underflow", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.antidefordot.Checked)
                    {
                        SettingItem<Protection> headers = new SettingItem<Protection>("anti de4dot", SettingItemAction.Add);
                        rule.Add(headers);
                    }

                    if (this.virtualization.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                    }

                }
                else
                {
                    if (this.virt.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                        SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(rename);
                        SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(md5);
                        SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                        rule.Add(modulere);
                    }
                    else if (this.virtStrong.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                        SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(rename);
                        SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(md5);
                        SettingItem<Protection> refproxy = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(refproxy);
                        SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                        rule.Add(modulere);
                        SettingItem<Protection> antitamper = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(antitamper);
                    }
                }

                proj.Rules.Add(rule);

                XmlDocument tempdarpr = proj.Save();
                tempdarpr.Save(Path.Combine(Environment.CurrentDirectory, "Projects", this.projName.Text + ".darkpr"));
            }
            else
            {
                MessageBox.Show("Projects folder doesn't exist, creating it...", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Projects"));
                }
                catch
                {
                    MessageBox.Show("Error while trying to create projects folder, contact dark#5000 if this isn't normal", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Overwrite Project

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Projects", this.listView1.SelectedItems[0].Text)))
            {
                return;
            }

            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Projects")))
            {
                ConfuserProject proj = new ConfuserProject();


                ProjectModule module = new ProjectModule();
                module.Path = Path.GetFileName(this.thirteenTextBox1.Text);
                proj.Add(module);
                Rule rule = new Rule("true", ProtectionPreset.None, false);
                if (this.groupBox1.Enabled == true)
                {
                    if (this.antiTamper.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDebug.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti debug", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiDump.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti dump", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.antiILDasm.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("anti ildasm", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.calli.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Calli Protection", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.constants.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("constants", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.controlFlow.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ctrl flow", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.invalidMetadat.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("invalid metadata", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.renamer.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(protection);
                    }
                    if (this.refProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.cleanRefProxy.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.moduleFlood.Checked)
                    {
                        SettingItem<Protection> moduleflood = new SettingItem<Protection>("module flood", SettingItemAction.Add);
                        rule.Add(moduleflood);
                    }

                    if (this.fakeNative.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Fake Native", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    SettingItem<Protection> rename = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                    rule.Add(rename);

                    if (this.mutations.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Mutations", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.mutateConst.Checked == true)
                    {
                        SettingItem<Protection> mutateconst = new SettingItem<Protection>("Mutate Constants", SettingItemAction.Add);
                        rule.Add(mutateconst);
                    }

                    if (this.localtofield.Checked)
                    {
                        SettingItem<Protection> field2field = new SettingItem<Protection>("lcltofield", SettingItemAction.Add);
                        rule.Add(field2field);
                    }

                    if (this.hideMethods.Checked)
                    {
                        SettingItem<Protection> entrypoint = new SettingItem<Protection>("Hide Methods", SettingItemAction.Add);
                        rule.Add(entrypoint);
                    }

                    if (this.md5Checksum.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.disConst.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Const disint", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.junk.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("Junk", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.stackUn.Checked)
                    {
                        SettingItem<Protection> protection = new SettingItem<Protection>("stack underflow", SettingItemAction.Add);
                        rule.Add(protection);
                    }

                    if (this.virtualization.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                    }

                    if (this.antidefordot.Checked)
                    {
                        SettingItem<Protection> headers = new SettingItem<Protection>("anti de4dot", SettingItemAction.Add);
                        rule.Add(headers);
                    }

                }
                else
                {
                    if (this.virt.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                        SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(rename);
                        SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(md5);
                        SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                        rule.Add(modulere);
                    }
                    else if (this.virtStrong.Checked)
                    {
                        proj.PluginPaths.Add(Directory.GetCurrentDirectory() + "\\KoiVM.Confuser.exe");
                        SettingItem<Protection> virtualization = new SettingItem<Protection>("virt", SettingItemAction.Add);
                        rule.Add(virtualization);
                        SettingItem<Protection> rename = new SettingItem<Protection>("rename", SettingItemAction.Add);
                        rule.Add(rename);
                        SettingItem<Protection> md5 = new SettingItem<Protection>("checksum", SettingItemAction.Add);
                        rule.Add(md5);
                        SettingItem<Protection> refproxy = new SettingItem<Protection>("Clean ref proxy", SettingItemAction.Add);
                        rule.Add(refproxy);
                        SettingItem<Protection> modulere = new SettingItem<Protection>("Rename Module", SettingItemAction.Add);
                        rule.Add(modulere);
                        SettingItem<Protection> antitamper = new SettingItem<Protection>("anti tamper", SettingItemAction.Add);
                        rule.Add(antitamper);
                    }
                }

                proj.Rules.Add(rule);

                File.Delete(Path.Combine(Environment.CurrentDirectory, "Projects", this.listView1.SelectedItems[0].Text));
                XmlDocument tempdarpr = proj.Save();
                tempdarpr.Save(Path.Combine(Environment.CurrentDirectory, "Projects", this.listView1.SelectedItems[0].Text));
            }
            else
            {
                MessageBox.Show("Projects folder doesn't exist, creating it...", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Projects"));
                }
                catch
                {
                    MessageBox.Show("Error while trying to create projects folder, contact dark#5000 if this isn't normal", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Load Project

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Projects", this.listView1.SelectedItems[0].Text)))
            {
                return;
            }

            string path = Path.Combine(Environment.CurrentDirectory, "Projects", this.listView1.SelectedItems[0].Text);

            this.clearProtections();
            ConfuserProject proj = new ConfuserProject();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                proj.Load(xmlDoc);
                ProjectVM project = new ProjectVM(proj, path);

                foreach (ProjectRuleVM r in project.Rules)
                {
                    foreach (ProjectSettingVM<Protection> s in r.Protections)
                    {
                        string name = s.Id;

                        if (name == "anti tamper")
                        {
                            this.antiTamper.Checked = true;
                        }

                        if (name == "anti debug")
                        {
                            this.antiDebug.Checked = true;
                        }

                        if (name == "anti dump")
                        {
                            this.antiDump.Checked = true;
                        }

                        if (name == "anti ildasm")
                        {
                            this.antiILDasm.Checked = true;
                        }

                        if (name == "Calli Protection")
                        {
                            this.calli.Checked = true;
                        }

                        if (name == "constants")
                        {
                            this.constants.Checked = true;
                        }

                        if (name == "ctrl flow")
                        {
                            this.controlFlow.Checked = true;
                        }

                        if (name == "invalid metadata")
                        {
                            this.invalidMetadat.Checked = true;
                        }

                        if (name == "rename")
                        {
                            this.renamer.Checked = true;
                        }

                        if (name == "ref proxy")
                        {
                            this.refProxy.Checked = true;
                        }

                        if (name == "Clean ref proxy")
                        {
                            this.cleanRefProxy.Checked = true;
                        }

                        if (name == "module flood")
                        {
                            this.moduleFlood.Checked = true;
                        }

                        if (name == "Fake Native")
                        {
                            this.fakeNative.Checked = true;
                        }

                        if (name == "Mutations")
                        {
                            this.mutations.Checked = true;
                        }

                        if (name == "lcltofield")
                        {
                            this.localtofield.Checked = true;
                        }

                        if (name == "Hide Methods")
                        {
                            this.hideMethods.Checked = true;
                        }

                        if (name == "checksum")
                        {
                            this.md5Checksum.Checked = true;
                        }

                        if (name == "Const disint")
                        {
                            this.disConst.Checked = true;
                        }

                        if (name == "Junk")
                        {
                            this.junk.Checked = true;

                        }

                        if (name == "Mutate Constants")
                        {
                            this.mutateConst.Checked = true;
                        }

                        if (name == "stack underflow")
                        {
                            this.stackUn.Checked = true;
                        }

                        if (name == "stack underflow")
                        {
                            this.stackUn.Checked = true;
                        }

                        if (name == "anti de4dot")
                        {
                            this.antidefordot.Checked = true;
                        }

                        if (name == "virt")
                        {
                            this.virtualization.Checked = true;
                        }
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Open Projects Folder

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Projects")))
            {
                Process.Start("explorer.exe", Path.Combine(Environment.CurrentDirectory, "Projects"));
            }
            else
            {
                MessageBox.Show("Projects folder doesn't exist, creating it...", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Projects"));
                }
                catch
                {
                    MessageBox.Show("Error while trying to create projects folder, contact dark#5000 if this isn't normal", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        #endregion

        #region Custom Renamer

        private void MetroButton6_Click(object sender, EventArgs e)
        {
            if (this.prefix.Text == "" && this.suffix.Text == "" && this.moduleName.Text == "" && this.assemblyName.Text == "" && this.lentgh.Value == 0 && this.renRus.Checked == false && this.renNum.Checked == false && this.renLet.Checked == false && this.renInv.Checked == false && this.renGre.Checked == false && this.renChi.Checked == false)
            {
                return;
            }

            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Config")))
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt")))
                {
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt"));
                }
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt")))
                {
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt"));
                }
                if (this.prefix.Text == "" && this.suffix.Text == "" && this.moduleName.Text != "" && this.assemblyName.Text != "" && this.lentgh.Value == 0 && this.renRus.Checked == false && this.renNum.Checked == false && this.renLet.Checked == false && this.renInv.Checked == false && this.renGre.Checked == false && this.renChi.Checked == false)
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt"), string.Concat(this.moduleName.Text, ":", this.assemblyName.Text));
                    return;
                }
                if (this.lentgh.Value == 0)
                {
                    MessageBox.Show("You can't use 0 for the trackbar", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (this.prefix.Text.Contains(" "))
                {
                    MessageBox.Show("You can't use space in prefix", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (this.suffix.Text.Contains(" "))
                {
                    MessageBox.Show("You can't use space in suffix", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string chinese = "他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你";
                string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string numbers = "1234567891234567891234567891234567891234567891234567";
                string invisible = "ᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠ";
                string russian = "αβγδεζηθικλμνξοπρστυφχψωαβγδεζηθικλμνξοπρστυφχψωαβγδε";

                this.charset = "";

                if (this.renChi.Checked)
                {
                    this.charset += chinese;
                }

                if (this.renRus.Checked)
                {
                    this.charset += russian;
                }

                if (this.renInv.Checked)
                {
                    this.charset += invisible;
                }

                if (this.renNum.Checked)
                {
                    this.charset += numbers;
                }

                if (this.renLet.Checked)
                {
                    this.charset += letters;
                }

                if (this.renameAll.Checked)
                {
                    this.charset += chinese + russian + invisible + numbers + letters;
                }

                if (this.charset == "")
                {
                    MessageBox.Show("You didn't select anything as preset!", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.rename = "";

                if (this.moduleName.Text != "" && this.assemblyName.Text != "")
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt"), string.Concat(this.moduleName.Text, ":", this.assemblyName.Text));
                }

                StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt"));
                try
                {
                    for (int i = 0; i < 40000; i++)
                    {
                        streamWriter.WriteLine(this.prefix.Text + smethod_2(this.lentgh.Value, this.charset) + this.suffix.Text);
                    }
                    return;
                }
                finally
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Config"));
                MessageBox.Show("Config directory has been created, please reclick on set!", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static readonly Random random = new Random();

        public static string smethod_2(int int_0, string string_10)
        {
            return new string(Enumerable.Repeat<string>(string_10, int_0).Select(new Func<string, char>(method_0)).ToArray<char>());
        }

        private static char method_0(string string_0)
        {
            return string_0[random.Next(string_0.Length)];
        }

        private void Lentgh_ValueChanged(object sender, EventArgs e)
        {
            this.lentghtext.Text = "Length: " + this.lentgh.Value;
        }

        private void MetroButton7_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Config")))
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt")))
                {
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt"));
                }
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt")))
                {
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt"));
                }

                this.charset = "他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789123456789123456789123456789123456789123456789ᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠαβγδεζηθικλμνξοπρστυφχψωαβγδεζηθικλμνξοπρστυφχψωαβγδεᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠ";

                this.rename = "";

                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Config", "ModulenAssembly.txt"), string.Concat("∂αякsρяσтεcтσя", ":", "∂αякsρяσтεcтσя"));

                StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt"));
                try
                {
                    for (int i = 0; i < 40000; i++)
                    {
                        streamWriter.WriteLine(this.prefix.Text + smethod_2(30, this.charset) + this.suffix.Text);
                    }
                    return;
                }
                finally
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Config"));
                MessageBox.Show("Config directory has been created, please reclick on set!", "DarksProtector", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Credits

        private void Label13_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/Lekysha");
            }
            catch { }
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/Lekysha");
            }
            catch { }
        }

        private void Label12_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/BedTheGod");
            }
            catch { }
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/BedTheGod");
            }
            catch { }
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/yck1509");
            }
            catch { }
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/yck1509");
            }
            catch { }
        }

        private void Label14_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cracked.to/");
            }
            catch { }
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cracked.to/");
            }
            catch { }
        }

        private void Label10_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://dev/d4rk.fr/");
            }
            catch { }
        }

        private void Label11_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cto.d4rk.fr/");
            }
            catch { }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cto.d4rk.fr/");
            }
            catch { }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cto.d4rk.fr/");
            }
            catch { }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cto.d4rk.fr/");
            }
            catch { }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://cto.d4rk.fr/");
            }
            catch { }
        }

        private void Label15_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new WebClient().DownloadString("https://d4rk.fr/projects/darksprotector.cto"));
            }
            catch { }
        }

        #endregion
    }
}
