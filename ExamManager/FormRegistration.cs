using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormRegistration : Form
    {
        public bool locked = true;
        public FormRegistration()
        {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "key"))
                {
                    if (CheckKey(sr.ReadLine()))
                    {
                        locked = false;
                        this.Dispose();
                        return;
                    }
                }
            }
            catch (Exception)
            { }
            InitializeComponent();
        }

        private void btn_checkkey_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void Check()
        {
            if (tb_key.Text.Length == 0) return;
            if (!cb_keepkey.Checked)
            {
                DialogResult result = MessageBox.Show("Schlüssel NICHT speichern?", "Achtung", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;
            }
            if (CheckKey(tb_key.Text))
            {
                locked = false;
                if (cb_keepkey.Checked)
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "key"))
                    {
                        sw.WriteLine(tb_key.Text);
                    }
                this.Dispose();
            }
            else { tb_key.Clear(); MessageBox.Show("falscher Schlüssel", "Achtung", MessageBoxButtons.OK); }
        }

        private static bool CheckKey(string keyword)
        {
            // shortTimeKeys
            // ...
            // fullTimeKeys
            string[] keys =
                { "04c6828c472c9183253b1ef6919e1947504ca1fabac5990fdd063b07d787a184",
                    "79cc676dda52825216240d742d2b32bee005b66f8b5b416eda1eaa1ef2c4cc33"};
            if (keys.Contains(CreateKey(keyword))) return true;
            return false;
        }
        private static string CreateKey(string text)
        {
            if (text.Length == 0) return null;
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
            foreach (byte theByte in crypto) hash += theByte.ToString("x2");
            return hash;
        }

        private void tb_key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) Check();
        }
    }
}
