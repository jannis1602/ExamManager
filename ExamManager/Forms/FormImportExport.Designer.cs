
namespace ExamManager
{
    partial class FormImportExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportExport));
            this.flp_main = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_exam = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_exam = new System.Windows.Forms.Label();
            this.btn_exam_export = new System.Windows.Forms.Button();
            this.btn_exam_import = new System.Windows.Forms.Button();
            this.flp_students = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_student = new System.Windows.Forms.Label();
            this.btn_student_export = new System.Windows.Forms.Button();
            this.btn_student_import = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_teacher = new System.Windows.Forms.Label();
            this.btn_teacher_export = new System.Windows.Forms.Button();
            this.btn_teacher_import = new System.Windows.Forms.Button();
            this.flp_main.SuspendLayout();
            this.flp_exam.SuspendLayout();
            this.flp_students.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flp_main
            // 
            this.flp_main.Controls.Add(this.flp_exam);
            this.flp_main.Controls.Add(this.flp_students);
            this.flp_main.Controls.Add(this.flowLayoutPanel1);
            this.flp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_main.Location = new System.Drawing.Point(0, 0);
            this.flp_main.Name = "flp_main";
            this.flp_main.Size = new System.Drawing.Size(678, 349);
            this.flp_main.TabIndex = 0;
            // 
            // flp_exam
            // 
            this.flp_exam.Controls.Add(this.lbl_exam);
            this.flp_exam.Controls.Add(this.btn_exam_export);
            this.flp_exam.Controls.Add(this.btn_exam_import);
            this.flp_exam.Location = new System.Drawing.Point(3, 3);
            this.flp_exam.Name = "flp_exam";
            this.flp_exam.Size = new System.Drawing.Size(675, 32);
            this.flp_exam.TabIndex = 0;
            // 
            // lbl_exam
            // 
            this.lbl_exam.AutoSize = true;
            this.lbl_exam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_exam.Location = new System.Drawing.Point(3, 3);
            this.lbl_exam.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_exam.Name = "lbl_exam";
            this.lbl_exam.Size = new System.Drawing.Size(130, 20);
            this.lbl_exam.TabIndex = 0;
            this.lbl_exam.Text = "Prüfungen (json):";
            // 
            // btn_exam_export
            // 
            this.btn_exam_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exam_export.Location = new System.Drawing.Point(139, 3);
            this.btn_exam_export.Name = "btn_exam_export";
            this.btn_exam_export.Size = new System.Drawing.Size(75, 23);
            this.btn_exam_export.TabIndex = 1;
            this.btn_exam_export.Text = "Export";
            this.btn_exam_export.UseVisualStyleBackColor = true;
            this.btn_exam_export.Click += new System.EventHandler(this.btn_exam_export_Click);
            // 
            // btn_exam_import
            // 
            this.btn_exam_import.Location = new System.Drawing.Point(220, 3);
            this.btn_exam_import.Name = "btn_exam_import";
            this.btn_exam_import.Size = new System.Drawing.Size(75, 23);
            this.btn_exam_import.TabIndex = 2;
            this.btn_exam_import.Text = "Import";
            this.btn_exam_import.UseVisualStyleBackColor = true;
            this.btn_exam_import.Click += new System.EventHandler(this.btn_exam_import_Click);
            // 
            // flp_students
            // 
            this.flp_students.Controls.Add(this.lbl_student);
            this.flp_students.Controls.Add(this.btn_student_export);
            this.flp_students.Controls.Add(this.btn_student_import);
            this.flp_students.Location = new System.Drawing.Point(3, 41);
            this.flp_students.Name = "flp_students";
            this.flp_students.Size = new System.Drawing.Size(675, 32);
            this.flp_students.TabIndex = 1;
            // 
            // lbl_student
            // 
            this.lbl_student.AutoSize = true;
            this.lbl_student.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_student.Location = new System.Drawing.Point(3, 3);
            this.lbl_student.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_student.Name = "lbl_student";
            this.lbl_student.Size = new System.Drawing.Size(110, 20);
            this.lbl_student.TabIndex = 0;
            this.lbl_student.Text = "Schüler (json):";
            // 
            // btn_student_export
            // 
            this.btn_student_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_student_export.Location = new System.Drawing.Point(119, 3);
            this.btn_student_export.Name = "btn_student_export";
            this.btn_student_export.Size = new System.Drawing.Size(75, 23);
            this.btn_student_export.TabIndex = 1;
            this.btn_student_export.Text = "Export";
            this.btn_student_export.UseVisualStyleBackColor = true;
            this.btn_student_export.Click += new System.EventHandler(this.btn_student_export_Click);
            // 
            // btn_student_import
            // 
            this.btn_student_import.Location = new System.Drawing.Point(200, 3);
            this.btn_student_import.Name = "btn_student_import";
            this.btn_student_import.Size = new System.Drawing.Size(75, 23);
            this.btn_student_import.TabIndex = 2;
            this.btn_student_import.Text = "Import";
            this.btn_student_import.UseVisualStyleBackColor = true;
            this.btn_student_import.Click += new System.EventHandler(this.btn_student_import_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_teacher);
            this.flowLayoutPanel1.Controls.Add(this.btn_teacher_export);
            this.flowLayoutPanel1.Controls.Add(this.btn_teacher_import);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 79);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(675, 32);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // lbl_teacher
            // 
            this.lbl_teacher.AutoSize = true;
            this.lbl_teacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_teacher.Location = new System.Drawing.Point(3, 3);
            this.lbl_teacher.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_teacher.Name = "lbl_teacher";
            this.lbl_teacher.Size = new System.Drawing.Size(102, 20);
            this.lbl_teacher.TabIndex = 0;
            this.lbl_teacher.Text = "Lehrer (json):";
            // 
            // btn_teacher_export
            // 
            this.btn_teacher_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_teacher_export.Location = new System.Drawing.Point(111, 3);
            this.btn_teacher_export.Name = "btn_teacher_export";
            this.btn_teacher_export.Size = new System.Drawing.Size(75, 23);
            this.btn_teacher_export.TabIndex = 1;
            this.btn_teacher_export.Text = "Export";
            this.btn_teacher_export.UseVisualStyleBackColor = true;
            this.btn_teacher_export.Click += new System.EventHandler(this.btn_teacher_export_Click);
            // 
            // btn_teacher_import
            // 
            this.btn_teacher_import.Location = new System.Drawing.Point(192, 3);
            this.btn_teacher_import.Name = "btn_teacher_import";
            this.btn_teacher_import.Size = new System.Drawing.Size(75, 23);
            this.btn_teacher_import.TabIndex = 2;
            this.btn_teacher_import.Text = "Import";
            this.btn_teacher_import.UseVisualStyleBackColor = true;
            this.btn_teacher_import.Click += new System.EventHandler(this.btn_teacher_import_Click);
            // 
            // FormImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 349);
            this.Controls.Add(this.flp_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormImportExport";
            this.Text = "Import Export";
            this.flp_main.ResumeLayout(false);
            this.flp_exam.ResumeLayout(false);
            this.flp_exam.PerformLayout();
            this.flp_students.ResumeLayout(false);
            this.flp_students.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp_main;
        private System.Windows.Forms.FlowLayoutPanel flp_students;
        private System.Windows.Forms.Label lbl_student;
        private System.Windows.Forms.Button btn_student_export;
        private System.Windows.Forms.Button btn_student_import;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbl_teacher;
        private System.Windows.Forms.Button btn_teacher_export;
        private System.Windows.Forms.Button btn_teacher_import;
        private System.Windows.Forms.FlowLayoutPanel flp_exam;
        private System.Windows.Forms.Label lbl_exam;
        private System.Windows.Forms.Button btn_exam_export;
        private System.Windows.Forms.Button btn_exam_import;
    }
}