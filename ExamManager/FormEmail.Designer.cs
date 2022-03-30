
namespace ExamManager
{
    partial class FormEmail
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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.rtb_email_text = new System.Windows.Forms.RichTextBox();
            this.flp_title = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_email_titel = new System.Windows.Forms.Label();
            this.tb_email_title = new System.Windows.Forms.TextBox();
            this.flp_var_btns = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_vars = new System.Windows.Forms.Label();
            this.tlp_receivers = new System.Windows.Forms.TableLayoutPanel();
            this.rtb_receivers = new System.Windows.Forms.RichTextBox();
            this.flp_receiveroptions = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_receivers = new System.Windows.Forms.Label();
            this.tlp_bottom = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_attachment = new System.Windows.Forms.Label();
            this.cb_fullexamlist = new System.Windows.Forms.CheckBox();
            this.cb_teacher_examlist = new System.Windows.Forms.CheckBox();
            this.cb_fulltimeline = new System.Windows.Forms.CheckBox();
            this.cb_teacher_timeline = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_preview = new System.Windows.Forms.Button();
            this.tlp_main.SuspendLayout();
            this.flp_title.SuspendLayout();
            this.flp_var_btns.SuspendLayout();
            this.tlp_receivers.SuspendLayout();
            this.flp_receiveroptions.SuspendLayout();
            this.tlp_bottom.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.rtb_email_text, 0, 2);
            this.tlp_main.Controls.Add(this.flp_title, 0, 1);
            this.tlp_main.Controls.Add(this.flp_var_btns, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_receivers, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_bottom, 0, 4);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 5;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(944, 501);
            this.tlp_main.TabIndex = 0;
            // 
            // rtb_email_text
            // 
            this.rtb_email_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_email_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_email_text.Location = new System.Drawing.Point(3, 123);
            this.rtb_email_text.Name = "rtb_email_text";
            this.rtb_email_text.Size = new System.Drawing.Size(938, 295);
            this.rtb_email_text.TabIndex = 2;
            this.rtb_email_text.Text = "";
            this.rtb_email_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_email_text_KeyDown);
            this.rtb_email_text.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtb_email_text_MouseDown);
            // 
            // flp_title
            // 
            this.flp_title.Controls.Add(this.lbl_email_titel);
            this.flp_title.Controls.Add(this.tb_email_title);
            this.flp_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_title.Location = new System.Drawing.Point(3, 83);
            this.flp_title.Name = "flp_title";
            this.flp_title.Size = new System.Drawing.Size(938, 34);
            this.flp_title.TabIndex = 2;
            // 
            // lbl_email_titel
            // 
            this.lbl_email_titel.AutoSize = true;
            this.lbl_email_titel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email_titel.Location = new System.Drawing.Point(3, 6);
            this.lbl_email_titel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_email_titel.Name = "lbl_email_titel";
            this.lbl_email_titel.Size = new System.Drawing.Size(42, 20);
            this.lbl_email_titel.TabIndex = 0;
            this.lbl_email_titel.Text = "Titel:";
            // 
            // tb_email_title
            // 
            this.tb_email_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_email_title.Location = new System.Drawing.Point(51, 3);
            this.tb_email_title.Name = "tb_email_title";
            this.tb_email_title.Size = new System.Drawing.Size(600, 26);
            this.tb_email_title.TabIndex = 1;
            // 
            // flp_var_btns
            // 
            this.flp_var_btns.Controls.Add(this.lbl_vars);
            this.flp_var_btns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_var_btns.Location = new System.Drawing.Point(1, 422);
            this.flp_var_btns.Margin = new System.Windows.Forms.Padding(1);
            this.flp_var_btns.Name = "flp_var_btns";
            this.flp_var_btns.Size = new System.Drawing.Size(942, 38);
            this.flp_var_btns.TabIndex = 4;
            // 
            // lbl_vars
            // 
            this.lbl_vars.AutoSize = true;
            this.lbl_vars.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_vars.Location = new System.Drawing.Point(3, 6);
            this.lbl_vars.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lbl_vars.Name = "lbl_vars";
            this.lbl_vars.Size = new System.Drawing.Size(80, 20);
            this.lbl_vars.TabIndex = 1;
            this.lbl_vars.Text = "Variablen:";
            // 
            // tlp_receivers
            // 
            this.tlp_receivers.ColumnCount = 3;
            this.tlp_receivers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_receivers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_receivers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_receivers.Controls.Add(this.rtb_receivers, 1, 0);
            this.tlp_receivers.Controls.Add(this.flp_receiveroptions, 0, 0);
            this.tlp_receivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_receivers.Location = new System.Drawing.Point(3, 3);
            this.tlp_receivers.Name = "tlp_receivers";
            this.tlp_receivers.RowCount = 1;
            this.tlp_receivers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_receivers.Size = new System.Drawing.Size(938, 74);
            this.tlp_receivers.TabIndex = 7;
            // 
            // rtb_receivers
            // 
            this.rtb_receivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_receivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_receivers.Location = new System.Drawing.Point(103, 3);
            this.rtb_receivers.Name = "rtb_receivers";
            this.rtb_receivers.ReadOnly = true;
            this.rtb_receivers.Size = new System.Drawing.Size(782, 68);
            this.rtb_receivers.TabIndex = 2;
            this.rtb_receivers.Text = "";
            // 
            // flp_receiveroptions
            // 
            this.flp_receiveroptions.Controls.Add(this.lbl_receivers);
            this.flp_receiveroptions.Location = new System.Drawing.Point(1, 1);
            this.flp_receiveroptions.Margin = new System.Windows.Forms.Padding(1);
            this.flp_receiveroptions.Name = "flp_receiveroptions";
            this.flp_receiveroptions.Size = new System.Drawing.Size(98, 72);
            this.flp_receiveroptions.TabIndex = 3;
            // 
            // lbl_receivers
            // 
            this.lbl_receivers.AutoSize = true;
            this.lbl_receivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_receivers.Location = new System.Drawing.Point(3, 6);
            this.lbl_receivers.Margin = new System.Windows.Forms.Padding(3, 6, 0, 3);
            this.lbl_receivers.Name = "lbl_receivers";
            this.lbl_receivers.Size = new System.Drawing.Size(92, 20);
            this.lbl_receivers.TabIndex = 1;
            this.lbl_receivers.Text = "Empfänger:";
            // 
            // tlp_bottom
            // 
            this.tlp_bottom.ColumnCount = 2;
            this.tlp_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlp_bottom.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tlp_bottom.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tlp_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_bottom.Location = new System.Drawing.Point(1, 462);
            this.tlp_bottom.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_bottom.Name = "tlp_bottom";
            this.tlp_bottom.RowCount = 1;
            this.tlp_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_bottom.Size = new System.Drawing.Size(942, 38);
            this.tlp_bottom.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbl_attachment);
            this.flowLayoutPanel1.Controls.Add(this.cb_fullexamlist);
            this.flowLayoutPanel1.Controls.Add(this.cb_teacher_examlist);
            this.flowLayoutPanel1.Controls.Add(this.cb_fulltimeline);
            this.flowLayoutPanel1.Controls.Add(this.cb_teacher_timeline);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(740, 36);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lbl_attachment
            // 
            this.lbl_attachment.AutoSize = true;
            this.lbl_attachment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_attachment.Location = new System.Drawing.Point(3, 6);
            this.lbl_attachment.Margin = new System.Windows.Forms.Padding(3, 6, 0, 3);
            this.lbl_attachment.Name = "lbl_attachment";
            this.lbl_attachment.Size = new System.Drawing.Size(78, 20);
            this.lbl_attachment.TabIndex = 2;
            this.lbl_attachment.Text = "Anhänge:";
            // 
            // cb_fullexamlist
            // 
            this.cb_fullexamlist.AutoSize = true;
            this.cb_fullexamlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_fullexamlist.Location = new System.Drawing.Point(84, 3);
            this.cb_fullexamlist.Name = "cb_fullexamlist";
            this.cb_fullexamlist.Size = new System.Drawing.Size(120, 24);
            this.cb_fullexamlist.TabIndex = 0;
            this.cb_fullexamlist.Text = "Prüfungsliste";
            this.cb_fullexamlist.UseVisualStyleBackColor = true;
            // 
            // cb_teacher_examlist
            // 
            this.cb_teacher_examlist.AutoSize = true;
            this.cb_teacher_examlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_teacher_examlist.Location = new System.Drawing.Point(210, 3);
            this.cb_teacher_examlist.Name = "cb_teacher_examlist";
            this.cb_teacher_examlist.Size = new System.Drawing.Size(170, 24);
            this.cb_teacher_examlist.TabIndex = 1;
            this.cb_teacher_examlist.Text = "Lehrer Prüfungsliste";
            this.cb_teacher_examlist.UseVisualStyleBackColor = true;
            // 
            // cb_fulltimeline
            // 
            this.cb_fulltimeline.AutoSize = true;
            this.cb_fulltimeline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_fulltimeline.Location = new System.Drawing.Point(386, 3);
            this.cb_fulltimeline.Name = "cb_fulltimeline";
            this.cb_fulltimeline.Size = new System.Drawing.Size(98, 24);
            this.cb_fulltimeline.TabIndex = 3;
            this.cb_fulltimeline.Text = "Zeitachse";
            this.cb_fulltimeline.UseVisualStyleBackColor = true;
            // 
            // cb_teacher_timeline
            // 
            this.cb_teacher_timeline.AutoSize = true;
            this.cb_teacher_timeline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_teacher_timeline.Location = new System.Drawing.Point(490, 3);
            this.cb_teacher_timeline.Name = "cb_teacher_timeline";
            this.cb_teacher_timeline.Size = new System.Drawing.Size(148, 24);
            this.cb_teacher_timeline.TabIndex = 4;
            this.cb_teacher_timeline.Text = "Lehrer Zeitachse";
            this.cb_teacher_timeline.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btn_send);
            this.flowLayoutPanel2.Controls.Add(this.btn_preview);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(743, 1);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(198, 36);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.Location = new System.Drawing.Point(115, 3);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(80, 31);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "Senden";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_preview
            // 
            this.btn_preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_preview.Location = new System.Drawing.Point(29, 3);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(80, 31);
            this.btn_preview.TabIndex = 5;
            this.btn_preview.Text = "Vorschau";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // FormEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.tlp_main);
            this.Name = "FormEmail";
            this.ShowIcon = false;
            this.Text = "Email";
            this.Load += new System.EventHandler(this.FormEmail_Load);
            this.tlp_main.ResumeLayout(false);
            this.flp_title.ResumeLayout(false);
            this.flp_title.PerformLayout();
            this.flp_var_btns.ResumeLayout(false);
            this.flp_var_btns.PerformLayout();
            this.tlp_receivers.ResumeLayout(false);
            this.flp_receiveroptions.ResumeLayout(false);
            this.flp_receiveroptions.PerformLayout();
            this.tlp_bottom.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.RichTextBox rtb_email_text;
        private System.Windows.Forms.FlowLayoutPanel flp_title;
        private System.Windows.Forms.Label lbl_email_titel;
        private System.Windows.Forms.TextBox tb_email_title;
        private System.Windows.Forms.FlowLayoutPanel flp_var_btns;
        private System.Windows.Forms.TableLayoutPanel tlp_receivers;
        private System.Windows.Forms.RichTextBox rtb_receivers;
        private System.Windows.Forms.Label lbl_vars;
        private System.Windows.Forms.FlowLayoutPanel flp_receiveroptions;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_preview;
        private System.Windows.Forms.TableLayoutPanel tlp_bottom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lbl_receivers;
        private System.Windows.Forms.Label lbl_attachment;
        private System.Windows.Forms.CheckBox cb_fullexamlist;
        private System.Windows.Forms.CheckBox cb_teacher_examlist;
        private System.Windows.Forms.CheckBox cb_fulltimeline;
        private System.Windows.Forms.CheckBox cb_teacher_timeline;
    }
}