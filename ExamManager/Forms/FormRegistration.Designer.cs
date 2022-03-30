
namespace ExamManager
{
    partial class FormRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistration));
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.btn_checkkey = new System.Windows.Forms.Button();
            this.cb_keepkey = new System.Windows.Forms.CheckBox();
            this.tb_key = new System.Windows.Forms.TextBox();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            resources.ApplyResources(this.tlp_main, "tlp_main");
            this.tlp_main.Controls.Add(this.btn_checkkey, 0, 2);
            this.tlp_main.Controls.Add(this.cb_keepkey, 0, 1);
            this.tlp_main.Controls.Add(this.tb_key, 0, 0);
            this.tlp_main.Name = "tlp_main";
            // 
            // btn_checkkey
            // 
            resources.ApplyResources(this.btn_checkkey, "btn_checkkey");
            this.btn_checkkey.Name = "btn_checkkey";
            this.btn_checkkey.UseVisualStyleBackColor = true;
            this.btn_checkkey.Click += new System.EventHandler(this.btn_checkkey_Click);
            // 
            // cb_keepkey
            // 
            resources.ApplyResources(this.cb_keepkey, "cb_keepkey");
            this.cb_keepkey.Checked = true;
            this.cb_keepkey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_keepkey.Name = "cb_keepkey";
            this.cb_keepkey.UseVisualStyleBackColor = true;
            // 
            // tb_key
            // 
            resources.ApplyResources(this.tb_key, "tb_key");
            this.tb_key.Name = "tb_key";
            this.tb_key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_key_KeyDown);
            // 
            // FormRegistration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRegistration";
            this.tlp_main.ResumeLayout(false);
            this.tlp_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.Button btn_checkkey;
        private System.Windows.Forms.CheckBox cb_keepkey;
        private System.Windows.Forms.TextBox tb_key;
    }
}