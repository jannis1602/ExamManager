
namespace ExamManager
{
    partial class FormRenameGrade
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_oldroom = new System.Windows.Forms.Label();
            this.cb_grade = new System.Windows.Forms.ComboBox();
            this.btn_change = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_newroom = new System.Windows.Forms.Label();
            this.tb_new = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_change, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 121);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lbl_oldroom);
            this.flowLayoutPanel2.Controls.Add(this.cb_grade);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(382, 38);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // lbl_oldroom
            // 
            this.lbl_oldroom.AutoSize = true;
            this.lbl_oldroom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_oldroom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_oldroom.Location = new System.Drawing.Point(5, 9);
            this.lbl_oldroom.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_oldroom.Name = "lbl_oldroom";
            this.lbl_oldroom.Size = new System.Drawing.Size(52, 20);
            this.lbl_oldroom.TabIndex = 0;
            this.lbl_oldroom.Text = "Stufe:";
            this.lbl_oldroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_grade
            // 
            this.cb_grade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_grade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_grade.FormattingEnabled = true;
            this.cb_grade.Location = new System.Drawing.Point(65, 3);
            this.cb_grade.Name = "cb_grade";
            this.cb_grade.Size = new System.Drawing.Size(112, 28);
            this.cb_grade.TabIndex = 9;
            // 
            // btn_change
            // 
            this.btn_change.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_change.Location = new System.Drawing.Point(50, 85);
            this.btn_change.Margin = new System.Windows.Forms.Padding(50, 5, 50, 5);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(284, 31);
            this.btn_change.TabIndex = 0;
            this.btn_change.Text = "Übernehmen";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_newroom);
            this.flowLayoutPanel1.Controls.Add(this.tb_new);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 41);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(382, 38);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // lbl_newroom
            // 
            this.lbl_newroom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_newroom.AutoSize = true;
            this.lbl_newroom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_newroom.Location = new System.Drawing.Point(5, 5);
            this.lbl_newroom.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_newroom.Name = "lbl_newroom";
            this.lbl_newroom.Size = new System.Drawing.Size(100, 28);
            this.lbl_newroom.TabIndex = 0;
            this.lbl_newroom.Text = "neuer Name:";
            this.lbl_newroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_new
            // 
            this.tb_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_new.Location = new System.Drawing.Point(112, 6);
            this.tb_new.Margin = new System.Windows.Forms.Padding(2, 6, 20, 6);
            this.tb_new.Name = "tb_new";
            this.tb_new.Size = new System.Drawing.Size(108, 26);
            this.tb_new.TabIndex = 2;
            // 
            // FormRenameGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 121);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormRenameGrade";
            this.Text = "Stufe umbenennen";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lbl_oldroom;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbl_newroom;
        private System.Windows.Forms.TextBox tb_new;
        private System.Windows.Forms.ComboBox cb_grade;
    }
}