
namespace ExamManager
{
    partial class FormStudentData
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
            this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_edit = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.flp_teacher_name = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_firstname = new System.Windows.Forms.Label();
            this.tb_firstname = new System.Windows.Forms.TextBox();
            this.lbl_lastname = new System.Windows.Forms.Label();
            this.tb_lastname = new System.Windows.Forms.TextBox();
            this.lbl_grade = new System.Windows.Forms.Label();
            this.tb_grade = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_phonenumber = new System.Windows.Forms.Label();
            this.tb_phonenumber = new System.Windows.Forms.TextBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.btn_email_generate = new System.Windows.Forms.Button();
            this.btn_add_student = new System.Windows.Forms.Button();
            this.flp_teacher_entitys = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel_main.SuspendLayout();
            this.tlp_edit.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flp_teacher_name.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel_main.Size = new System.Drawing.Size(984, 501);
            this.tableLayoutPanel_main.TabIndex = 1;
            // 
            // tlp_edit
            // 
            this.tlp_edit.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tlp_edit.ColumnCount = 1;
            this.tlp_edit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_edit.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlp_edit.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tlp_edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_edit.Location = new System.Drawing.Point(0, 421);
            this.tlp_edit.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_edit.Name = "tlp_edit";
            this.tlp_edit.RowCount = 2;
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_edit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_edit.Size = new System.Drawing.Size(984, 80);
            this.tlp_edit.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.19068F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.80932F));
            this.tableLayoutPanel2.Controls.Add(this.btn_cancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flp_teacher_name, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(984, 40);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(889, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 34);
            this.btn_cancel.TabIndex = 8;
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
            this.flp_teacher_name.Controls.Add(this.lbl_grade);
            this.flp_teacher_name.Controls.Add(this.tb_grade);
            this.flp_teacher_name.Location = new System.Drawing.Point(3, 3);
            this.flp_teacher_name.Name = "flp_teacher_name";
            this.flp_teacher_name.Size = new System.Drawing.Size(783, 32);
            this.flp_teacher_name.TabIndex = 1;
            // 
            // lbl_firstname
            // 
            this.lbl_firstname.AutoSize = true;
            this.lbl_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_firstname.Location = new System.Drawing.Point(3, 6);
            this.lbl_firstname.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_firstname.Name = "lbl_firstname";
            this.lbl_firstname.Size = new System.Drawing.Size(78, 20);
            this.lbl_firstname.TabIndex = 3;
            this.lbl_firstname.Text = "Vorname:";
            // 
            // tb_firstname
            // 
            this.tb_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_firstname.Location = new System.Drawing.Point(87, 3);
            this.tb_firstname.Name = "tb_firstname";
            this.tb_firstname.Size = new System.Drawing.Size(160, 26);
            this.tb_firstname.TabIndex = 1;
            // 
            // lbl_lastname
            // 
            this.lbl_lastname.AutoSize = true;
            this.lbl_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lastname.Location = new System.Drawing.Point(253, 6);
            this.lbl_lastname.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_lastname.Name = "lbl_lastname";
            this.lbl_lastname.Size = new System.Drawing.Size(90, 20);
            this.lbl_lastname.TabIndex = 4;
            this.lbl_lastname.Text = "Nachname:";
            // 
            // tb_lastname
            // 
            this.tb_lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lastname.Location = new System.Drawing.Point(349, 3);
            this.tb_lastname.Name = "tb_lastname";
            this.tb_lastname.Size = new System.Drawing.Size(160, 26);
            this.tb_lastname.TabIndex = 2;
            // 
            // lbl_grade
            // 
            this.lbl_grade.AutoSize = true;
            this.lbl_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_grade.Location = new System.Drawing.Point(515, 6);
            this.lbl_grade.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_grade.Name = "lbl_grade";
            this.lbl_grade.Size = new System.Drawing.Size(52, 20);
            this.lbl_grade.TabIndex = 5;
            this.lbl_grade.Text = "Stufe:";
            // 
            // tb_grade
            // 
            this.tb_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_grade.Location = new System.Drawing.Point(573, 3);
            this.tb_grade.Name = "tb_grade";
            this.tb_grade.Size = new System.Drawing.Size(60, 26);
            this.tb_grade.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_add_student, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 40);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lbl_phonenumber);
            this.flowLayoutPanel3.Controls.Add(this.tb_phonenumber);
            this.flowLayoutPanel3.Controls.Add(this.lbl_email);
            this.flowLayoutPanel3.Controls.Add(this.tb_email);
            this.flowLayoutPanel3.Controls.Add(this.btn_email_generate);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(763, 32);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // lbl_phonenumber
            // 
            this.lbl_phonenumber.AutoSize = true;
            this.lbl_phonenumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_phonenumber.Location = new System.Drawing.Point(3, 6);
            this.lbl_phonenumber.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_phonenumber.Name = "lbl_phonenumber";
            this.lbl_phonenumber.Size = new System.Drawing.Size(124, 20);
            this.lbl_phonenumber.TabIndex = 3;
            this.lbl_phonenumber.Text = "Telefonnummer:";
            // 
            // tb_phonenumber
            // 
            this.tb_phonenumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_phonenumber.Location = new System.Drawing.Point(133, 3);
            this.tb_phonenumber.Name = "tb_phonenumber";
            this.tb_phonenumber.Size = new System.Drawing.Size(160, 26);
            this.tb_phonenumber.TabIndex = 4;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email.Location = new System.Drawing.Point(299, 6);
            this.lbl_email.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(52, 20);
            this.lbl_email.TabIndex = 4;
            this.lbl_email.Text = "Email:";
            // 
            // tb_email
            // 
            this.tb_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_email.Location = new System.Drawing.Point(357, 3);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(240, 26);
            this.tb_email.TabIndex = 5;
            // 
            // btn_email_generate
            // 
            this.btn_email_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_email_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_email_generate.Location = new System.Drawing.Point(603, 3);
            this.btn_email_generate.Name = "btn_email_generate";
            this.btn_email_generate.Size = new System.Drawing.Size(124, 26);
            this.btn_email_generate.TabIndex = 6;
            this.btn_email_generate.Text = "Email generieren";
            this.btn_email_generate.UseVisualStyleBackColor = true;
            this.btn_email_generate.Click += new System.EventHandler(this.btn_email_generate_Click);
            // 
            // btn_add_student
            // 
            this.btn_add_student.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add_student.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_student.Location = new System.Drawing.Point(831, 3);
            this.btn_add_student.Name = "btn_add_student";
            this.btn_add_student.Size = new System.Drawing.Size(150, 34);
            this.btn_add_student.TabIndex = 7;
            this.btn_add_student.Text = "Schüler hinzufügen";
            this.btn_add_student.UseVisualStyleBackColor = true;
            this.btn_add_student.Click += new System.EventHandler(this.btn_add_student_Click);
            // 
            // flp_teacher_entitys
            // 
            this.flp_teacher_entitys.AutoScroll = true;
            this.flp_teacher_entitys.BackColor = System.Drawing.Color.Silver;
            this.flp_teacher_entitys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_teacher_entitys.Location = new System.Drawing.Point(0, 0);
            this.flp_teacher_entitys.Margin = new System.Windows.Forms.Padding(0);
            this.flp_teacher_entitys.Name = "flp_teacher_entitys";
            this.flp_teacher_entitys.Size = new System.Drawing.Size(984, 421);
            this.flp_teacher_entitys.TabIndex = 1;
            // 
            // FormStudentData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 501);
            this.Controls.Add(this.tableLayoutPanel_main);
            this.Name = "FormStudentData";
            this.Text = "Schüler hinzufügen";
            this.Load += new System.EventHandler(this.FormStudentData_Load);
            this.tableLayoutPanel_main.ResumeLayout(false);
            this.tlp_edit.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flp_teacher_name.ResumeLayout(false);
            this.flp_teacher_name.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_main;
        private System.Windows.Forms.TableLayoutPanel tlp_edit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lbl_phonenumber;
        private System.Windows.Forms.TextBox tb_phonenumber;
        private System.Windows.Forms.Button btn_add_student;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.FlowLayoutPanel flp_teacher_name;
        private System.Windows.Forms.Label lbl_firstname;
        private System.Windows.Forms.TextBox tb_firstname;
        private System.Windows.Forms.Label lbl_lastname;
        private System.Windows.Forms.TextBox tb_lastname;
        private System.Windows.Forms.Label lbl_grade;
        private System.Windows.Forms.TextBox tb_grade;
        private System.Windows.Forms.FlowLayoutPanel flp_teacher_entitys;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.Button btn_email_generate;
    }
}