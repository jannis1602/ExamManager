
namespace ExamManager
{
    partial class FormLoadTable
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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.flp_main = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_file = new System.Windows.Forms.Label();
            this.btn_select_file = new System.Windows.Forms.Button();
            this.flp_grade = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_grade = new System.Windows.Forms.Label();
            this.cb_grade = new System.Windows.Forms.ComboBox();
            this.flp_date = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_date = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_duration = new System.Windows.Forms.TextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_subjectname = new System.Windows.Forms.Label();
            this.cb_remove_numbers = new System.Windows.Forms.CheckBox();
            this.tlp_main.SuspendLayout();
            this.flp_main.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flp_grade.SuspendLayout();
            this.flp_date.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.flp_main, 0, 0);
            this.tlp_main.Controls.Add(this.btn_add, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.Size = new System.Drawing.Size(800, 450);
            this.tlp_main.TabIndex = 0;
            // 
            // flp_main
            // 
            this.flp_main.Controls.Add(this.flowLayoutPanel1);
            this.flp_main.Controls.Add(this.flp_grade);
            this.flp_main.Controls.Add(this.flp_date);
            this.flp_main.Controls.Add(this.flowLayoutPanel2);
            this.flp_main.Controls.Add(this.flowLayoutPanel3);
            this.flp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_main.Location = new System.Drawing.Point(3, 3);
            this.flp_main.Name = "flp_main";
            this.flp_main.Size = new System.Drawing.Size(794, 404);
            this.flp_main.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_file);
            this.flowLayoutPanel1.Controls.Add(this.btn_select_file);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lbl_file
            // 
            this.lbl_file.AutoSize = true;
            this.lbl_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_file.Location = new System.Drawing.Point(3, 3);
            this.lbl_file.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_file.Name = "lbl_file";
            this.lbl_file.Size = new System.Drawing.Size(144, 20);
            this.lbl_file.TabIndex = 0;
            this.lbl_file.Text = "Tabelle auswählen:";
            this.lbl_file.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_select_file
            // 
            this.btn_select_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_select_file.Location = new System.Drawing.Point(153, 3);
            this.btn_select_file.Name = "btn_select_file";
            this.btn_select_file.Size = new System.Drawing.Size(150, 27);
            this.btn_select_file.TabIndex = 1;
            this.btn_select_file.Text = "Auswählen";
            this.btn_select_file.UseVisualStyleBackColor = true;
            this.btn_select_file.Click += new System.EventHandler(this.btn_select_file_Click);
            // 
            // flp_grade
            // 
            this.flp_grade.Controls.Add(this.lbl_grade);
            this.flp_grade.Controls.Add(this.cb_grade);
            this.flp_grade.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_grade.Location = new System.Drawing.Point(3, 39);
            this.flp_grade.Name = "flp_grade";
            this.flp_grade.Size = new System.Drawing.Size(794, 30);
            this.flp_grade.TabIndex = 1;
            // 
            // lbl_grade
            // 
            this.lbl_grade.AutoSize = true;
            this.lbl_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_grade.Location = new System.Drawing.Point(3, 3);
            this.lbl_grade.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_grade.Name = "lbl_grade";
            this.lbl_grade.Size = new System.Drawing.Size(52, 20);
            this.lbl_grade.TabIndex = 1;
            this.lbl_grade.Text = "Stufe:";
            this.lbl_grade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_grade
            // 
            this.cb_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_grade.FormattingEnabled = true;
            this.cb_grade.Location = new System.Drawing.Point(61, 3);
            this.cb_grade.Name = "cb_grade";
            this.cb_grade.Size = new System.Drawing.Size(100, 24);
            this.cb_grade.TabIndex = 2;
            // 
            // flp_date
            // 
            this.flp_date.Controls.Add(this.lbl_date);
            this.flp_date.Controls.Add(this.dtp_date);
            this.flp_date.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_date.Location = new System.Drawing.Point(3, 75);
            this.flp_date.Name = "flp_date";
            this.flp_date.Size = new System.Drawing.Size(794, 30);
            this.flp_date.TabIndex = 4;
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(3, 3);
            this.lbl_date.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(40, 20);
            this.lbl_date.TabIndex = 1;
            this.lbl_date.Text = "Tag:";
            this.lbl_date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_date
            // 
            this.dtp_date.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtp_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(47, 1);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(1);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(100, 23);
            this.dtp_date.TabIndex = 4;
            this.dtp_date.Value = new System.DateTime(2022, 1, 29, 0, 0, 0, 0);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.tb_duration);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 111);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(794, 30);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prüfungsdauer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_duration
            // 
            this.tb_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_duration.Location = new System.Drawing.Point(127, 3);
            this.tb_duration.Name = "tb_duration";
            this.tb_duration.Size = new System.Drawing.Size(50, 23);
            this.tb_duration.TabIndex = 2;
            this.tb_duration.Text = "30";
            this.tb_duration.TextChanged += new System.EventHandler(this.tb_duration_TextChanged);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(699, 413);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(98, 34);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lbl_subjectname);
            this.flowLayoutPanel3.Controls.Add(this.cb_remove_numbers);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 147);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(794, 30);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // lbl_subjectname
            // 
            this.lbl_subjectname.AutoSize = true;
            this.lbl_subjectname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subjectname.Location = new System.Drawing.Point(3, 3);
            this.lbl_subjectname.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_subjectname.Name = "lbl_subjectname";
            this.lbl_subjectname.Size = new System.Drawing.Size(112, 20);
            this.lbl_subjectname.TabIndex = 1;
            this.lbl_subjectname.Text = "Fächernamen:";
            this.lbl_subjectname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_remove_numbers
            // 
            this.cb_remove_numbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_remove_numbers.AutoSize = true;
            this.cb_remove_numbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_remove_numbers.Location = new System.Drawing.Point(121, 3);
            this.cb_remove_numbers.Name = "cb_remove_numbers";
            this.cb_remove_numbers.Size = new System.Drawing.Size(136, 22);
            this.cb_remove_numbers.TabIndex = 2;
            this.cb_remove_numbers.Text = "Zahlen entfernen";
            this.cb_remove_numbers.UseVisualStyleBackColor = true;
            // 
            // FormLoadTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormLoadTable";
            this.Text = "Tabelle laden";
            this.tlp_main.ResumeLayout(false);
            this.flp_main.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flp_grade.ResumeLayout(false);
            this.flp_grade.PerformLayout();
            this.flp_date.ResumeLayout(false);
            this.flp_date.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.FlowLayoutPanel flp_main;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbl_file;
        private System.Windows.Forms.Button btn_select_file;
        private System.Windows.Forms.FlowLayoutPanel flp_grade;
        private System.Windows.Forms.Label lbl_grade;
        private System.Windows.Forms.ComboBox cb_grade;
        private System.Windows.Forms.FlowLayoutPanel flp_date;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_duration;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lbl_subjectname;
        private System.Windows.Forms.CheckBox cb_remove_numbers;
    }
}