
namespace ExamManager
{
    partial class FormExamDateListView
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
            this.lb_exam_date = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_exam_date
            // 
            this.lb_exam_date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_exam_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_exam_date.FormattingEnabled = true;
            this.lb_exam_date.ItemHeight = 20;
            this.lb_exam_date.Location = new System.Drawing.Point(0, 0);
            this.lb_exam_date.Name = "lb_exam_date";
            this.lb_exam_date.Size = new System.Drawing.Size(234, 261);
            this.lb_exam_date.TabIndex = 0;
            this.lb_exam_date.DoubleClick += new System.EventHandler(this.lb_exam_date_DoubleClick);
            // 
            // FormExamDateListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 261);
            this.Controls.Add(this.lb_exam_date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormExamDateListView";
            this.Text = "Prüfungstage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_exam_date;
    }
}