
namespace ExamManager
{
    partial class FormSettings
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
            this.btn_set = new System.Windows.Forms.Button();
            this.flp_top = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_maildomain = new System.Windows.Forms.Label();
            this.tb_maildomain = new System.Windows.Forms.TextBox();
            this.lbl_example = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flp_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_set, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flp_top, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 81);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btn_set
            // 
            this.btn_set.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_set.Location = new System.Drawing.Point(50, 46);
            this.btn_set.Margin = new System.Windows.Forms.Padding(50, 5, 50, 5);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(284, 30);
            this.btn_set.TabIndex = 1;
            this.btn_set.Text = "Übernehmen";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // flp_top
            // 
            this.flp_top.Controls.Add(this.lbl_maildomain);
            this.flp_top.Controls.Add(this.tb_maildomain);
            this.flp_top.Controls.Add(this.lbl_example);
            this.flp_top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_top.Location = new System.Drawing.Point(3, 3);
            this.flp_top.Name = "flp_top";
            this.flp_top.Size = new System.Drawing.Size(378, 35);
            this.flp_top.TabIndex = 2;
            // 
            // lbl_maildomain
            // 
            this.lbl_maildomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_maildomain.AutoSize = true;
            this.lbl_maildomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_maildomain.Location = new System.Drawing.Point(5, 5);
            this.lbl_maildomain.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_maildomain.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_maildomain.Name = "lbl_maildomain";
            this.lbl_maildomain.Size = new System.Drawing.Size(108, 28);
            this.lbl_maildomain.TabIndex = 3;
            this.lbl_maildomain.Text = "Email domain:";
            this.lbl_maildomain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_maildomain
            // 
            this.tb_maildomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_maildomain.Location = new System.Drawing.Point(120, 6);
            this.tb_maildomain.Margin = new System.Windows.Forms.Padding(2, 6, 5, 6);
            this.tb_maildomain.Name = "tb_maildomain";
            this.tb_maildomain.Size = new System.Drawing.Size(130, 26);
            this.tb_maildomain.TabIndex = 4;
            // 
            // lbl_example
            // 
            this.lbl_example.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_example.AutoSize = true;
            this.lbl_example.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_example.Location = new System.Drawing.Point(256, 5);
            this.lbl_example.Margin = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.lbl_example.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_example.Name = "lbl_example";
            this.lbl_example.Size = new System.Drawing.Size(89, 28);
            this.lbl_example.TabIndex = 5;
            this.lbl_example.Text = "Bsp.: mail.de";
            this.lbl_example.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSettings";
            this.Text = "Einstellungen";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flp_top.ResumeLayout(false);
            this.flp_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.FlowLayoutPanel flp_top;
        private System.Windows.Forms.Label lbl_maildomain;
        private System.Windows.Forms.TextBox tb_maildomain;
        private System.Windows.Forms.Label lbl_example;
    }
}