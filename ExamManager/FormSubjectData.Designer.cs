
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
            this.tb_add = new System.Windows.Forms.TextBox();
            this.lb_subjectlist = new System.Windows.Forms.ListBox();
            this.tlp_btn = new System.Windows.Forms.TableLayoutPanel();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlp_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tb_add, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_subjectlist, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlp_btn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 361);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tb_add
            // 
            this.tb_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_add.Location = new System.Drawing.Point(30, 270);
            this.tb_add.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.tb_add.Name = "tb_add";
            this.tb_add.Size = new System.Drawing.Size(224, 26);
            this.tb_add.TabIndex = 0;
            this.tb_add.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_add_KeyPress);
            // 
            // lb_subjectlist
            // 
            this.lb_subjectlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_subjectlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_subjectlist.FormattingEnabled = true;
            this.lb_subjectlist.ItemHeight = 16;
            this.lb_subjectlist.Location = new System.Drawing.Point(3, 3);
            this.lb_subjectlist.Name = "lb_subjectlist";
            this.lb_subjectlist.Size = new System.Drawing.Size(278, 249);
            this.lb_subjectlist.TabIndex = 2;
            this.lb_subjectlist.DoubleClick += new System.EventHandler(this.lb_subjectlist_DoubleClick);
            // 
            // tlp_btn
            // 
            this.tlp_btn.ColumnCount = 2;
            this.tlp_btn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_btn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_btn.Controls.Add(this.btn_add, 0, 0);
            this.tlp_btn.Controls.Add(this.btn_delete, 0, 0);
            this.tlp_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_btn.Location = new System.Drawing.Point(3, 314);
            this.tlp_btn.Name = "tlp_btn";
            this.tlp_btn.RowCount = 1;
            this.tlp_btn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_btn.Size = new System.Drawing.Size(278, 44);
            this.tlp_btn.TabIndex = 3;
            // 
            // btn_delete
            // 
            this.btn_delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(20, 8);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(99, 28);
            this.btn_delete.TabIndex = 3;
            this.btn_delete.Text = "Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_add
            // 
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(159, 8);
            this.btn_add.Margin = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(99, 28);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // FormSubjectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSubjectData";
            this.Text = "Fach hinzufügen";
            this.Load += new System.EventHandler(this.FormSubjectData_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlp_btn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tb_add;
        private System.Windows.Forms.ListBox lb_subjectlist;
        private System.Windows.Forms.TableLayoutPanel tlp_btn;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_delete;
    }
}