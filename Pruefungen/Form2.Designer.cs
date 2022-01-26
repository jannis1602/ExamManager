
namespace Pruefungen
{
    partial class Form_grid
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exam_room = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.preparation_room = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_vorsitz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_pruefer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_protokoll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.date,
            this.time,
            this.exam_room,
            this.preparation_room,
            this.student,
            this.teacher_vorsitz,
            this.teacher_pruefer,
            this.teacher_protokoll,
            this.subject,
            this.duration});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(881, 217);
            this.dataGridView1.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // date
            // 
            this.date.HeaderText = "Datum";
            this.date.Name = "date";
            // 
            // time
            // 
            this.time.HeaderText = "Zeit";
            this.time.Name = "time";
            // 
            // exam_room
            // 
            this.exam_room.HeaderText = "Prüfungsraum";
            this.exam_room.Name = "exam_room";
            // 
            // preparation_room
            // 
            this.preparation_room.HeaderText = "Vorbereitungsraum";
            this.preparation_room.Name = "preparation_room";
            // 
            // student
            // 
            this.student.HeaderText = "Schüler";
            this.student.Name = "student";
            // 
            // teacher_vorsitz
            // 
            this.teacher_vorsitz.HeaderText = "Lehrer Vorsitz";
            this.teacher_vorsitz.Name = "teacher_vorsitz";
            // 
            // teacher_pruefer
            // 
            this.teacher_pruefer.HeaderText = "Lehrer Prüfer";
            this.teacher_pruefer.Name = "teacher_pruefer";
            // 
            // teacher_protokoll
            // 
            this.teacher_protokoll.HeaderText = "Lehrer Protokoll";
            this.teacher_protokoll.Name = "teacher_protokoll";
            // 
            // subject
            // 
            this.subject.HeaderText = "Fach";
            this.subject.Name = "subject";
            // 
            // duration
            // 
            this.duration.HeaderText = "Dauer";
            this.duration.Name = "duration";
            // 
            // Form_grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 447);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form_grid";
            this.Text = "Tabelle";
            this.Load += new System.EventHandler(this.Form_grid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn exam_room;
        private System.Windows.Forms.DataGridViewTextBoxColumn preparation_room;
        private System.Windows.Forms.DataGridViewTextBoxColumn student;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_vorsitz;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_pruefer;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_protokoll;
        private System.Windows.Forms.DataGridViewTextBoxColumn subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn duration;
    }
}