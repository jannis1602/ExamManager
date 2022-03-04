
namespace ExamManager
{
    partial class KeepDataForm
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
            this.clb1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // clb1
            // 
            this.clb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clb1.FormattingEnabled = true;
            this.clb1.Items.AddRange(new object[] {
            "Fach",
            "Prüfungsraum",
            "Vorbereitungsraum",
            "Lehrer",
            "Stufe",
            "Schüler"});
            this.clb1.Location = new System.Drawing.Point(0, 0);
            this.clb1.Name = "clb1";
            this.clb1.Size = new System.Drawing.Size(234, 196);
            this.clb1.TabIndex = 0;
            this.clb1.SelectedIndexChanged += new System.EventHandler(this.clb1_SelectedIndexChanged);
            this.clb1.DoubleClick += new System.EventHandler(this.clb1_DoubleClick);
            // 
            // KeepDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 196);
            this.Controls.Add(this.clb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeepDataForm";
            this.Text = "Daten behalten";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clb1;
    }
}