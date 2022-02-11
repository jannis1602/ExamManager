
namespace ExamManager
{
    partial class FormTeacherData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTeacherData));
            this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_edit = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_phonenumber = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_phonenumber = new System.Windows.Forms.Label();
            this.tb_phonenumber = new System.Windows.Forms.TextBox();
            this.btn_add_teacher = new System.Windows.Forms.Button();
            this.flp_subjects = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_subject1 = new System.Windows.Forms.Label();
            this.cb_subject1 = new System.Windows.Forms.ComboBox();
            this.lbl_subject2 = new System.Windows.Forms.Label();
            this.cb_subject2 = new System.Windows.Forms.ComboBox();
            this.lbl_subject3 = new System.Windows.Forms.Label();
            this.cb_subject3 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.flp_teacher_name = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_firstname = new System.Windows.Forms.Label();
            this.tb_firstname = new System.Windows.Forms.TextBox();
            this.lbl_lastname = new System.Windows.Forms.Label();
            this.tb_lastname = new System.Windows.Forms.TextBox();
            this.lbl_shortname = new System.Windows.Forms.Label();
            this.tb_shortname = new System.Windows.Forms.TextBox();
            this.btn_hint = new System.Windows.Forms.Button();
            this.flp_teacher_entitys = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel_main.SuspendLayout();
            this.tlp_edit.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlp_phonenumber.SuspendLayout();
            this.flp_subjects.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flp_teacher_name.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_main
            // 
            this.tableLayoutPanel_main.ColumnCount = 1;
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_main.Controls.Add(this.tlp_edit, 0, 1);
            this.tableLayoutPanel_main.Controls.Add(this.flp_teacher_entitys, 0, 0);
            this.tableLayoutPanel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_main.Name = "tableLayoutPanel_main";
            this.tableLayoutPanel_main.RowCount = 2;
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel_main.Size = new System.Drawing.Size(984, 501);
            this.tableLayoutPanel_main.TabIndex = 0;
            // 
            // tlp_edit
            // 
            this.tlp_edit.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tlp_edit.ColumnCount = 1;
            this.tlp_edit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_edit.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tlp_edit.Controls.Add(this.flp_subjects, 0, 1);
            this.tlp_edit.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_edit.Location = new System.Drawing.Point(0, 381);
            this.tlp_edit.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_edit.Name = "tlp_edit";
            this.tlp_edit.RowCount = 3;
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_edit.Size = new System.Drawing.Size(984, 120);
            this.tlp_edit.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tlp_phonenumber, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_add_teacher, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(984, 40);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tlp_phonenumber
            // 
            this.tlp_phonenumber.Controls.Add(this.lbl_phonenumber);
            this.tlp_phonenumber.Controls.Add(this.tb_phonenumber);
            this.tlp_phonenumber.Location = new System.Drawing.Point(3, 3);
            this.tlp_phonenumber.Name = "tlp_phonenumber";
            this.tlp_phonenumber.Size = new System.Drawing.Size(781, 32);
            this.tlp_phonenumber.TabIndex = 3;
            // 
            // lbl_phonenumber
            // 
            this.lbl_phonenumber.AutoSize = true;
            this.lbl_phonenumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_phonenumber.Location = new System.Drawing.Point(3, 6);
            this.lbl_phonenumber.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_phonenumber.Name = "lbl_phonenumber";
            this.lbl_phonenumber.Size = new System.Drawing.Size(124, 20);
            this.lbl_phonenumber.TabIndex = 0;
            this.lbl_phonenumber.Text = "Telefonnummer:";
            // 
            // tb_phonenumber
            // 
            this.tb_phonenumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_phonenumber.Location = new System.Drawing.Point(133, 3);
            this.tb_phonenumber.Name = "tb_phonenumber";
            this.tb_phonenumber.Size = new System.Drawing.Size(160, 26);
            this.tb_phonenumber.TabIndex = 10;
            // 
            // btn_add_teacher
            // 
            this.btn_add_teacher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add_teacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_teacher.Location = new System.Drawing.Point(831, 3);
            this.btn_add_teacher.Name = "btn_add_teacher";
            this.btn_add_teacher.Size = new System.Drawing.Size(150, 34);
            this.btn_add_teacher.TabIndex = 11;
            this.btn_add_teacher.Text = "Lehrer hinzufügen";
            this.btn_add_teacher.UseVisualStyleBackColor = true;
            this.btn_add_teacher.Click += new System.EventHandler(this.btn_add_teacher_Click);
            // 
            // flp_subjects
            // 
            this.flp_subjects.Controls.Add(this.lbl_subject1);
            this.flp_subjects.Controls.Add(this.cb_subject1);
            this.flp_subjects.Controls.Add(this.lbl_subject2);
            this.flp_subjects.Controls.Add(this.cb_subject2);
            this.flp_subjects.Controls.Add(this.lbl_subject3);
            this.flp_subjects.Controls.Add(this.cb_subject3);
            this.flp_subjects.Location = new System.Drawing.Point(3, 43);
            this.flp_subjects.Name = "flp_subjects";
            this.flp_subjects.Size = new System.Drawing.Size(781, 32);
            this.flp_subjects.TabIndex = 2;
            // 
            // lbl_subject1
            // 
            this.lbl_subject1.AutoSize = true;
            this.lbl_subject1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subject1.Location = new System.Drawing.Point(3, 6);
            this.lbl_subject1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_subject1.Name = "lbl_subject1";
            this.lbl_subject1.Size = new System.Drawing.Size(68, 20);
            this.lbl_subject1.TabIndex = 0;
            this.lbl_subject1.Text = "Fach 1*:";
            // 
            // cb_subject1
            // 
            this.cb_subject1.DropDownHeight = 100;
            this.cb_subject1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_subject1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_subject1.FormattingEnabled = true;
            this.cb_subject1.IntegralHeight = false;
            this.cb_subject1.Location = new System.Drawing.Point(77, 3);
            this.cb_subject1.MaximumSize = new System.Drawing.Size(180, 0);
            this.cb_subject1.Name = "cb_subject1";
            this.cb_subject1.Size = new System.Drawing.Size(180, 28);
            this.cb_subject1.TabIndex = 10;
            // 
            // lbl_subject2
            // 
            this.lbl_subject2.AutoSize = true;
            this.lbl_subject2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subject2.Location = new System.Drawing.Point(263, 6);
            this.lbl_subject2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_subject2.Name = "lbl_subject2";
            this.lbl_subject2.Size = new System.Drawing.Size(62, 20);
            this.lbl_subject2.TabIndex = 5;
            this.lbl_subject2.Text = "Fach 2:";
            // 
            // cb_subject2
            // 
            this.cb_subject2.DropDownHeight = 100;
            this.cb_subject2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_subject2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_subject2.FormattingEnabled = true;
            this.cb_subject2.IntegralHeight = false;
            this.cb_subject2.Location = new System.Drawing.Point(331, 3);
            this.cb_subject2.MaximumSize = new System.Drawing.Size(180, 0);
            this.cb_subject2.Name = "cb_subject2";
            this.cb_subject2.Size = new System.Drawing.Size(180, 28);
            this.cb_subject2.TabIndex = 11;
            // 
            // lbl_subject3
            // 
            this.lbl_subject3.AutoSize = true;
            this.lbl_subject3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subject3.Location = new System.Drawing.Point(517, 6);
            this.lbl_subject3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_subject3.Name = "lbl_subject3";
            this.lbl_subject3.Size = new System.Drawing.Size(62, 20);
            this.lbl_subject3.TabIndex = 6;
            this.lbl_subject3.Text = "Fach 3:";
            // 
            // cb_subject3
            // 
            this.cb_subject3.DropDownHeight = 100;
            this.cb_subject3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_subject3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_subject3.FormattingEnabled = true;
            this.cb_subject3.IntegralHeight = false;
            this.cb_subject3.Location = new System.Drawing.Point(585, 3);
            this.cb_subject3.MaximumSize = new System.Drawing.Size(180, 0);
            this.cb_subject3.Name = "cb_subject3";
            this.cb_subject3.Size = new System.Drawing.Size(180, 28);
            this.cb_subject3.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.19068F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.80932F));
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flp_teacher_name, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 40);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(889, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 34);
            this.btn_cancel.TabIndex = 12;
            this.btn_cancel.TabStop = false;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // flp_teacher_name
            // 
            this.flp_teacher_name.Controls.Add(this.lbl_firstname);
            this.flp_teacher_name.Controls.Add(this.tb_firstname);
            this.flp_teacher_name.Controls.Add(this.lbl_lastname);
            this.flp_teacher_name.Controls.Add(this.tb_lastname);
            this.flp_teacher_name.Controls.Add(this.lbl_shortname);
            this.flp_teacher_name.Controls.Add(this.tb_shortname);
            this.flp_teacher_name.Controls.Add(this.btn_hint);
            this.flp_teacher_name.Location = new System.Drawing.Point(3, 3);
            this.flp_teacher_name.Name = "flp_teacher_name";
            this.flp_teacher_name.Size = new System.Drawing.Size(781, 32);
            this.flp_teacher_name.TabIndex = 1;
            // 
            // lbl_firstname
            // 
            this.lbl_firstname.AutoSize = true;
            this.lbl_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_firstname.Location = new System.Drawing.Point(3, 6);
            this.lbl_firstname.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_firstname.Name = "lbl_firstname";
            this.lbl_firstname.Size = new System.Drawing.Size(84, 20);
            this.lbl_firstname.TabIndex = 0;
            this.lbl_firstname.Text = "Vorname*:";
            // 
            // tb_firstname
            // 
            this.tb_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_firstname.Location = new System.Drawing.Point(93, 3);
            this.tb_firstname.Name = "tb_firstname";
            this.tb_firstname.Size = new System.Drawing.Size(160, 26);
            this.tb_firstname.TabIndex = 4;
            this.tb_firstname.TextChanged += new System.EventHandler(this.tb_firstname_TextChanged);
            // 
            // lbl_lastname
            // 
            this.lbl_lastname.AutoSize = true;
            this.lbl_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lastname.Location = new System.Drawing.Point(259, 6);
            this.lbl_lastname.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_lastname.Name = "lbl_lastname";
            this.lbl_lastname.Size = new System.Drawing.Size(96, 20);
            this.lbl_lastname.TabIndex = 0;
            this.lbl_lastname.Text = "Nachname*:";
            // 
            // tb_lastname
            // 
            this.tb_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lastname.Location = new System.Drawing.Point(361, 3);
            this.tb_lastname.Name = "tb_lastname";
            this.tb_lastname.Size = new System.Drawing.Size(160, 26);
            this.tb_lastname.TabIndex = 5;
            this.tb_lastname.TextChanged += new System.EventHandler(this.tb_firstname_TextChanged);
            // 
            // lbl_shortname
            // 
            this.lbl_shortname.AutoSize = true;
            this.lbl_shortname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shortname.Location = new System.Drawing.Point(527, 6);
            this.lbl_shortname.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_shortname.Name = "lbl_shortname";
            this.lbl_shortname.Size = new System.Drawing.Size(63, 20);
            this.lbl_shortname.TabIndex = 0;
            this.lbl_shortname.Text = "Kürzel*:";
            // 
            // tb_shortname
            // 
            this.tb_shortname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_shortname.Location = new System.Drawing.Point(596, 3);
            this.tb_shortname.Name = "tb_shortname";
            this.tb_shortname.Size = new System.Drawing.Size(60, 26);
            this.tb_shortname.TabIndex = 6;
            // 
            // btn_hint
            // 
            this.btn_hint.BackColor = System.Drawing.SystemColors.Control;
            this.btn_hint.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_hint.FlatAppearance.BorderSize = 2;
            this.btn_hint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_hint.Location = new System.Drawing.Point(662, 3);
            this.btn_hint.Name = "btn_hint";
            this.btn_hint.Size = new System.Drawing.Size(50, 26);
            this.btn_hint.TabIndex = 8;
            this.btn_hint.Text = "Tipp";
            this.btn_hint.UseVisualStyleBackColor = false;
            this.btn_hint.Visible = false;
            this.btn_hint.Click += new System.EventHandler(this.btn_hint_Click);
            // 
            // flp_teacher_entitys
            // 
            this.flp_teacher_entitys.AutoScroll = true;
            this.flp_teacher_entitys.BackColor = System.Drawing.Color.Silver;
            this.flp_teacher_entitys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_teacher_entitys.Location = new System.Drawing.Point(0, 0);
            this.flp_teacher_entitys.Margin = new System.Windows.Forms.Padding(0);
            this.flp_teacher_entitys.Name = "flp_teacher_entitys";
            this.flp_teacher_entitys.Size = new System.Drawing.Size(984, 381);
            this.flp_teacher_entitys.TabIndex = 20;
            this.flp_teacher_entitys.SizeChanged += new System.EventHandler(this.flp_teacher_entitys_SizeChanged);
            // 
            // FormTeacherData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 501);
            this.Controls.Add(this.tableLayoutPanel_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTeacherData";
            this.Text = "Lehrer hinzufügen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel_main.ResumeLayout(false);
            this.tlp_edit.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tlp_phonenumber.ResumeLayout(false);
            this.tlp_phonenumber.PerformLayout();
            this.flp_subjects.ResumeLayout(false);
            this.flp_subjects.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flp_teacher_name.ResumeLayout(false);
            this.flp_teacher_name.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_main;
        private System.Windows.Forms.TableLayoutPanel tlp_edit;
        private System.Windows.Forms.FlowLayoutPanel flp_subjects;
        private System.Windows.Forms.TextBox tb_firstname;
        private System.Windows.Forms.FlowLayoutPanel flp_teacher_entitys;
        private System.Windows.Forms.Label lbl_firstname;
        private System.Windows.Forms.TextBox tb_lastname;
        private System.Windows.Forms.Label lbl_lastname;
        private System.Windows.Forms.Label lbl_shortname;
        private System.Windows.Forms.TextBox tb_shortname;
        private System.Windows.Forms.FlowLayoutPanel tlp_phonenumber;
        private System.Windows.Forms.Label lbl_subject1;
        private System.Windows.Forms.Label lbl_subject2;
        private System.Windows.Forms.Label lbl_subject3;
        private System.Windows.Forms.FlowLayoutPanel flp_teacher_name;
        private System.Windows.Forms.TextBox tb_phonenumber;
        private System.Windows.Forms.Label lbl_phonenumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_add_teacher;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_hint;
        private System.Windows.Forms.ComboBox cb_subject1;
        private System.Windows.Forms.ComboBox cb_subject2;
        private System.Windows.Forms.ComboBox cb_subject3;
    }
}