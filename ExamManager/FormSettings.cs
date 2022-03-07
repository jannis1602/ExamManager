using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormSettings : Form
    {
        private bool saved = false;
        private bool changedColor = false;
        public FormSettings()
        {
            InitializeComponent();
        }
        public event EventHandler UpdateColor;
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (changedColor)
                switch (cb_color.SelectedIndex)
                {
                    case 0: Colors.ColorTheme(Colors.Theme.light); break;
                    case 1: Colors.ColorTheme(Colors.Theme.dark); break;
                    case 2: Colors.ColorTheme(Colors.Theme.bw); break;
                }
            UpdateColor.Invoke(this, null);
            Properties.Settings.Default.email_domain = tb_emaildomain.Text; // -> on change
                                                                            //Properties.Settings.Default.Reload(); // TODO restore old settings
            Properties.Settings.Default.Save(); // on exit
            saved = true;
            // save propeties...
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Schließen ohne zu speichern?", "Achtung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) this.Dispose();
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            else this.Dispose();
        }

        private void btn_show_smtp_pwd_Click(object sender, EventArgs e)
        {
            tb_smtp_pwd.UseSystemPasswordChar = !tb_smtp_pwd.UseSystemPasswordChar;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            tb_emaildomain.Text = Properties.Settings.Default.email_domain;
            cb_color.SelectedIndex = Properties.Settings.Default.color_theme;
            lbl_current_database_path.Text = Properties.Settings.Default.databasePath;
        }

        private void cb_color_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.color_theme = cb_color.SelectedIndex;
            changedColor = true;
        }

        private void lbl_daefaultdb_Click(object sender, EventArgs e)
        {
            string path = Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager\\") + "database.db";
            Properties.Settings.Default.databasePath = path;
        }

        private void btn_select_localdb_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "database files (*.db)|*.db|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Lokale Datenbank auswählen";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Properties.Settings.Default.databasePath = filePath;
                    Properties.Settings.Default.Save();
                    Application.Restart();
                    Environment.Exit(0);
                    // TODO: no restart
                }
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            // Properties.Settings.Default.Reset(); // TODO reset settings
        }
        private void btn_settings_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Einstellungen speichern",
                FileName = "ExammanagerSettings.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            SettingsObject settings = new SettingsObject
            {
                DatabasePath = Properties.Settings.Default.databasePath,
                EmailDomain = Properties.Settings.Default.email_domain,
                SMTPSettings = new SMTP_Settings // TODO: get from settings
                {
                    Server = tb_smtp_server.Text,
                    Port = tb_smtp_port.Text,
                    Email = tb_smtp_email.Text,
                    Password = tb_smtp_pwd.Text,
                    SenderName = tb_smtp_sendername.Text,
                    EmailTitel = tb_smtp_email_titel.Text
                }
            };

            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_settings_import_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    SettingsObject settings;
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string jsonString = r.ReadToEnd();
                        settings = JsonConvert.DeserializeObject<SettingsObject>(jsonString);
                    }
                    Console.WriteLine(settings.DatabasePath + "  " + settings.EmailDomain + "  " + settings.SMTPSettings);
                    if (settings.EmailDomain != null)
                    {
                        Properties.Settings.Default.email_domain = settings.EmailDomain;
                        tb_emaildomain.Text = settings.EmailDomain;
                    }
                    if (settings.DatabasePath != null)
                    {
                        Properties.Settings.Default.databasePath = settings.DatabasePath;
                        lbl_current_database_path.Text = settings.DatabasePath;
                    }
                    if (settings.SMTPSettings != null)
                    {
                        tb_smtp_server.Text = settings.SMTPSettings.Server;
                        tb_smtp_port.Text = settings.SMTPSettings.Port;
                    }
                }
            }
        }
    }
    class SettingsObject
    {
        public string DatabasePath { get; set; }
        public string EmailDomain { get; set; }
        public SMTP_Settings SMTPSettings { get; set; }

    }
    class SMTP_Settings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string EmailTitel { get; set; }

    }
}
