
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
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.panel_main = new System.Windows.Forms.TabControl();
            this.tp_common = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_exambreak = new System.Windows.Forms.Label();
            this.dtp_exambreak = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_teacher_nameorder = new System.Windows.Forms.ComboBox();
            this.flp_nameorder = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_nameorder = new System.Windows.Forms.Label();
            this.cb_student_nameorder = new System.Windows.Forms.ComboBox();
            this.flp_settings_options = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_settings_export = new System.Windows.Forms.Button();
            this.btn_settings_import = new System.Windows.Forms.Button();
            this.flp_color = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_color = new System.Windows.Forms.Label();
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.tp_email = new System.Windows.Forms.TabPage();
            this.flp_email_main = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_top = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_maildomain = new System.Windows.Forms.Label();
            this.tb_emaildomain = new System.Windows.Forms.TextBox();
            this.flp_border_smtp = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_border_smtp_server = new System.Windows.Forms.Label();
            this.flp_smtp_server_port = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_smtp_server_name = new System.Windows.Forms.Label();
            this.tb_smtp_server = new System.Windows.Forms.TextBox();
            this.lbl_smtp_server_port = new System.Windows.Forms.Label();
            this.tb_smtp_port = new System.Windows.Forms.TextBox();
            this.flp_smtp_user = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_smtp_email = new System.Windows.Forms.Label();
            this.tb_smtp_email = new System.Windows.Forms.TextBox();
            this.lbl_smtp_pwd = new System.Windows.Forms.Label();
            this.tb_smtp_pwd = new System.Windows.Forms.TextBox();
            this.btn_show_smtp_pwd = new System.Windows.Forms.Button();
            this.flp_smtp_msg = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_smtp_sendername = new System.Windows.Forms.Label();
            this.tb_smtp_sendername = new System.Windows.Forms.TextBox();
            this.lbl_titel = new System.Windows.Forms.Label();
            this.tb_smtp_email_titel = new System.Windows.Forms.TextBox();
            this.tp_database = new System.Windows.Forms.TabPage();
            this.flp_db_main = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_current_db = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_current_database = new System.Windows.Forms.Label();
            this.lbl_current_database_path = new System.Windows.Forms.Label();
            this.flp_select_localdb = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_localdb = new System.Windows.Forms.Label();
            this.btn_select_localdb = new System.Windows.Forms.Button();
            this.btn_new_database = new System.Windows.Forms.Button();
            this.lbl_daefaultdb = new System.Windows.Forms.Button();
            this.lbl_changedb_info = new System.Windows.Forms.Label();
            this.tlp_btns = new System.Windows.Forms.TableLayoutPanel();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btnm_cancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_exampreview = new System.Windows.Forms.CheckBox();
            this.tlp_main.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.tp_common.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flp_nameorder.SuspendLayout();
            this.flp_settings_options.SuspendLayout();
            this.flp_color.SuspendLayout();
            this.tp_email.SuspendLayout();
            this.flp_email_main.SuspendLayout();
            this.flp_top.SuspendLayout();
            this.flp_border_smtp.SuspendLayout();
            this.flp_smtp_server_port.SuspendLayout();
            this.flp_smtp_user.SuspendLayout();
            this.flp_smtp_msg.SuspendLayout();
            this.tp_database.SuspendLayout();
            this.flp_db_main.SuspendLayout();
            this.flp_current_db.SuspendLayout();
            this.flp_select_localdb.SuspendLayout();
            this.tlp_btns.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.panel_main, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_btns, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.Size = new System.Drawing.Size(800, 450);
            this.tlp_main.TabIndex = 0;
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.tp_common);
            this.panel_main.Controls.Add(this.tp_email);
            this.panel_main.Controls.Add(this.tp_database);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(3, 3);
            this.panel_main.Name = "panel_main";
            this.panel_main.SelectedIndex = 0;
            this.panel_main.Size = new System.Drawing.Size(794, 404);
            this.panel_main.TabIndex = 1;
            // 
            // tp_common
            // 
            this.tp_common.Controls.Add(this.flowLayoutPanel3);
            this.tp_common.Controls.Add(this.flowLayoutPanel2);
            this.tp_common.Controls.Add(this.flowLayoutPanel1);
            this.tp_common.Controls.Add(this.flp_nameorder);
            this.tp_common.Controls.Add(this.flp_settings_options);
            this.tp_common.Controls.Add(this.flp_color);
            this.tp_common.Location = new System.Drawing.Point(4, 25);
            this.tp_common.Name = "tp_common";
            this.tp_common.Padding = new System.Windows.Forms.Padding(3);
            this.tp_common.Size = new System.Drawing.Size(786, 375);
            this.tp_common.TabIndex = 0;
            this.tp_common.Text = "Allgemein";
            this.tp_common.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.lbl_exambreak);
            this.flowLayoutPanel2.Controls.Add(this.dtp_exambreak);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 87);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(780, 28);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // lbl_exambreak
            // 
            this.lbl_exambreak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_exambreak.AutoSize = true;
            this.lbl_exambreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_exambreak.Location = new System.Drawing.Point(5, 5);
            this.lbl_exambreak.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_exambreak.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_exambreak.Name = "lbl_exambreak";
            this.lbl_exambreak.Size = new System.Drawing.Size(199, 20);
            this.lbl_exambreak.TabIndex = 4;
            this.lbl_exambreak.Text = "Pause nach Prüfung[DEV]:";
            this.lbl_exambreak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtp_exambreak
            // 
            this.dtp_exambreak.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtp_exambreak.CustomFormat = "mm";
            this.dtp_exambreak.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtp_exambreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtp_exambreak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_exambreak.Location = new System.Drawing.Point(212, 4);
            this.dtp_exambreak.MaximumSize = new System.Drawing.Size(100, 26);
            this.dtp_exambreak.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtp_exambreak.Name = "dtp_exambreak";
            this.dtp_exambreak.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtp_exambreak.ShowUpDown = true;
            this.dtp_exambreak.Size = new System.Drawing.Size(60, 23);
            this.dtp_exambreak.TabIndex = 11;
            this.dtp_exambreak.Value = new System.DateTime(2022, 1, 24, 8, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cb_teacher_nameorder);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 59);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(780, 28);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.MinimumSize = new System.Drawing.Size(60, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Namensreihenfolge Lehrer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_teacher_nameorder
            // 
            this.cb_teacher_nameorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_teacher_nameorder.FormattingEnabled = true;
            this.cb_teacher_nameorder.Items.AddRange(new object[] {
            "Vorname, Nachname",
            "Nachname, Vorname"});
            this.cb_teacher_nameorder.Location = new System.Drawing.Point(214, 3);
            this.cb_teacher_nameorder.Name = "cb_teacher_nameorder";
            this.cb_teacher_nameorder.Size = new System.Drawing.Size(160, 24);
            this.cb_teacher_nameorder.TabIndex = 5;
            this.cb_teacher_nameorder.SelectedIndexChanged += new System.EventHandler(this.cb_teacher_nameorder_SelectedIndexChanged);
            // 
            // flp_nameorder
            // 
            this.flp_nameorder.BackColor = System.Drawing.Color.Transparent;
            this.flp_nameorder.Controls.Add(this.lbl_nameorder);
            this.flp_nameorder.Controls.Add(this.cb_student_nameorder);
            this.flp_nameorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_nameorder.Location = new System.Drawing.Point(3, 31);
            this.flp_nameorder.Name = "flp_nameorder";
            this.flp_nameorder.Size = new System.Drawing.Size(780, 28);
            this.flp_nameorder.TabIndex = 7;
            // 
            // lbl_nameorder
            // 
            this.lbl_nameorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_nameorder.AutoSize = true;
            this.lbl_nameorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_nameorder.Location = new System.Drawing.Point(5, 5);
            this.lbl_nameorder.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_nameorder.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_nameorder.Name = "lbl_nameorder";
            this.lbl_nameorder.Size = new System.Drawing.Size(209, 20);
            this.lbl_nameorder.TabIndex = 4;
            this.lbl_nameorder.Text = "Namensreihenfolge Schüler:";
            this.lbl_nameorder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_student_nameorder
            // 
            this.cb_student_nameorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_student_nameorder.FormattingEnabled = true;
            this.cb_student_nameorder.Items.AddRange(new object[] {
            "Vorname, Nachname",
            "Nachname, Vorname"});
            this.cb_student_nameorder.Location = new System.Drawing.Point(222, 3);
            this.cb_student_nameorder.Name = "cb_student_nameorder";
            this.cb_student_nameorder.Size = new System.Drawing.Size(160, 24);
            this.cb_student_nameorder.TabIndex = 5;
            this.cb_student_nameorder.SelectedIndexChanged += new System.EventHandler(this.cb_student_nameorder_SelectedIndexChanged);
            // 
            // flp_settings_options
            // 
            this.flp_settings_options.Controls.Add(this.btn_settings_export);
            this.flp_settings_options.Controls.Add(this.btn_settings_import);
            this.flp_settings_options.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flp_settings_options.Location = new System.Drawing.Point(3, 342);
            this.flp_settings_options.Name = "flp_settings_options";
            this.flp_settings_options.Size = new System.Drawing.Size(780, 30);
            this.flp_settings_options.TabIndex = 6;
            // 
            // btn_settings_export
            // 
            this.btn_settings_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_settings_export.Location = new System.Drawing.Point(2, 2);
            this.btn_settings_export.Margin = new System.Windows.Forms.Padding(2);
            this.btn_settings_export.Name = "btn_settings_export";
            this.btn_settings_export.Size = new System.Drawing.Size(140, 26);
            this.btn_settings_export.TabIndex = 9;
            this.btn_settings_export.Text = "Einstellungen exportieren";
            this.btn_settings_export.UseVisualStyleBackColor = true;
            this.btn_settings_export.Click += new System.EventHandler(this.btn_settings_export_Click);
            // 
            // btn_settings_import
            // 
            this.btn_settings_import.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_settings_import.Location = new System.Drawing.Point(146, 2);
            this.btn_settings_import.Margin = new System.Windows.Forms.Padding(2);
            this.btn_settings_import.Name = "btn_settings_import";
            this.btn_settings_import.Size = new System.Drawing.Size(140, 26);
            this.btn_settings_import.TabIndex = 10;
            this.btn_settings_import.Text = "Einstellungen importieren";
            this.btn_settings_import.UseVisualStyleBackColor = true;
            this.btn_settings_import.Click += new System.EventHandler(this.btn_settings_import_Click);
            // 
            // flp_color
            // 
            this.flp_color.BackColor = System.Drawing.Color.Transparent;
            this.flp_color.Controls.Add(this.lbl_color);
            this.flp_color.Controls.Add(this.cb_color);
            this.flp_color.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_color.Location = new System.Drawing.Point(3, 3);
            this.flp_color.Name = "flp_color";
            this.flp_color.Size = new System.Drawing.Size(780, 28);
            this.flp_color.TabIndex = 5;
            // 
            // lbl_color
            // 
            this.lbl_color.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_color.AutoSize = true;
            this.lbl_color.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
            this.lbl_color.Location = new System.Drawing.Point(5, 5);
            this.lbl_color.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_color.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_color.Name = "lbl_color";
            this.lbl_color.Size = new System.Drawing.Size(94, 20);
            this.lbl_color.TabIndex = 4;
            this.lbl_color.Text = "Darstellung:";
            this.lbl_color.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_color
            // 
            this.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_color.FormattingEnabled = true;
            this.cb_color.Items.AddRange(new object[] {
            "Hell",
            "Dunkel",
            "Weiß"});
            this.cb_color.Location = new System.Drawing.Point(107, 3);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(121, 24);
            this.cb_color.TabIndex = 5;
            this.cb_color.SelectedIndexChanged += new System.EventHandler(this.cb_color_SelectedIndexChanged);
            // 
            // tp_email
            // 
            this.tp_email.Controls.Add(this.flp_email_main);
            this.tp_email.Location = new System.Drawing.Point(4, 25);
            this.tp_email.Name = "tp_email";
            this.tp_email.Padding = new System.Windows.Forms.Padding(3);
            this.tp_email.Size = new System.Drawing.Size(786, 375);
            this.tp_email.TabIndex = 1;
            this.tp_email.Text = "Email";
            this.tp_email.UseVisualStyleBackColor = true;
            // 
            // flp_email_main
            // 
            this.flp_email_main.Controls.Add(this.flp_top);
            this.flp_email_main.Controls.Add(this.flp_border_smtp);
            this.flp_email_main.Controls.Add(this.flp_smtp_server_port);
            this.flp_email_main.Controls.Add(this.flp_smtp_user);
            this.flp_email_main.Controls.Add(this.flp_smtp_msg);
            this.flp_email_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_email_main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_email_main.Location = new System.Drawing.Point(3, 3);
            this.flp_email_main.Margin = new System.Windows.Forms.Padding(1);
            this.flp_email_main.Name = "flp_email_main";
            this.flp_email_main.Size = new System.Drawing.Size(780, 369);
            this.flp_email_main.TabIndex = 0;
            // 
            // flp_top
            // 
            this.flp_top.Controls.Add(this.lbl_maildomain);
            this.flp_top.Controls.Add(this.tb_emaildomain);
            this.flp_top.Location = new System.Drawing.Point(3, 3);
            this.flp_top.Name = "flp_top";
            this.flp_top.Size = new System.Drawing.Size(774, 35);
            this.flp_top.TabIndex = 3;
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
            // tb_emaildomain
            // 
            this.tb_emaildomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_emaildomain.Location = new System.Drawing.Point(120, 6);
            this.tb_emaildomain.Margin = new System.Windows.Forms.Padding(2, 6, 5, 6);
            this.tb_emaildomain.Name = "tb_emaildomain";
            this.tb_emaildomain.Size = new System.Drawing.Size(130, 26);
            this.tb_emaildomain.TabIndex = 4;
            // 
            // flp_border_smtp
            // 
            this.flp_border_smtp.BackColor = System.Drawing.Color.Transparent;
            this.flp_border_smtp.Controls.Add(this.lbl_border_smtp_server);
            this.flp_border_smtp.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_border_smtp.Location = new System.Drawing.Point(3, 44);
            this.flp_border_smtp.Name = "flp_border_smtp";
            this.flp_border_smtp.Size = new System.Drawing.Size(774, 28);
            this.flp_border_smtp.TabIndex = 4;
            // 
            // lbl_border_smtp_server
            // 
            this.lbl_border_smtp_server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_border_smtp_server.AutoSize = true;
            this.lbl_border_smtp_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
            this.lbl_border_smtp_server.Location = new System.Drawing.Point(5, 5);
            this.lbl_border_smtp_server.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_border_smtp_server.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_border_smtp_server.Name = "lbl_border_smtp_server";
            this.lbl_border_smtp_server.Size = new System.Drawing.Size(107, 20);
            this.lbl_border_smtp_server.TabIndex = 4;
            this.lbl_border_smtp_server.Text = "SMTP-Server:";
            this.lbl_border_smtp_server.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flp_smtp_server_port
            // 
            this.flp_smtp_server_port.Controls.Add(this.lbl_smtp_server_name);
            this.flp_smtp_server_port.Controls.Add(this.tb_smtp_server);
            this.flp_smtp_server_port.Controls.Add(this.lbl_smtp_server_port);
            this.flp_smtp_server_port.Controls.Add(this.tb_smtp_port);
            this.flp_smtp_server_port.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_smtp_server_port.Location = new System.Drawing.Point(3, 78);
            this.flp_smtp_server_port.Name = "flp_smtp_server_port";
            this.flp_smtp_server_port.Size = new System.Drawing.Size(774, 30);
            this.flp_smtp_server_port.TabIndex = 0;
            // 
            // lbl_smtp_server_name
            // 
            this.lbl_smtp_server_name.AutoSize = true;
            this.lbl_smtp_server_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smtp_server_name.Location = new System.Drawing.Point(5, 5);
            this.lbl_smtp_server_name.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_smtp_server_name.Name = "lbl_smtp_server_name";
            this.lbl_smtp_server_name.Size = new System.Drawing.Size(59, 20);
            this.lbl_smtp_server_name.TabIndex = 0;
            this.lbl_smtp_server_name.Text = "Server:";
            // 
            // tb_smtp_server
            // 
            this.tb_smtp_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_server.Location = new System.Drawing.Point(72, 3);
            this.tb_smtp_server.Name = "tb_smtp_server";
            this.tb_smtp_server.Size = new System.Drawing.Size(175, 23);
            this.tb_smtp_server.TabIndex = 1;
            // 
            // lbl_smtp_server_port
            // 
            this.lbl_smtp_server_port.AutoSize = true;
            this.lbl_smtp_server_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smtp_server_port.Location = new System.Drawing.Point(255, 5);
            this.lbl_smtp_server_port.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_smtp_server_port.Name = "lbl_smtp_server_port";
            this.lbl_smtp_server_port.Size = new System.Drawing.Size(42, 20);
            this.lbl_smtp_server_port.TabIndex = 7;
            this.lbl_smtp_server_port.Text = "Port:";
            // 
            // tb_smtp_port
            // 
            this.tb_smtp_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_port.Location = new System.Drawing.Point(305, 3);
            this.tb_smtp_port.Name = "tb_smtp_port";
            this.tb_smtp_port.Size = new System.Drawing.Size(175, 23);
            this.tb_smtp_port.TabIndex = 8;
            // 
            // flp_smtp_user
            // 
            this.flp_smtp_user.Controls.Add(this.lbl_smtp_email);
            this.flp_smtp_user.Controls.Add(this.tb_smtp_email);
            this.flp_smtp_user.Controls.Add(this.lbl_smtp_pwd);
            this.flp_smtp_user.Controls.Add(this.tb_smtp_pwd);
            this.flp_smtp_user.Controls.Add(this.btn_show_smtp_pwd);
            this.flp_smtp_user.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_smtp_user.Location = new System.Drawing.Point(3, 114);
            this.flp_smtp_user.Name = "flp_smtp_user";
            this.flp_smtp_user.Size = new System.Drawing.Size(774, 30);
            this.flp_smtp_user.TabIndex = 5;
            // 
            // lbl_smtp_email
            // 
            this.lbl_smtp_email.AutoSize = true;
            this.lbl_smtp_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smtp_email.Location = new System.Drawing.Point(5, 5);
            this.lbl_smtp_email.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_smtp_email.Name = "lbl_smtp_email";
            this.lbl_smtp_email.Size = new System.Drawing.Size(52, 20);
            this.lbl_smtp_email.TabIndex = 0;
            this.lbl_smtp_email.Text = "Email:";
            // 
            // tb_smtp_email
            // 
            this.tb_smtp_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_email.Location = new System.Drawing.Point(65, 3);
            this.tb_smtp_email.Name = "tb_smtp_email";
            this.tb_smtp_email.Size = new System.Drawing.Size(202, 23);
            this.tb_smtp_email.TabIndex = 1;
            // 
            // lbl_smtp_pwd
            // 
            this.lbl_smtp_pwd.AutoSize = true;
            this.lbl_smtp_pwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smtp_pwd.Location = new System.Drawing.Point(275, 5);
            this.lbl_smtp_pwd.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_smtp_pwd.Name = "lbl_smtp_pwd";
            this.lbl_smtp_pwd.Size = new System.Drawing.Size(78, 20);
            this.lbl_smtp_pwd.TabIndex = 7;
            this.lbl_smtp_pwd.Text = "Passwort:";
            // 
            // tb_smtp_pwd
            // 
            this.tb_smtp_pwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_pwd.Location = new System.Drawing.Point(361, 3);
            this.tb_smtp_pwd.Name = "tb_smtp_pwd";
            this.tb_smtp_pwd.Size = new System.Drawing.Size(175, 23);
            this.tb_smtp_pwd.TabIndex = 8;
            this.tb_smtp_pwd.UseSystemPasswordChar = true;
            // 
            // btn_show_smtp_pwd
            // 
            this.btn_show_smtp_pwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_show_smtp_pwd.Location = new System.Drawing.Point(542, 3);
            this.btn_show_smtp_pwd.Name = "btn_show_smtp_pwd";
            this.btn_show_smtp_pwd.Size = new System.Drawing.Size(50, 23);
            this.btn_show_smtp_pwd.TabIndex = 9;
            this.btn_show_smtp_pwd.Text = "zeigen";
            this.btn_show_smtp_pwd.UseVisualStyleBackColor = true;
            this.btn_show_smtp_pwd.Click += new System.EventHandler(this.btn_show_smtp_pwd_Click);
            // 
            // flp_smtp_msg
            // 
            this.flp_smtp_msg.Controls.Add(this.lbl_smtp_sendername);
            this.flp_smtp_msg.Controls.Add(this.tb_smtp_sendername);
            this.flp_smtp_msg.Controls.Add(this.lbl_titel);
            this.flp_smtp_msg.Controls.Add(this.tb_smtp_email_titel);
            this.flp_smtp_msg.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_smtp_msg.Location = new System.Drawing.Point(3, 150);
            this.flp_smtp_msg.Name = "flp_smtp_msg";
            this.flp_smtp_msg.Size = new System.Drawing.Size(774, 30);
            this.flp_smtp_msg.TabIndex = 6;
            // 
            // lbl_smtp_sendername
            // 
            this.lbl_smtp_sendername.AutoSize = true;
            this.lbl_smtp_sendername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smtp_sendername.Location = new System.Drawing.Point(5, 5);
            this.lbl_smtp_sendername.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_smtp_sendername.Name = "lbl_smtp_sendername";
            this.lbl_smtp_sendername.Size = new System.Drawing.Size(122, 20);
            this.lbl_smtp_sendername.TabIndex = 0;
            this.lbl_smtp_sendername.Text = "Absendername:";
            // 
            // tb_smtp_sendername
            // 
            this.tb_smtp_sendername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_sendername.Location = new System.Drawing.Point(135, 3);
            this.tb_smtp_sendername.Name = "tb_smtp_sendername";
            this.tb_smtp_sendername.Size = new System.Drawing.Size(140, 23);
            this.tb_smtp_sendername.TabIndex = 1;
            // 
            // lbl_titel
            // 
            this.lbl_titel.AutoSize = true;
            this.lbl_titel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titel.Location = new System.Drawing.Point(283, 5);
            this.lbl_titel.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_titel.Name = "lbl_titel";
            this.lbl_titel.Size = new System.Drawing.Size(86, 20);
            this.lbl_titel.TabIndex = 7;
            this.lbl_titel.Text = "Email-Titel:";
            // 
            // tb_smtp_email_titel
            // 
            this.tb_smtp_email_titel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_smtp_email_titel.Location = new System.Drawing.Point(377, 3);
            this.tb_smtp_email_titel.Name = "tb_smtp_email_titel";
            this.tb_smtp_email_titel.Size = new System.Drawing.Size(175, 23);
            this.tb_smtp_email_titel.TabIndex = 8;
            this.tb_smtp_email_titel.UseSystemPasswordChar = true;
            // 
            // tp_database
            // 
            this.tp_database.Controls.Add(this.flp_db_main);
            this.tp_database.Location = new System.Drawing.Point(4, 25);
            this.tp_database.Name = "tp_database";
            this.tp_database.Padding = new System.Windows.Forms.Padding(3);
            this.tp_database.Size = new System.Drawing.Size(786, 375);
            this.tp_database.TabIndex = 2;
            this.tp_database.Text = "Datenbank";
            this.tp_database.UseVisualStyleBackColor = true;
            // 
            // flp_db_main
            // 
            this.flp_db_main.Controls.Add(this.flp_current_db);
            this.flp_db_main.Controls.Add(this.flp_select_localdb);
            this.flp_db_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_db_main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_db_main.Location = new System.Drawing.Point(3, 3);
            this.flp_db_main.Margin = new System.Windows.Forms.Padding(1);
            this.flp_db_main.Name = "flp_db_main";
            this.flp_db_main.Size = new System.Drawing.Size(780, 369);
            this.flp_db_main.TabIndex = 1;
            // 
            // flp_current_db
            // 
            this.flp_current_db.Controls.Add(this.lbl_current_database);
            this.flp_current_db.Controls.Add(this.lbl_current_database_path);
            this.flp_current_db.Location = new System.Drawing.Point(3, 3);
            this.flp_current_db.Name = "flp_current_db";
            this.flp_current_db.Size = new System.Drawing.Size(774, 30);
            this.flp_current_db.TabIndex = 3;
            // 
            // lbl_current_database
            // 
            this.lbl_current_database.AutoSize = true;
            this.lbl_current_database.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_current_database.Location = new System.Drawing.Point(5, 5);
            this.lbl_current_database.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_current_database.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_current_database.Name = "lbl_current_database";
            this.lbl_current_database.Size = new System.Drawing.Size(153, 20);
            this.lbl_current_database.TabIndex = 3;
            this.lbl_current_database.Text = "Aktuelle Datenbank:";
            this.lbl_current_database.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_current_database_path
            // 
            this.lbl_current_database_path.AutoSize = true;
            this.lbl_current_database_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_current_database_path.Location = new System.Drawing.Point(168, 5);
            this.lbl_current_database_path.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_current_database_path.MinimumSize = new System.Drawing.Size(60, 0);
            this.lbl_current_database_path.Name = "lbl_current_database_path";
            this.lbl_current_database_path.Size = new System.Drawing.Size(60, 20);
            this.lbl_current_database_path.TabIndex = 4;
            this.lbl_current_database_path.Text = "-";
            this.lbl_current_database_path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flp_select_localdb
            // 
            this.flp_select_localdb.Controls.Add(this.lbl_localdb);
            this.flp_select_localdb.Controls.Add(this.btn_select_localdb);
            this.flp_select_localdb.Controls.Add(this.btn_new_database);
            this.flp_select_localdb.Controls.Add(this.lbl_daefaultdb);
            this.flp_select_localdb.Controls.Add(this.lbl_changedb_info);
            this.flp_select_localdb.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_select_localdb.Location = new System.Drawing.Point(3, 39);
            this.flp_select_localdb.Name = "flp_select_localdb";
            this.flp_select_localdb.Size = new System.Drawing.Size(774, 30);
            this.flp_select_localdb.TabIndex = 5;
            // 
            // lbl_localdb
            // 
            this.lbl_localdb.AutoSize = true;
            this.lbl_localdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_localdb.Location = new System.Drawing.Point(5, 5);
            this.lbl_localdb.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_localdb.Name = "lbl_localdb";
            this.lbl_localdb.Size = new System.Drawing.Size(92, 20);
            this.lbl_localdb.TabIndex = 0;
            this.lbl_localdb.Text = "Datenbank:";
            // 
            // btn_select_localdb
            // 
            this.btn_select_localdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_select_localdb.Location = new System.Drawing.Point(104, 2);
            this.btn_select_localdb.Margin = new System.Windows.Forms.Padding(2);
            this.btn_select_localdb.Name = "btn_select_localdb";
            this.btn_select_localdb.Size = new System.Drawing.Size(130, 26);
            this.btn_select_localdb.TabIndex = 9;
            this.btn_select_localdb.Text = "Datenbank auswählen";
            this.btn_select_localdb.UseVisualStyleBackColor = true;
            this.btn_select_localdb.Click += new System.EventHandler(this.btn_select_localdb_Click);
            // 
            // btn_new_database
            // 
            this.btn_new_database.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new_database.Location = new System.Drawing.Point(238, 2);
            this.btn_new_database.Margin = new System.Windows.Forms.Padding(2);
            this.btn_new_database.Name = "btn_new_database";
            this.btn_new_database.Size = new System.Drawing.Size(100, 26);
            this.btn_new_database.TabIndex = 12;
            this.btn_new_database.Text = "neue Datenbank";
            this.btn_new_database.UseVisualStyleBackColor = true;
            this.btn_new_database.Click += new System.EventHandler(this.btn_new_database_Click);
            // 
            // lbl_daefaultdb
            // 
            this.lbl_daefaultdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_daefaultdb.Location = new System.Drawing.Point(342, 2);
            this.lbl_daefaultdb.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_daefaultdb.Name = "lbl_daefaultdb";
            this.lbl_daefaultdb.Size = new System.Drawing.Size(117, 26);
            this.lbl_daefaultdb.TabIndex = 10;
            this.lbl_daefaultdb.Text = "Standart Datenbank";
            this.lbl_daefaultdb.UseVisualStyleBackColor = true;
            this.lbl_daefaultdb.Click += new System.EventHandler(this.lbl_daefaultdb_Click);
            // 
            // lbl_changedb_info
            // 
            this.lbl_changedb_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_changedb_info.AutoSize = true;
            this.lbl_changedb_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_changedb_info.ForeColor = System.Drawing.Color.Red;
            this.lbl_changedb_info.Location = new System.Drawing.Point(466, 8);
            this.lbl_changedb_info.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_changedb_info.Name = "lbl_changedb_info";
            this.lbl_changedb_info.Size = new System.Drawing.Size(138, 17);
            this.lbl_changedb_info.TabIndex = 11;
            this.lbl_changedb_info.Text = "Neustart erforderlich";
            // 
            // tlp_btns
            // 
            this.tlp_btns.ColumnCount = 4;
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_btns.Controls.Add(this.btn_reset, 0, 0);
            this.tlp_btns.Controls.Add(this.btn_save, 2, 0);
            this.tlp_btns.Controls.Add(this.btnm_cancel, 3, 0);
            this.tlp_btns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_btns.Location = new System.Drawing.Point(3, 413);
            this.tlp_btns.Name = "tlp_btns";
            this.tlp_btns.RowCount = 1;
            this.tlp_btns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_btns.Size = new System.Drawing.Size(794, 34);
            this.tlp_btns.TabIndex = 2;
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(3, 3);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(114, 28);
            this.btn_reset.TabIndex = 2;
            this.btn_reset.Text = "Zurücksetzen";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(597, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(94, 28);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btnm_cancel
            // 
            this.btnm_cancel.Location = new System.Drawing.Point(697, 3);
            this.btnm_cancel.Name = "btnm_cancel";
            this.btnm_cancel.Size = new System.Drawing.Size(94, 28);
            this.btnm_cancel.TabIndex = 1;
            this.btnm_cancel.Text = "Abbrechen";
            this.btnm_cancel.UseVisualStyleBackColor = true;
            this.btnm_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel3.Controls.Add(this.cb_exampreview);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 115);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(780, 28);
            this.flowLayoutPanel3.TabIndex = 11;
            // 
            // cb_exampreview
            // 
            this.cb_exampreview.AutoSize = true;
            this.cb_exampreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_exampreview.Location = new System.Drawing.Point(5, 3);
            this.cb_exampreview.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.cb_exampreview.Name = "cb_exampreview";
            this.cb_exampreview.Size = new System.Drawing.Size(156, 24);
            this.cb_exampreview.TabIndex = 5;
            this.cb_exampreview.Text = "Prüfungsvorschau";
            this.cb_exampreview.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSettings";
            this.Text = "Einstellungen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tlp_main.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.tp_common.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flp_nameorder.ResumeLayout(false);
            this.flp_nameorder.PerformLayout();
            this.flp_settings_options.ResumeLayout(false);
            this.flp_color.ResumeLayout(false);
            this.flp_color.PerformLayout();
            this.tp_email.ResumeLayout(false);
            this.flp_email_main.ResumeLayout(false);
            this.flp_top.ResumeLayout(false);
            this.flp_top.PerformLayout();
            this.flp_border_smtp.ResumeLayout(false);
            this.flp_border_smtp.PerformLayout();
            this.flp_smtp_server_port.ResumeLayout(false);
            this.flp_smtp_server_port.PerformLayout();
            this.flp_smtp_user.ResumeLayout(false);
            this.flp_smtp_user.PerformLayout();
            this.flp_smtp_msg.ResumeLayout(false);
            this.flp_smtp_msg.PerformLayout();
            this.tp_database.ResumeLayout(false);
            this.flp_db_main.ResumeLayout(false);
            this.flp_current_db.ResumeLayout(false);
            this.flp_current_db.PerformLayout();
            this.flp_select_localdb.ResumeLayout(false);
            this.flp_select_localdb.PerformLayout();
            this.tlp_btns.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TabControl panel_main;
        private System.Windows.Forms.TabPage tp_common;
        private System.Windows.Forms.TabPage tp_email;
        private System.Windows.Forms.FlowLayoutPanel flp_email_main;
        private System.Windows.Forms.FlowLayoutPanel flp_smtp_server_port;
        private System.Windows.Forms.Label lbl_smtp_server_name;
        private System.Windows.Forms.TextBox tb_smtp_server;
        private System.Windows.Forms.TableLayoutPanel tlp_btns;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btnm_cancel;
        private System.Windows.Forms.FlowLayoutPanel flp_top;
        private System.Windows.Forms.Label lbl_maildomain;
        private System.Windows.Forms.TextBox tb_emaildomain;
        private System.Windows.Forms.FlowLayoutPanel flp_border_smtp;
        private System.Windows.Forms.Label lbl_border_smtp_server;
        private System.Windows.Forms.Label lbl_smtp_server_port;
        private System.Windows.Forms.TextBox tb_smtp_port;
        private System.Windows.Forms.FlowLayoutPanel flp_smtp_user;
        private System.Windows.Forms.Label lbl_smtp_email;
        private System.Windows.Forms.TextBox tb_smtp_email;
        private System.Windows.Forms.Label lbl_smtp_pwd;
        private System.Windows.Forms.TextBox tb_smtp_pwd;
        private System.Windows.Forms.Button btn_show_smtp_pwd;
        private System.Windows.Forms.FlowLayoutPanel flp_color;
        private System.Windows.Forms.Label lbl_color;
        private System.Windows.Forms.ComboBox cb_color;
        private System.Windows.Forms.TabPage tp_database;
        private System.Windows.Forms.FlowLayoutPanel flp_db_main;
        private System.Windows.Forms.FlowLayoutPanel flp_current_db;
        private System.Windows.Forms.Label lbl_current_database;
        private System.Windows.Forms.Label lbl_current_database_path;
        private System.Windows.Forms.FlowLayoutPanel flp_select_localdb;
        private System.Windows.Forms.Label lbl_localdb;
        private System.Windows.Forms.Button btn_select_localdb;
        private System.Windows.Forms.Button lbl_daefaultdb;
        private System.Windows.Forms.Label lbl_changedb_info;
        private System.Windows.Forms.FlowLayoutPanel flp_smtp_msg;
        private System.Windows.Forms.Label lbl_smtp_sendername;
        private System.Windows.Forms.TextBox tb_smtp_sendername;
        private System.Windows.Forms.Label lbl_titel;
        private System.Windows.Forms.TextBox tb_smtp_email_titel;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.FlowLayoutPanel flp_settings_options;
        private System.Windows.Forms.Button btn_settings_export;
        private System.Windows.Forms.Button btn_settings_import;
        private System.Windows.Forms.FlowLayoutPanel flp_nameorder;
        private System.Windows.Forms.Label lbl_nameorder;
        private System.Windows.Forms.ComboBox cb_student_nameorder;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_teacher_nameorder;
        private System.Windows.Forms.Button btn_new_database;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lbl_exambreak;
        private System.Windows.Forms.DateTimePicker dtp_exambreak;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox cb_exampreview;
    }
}