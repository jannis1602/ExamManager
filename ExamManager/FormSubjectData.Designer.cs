﻿
namespace ExamManager
{
    partial class FormSubjectData
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
            this.btn_add = new System.Windows.Forms.Button();
            this.tb_add = new System.Windows.Forms.TextBox();
            this.lb_subjectlist = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_add, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_add, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_subjectlist, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 211);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btn_add
            // 
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_add.Location = new System.Drawing.Point(50, 171);
            this.btn_add.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(284, 30);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // tb_add
            // 
            this.tb_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_add.Location = new System.Drawing.Point(30, 120);
            this.tb_add.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.tb_add.Name = "tb_add";
            this.tb_add.Size = new System.Drawing.Size(324, 26);
            this.tb_add.TabIndex = 0;
            this.tb_add.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_add_KeyPress);
            // 
            // lb_subjectlist
            // 
            this.lb_subjectlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_subjectlist.FormattingEnabled = true;
            this.lb_subjectlist.Location = new System.Drawing.Point(3, 3);
            this.lb_subjectlist.Name = "lb_subjectlist";
            this.lb_subjectlist.Size = new System.Drawing.Size(378, 99);
            this.lb_subjectlist.TabIndex = 2;
            this.lb_subjectlist.DoubleClick += new System.EventHandler(this.lb_subjectlist_DoubleClick);
            // 
            // FormSubjectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSubjectData";
            this.Text = "Fach hinzufügen";
            this.Load += new System.EventHandler(this.FormSubjectData_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox tb_add;
        private System.Windows.Forms.ListBox lb_subjectlist;
    }
}