using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GetChromePasswords
{
    public partial class Form1 : Form
    {
        List<string> passList = new List<string>();
        Point cell;

        public Form1()
        {
            InitializeComponent();
            HeaderFormatStyle hs = new HeaderFormatStyle();
            hs.SetBackColor(Color.FromArgb(241, 199, 22));
            hs.SetFont(new Font(FontFamily.GenericMonospace,10, FontStyle.Bold));
            listView.HeaderFormatStyle = hs;
        }

        private bool KillChrome()
        {
            Process[] processNames = Process.GetProcessesByName("chrome");
            bool kill = false;
            foreach (Process item in processNames)
            {
                if (!kill)
                {
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    DialogResult dialogResult = MessageBox.Show("Please close Chrome before continuing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    kill = true;
                }
            }
            processNames = Process.GetProcessesByName("chrome");
            return !(processNames.Length > 0);
        }

        private List<string[]> GetChromePass()
        {
            List<string[]> entries = new List<string[]>();
            string retMessage = String.Empty;
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string connPath = appDataPath + "\\Google\\Chrome\\User Data\\Default\\Login Data";
            if (File.Exists(connPath))
            {
                DataTable dt = new DataTable();
                try
                {
                    SQLiteConnection connection = new SQLiteConnection("Data Source=data" + ";New=True;UseUTF16Encoding=True");
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(connection);
                    command.CommandText = "SELECT origin_url, username_value, password_value FROM logins";
                    SQLiteDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                    connection = null;
                }
                catch { }
                foreach (DataRow table in dt.Rows)
                {
                    string name = (string)table["username_value"];
                    if (!String.IsNullOrEmpty(name))
                    {
                        try
                        {
                            string[] entry = new string[3];
                            entry[0] = (string)table["origin_url"];
                            entry[1] = name;
                            entry[2] = Encoding.ASCII.GetString(DPAPI.decrypt((byte[])table["password_value"]));
                            entries.Add(entry);
                        }
                        catch { }
                    }
                }
            }
            return entries;
        }

        private void Execute()
        {
            if (FileCopy(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data", "data"))
            {
                listView.Items.Clear();
                pictureBox2.Visible = false;
                List<string[]> entries = GetChromePass();
                List<Entry> list = new List<Entry>();
                foreach (string[] entry in entries)
                {
                    Entry item = new Entry((string)entry[0], (string)entry[1], "********");
                    list.Add(item);
                    passList.Add(entry[2]);
                }
                listView.SetObjects(list);
                listView.ContextMenuStrip = contextMenuStrip;
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Mouse-Over the ******** to reveal the password." + Environment.NewLine + Environment.NewLine + "Right-Click the list to save your data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool FileCopy(string srcfilename, string destfilename)
        {
            if (System.IO.File.Exists(srcfilename) == false)
                return false;
            System.IO.Stream s1 = System.IO.File.Open(srcfilename, System.IO.FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            System.IO.Stream s2 = System.IO.File.Open(destfilename, System.IO.FileMode.Create,FileAccess.ReadWrite,FileShare.Delete);
            System.IO.BinaryReader f1 = new System.IO.BinaryReader(s1);
            System.IO.BinaryWriter f2 = new System.IO.BinaryWriter(s2);
            while (true)
            {
                byte[] buf = new byte[10240];
                int sz = f1.Read(buf, 0, 10240);
                if (sz <= 0)
                    break;
                f2.Write(buf, 0, sz);
                if (sz < 10240)
                    break;
            }
            f1.Close();
            f2.Close();
            return true;
        }

        private void SaveToTextFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "TEXT|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    StringBuilder line = new StringBuilder();
                    int cnt = 0;
                    foreach (ListViewItem item in listView.Items)
                    {
                        line.Length = 0;
                        for (int i = 0; i < item.SubItems.Count; i++)
                        {
                            if (i > 0)
                                line.Append(",");
                            if (i < 2)
                                line.Append(item.SubItems[i].Text);
                            else
                                line.Append(passList[cnt]);
                        }
                        cnt++;
                        sw.WriteLine(line);
                    }
                }
        }

        private void SaveToCSVFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    StringBuilder line = new StringBuilder();
                    sw.WriteLine("\"URL\",\"USERNAME\",\"PASSWORD\"");
                    int cnt = 0;
                    foreach (ListViewItem item in listView.Items)
                    {
                        line.Length = 0;
                        for (int i = 0; i < item.SubItems.Count; i++)
                        {
                            if (i > 0)
                                line.Append(",");
                            if (i < 2)
                                line.Append(item.SubItems[i].Text);
                            else
                                line.Append(passList[cnt]);
                        }
                        cnt++;
                        sw.WriteLine(line);
                    }
                }
        }

        private void saveToTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToTextFile();
        }

        private void saveToCSVFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToCSVFile();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void listView_CellOver(object sender, CellOverEventArgs e)
        {
            if (e.Column != null && e.ColumnIndex == 2)
            {
                if (e.SubItem.Text == "********")
                {
                    e.SubItem.Text = passList[e.RowIndex];
                    cell = new Point(e.RowIndex, e.ColumnIndex);
                }
                Point here = new Point(e.RowIndex, e.ColumnIndex);
                if (cell != here)
                    listView.Items[cell.X].SubItems[cell.Y].Text = "********";
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DateTime start = DateTime.Now;
            while (File.Exists("data") && (DateTime.Now - start).TotalSeconds < 5)
            {
                try
                {
                    File.Delete("data");
                }
                catch { }
            }
        }
    }

    class Entry
    {
        string _url, _username, _password;
        public Entry(string url, string username, string password)
        {
            this._url = url;
            this._username = username;
            this._password = password;
        }
        public string Url { get { return _url; } set { _url = value; } }
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
    }
}