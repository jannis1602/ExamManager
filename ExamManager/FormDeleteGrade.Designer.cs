
namespace ExamManager
{
    partial class FormDeleteGrade
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_delete = new System.Windows.Forms.Button();
            this.flp_top = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_grade = new System.Windows.Forms.Label();
            this.cb_grade = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flp_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_delete, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flp_top, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 81);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_delete
            // 
            this.btn_delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_delete.Location = new System.Drawing.Point(50, 46);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(50, 5, 50, 5);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(284, 30);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // flp_top
            // 
            this.flp_top.Controls.Add(this.lbl_grade);
            this.flp_top.Controls.Add(this.cb_grade);
            this.flp_top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_top.Location = new System.Drawing.Point(3, 3);
            this.flp_top.Name = "flp_top";
            this.flp_top.Size = new System.Drawing.Size(378, 35);
            this.flp_top.TabIndex = 2;
            // 
            // lbl_grade
            // 
            this.lbl_grade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_grade.AutoSize = true;
            this.lbl_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_grade.Location = new System.Drawing.Point(5, 5);
            this.lbl_grade.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_grade.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_grade.Name = "lbl_grade";
            this.lbl_grade.Size = new System.Drawing.Size(60, 24);
            this.lbl_grade.TabIndex = 3;
            this.lbl_grade.Text = "Stufe:";
            this.lbl_grade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_grade
            // 
            this.cb_grade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_grade.FormattingEnabled = true;
            this.cb_grade.Location = new System.Drawing.Point(73, 3);
            this.cb_grade.MaximumSize = new System.Drawing.Size(100, 0);
            this.cb_grade.Name = "cb_grade";
            this.cb_grade.Size = new System.Drawing.Size(100, 28);
            this.cb_grade.TabIndex = 9;
            // 
            // FormDeleteGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormDeleteGrade";
            this.Text = "Stufe löschen";
            this.Load += new System.EventHandler(this.FormDeleteGrade_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flp_top.ResumeLayout(false);
            this.flp_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.FlowLayoutPanel flp_top;
        private System.Windows.Forms.Label lbl_grade;
        private System.Windows.Forms.ComboBox cb_grade;
    }
}