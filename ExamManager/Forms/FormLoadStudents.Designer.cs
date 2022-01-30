
namespace ExamManager
{
    partial class FormLoadStudents
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
            this.lbl_existinggrade = new System.Windows.Forms.Label();
            this.cb_existinggrade = new System.Windows.Forms.ComboBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_newgrade = new System.Windows.Forms.Label();
            this.tb_newgrade = new System.Windows.Forms.TextBox();
            this.cb_mailgenerator = new System.Windows.Forms.CheckBox();
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
            this.tableLayoutPanel1.Controls.Add(this.btn_add, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_mailgenerator, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 161);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lbl_existinggrade);
            this.flowLayoutPanel2.Controls.Add(this.cb_existinggrade);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(382, 38);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // lbl_existinggrade
            // 
            this.lbl_existinggrade.AutoSize = true;
            this.lbl_existinggrade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_existinggrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_existinggrade.Location = new System.Drawing.Point(5, 9);
            this.lbl_existinggrade.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_existinggrade.Name = "lbl_existinggrade";
            this.lbl_existinggrade.Size = new System.Drawing.Size(140, 20);
            this.lbl_existinggrade.TabIndex = 0;
            this.lbl_existinggrade.Text = "vorhandene Stufe:";
            this.lbl_existinggrade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_existinggrade
            // 
            this.cb_existinggrade.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_existinggrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_existinggrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_existinggrade.FormattingEnabled = true;
            this.cb_existinggrade.Location = new System.Drawing.Point(153, 3);
            this.cb_existinggrade.Name = "cb_existinggrade";
            this.cb_existinggrade.Size = new System.Drawing.Size(112, 28);
            this.cb_existinggrade.TabIndex = 9;
            this.cb_existinggrade.SelectedIndexChanged += new System.EventHandler(this.cb_existinggrade_SelectedIndexChanged);
            // 
            // btn_add
            // 
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_add.Location = new System.Drawing.Point(50, 125);
            this.btn_add.Margin = new System.Windows.Forms.Padding(50, 5, 50, 5);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(284, 31);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_newgrade);
            this.flowLayoutPanel1.Controls.Add(this.tb_newgrade);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 41);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(382, 38);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // lbl_newgrade
            // 
            this.lbl_newgrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_newgrade.AutoSize = true;
            this.lbl_newgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_newgrade.Location = new System.Drawing.Point(5, 5);
            this.lbl_newgrade.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_newgrade.Name = "lbl_newgrade";
            this.lbl_newgrade.Size = new System.Drawing.Size(92, 28);
            this.lbl_newgrade.TabIndex = 0;
            this.lbl_newgrade.Text = "neue Stufe:";
            this.lbl_newgrade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_newgrade
            // 
            this.tb_newgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_newgrade.Location = new System.Drawing.Point(104, 6);
            this.tb_newgrade.Margin = new System.Windows.Forms.Padding(2, 6, 20, 6);
            this.tb_newgrade.Name = "tb_newgrade";
            this.tb_newgrade.Size = new System.Drawing.Size(108, 26);
            this.tb_newgrade.TabIndex = 2;
            // 
            // cb_mailgenerator
            // 
            this.cb_mailgenerator.AutoSize = true;
            this.cb_mailgenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_mailgenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_mailgenerator.Location = new System.Drawing.Point(3, 83);
            this.cb_mailgenerator.Name = "cb_mailgenerator";
            this.cb_mailgenerator.Size = new System.Drawing.Size(378, 34);
            this.cb_mailgenerator.TabIndex = 4;
            this.cb_mailgenerator.Text = "Emails generieren";
            this.cb_mailgenerator.UseVisualStyleBackColor = true;
            // 
            // FormLoadStudents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoadStudents";
            this.Text = "Schüler laden";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lbl_existinggrade;
        private System.Windows.Forms.ComboBox cb_existinggrade;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbl_newgrade;
        private System.Windows.Forms.TextBox tb_newgrade;
        private System.Windows.Forms.CheckBox cb_mailgenerator;
    }
}