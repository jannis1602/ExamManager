namespace ExamManager
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.flp_menu = new System.Windows.Forms.FlowLayoutPanel();
            this.dtp_timeline_date = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_students = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_teachers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_rooms = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_subjects = new System.Windows.Forms.ToolStripMenuItem();
            this.stufeVerschiebenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_editgrade_move = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_editgrade_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.filedataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_loadstudents = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_loadteacher = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_teacher = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_student = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_subject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_room = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_grade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter_grade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter_subject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter_teacher = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter_room = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_filter_all = new System.Windows.Forms.ToolStripMenuItem();
            this.examToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_exam_changeroom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_exam_examdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_tools_deleteOldExams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_tools_export = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_tools_exportexamday = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_import_export = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_open_excel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_options = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_options_keepdata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_options_table = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_table_exams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_table_students = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_table_teacher = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_options_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_edit = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_add_exam = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_grade = new System.Windows.Forms.ComboBox();
            this.lbl_grade = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_student3 = new System.Windows.Forms.ComboBox();
            this.lbl_student3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_student2 = new System.Windows.Forms.ComboBox();
            this.lbl_student2 = new System.Windows.Forms.Label();
            this.tlp_student = new System.Windows.Forms.TableLayoutPanel();
            this.cb_student = new System.Windows.Forms.ComboBox();
            this.lbl_student = new System.Windows.Forms.Label();
            this.tlp_4 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_teacher3 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_teacher3 = new System.Windows.Forms.ComboBox();
            this.lbl_teacher3 = new System.Windows.Forms.Label();
            this.tlp_teacher1 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_teacher1 = new System.Windows.Forms.ComboBox();
            this.lbl_teacher1 = new System.Windows.Forms.Label();
            this.tlp_teacher2 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_teacher2 = new System.Windows.Forms.ComboBox();
            this.lbl_teacher2 = new System.Windows.Forms.Label();
            this.tlp_3 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_subject = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_subject = new System.Windows.Forms.Label();
            this.cb_subject = new System.Windows.Forms.ComboBox();
            this.tlp_preparation_room = new System.Windows.Forms.TableLayoutPanel();
            this.cb_preparation_room = new System.Windows.Forms.ComboBox();
            this.lbl_preparation_room = new System.Windows.Forms.Label();
            this.tlp_exam_room = new System.Windows.Forms.TableLayoutPanel();
            this.cb_exam_room = new System.Windows.Forms.ComboBox();
            this.lbl_exam_room = new System.Windows.Forms.Label();
            this.tlp_1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode = new System.Windows.Forms.Label();
            this.flp_edit_btns = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_reuse_exam = new System.Windows.Forms.Button();
            this.btn_delete_exam = new System.Windows.Forms.Button();
            this.tlp_config = new System.Windows.Forms.TableLayoutPanel();
            this.cb_student_onetime = new System.Windows.Forms.CheckBox();
            this.cb_add_next_time = new System.Windows.Forms.CheckBox();
            this.cb_keep_data = new System.Windows.Forms.CheckBox();
            this.cb_show_subjectteacher = new System.Windows.Forms.CheckBox();
            this.tlp_2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_date = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_date = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.tlp_duration = new System.Windows.Forms.TableLayoutPanel();
            this.tb_duration = new System.Windows.Forms.TextBox();
            this.lbl_duration = new System.Windows.Forms.Label();
            this.tlp_time = new System.Windows.Forms.TableLayoutPanel();
            this.dtp_time = new System.Windows.Forms.DateTimePicker();
            this.lbl_time = new System.Windows.Forms.Label();
            this.tlp_timeline_view = new System.Windows.Forms.TableLayoutPanel();
            this.panel_side_room = new System.Windows.Forms.Panel();
            this.panel_sidetop_empty = new System.Windows.Forms.Panel();
            this.lbl_search = new System.Windows.Forms.Label();
            this.panel_time_line = new System.Windows.Forms.Panel();
            this.panel_top_time = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tooltip_search_filter = new System.Windows.Forms.ToolTip(this.components);
            this.tsmi_options_sendmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_main.SuspendLayout();
            this.flp_menu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tlp_edit.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlp_student.SuspendLayout();
            this.tlp_4.SuspendLayout();
            this.tlp_teacher3.SuspendLayout();
            this.tlp_teacher1.SuspendLayout();
            this.tlp_teacher2.SuspendLayout();
            this.tlp_3.SuspendLayout();
            this.tlp_subject.SuspendLayout();
            this.tlp_preparation_room.SuspendLayout();
            this.tlp_exam_room.SuspendLayout();
            this.tlp_1.SuspendLayout();
            this.flp_edit_btns.SuspendLayout();
            this.tlp_config.SuspendLayout();
            this.tlp_2.SuspendLayout();
            this.tlp_date.SuspendLayout();
            this.tlp_duration.SuspendLayout();
            this.tlp_time.SuspendLayout();
            this.tlp_timeline_view.SuspendLayout();
            this.panel_side_room.SuspendLayout();
            this.panel_sidetop_empty.SuspendLayout();
            this.panel_time_line.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            resources.ApplyResources(this.tlp_main, "tlp_main");
            this.tlp_main.Controls.Add(this.flp_menu, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_edit, 0, 2);
            this.tlp_main.Controls.Add(this.tlp_timeline_view, 0, 1);
            this.tlp_main.Name = "tlp_main";
            // 
            // flp_menu
            // 
            this.flp_menu.Controls.Add(this.dtp_timeline_date);
            this.flp_menu.Controls.Add(this.menuStrip1);
            resources.ApplyResources(this.flp_menu, "flp_menu");
            this.flp_menu.Name = "flp_menu";
            // 
            // dtp_timeline_date
            // 
            resources.ApplyResources(this.dtp_timeline_date, "dtp_timeline_date");
            this.dtp_timeline_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_timeline_date.Name = "dtp_timeline_date";
            this.dtp_timeline_date.Value = new System.DateTime(2022, 1, 29, 0, 0, 0, 0);
            this.dtp_timeline_date.ValueChanged += new System.EventHandler(this.dtp_timeline_date_ValueChanged);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.tsmi_filter,
            this.examToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.tsmi_options});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_data_students,
            this.tsmi_data_teachers,
            this.tsmi_data_rooms,
            this.tsmi_data_subjects,
            this.stufeVerschiebenToolStripMenuItem,
            this.filedataToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            resources.ApplyResources(this.dataToolStripMenuItem, "dataToolStripMenuItem");
            // 
            // tsmi_data_students
            // 
            this.tsmi_data_students.Name = "tsmi_data_students";
            resources.ApplyResources(this.tsmi_data_students, "tsmi_data_students");
            this.tsmi_data_students.Click += new System.EventHandler(this.tsmi_data_students_Click);
            // 
            // tsmi_data_teachers
            // 
            this.tsmi_data_teachers.Name = "tsmi_data_teachers";
            resources.ApplyResources(this.tsmi_data_teachers, "tsmi_data_teachers");
            this.tsmi_data_teachers.Click += new System.EventHandler(this.tsmi_data_teachers_Click);
            // 
            // tsmi_data_rooms
            // 
            this.tsmi_data_rooms.Name = "tsmi_data_rooms";
            resources.ApplyResources(this.tsmi_data_rooms, "tsmi_data_rooms");
            this.tsmi_data_rooms.Click += new System.EventHandler(this.tsmi_data_rooms_Click);
            // 
            // tsmi_data_subjects
            // 
            this.tsmi_data_subjects.Name = "tsmi_data_subjects";
            resources.ApplyResources(this.tsmi_data_subjects, "tsmi_data_subjects");
            this.tsmi_data_subjects.Click += new System.EventHandler(this.tsmi_data_subjects_Click);
            // 
            // stufeVerschiebenToolStripMenuItem
            // 
            this.stufeVerschiebenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_data_editgrade_move,
            this.tsmi_data_editgrade_delete});
            this.stufeVerschiebenToolStripMenuItem.Name = "stufeVerschiebenToolStripMenuItem";
            resources.ApplyResources(this.stufeVerschiebenToolStripMenuItem, "stufeVerschiebenToolStripMenuItem");
            // 
            // tsmi_data_editgrade_move
            // 
            this.tsmi_data_editgrade_move.Name = "tsmi_data_editgrade_move";
            resources.ApplyResources(this.tsmi_data_editgrade_move, "tsmi_data_editgrade_move");
            this.tsmi_data_editgrade_move.Click += new System.EventHandler(this.tsmi_data_editgrade_move_Click);
            // 
            // tsmi_data_editgrade_delete
            // 
            this.tsmi_data_editgrade_delete.Name = "tsmi_data_editgrade_delete";
            resources.ApplyResources(this.tsmi_data_editgrade_delete, "tsmi_data_editgrade_delete");
            this.tsmi_data_editgrade_delete.Click += new System.EventHandler(this.tsmi_data_editgrade_delete_Click);
            // 
            // filedataToolStripMenuItem
            // 
            this.filedataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_data_loadstudents,
            this.tsmi_data_loadteacher});
            this.filedataToolStripMenuItem.Name = "filedataToolStripMenuItem";
            resources.ApplyResources(this.filedataToolStripMenuItem, "filedataToolStripMenuItem");
            // 
            // tsmi_data_loadstudents
            // 
            this.tsmi_data_loadstudents.Name = "tsmi_data_loadstudents";
            resources.ApplyResources(this.tsmi_data_loadstudents, "tsmi_data_loadstudents");
            this.tsmi_data_loadstudents.Click += new System.EventHandler(this.tsmi_data_loadstudents_Click);
            // 
            // tsmi_data_loadteacher
            // 
            this.tsmi_data_loadteacher.Name = "tsmi_data_loadteacher";
            resources.ApplyResources(this.tsmi_data_loadteacher, "tsmi_data_loadteacher");
            this.tsmi_data_loadteacher.Click += new System.EventHandler(this.tsmi_data_loadteacher_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_search_teacher,
            this.tsmi_search_student,
            this.tsmi_search_subject,
            this.tsmi_search_room,
            this.tsmi_search_grade,
            this.tsmi_search_delete});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            // 
            // tsmi_search_teacher
            // 
            this.tsmi_search_teacher.Name = "tsmi_search_teacher";
            resources.ApplyResources(this.tsmi_search_teacher, "tsmi_search_teacher");
            this.tsmi_search_teacher.Click += new System.EventHandler(this.tsmi_search_teacher_Click);
            // 
            // tsmi_search_student
            // 
            this.tsmi_search_student.Name = "tsmi_search_student";
            resources.ApplyResources(this.tsmi_search_student, "tsmi_search_student");
            this.tsmi_search_student.Click += new System.EventHandler(this.tsmi_search_student_Click);
            // 
            // tsmi_search_subject
            // 
            this.tsmi_search_subject.Name = "tsmi_search_subject";
            resources.ApplyResources(this.tsmi_search_subject, "tsmi_search_subject");
            this.tsmi_search_subject.Click += new System.EventHandler(this.tsmi_search_subject_Click);
            // 
            // tsmi_search_room
            // 
            this.tsmi_search_room.Name = "tsmi_search_room";
            resources.ApplyResources(this.tsmi_search_room, "tsmi_search_room");
            this.tsmi_search_room.Click += new System.EventHandler(this.tsmi_search_room_Click);
            // 
            // tsmi_search_grade
            // 
            this.tsmi_search_grade.Name = "tsmi_search_grade";
            resources.ApplyResources(this.tsmi_search_grade, "tsmi_search_grade");
            this.tsmi_search_grade.Click += new System.EventHandler(this.tsmi_search_grade_Click);
            // 
            // tsmi_search_delete
            // 
            this.tsmi_search_delete.Name = "tsmi_search_delete";
            resources.ApplyResources(this.tsmi_search_delete, "tsmi_search_delete");
            this.tsmi_search_delete.Click += new System.EventHandler(this.tsmi_search_delete_Click);
            // 
            // tsmi_filter
            // 
            this.tsmi_filter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_filter_grade,
            this.tsmi_filter_subject,
            this.tsmi_filter_teacher,
            this.tsmi_filter_room,
            this.tsmi_filter_all});
            this.tsmi_filter.Name = "tsmi_filter";
            resources.ApplyResources(this.tsmi_filter, "tsmi_filter");
            // 
            // tsmi_filter_grade
            // 
            this.tsmi_filter_grade.Name = "tsmi_filter_grade";
            resources.ApplyResources(this.tsmi_filter_grade, "tsmi_filter_grade");
            this.tsmi_filter_grade.Click += new System.EventHandler(this.tsmi_filter_grade_Click);
            // 
            // tsmi_filter_subject
            // 
            this.tsmi_filter_subject.Name = "tsmi_filter_subject";
            resources.ApplyResources(this.tsmi_filter_subject, "tsmi_filter_subject");
            this.tsmi_filter_subject.Click += new System.EventHandler(this.tsmi_filter_subject_Click);
            // 
            // tsmi_filter_teacher
            // 
            this.tsmi_filter_teacher.Name = "tsmi_filter_teacher";
            resources.ApplyResources(this.tsmi_filter_teacher, "tsmi_filter_teacher");
            this.tsmi_filter_teacher.Click += new System.EventHandler(this.tsmi_filter_teacher_Click);
            // 
            // tsmi_filter_room
            // 
            this.tsmi_filter_room.Name = "tsmi_filter_room";
            resources.ApplyResources(this.tsmi_filter_room, "tsmi_filter_room");
            this.tsmi_filter_room.Click += new System.EventHandler(this.tsmi_filter_room_Click);
            // 
            // tsmi_filter_all
            // 
            this.tsmi_filter_all.Name = "tsmi_filter_all";
            resources.ApplyResources(this.tsmi_filter_all, "tsmi_filter_all");
            this.tsmi_filter_all.Click += new System.EventHandler(this.tsmi_filter_all_Click);
            // 
            // examToolStripMenuItem
            // 
            this.examToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_exam_changeroom,
            this.tsmi_exam_examdates});
            this.examToolStripMenuItem.Name = "examToolStripMenuItem";
            resources.ApplyResources(this.examToolStripMenuItem, "examToolStripMenuItem");
            // 
            // tsmi_exam_changeroom
            // 
            this.tsmi_exam_changeroom.Name = "tsmi_exam_changeroom";
            resources.ApplyResources(this.tsmi_exam_changeroom, "tsmi_exam_changeroom");
            this.tsmi_exam_changeroom.Click += new System.EventHandler(this.tsmi_exam_changeroom_Click);
            // 
            // tsmi_exam_examdates
            // 
            this.tsmi_exam_examdates.Name = "tsmi_exam_examdates";
            resources.ApplyResources(this.tsmi_exam_examdates, "tsmi_exam_examdates");
            this.tsmi_exam_examdates.Click += new System.EventHandler(this.tsmi_exam_examdates_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_tools_deleteOldExams,
            this.tsmi_tools_export,
            this.tsmi_tools_exportexamday,
            this.tsmi_import_export,
            this.tsmi_open_excel});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // tsmi_tools_deleteOldExams
            // 
            this.tsmi_tools_deleteOldExams.Name = "tsmi_tools_deleteOldExams";
            resources.ApplyResources(this.tsmi_tools_deleteOldExams, "tsmi_tools_deleteOldExams");
            this.tsmi_tools_deleteOldExams.Click += new System.EventHandler(this.tsmi_tools_deleteOldExams_Click);
            // 
            // tsmi_tools_export
            // 
            this.tsmi_tools_export.Name = "tsmi_tools_export";
            resources.ApplyResources(this.tsmi_tools_export, "tsmi_tools_export");
            this.tsmi_tools_export.Click += new System.EventHandler(this.tsmi_tools_export_Click);
            // 
            // tsmi_tools_exportexamday
            // 
            this.tsmi_tools_exportexamday.Name = "tsmi_tools_exportexamday";
            resources.ApplyResources(this.tsmi_tools_exportexamday, "tsmi_tools_exportexamday");
            this.tsmi_tools_exportexamday.Click += new System.EventHandler(this.tsmi_tools_exportexamday_Click);
            // 
            // tsmi_import_export
            // 
            this.tsmi_import_export.Name = "tsmi_import_export";
            resources.ApplyResources(this.tsmi_import_export, "tsmi_import_export");
            this.tsmi_import_export.Click += new System.EventHandler(this.tsmi_import_export_Click);
            // 
            // tsmi_open_excel
            // 
            this.tsmi_open_excel.Name = "tsmi_open_excel";
            resources.ApplyResources(this.tsmi_open_excel, "tsmi_open_excel");
            this.tsmi_open_excel.Click += new System.EventHandler(this.tsmi_open_excel_Click);
            // 
            // tsmi_options
            // 
            this.tsmi_options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_options_keepdata,
            this.tsmi_options_table,
            this.tsmi_options_settings,
            this.tsmi_options_sendmail});
            this.tsmi_options.Name = "tsmi_options";
            resources.ApplyResources(this.tsmi_options, "tsmi_options");
            // 
            // tsmi_options_keepdata
            // 
            this.tsmi_options_keepdata.Name = "tsmi_options_keepdata";
            resources.ApplyResources(this.tsmi_options_keepdata, "tsmi_options_keepdata");
            this.tsmi_options_keepdata.Click += new System.EventHandler(this.tsmi_options_keepdata_Click);
            // 
            // tsmi_options_table
            // 
            this.tsmi_options_table.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_table_exams,
            this.tsmi_table_students,
            this.tsmi_table_teacher});
            this.tsmi_options_table.Name = "tsmi_options_table";
            resources.ApplyResources(this.tsmi_options_table, "tsmi_options_table");
            // 
            // tsmi_table_exams
            // 
            this.tsmi_table_exams.Name = "tsmi_table_exams";
            resources.ApplyResources(this.tsmi_table_exams, "tsmi_table_exams");
            this.tsmi_table_exams.Click += new System.EventHandler(this.tsmi_table_exams_Click);
            // 
            // tsmi_table_students
            // 
            this.tsmi_table_students.Name = "tsmi_table_students";
            resources.ApplyResources(this.tsmi_table_students, "tsmi_table_students");
            this.tsmi_table_students.Click += new System.EventHandler(this.tsmi_table_students_Click);
            // 
            // tsmi_table_teacher
            // 
            this.tsmi_table_teacher.Name = "tsmi_table_teacher";
            resources.ApplyResources(this.tsmi_table_teacher, "tsmi_table_teacher");
            this.tsmi_table_teacher.Click += new System.EventHandler(this.tsmi_table_teacher_Click);
            // 
            // tsmi_options_settings
            // 
            this.tsmi_options_settings.Name = "tsmi_options_settings";
            resources.ApplyResources(this.tsmi_options_settings, "tsmi_options_settings");
            this.tsmi_options_settings.Click += new System.EventHandler(this.tsmi_options_settings_Click);
            // 
            // tlp_edit
            // 
            resources.ApplyResources(this.tlp_edit, "tlp_edit");
            this.tlp_edit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tlp_edit.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tlp_edit.Controls.Add(this.tlp_4, 0, 3);
            this.tlp_edit.Controls.Add(this.tlp_3, 0, 2);
            this.tlp_edit.Controls.Add(this.tlp_1, 0, 0);
            this.tlp_edit.Controls.Add(this.tlp_2, 0, 1);
            this.tlp_edit.Name = "tlp_edit";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btn_add_exam, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btn_add_exam
            // 
            resources.ApplyResources(this.btn_add_exam, "btn_add_exam");
            this.btn_add_exam.Name = "btn_add_exam";
            this.btn_add_exam.UseVisualStyleBackColor = true;
            this.btn_add_exam.Click += new System.EventHandler(this.btn_add_exam_Click);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.cb_grade, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_grade, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // cb_grade
            // 
            resources.ApplyResources(this.cb_grade, "cb_grade");
            this.cb_grade.DropDownHeight = 100;
            this.cb_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_grade.FormattingEnabled = true;
            this.cb_grade.Name = "cb_grade";
            this.cb_grade.SelectedIndexChanged += new System.EventHandler(this.cb_grade_SelectedIndexChanged);
            // 
            // lbl_grade
            // 
            resources.ApplyResources(this.lbl_grade, "lbl_grade");
            this.lbl_grade.Name = "lbl_grade";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlp_student, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.cb_student3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_student3, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // cb_student3
            // 
            resources.ApplyResources(this.cb_student3, "cb_student3");
            this.cb_student3.DropDownHeight = 200;
            this.cb_student3.FormattingEnabled = true;
            this.cb_student3.Name = "cb_student3";
            // 
            // lbl_student3
            // 
            resources.ApplyResources(this.lbl_student3, "lbl_student3");
            this.lbl_student3.Name = "lbl_student3";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.cb_student2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_student2, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // cb_student2
            // 
            resources.ApplyResources(this.cb_student2, "cb_student2");
            this.cb_student2.DropDownHeight = 200;
            this.cb_student2.FormattingEnabled = true;
            this.cb_student2.Name = "cb_student2";
            // 
            // lbl_student2
            // 
            resources.ApplyResources(this.lbl_student2, "lbl_student2");
            this.lbl_student2.Name = "lbl_student2";
            // 
            // tlp_student
            // 
            resources.ApplyResources(this.tlp_student, "tlp_student");
            this.tlp_student.Controls.Add(this.cb_student, 0, 0);
            this.tlp_student.Controls.Add(this.lbl_student, 0, 0);
            this.tlp_student.Name = "tlp_student";
            // 
            // cb_student
            // 
            resources.ApplyResources(this.cb_student, "cb_student");
            this.cb_student.DropDownHeight = 200;
            this.cb_student.FormattingEnabled = true;
            this.cb_student.Name = "cb_student";
            // 
            // lbl_student
            // 
            resources.ApplyResources(this.lbl_student, "lbl_student");
            this.lbl_student.Name = "lbl_student";
            // 
            // tlp_4
            // 
            resources.ApplyResources(this.tlp_4, "tlp_4");
            this.tlp_4.Controls.Add(this.tlp_teacher3, 2, 0);
            this.tlp_4.Controls.Add(this.tlp_teacher1, 0, 0);
            this.tlp_4.Controls.Add(this.tlp_teacher2, 1, 0);
            this.tlp_4.Name = "tlp_4";
            // 
            // tlp_teacher3
            // 
            resources.ApplyResources(this.tlp_teacher3, "tlp_teacher3");
            this.tlp_teacher3.Controls.Add(this.cb_teacher3, 0, 0);
            this.tlp_teacher3.Controls.Add(this.lbl_teacher3, 0, 0);
            this.tlp_teacher3.Name = "tlp_teacher3";
            // 
            // cb_teacher3
            // 
            resources.ApplyResources(this.cb_teacher3, "cb_teacher3");
            this.cb_teacher3.DropDownHeight = 200;
            this.cb_teacher3.FormattingEnabled = true;
            this.cb_teacher3.Name = "cb_teacher3";
            // 
            // lbl_teacher3
            // 
            resources.ApplyResources(this.lbl_teacher3, "lbl_teacher3");
            this.lbl_teacher3.Name = "lbl_teacher3";
            // 
            // tlp_teacher1
            // 
            resources.ApplyResources(this.tlp_teacher1, "tlp_teacher1");
            this.tlp_teacher1.Controls.Add(this.cb_teacher1, 0, 0);
            this.tlp_teacher1.Controls.Add(this.lbl_teacher1, 0, 0);
            this.tlp_teacher1.Name = "tlp_teacher1";
            // 
            // cb_teacher1
            // 
            resources.ApplyResources(this.cb_teacher1, "cb_teacher1");
            this.cb_teacher1.DropDownHeight = 200;
            this.cb_teacher1.FormattingEnabled = true;
            this.cb_teacher1.Name = "cb_teacher1";
            // 
            // lbl_teacher1
            // 
            resources.ApplyResources(this.lbl_teacher1, "lbl_teacher1");
            this.lbl_teacher1.Name = "lbl_teacher1";
            // 
            // tlp_teacher2
            // 
            resources.ApplyResources(this.tlp_teacher2, "tlp_teacher2");
            this.tlp_teacher2.Controls.Add(this.cb_teacher2, 0, 0);
            this.tlp_teacher2.Controls.Add(this.lbl_teacher2, 0, 0);
            this.tlp_teacher2.Name = "tlp_teacher2";
            // 
            // cb_teacher2
            // 
            resources.ApplyResources(this.cb_teacher2, "cb_teacher2");
            this.cb_teacher2.DropDownHeight = 200;
            this.cb_teacher2.FormattingEnabled = true;
            this.cb_teacher2.Name = "cb_teacher2";
            // 
            // lbl_teacher2
            // 
            resources.ApplyResources(this.lbl_teacher2, "lbl_teacher2");
            this.lbl_teacher2.Name = "lbl_teacher2";
            // 
            // tlp_3
            // 
            resources.ApplyResources(this.tlp_3, "tlp_3");
            this.tlp_3.Controls.Add(this.tlp_subject, 0, 0);
            this.tlp_3.Controls.Add(this.tlp_preparation_room, 2, 0);
            this.tlp_3.Controls.Add(this.tlp_exam_room, 1, 0);
            this.tlp_3.Name = "tlp_3";
            // 
            // tlp_subject
            // 
            resources.ApplyResources(this.tlp_subject, "tlp_subject");
            this.tlp_subject.Controls.Add(this.lbl_subject, 0, 0);
            this.tlp_subject.Controls.Add(this.cb_subject, 1, 0);
            this.tlp_subject.Name = "tlp_subject";
            // 
            // lbl_subject
            // 
            resources.ApplyResources(this.lbl_subject, "lbl_subject");
            this.lbl_subject.Name = "lbl_subject";
            // 
            // cb_subject
            // 
            resources.ApplyResources(this.cb_subject, "cb_subject");
            this.cb_subject.DropDownHeight = 200;
            this.cb_subject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_subject.FormattingEnabled = true;
            this.cb_subject.Name = "cb_subject";
            this.cb_subject.SelectedIndexChanged += new System.EventHandler(this.cb_subject_SelectedIndexChanged);
            // 
            // tlp_preparation_room
            // 
            resources.ApplyResources(this.tlp_preparation_room, "tlp_preparation_room");
            this.tlp_preparation_room.Controls.Add(this.cb_preparation_room, 0, 0);
            this.tlp_preparation_room.Controls.Add(this.lbl_preparation_room, 0, 0);
            this.tlp_preparation_room.Name = "tlp_preparation_room";
            // 
            // cb_preparation_room
            // 
            resources.ApplyResources(this.cb_preparation_room, "cb_preparation_room");
            this.cb_preparation_room.DropDownHeight = 200;
            this.cb_preparation_room.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_preparation_room.FormattingEnabled = true;
            this.cb_preparation_room.Name = "cb_preparation_room";
            // 
            // lbl_preparation_room
            // 
            resources.ApplyResources(this.lbl_preparation_room, "lbl_preparation_room");
            this.lbl_preparation_room.Name = "lbl_preparation_room";
            // 
            // tlp_exam_room
            // 
            resources.ApplyResources(this.tlp_exam_room, "tlp_exam_room");
            this.tlp_exam_room.Controls.Add(this.cb_exam_room, 0, 0);
            this.tlp_exam_room.Controls.Add(this.lbl_exam_room, 0, 0);
            this.tlp_exam_room.Name = "tlp_exam_room";
            // 
            // cb_exam_room
            // 
            resources.ApplyResources(this.cb_exam_room, "cb_exam_room");
            this.cb_exam_room.DropDownHeight = 200;
            this.cb_exam_room.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_exam_room.FormattingEnabled = true;
            this.cb_exam_room.Name = "cb_exam_room";
            this.cb_exam_room.SelectedIndexChanged += new System.EventHandler(this.cb_exam_room_SelectedIndexChanged);
            // 
            // lbl_exam_room
            // 
            resources.ApplyResources(this.lbl_exam_room, "lbl_exam_room");
            this.lbl_exam_room.Name = "lbl_exam_room";
            // 
            // tlp_1
            // 
            resources.ApplyResources(this.tlp_1, "tlp_1");
            this.tlp_1.Controls.Add(this.lbl_mode, 0, 0);
            this.tlp_1.Controls.Add(this.flp_edit_btns, 4, 0);
            this.tlp_1.Controls.Add(this.tlp_config, 1, 0);
            this.tlp_1.Name = "tlp_1";
            // 
            // lbl_mode
            // 
            resources.ApplyResources(this.lbl_mode, "lbl_mode");
            this.lbl_mode.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_mode.Name = "lbl_mode";
            // 
            // flp_edit_btns
            // 
            this.flp_edit_btns.Controls.Add(this.btn_cancel);
            this.flp_edit_btns.Controls.Add(this.btn_reuse_exam);
            this.flp_edit_btns.Controls.Add(this.btn_delete_exam);
            resources.ApplyResources(this.flp_edit_btns, "flp_edit_btns");
            this.flp_edit_btns.Name = "flp_edit_btns";
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackgroundImage = global::ExamManager.Properties.Resources.exit_ing;
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.toolTip.SetToolTip(this.btn_cancel, resources.GetString("btn_cancel.ToolTip"));
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_reuse_exam
            // 
            this.btn_reuse_exam.BackgroundImage = global::ExamManager.Properties.Resources.copy_img;
            resources.ApplyResources(this.btn_reuse_exam, "btn_reuse_exam");
            this.btn_reuse_exam.Name = "btn_reuse_exam";
            this.toolTip.SetToolTip(this.btn_reuse_exam, resources.GetString("btn_reuse_exam.ToolTip"));
            this.btn_reuse_exam.UseVisualStyleBackColor = true;
            this.btn_reuse_exam.Click += new System.EventHandler(this.btn_reuse_exam_Click);
            // 
            // btn_delete_exam
            // 
            this.btn_delete_exam.BackgroundImage = global::ExamManager.Properties.Resources.trash_img;
            resources.ApplyResources(this.btn_delete_exam, "btn_delete_exam");
            this.btn_delete_exam.Name = "btn_delete_exam";
            this.toolTip.SetToolTip(this.btn_delete_exam, resources.GetString("btn_delete_exam.ToolTip"));
            this.btn_delete_exam.UseVisualStyleBackColor = true;
            this.btn_delete_exam.Click += new System.EventHandler(this.btn_delete_exam_Click);
            // 
            // tlp_config
            // 
            resources.ApplyResources(this.tlp_config, "tlp_config");
            this.tlp_config.Controls.Add(this.cb_student_onetime, 1, 1);
            this.tlp_config.Controls.Add(this.cb_add_next_time, 0, 1);
            this.tlp_config.Controls.Add(this.cb_keep_data, 0, 0);
            this.tlp_config.Controls.Add(this.cb_show_subjectteacher, 1, 0);
            this.tlp_config.Name = "tlp_config";
            // 
            // cb_student_onetime
            // 
            resources.ApplyResources(this.cb_student_onetime, "cb_student_onetime");
            this.cb_student_onetime.Name = "cb_student_onetime";
            this.cb_student_onetime.UseVisualStyleBackColor = true;
            this.cb_student_onetime.CheckedChanged += new System.EventHandler(this.update_autocomplete_Event);
            // 
            // cb_add_next_time
            // 
            resources.ApplyResources(this.cb_add_next_time, "cb_add_next_time");
            this.cb_add_next_time.Name = "cb_add_next_time";
            this.cb_add_next_time.UseVisualStyleBackColor = true;
            // 
            // cb_keep_data
            // 
            this.cb_keep_data.Checked = true;
            this.cb_keep_data.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.cb_keep_data, "cb_keep_data");
            this.cb_keep_data.Name = "cb_keep_data";
            this.cb_keep_data.UseVisualStyleBackColor = true;
            // 
            // cb_show_subjectteacher
            // 
            this.cb_show_subjectteacher.Checked = true;
            this.cb_show_subjectteacher.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.cb_show_subjectteacher, "cb_show_subjectteacher");
            this.cb_show_subjectteacher.Name = "cb_show_subjectteacher";
            this.cb_show_subjectteacher.UseVisualStyleBackColor = true;
            this.cb_show_subjectteacher.CheckedChanged += new System.EventHandler(this.cb_show_subjectteacher_CheckedChanged);
            // 
            // tlp_2
            // 
            resources.ApplyResources(this.tlp_2, "tlp_2");
            this.tlp_2.Controls.Add(this.tlp_date, 0, 0);
            this.tlp_2.Controls.Add(this.tlp_duration, 2, 0);
            this.tlp_2.Controls.Add(this.tlp_time, 1, 0);
            this.tlp_2.Name = "tlp_2";
            // 
            // tlp_date
            // 
            resources.ApplyResources(this.tlp_date, "tlp_date");
            this.tlp_date.Controls.Add(this.lbl_date, 0, 0);
            this.tlp_date.Controls.Add(this.dtp_date, 1, 0);
            this.tlp_date.Name = "tlp_date";
            // 
            // lbl_date
            // 
            resources.ApplyResources(this.lbl_date, "lbl_date");
            this.lbl_date.Name = "lbl_date";
            // 
            // dtp_date
            // 
            resources.ApplyResources(this.dtp_date, "dtp_date");
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Value = new System.DateTime(2022, 1, 29, 0, 0, 0, 0);
            // 
            // tlp_duration
            // 
            resources.ApplyResources(this.tlp_duration, "tlp_duration");
            this.tlp_duration.Controls.Add(this.tb_duration, 1, 0);
            this.tlp_duration.Controls.Add(this.lbl_duration, 0, 0);
            this.tlp_duration.Name = "tlp_duration";
            // 
            // tb_duration
            // 
            resources.ApplyResources(this.tb_duration, "tb_duration");
            this.tb_duration.Name = "tb_duration";
            this.tb_duration.TextChanged += new System.EventHandler(this.tb_duration_TextChanged);
            // 
            // lbl_duration
            // 
            resources.ApplyResources(this.lbl_duration, "lbl_duration");
            this.lbl_duration.Name = "lbl_duration";
            // 
            // tlp_time
            // 
            resources.ApplyResources(this.tlp_time, "tlp_time");
            this.tlp_time.Controls.Add(this.dtp_time, 0, 0);
            this.tlp_time.Controls.Add(this.lbl_time, 0, 0);
            this.tlp_time.Name = "tlp_time";
            // 
            // dtp_time
            // 
            resources.ApplyResources(this.dtp_time, "dtp_time");
            this.dtp_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_time.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtp_time.Name = "dtp_time";
            this.dtp_time.ShowUpDown = true;
            this.dtp_time.Value = new System.DateTime(2022, 1, 24, 8, 0, 0, 0);
            // 
            // lbl_time
            // 
            resources.ApplyResources(this.lbl_time, "lbl_time");
            this.lbl_time.Name = "lbl_time";
            // 
            // tlp_timeline_view
            // 
            resources.ApplyResources(this.tlp_timeline_view, "tlp_timeline_view");
            this.tlp_timeline_view.Controls.Add(this.panel_side_room, 0, 0);
            this.tlp_timeline_view.Controls.Add(this.panel_time_line, 1, 0);
            this.tlp_timeline_view.Name = "tlp_timeline_view";
            // 
            // panel_side_room
            // 
            resources.ApplyResources(this.panel_side_room, "panel_side_room");
            this.panel_side_room.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel_side_room.Controls.Add(this.panel_sidetop_empty);
            this.panel_side_room.Name = "panel_side_room";
            this.panel_side_room.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_side_room_Paint);
            // 
            // panel_sidetop_empty
            // 
            this.panel_sidetop_empty.BackColor = System.Drawing.Color.Transparent;
            this.panel_sidetop_empty.Controls.Add(this.lbl_search);
            resources.ApplyResources(this.panel_sidetop_empty, "panel_sidetop_empty");
            this.panel_sidetop_empty.Name = "panel_sidetop_empty";
            // 
            // lbl_search
            // 
            resources.ApplyResources(this.lbl_search, "lbl_search");
            this.lbl_search.Name = "lbl_search";
            // 
            // panel_time_line
            // 
            resources.ApplyResources(this.panel_time_line, "panel_time_line");
            this.panel_time_line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel_time_line.Controls.Add(this.panel_top_time);
            this.panel_time_line.Name = "panel_time_line";
            this.panel_time_line.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_time_line_master_Paint);
            this.panel_time_line.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panel_time_line_PreviewKeyDown);
            // 
            // panel_top_time
            // 
            this.panel_top_time.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.panel_top_time, "panel_top_time");
            this.panel_top_time.Name = "panel_top_time";
            this.panel_top_time.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_top_time_Paint);
            // 
            // toolTip
            // 
            this.toolTip.Tag = "";
            // 
            // tsmi_options_sendmail
            // 
            this.tsmi_options_sendmail.Name = "tsmi_options_sendmail";
            resources.ApplyResources(this.tsmi_options_sendmail, "tsmi_options_sendmail");
            this.tsmi_options_sendmail.Click += new System.EventHandler(this.tsmi_options_sendmail_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tlp_main);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tlp_main.ResumeLayout(false);
            this.flp_menu.ResumeLayout(false);
            this.flp_menu.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlp_edit.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlp_student.ResumeLayout(false);
            this.tlp_student.PerformLayout();
            this.tlp_4.ResumeLayout(false);
            this.tlp_teacher3.ResumeLayout(false);
            this.tlp_teacher3.PerformLayout();
            this.tlp_teacher1.ResumeLayout(false);
            this.tlp_teacher1.PerformLayout();
            this.tlp_teacher2.ResumeLayout(false);
            this.tlp_teacher2.PerformLayout();
            this.tlp_3.ResumeLayout(false);
            this.tlp_subject.ResumeLayout(false);
            this.tlp_subject.PerformLayout();
            this.tlp_preparation_room.ResumeLayout(false);
            this.tlp_preparation_room.PerformLayout();
            this.tlp_exam_room.ResumeLayout(false);
            this.tlp_exam_room.PerformLayout();
            this.tlp_1.ResumeLayout(false);
            this.tlp_1.PerformLayout();
            this.flp_edit_btns.ResumeLayout(false);
            this.tlp_config.ResumeLayout(false);
            this.tlp_config.PerformLayout();
            this.tlp_2.ResumeLayout(false);
            this.tlp_date.ResumeLayout(false);
            this.tlp_date.PerformLayout();
            this.tlp_duration.ResumeLayout(false);
            this.tlp_duration.PerformLayout();
            this.tlp_time.ResumeLayout(false);
            this.tlp_time.PerformLayout();
            this.tlp_timeline_view.ResumeLayout(false);
            this.panel_side_room.ResumeLayout(false);
            this.panel_sidetop_empty.ResumeLayout(false);
            this.panel_sidetop_empty.PerformLayout();
            this.panel_time_line.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_edit;
        private System.Windows.Forms.TableLayoutPanel tlp_2;
        private System.Windows.Forms.TableLayoutPanel tlp_1;
        private System.Windows.Forms.Label lbl_mode;
        private System.Windows.Forms.TableLayoutPanel tlp_time;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.TableLayoutPanel tlp_duration;
        private System.Windows.Forms.TextBox tb_duration;
        private System.Windows.Forms.Label lbl_duration;
        private System.Windows.Forms.TableLayoutPanel tlp_3;
        private System.Windows.Forms.TableLayoutPanel tlp_subject;
        private System.Windows.Forms.Label lbl_subject;
        private System.Windows.Forms.TableLayoutPanel tlp_4;
        private System.Windows.Forms.TableLayoutPanel tlp_teacher1;
        private System.Windows.Forms.Label lbl_teacher1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tlp_student;
        private System.Windows.Forms.Label lbl_student;
        private System.Windows.Forms.TableLayoutPanel tlp_teacher3;
        private System.Windows.Forms.Label lbl_teacher3;
        private System.Windows.Forms.TableLayoutPanel tlp_teacher2;
        private System.Windows.Forms.Label lbl_teacher2;
        private System.Windows.Forms.TableLayoutPanel tlp_preparation_room;
        private System.Windows.Forms.Label lbl_preparation_room;
        private System.Windows.Forms.TableLayoutPanel tlp_exam_room;
        private System.Windows.Forms.Label lbl_exam_room;
        private System.Windows.Forms.Button btn_add_exam;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.TableLayoutPanel tlp_date;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private System.Windows.Forms.ComboBox cb_subject;
        private System.Windows.Forms.DateTimePicker dtp_timeline_date;
        private System.Windows.Forms.FlowLayoutPanel flp_menu;
        private System.Windows.Forms.Button btn_delete_exam;
        private System.Windows.Forms.Panel panel_side_room;
        private System.Windows.Forms.Button btn_reuse_exam;
        private System.Windows.Forms.Panel panel_top_time;
        private System.Windows.Forms.Panel panel_time_line;
        private System.Windows.Forms.TableLayoutPanel tlp_timeline_view;
        private System.Windows.Forms.FlowLayoutPanel flp_edit_btns;
        private System.Windows.Forms.TableLayoutPanel tlp_config;
        private System.Windows.Forms.CheckBox cb_add_next_time;
        private System.Windows.Forms.CheckBox cb_keep_data;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_student;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_teacher;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_subject;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_students;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_teachers;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_subjects;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_rooms;
        private System.Windows.Forms.ToolStripMenuItem stufeVerschiebenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_editgrade_move;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_editgrade_delete;
        private System.Windows.Forms.ToolStripMenuItem examToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exam_changeroom;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_room;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exam_examdates;
        private System.Windows.Forms.ToolStripMenuItem filedataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_loadstudents;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbl_grade;
        private System.Windows.Forms.ComboBox cb_grade;
        private System.Windows.Forms.ComboBox cb_preparation_room;
        private System.Windows.Forms.ComboBox cb_exam_room;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter;
        private System.Windows.Forms.ToolStripMenuItem tsmi_search_grade;
        private System.Windows.Forms.ComboBox cb_teacher1;
        private System.Windows.Forms.ComboBox cb_teacher3;
        private System.Windows.Forms.ComboBox cb_teacher2;
        private System.Windows.Forms.ComboBox cb_student;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter_grade;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_tools_deleteOldExams;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options_table;
        private System.Windows.Forms.ToolStripMenuItem tsmi_table_exams;
        private System.Windows.Forms.ToolStripMenuItem tsmi_table_students;
        private System.Windows.Forms.ToolStripMenuItem tsmi_table_teacher;
        private System.Windows.Forms.Panel panel_sidetop_empty;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_loadteacher;
        private System.Windows.Forms.CheckBox cb_show_subjectteacher;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter_all;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter_teacher;
        private System.Windows.Forms.ToolTip tooltip_search_filter;
        private System.Windows.Forms.CheckBox cb_student_onetime;
        private System.Windows.Forms.ToolStripMenuItem tsmi_tools_export;
        private System.Windows.Forms.ToolStripMenuItem tsmi_tools_exportexamday;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox cb_student3;
        private System.Windows.Forms.Label lbl_student3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cb_student2;
        private System.Windows.Forms.Label lbl_student2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_import_export;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options_keepdata;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options_settings;
        private System.Windows.Forms.ToolStripMenuItem tsmi_open_excel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter_subject;
        private System.Windows.Forms.ToolStripMenuItem tsmi_filter_room;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options_sendmail;
    }
}
