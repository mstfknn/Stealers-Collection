using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MozillaPasswords
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public void RefreshLists()
        {
            lvFireFox.Items.Clear();
            lvNoPassFF.Items.Clear();
            lvThunderbird.Items.Clear();

            //FireFox
            MozillaProfiles profiles = new MozillaProfiles(MozillaSoft.FireFox);
            MozillaSignons signons = null;

            //pour chaque profile
            foreach (MozillaProfile p in profiles)
            {
                //récupère les mots de passes de signons2.txt (si présent)
                signons = p.GetSignons(true);

                for (int i = 0;i < signons.NoPasswordSiteCount;i++)
                {
                    ListViewItem li = lvNoPassFF.Items.Add(p.Name);
                    li.SubItems.Add(signons.GetNoPasswordSite(i));
                }

                foreach (MozillaPassword pass in signons)
                {
                    ListViewItem li = lvFireFox.Items.Add(p.Name);
                    li.SubItems.Add(pass.SiteName);
                    li.SubItems.Add(pass.Username);
                    li.SubItems.Add(pass.Password);
                    li.SubItems.Add(pass.UsernameField);
                    li.SubItems.Add(pass.PasswordField);
                    li.SubItems.Add(pass.Domain);
                }

                //récupère les mots de passes de signons.txt (si présent)
                signons = p.GetSignons(false);

                for (int i = 0; i < signons.NoPasswordSiteCount; i++)
                {
                    ListViewItem li = lvNoPassFF.Items.Add(p.Name);
                    li.SubItems.Add(signons.GetNoPasswordSite(i));
                }

                foreach (MozillaPassword pass in signons)
                {
                    ListViewItem li = lvFireFox.Items.Add(p.Name);
                    li.SubItems.Add(pass.SiteName);
                    li.SubItems.Add(pass.Username);
                    li.SubItems.Add(pass.Password);
                    li.SubItems.Add(pass.UsernameField);
                    li.SubItems.Add(pass.PasswordField);
                    li.SubItems.Add("");
                }
            }

            profiles = new MozillaProfiles(MozillaSoft.Thunderbird);
            foreach (MozillaProfile p in profiles)
            {
                //récupère les mots de passes de signons2.txt (si présent)
                signons = p.GetSignons(true);

                foreach (MozillaPassword pass in signons)
                {
                    ListViewItem li = lvThunderbird.Items.Add(p.Name);
                    li.SubItems.Add(pass.SiteName);
                    li.SubItems.Add(pass.Username);
                    li.SubItems.Add(pass.Password);
                    li.SubItems.Add(pass.UsernameField);
                    li.SubItems.Add(pass.PasswordField);
                    li.SubItems.Add(pass.Domain);
                }

                //récupère les mots de passes de signons.txt (si présent)
                signons = p.GetSignons(false);

                foreach (MozillaPassword pass in signons)
                {
                    ListViewItem li = lvThunderbird.Items.Add(p.Name);
                    li.SubItems.Add(pass.SiteName);
                    li.SubItems.Add(pass.Username);
                    li.SubItems.Add(pass.Password);
                    li.SubItems.Add(pass.UsernameField);
                    li.SubItems.Add(pass.PasswordField);
                    li.SubItems.Add("");
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLists();
        }
    }
}