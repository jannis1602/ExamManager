using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    class TimeLineObject
    {
        readonly string date;
        readonly LinkedList<ExamObject> examObjects;
        public TableLayoutPanel Panel;
        Panel panel_time_line;
        Panel panel_top_time;
        Panel panel_side_room;
        Panel panel_side_top;
        Panel panel_room_bottom;

        public LinkedList<Panel> time_line_room_list;
        public LinkedList<Panel> time_line_list;
        public LinkedList<ExamObject> tl_exam_entity_list;

        private readonly int StartTimeTL = 7;
        private readonly int LengthTL = 12;
        private readonly int PixelPerHour = Properties.Settings.Default.PixelPerHour;

        public TimeLineObject(string date, LinkedList<ExamObject> examList = null)
        {
            this.date = date;
            this.examObjects = examList;
            time_line_room_list = new LinkedList<Panel>();
            time_line_list = new LinkedList<Panel>();
            if (examList != null) tl_exam_entity_list = examList;
            else tl_exam_entity_list = new LinkedList<ExamObject>();

            this.Panel = new TableLayoutPanel();
            this.Panel.Dock = DockStyle.Fill;
            Panel.ColumnCount = 2;
            Panel.RowCount = 1;
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            this.panel_time_line = new Panel();
            this.panel_time_line.Margin = new Padding(0);
            this.panel_time_line.Dock = DockStyle.Fill;
            this.panel_time_line.BackColor = Color.LightGray;
            this.panel_time_line.Size = new Size(PixelPerHour * LengthTL, 225);
            this.panel_time_line.AutoScroll = true;
            CreateTimeRoomPanel();
            this.Panel.Controls.Add(this.panel_side_room, 0, 0);
            this.Panel.Controls.Add(this.panel_time_line, 1, 0);
            panel_top_time.Width = PixelPerHour * LengthTL;
            //UpdateTimeline();
            UpdateTimeLineEntities();

            //ExportPNG();
        }

        private void CreateTimeRoomPanel()
        {
            // time
            this.panel_top_time = new Panel();
            this.panel_top_time.BackColor = Color.Gray;
            this.panel_top_time.Name = "panel_top_time";
            this.panel_top_time.Paint += new PaintEventHandler(this.panel_top_time_Paint);
            this.panel_top_time.Dock = DockStyle.Top;
            this.panel_top_time.Location = new Point(120, 0);
            this.panel_top_time.MaximumSize = new Size(0, 40);
            this.panel_top_time.Size = new Size(PixelPerHour * LengthTL, 40);
            this.panel_time_line.Controls.Add(panel_top_time);
            // room
            this.panel_side_room = new Panel();
            this.panel_side_room.Margin = new Padding(0);
            this.panel_side_room.BackColor = SystemColors.ControlDarkDark;
            this.panel_side_room.AutoScroll = true;
            this.panel_side_room.Name = "panel_side_room";
            this.panel_side_room.Dock = DockStyle.Fill;
            this.panel_side_room.MaximumSize = new Size(120, 0);
            panel_side_top = new Panel();
            panel_side_top.BackColor = Color.Aqua;
            panel_side_top.Size = new Size(100, 40);
            panel_side_room.Controls.Add(panel_side_top);
        }

        public void UpdateTimeline()
        {
            //if ((filter == null || filter.Length == 0) && RoomNameFilterList == null) filterMode = Filter.all;
            if (panel_room_bottom != null) panel_side_room.Controls.Remove(panel_room_bottom);
            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            time_line_room_list.Clear();
            // create new timeline
            LinkedList<ExamObject> examList = examObjects; // Program.database.GetAllExamsAtDate(date);
            tl_exam_entity_list = examList;
            UpdateTimeLineEntities();
        }

        private void UpdateTimeLineEntities()
        {
            LinkedList<string> room_list = new LinkedList<string>();
            foreach (ExamObject s in tl_exam_entity_list)
            { if (!room_list.Contains(s.Examroom)) room_list.AddLast(s.Examroom); }
            List<string> temp_room_list = new List<string>(room_list);
            temp_room_list.Sort();
            room_list = new LinkedList<string>(temp_room_list);
            // TODO: topTimePanel add panel? -> dock top
            foreach (string s in room_list) AddTimeline(s);
            // SideBottomPanel 
            if (panel_room_bottom == null) panel_room_bottom = new Panel();
            panel_room_bottom.Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count);
            panel_room_bottom.Size = new Size(panel_side_room.Width - 17, 12);
            panel_side_room.Controls.Add(panel_room_bottom);

            DateTime tlStartTime = DateTime.ParseExact("00:00", "HH:mm", null).AddHours(StartTimeTL);
            DateTime tlEndTime = DateTime.ParseExact("00:00", "HH:mm", null).AddHours(StartTimeTL + LengthTL);
            foreach (ExamObject exam in tl_exam_entity_list)
            {
                DateTime examStartTime = DateTime.ParseExact(exam.Time, "HH:mm", null);
                DateTime examEndTime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration);
                if (examStartTime >= tlStartTime && examEndTime <= tlEndTime) foreach (Panel p in time_line_list)
                        if (p.Name.Equals(exam.Examroom))
                        { exam.CreatePanel(); p.Controls.Add(exam.GetTimelineEntity()); break; }
            }
        }

        public void AddTimeline(string room)
        {
            // -- roomlist --
            Panel panel_room = new Panel
            {
                Location = new Point(2, 6 + panel_top_time.Height + 5 + 85 * time_line_list.Count),
                Size = new Size(panel_side_room.Width - 17 - 4, 80 - 12),
                Padding = new Padding(3),
                BackColor = Colors.TL_RoomBorder,
                Name = room
            };
            Label lbl_room = new Label
            {
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Dock = DockStyle.Fill,
                Name = "lbl_room",
                BackColor = Colors.TL_RoomEntityBg,
                Text = room,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel_room.Controls.Add(lbl_room);
            this.panel_side_room.HorizontalScroll.Value = 0;
            panel_side_room.Controls.Add(panel_room);
            time_line_room_list.AddLast(panel_room);
            // -- timeline --
            this.panel_time_line.HorizontalScroll.Value = 0;
            Panel panel_tl = new Panel
            {
                Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count),
                Name = room,
                Size = new Size(2400, 80),
                BackColor = Colors.TL_TimeLineBg,
                // Dock = DockStyle.Top,  // TODO: TL Dock top ----------------------------------------------
            };
            panel_tl.Width = PixelPerHour * LengthTL; // TEST
                                                      // panel_tl.Width = panel_top_time.Width;
            panel_tl.Paint += panel_time_line_Paint;
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }

        private void panel_time_line_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            for (int i = 0; i < LengthTL; i++)
            {
                float[] dashValues = { 2, 2 };
                float[] dashValues2 = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues2;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), 4 + panel_tl.Width / LengthTL * i, 4, 4 + panel_tl.Width / LengthTL * i, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen2, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, panel_tl.Height - 4);
            }
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid);
        }

        private void panel_top_time_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            Color c = Colors.TL_TimeBorder;
            byte b = 4; // border
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Microsoft Sans Serif", 10);
            StringFormat drawFormat = new StringFormat();
            for (int i = 0; i < LengthTL; i++)
            {
                float[] dashValues = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), b + panel_tl.Width / LengthTL * i, b, b + panel_tl.Width / LengthTL * i, panel_tl.Height - b);
                e.Graphics.DrawLine(pen2, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, panel_tl.Height - b);
                e.Graphics.DrawString(StartTimeTL + i + " Uhr", drawFont, new SolidBrush(Colors.TL_MinLine), 5 + panel_tl.Width / LengthTL * i, panel_tl.Height - 20, drawFormat);
            }
        }
        public void ExportPNG(bool split = false, string file = null, string fileP1 = null, string fileP2 = null)
        {
            /*DialogResult resultSplit = MessageBox.Show("Zeitachse teilen?", "Achtung", MessageBoxButtons.YesNoCancel);
            if (resultSplit == DialogResult.Yes)
            {
                split = true;
                *//*if (time_line_room_list.Count > 10)
                {
                    MessageBox.Show("Räume auswählen", "Info");
                    LinkedList<string> roomNameList = new LinkedList<string>();
                    foreach (Panel p in time_line_room_list)
                        roomNameList.AddLast(p.Name);
                    FormRoomFilter form = new FormRoomFilter(roomNameList);
                    form.SelectedRoomList += roomList_Event;
                    form.ShowDialog();

                    void roomList_Event(object sender1, EventArgs a)
                    {
                        LinkedList<string> list = sender1 as LinkedList<string>;
                        while (list.Count > 10) list.RemoveLast();
                        filterMode = Filter.room;
                        RoomNameFilterList = list;
                        UpdateTimeline();
                    }
                }*//*
            }
            if (resultSplit == DialogResult.Cancel) { return; }*/

            Colors.Theme tempTheme = Colors.theme;
            /*bool blackwhite = false;
            DialogResult result = MessageBox.Show("Zeitstrahl in schwarz-weiß exportieren?", "Achtung!", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes) { blackwhite = true; }
            if (result == DialogResult.Cancel) { return; }*/

            bool blackwhite = true;
            DateTime lastTime = DateTime.ParseExact("07:00", "HH:mm", null);
            foreach (ExamObject exam in tl_exam_entity_list)
            {
                if (lastTime < DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration))
                    lastTime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration);
            }
            // TODO if Height<Width || Width<Height
            Double cutTime = (DateTime.ParseExact("18:30", "HH:mm", null) - lastTime).TotalMinutes;
            float unit_per_minute = PixelPerHour / 60F;
            int cutMinutes = Convert.ToInt32(cutTime * unit_per_minute);

            float fullWidth = panel_side_room.Width + PixelPerHour * LengthTL; // panel_top_time.Width
            float fullHeight = fullWidth / 297f * 210f;
            float BmpWidth = (panel_side_room.Width + PixelPerHour * LengthTL - cutMinutes);
            float BmpHeight = BmpWidth / 297f * 210f;
            if (blackwhite)
            {
                Colors.ColorTheme(Colors.Theme.bw);
                UpdateTimeline(); // render on export colorchange
                panel_side_top.BackColor = Colors.TL_RoomBg;
                panel_time_line.BackColor = Colors.TL_Bg;
                panel_top_time.BackColor = Colors.TL_TimeBg;
                panel_side_room.BackColor = Colors.TL_RoomBg;
            }
            //lbl_search.Text = date; // ----------------------------------------------------------------------------------------------------<-<-<-<-<-<-<-
            Panel tempPanel = new Panel
            { Width = Convert.ToInt32(fullWidth), Height = Convert.ToInt32(fullHeight), BackColor = Color.White };
            tempPanel.Controls.Add(Panel);
            Bitmap bmp = new Bitmap(Convert.ToInt32(BmpWidth), Convert.ToInt32(BmpHeight)); // -20
            tempPanel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

            // TODO: check if Height<Width || Width<Height

            Panel tPanelRoom = new Panel { Width = 120, Height = panel_side_room.Height, BackColor = Color.White };
            Panel tPanelTL = new Panel { Width = panel_top_time.Width, Height = panel_side_room.Height, BackColor = Color.White };
            tPanelRoom.Controls.Add(panel_side_room);
            tPanelTL.Controls.Add(panel_time_line);
            Bitmap bmpRoom = new Bitmap(120, panel_side_room.Height);
            Bitmap bmpTL = new Bitmap(LengthTL * PixelPerHour, panel_side_room.Height);
            tPanelRoom.DrawToBitmap(bmpRoom, new Rectangle(0, 0, bmpRoom.Width, bmpRoom.Height));
            tPanelTL.DrawToBitmap(bmpTL, new Rectangle(0, 0, bmpTL.Width, bmpTL.Height));

            if (!split)
            {
                if (file == null)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Title = "Save Timeline",
                        FileName = "Prüfungen-" + date + ".png",
                        DefaultExt = "png",
                        Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                        FilterIndex = 2,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                        bmp.Save(sfd.FileName, ImageFormat.Png);
                }
                else bmp.Save(file, ImageFormat.Png);
            }
            else if (split)
            {
                bmpTL = bmpTL.Clone(new Rectangle(0, 0, bmpTL.Width, bmpTL.Height), PixelFormat.DontCare);
                Bitmap newTLbmp = new Bitmap(120 + (LengthTL * PixelPerHour) / 2 + 5, panel_side_room.Height);
                Graphics g = Graphics.FromImage(newTLbmp);
                g.DrawImage(bmpRoom, new Rectangle(0, 0, 120, newTLbmp.Height));
                g.DrawImage(bmpTL, new Rectangle(120, 0, LengthTL * PixelPerHour, newTLbmp.Height));
                Bitmap bmpPart1 = ImageToDinA4(newTLbmp);

                bmpTL = bmpTL.Clone(new Rectangle((LengthTL * PixelPerHour) / 2 + 3, 0, bmpTL.Width - ((LengthTL * PixelPerHour) / 2 + 3), bmpTL.Height), PixelFormat.DontCare);
                newTLbmp = new Bitmap(120 + (LengthTL * PixelPerHour) / 2, panel_side_room.Height);
                g = Graphics.FromImage(newTLbmp);
                g.DrawImage(bmpRoom, new Rectangle(0, 0, 120, newTLbmp.Height));
                g.DrawImage(bmpTL, new Rectangle(120, 0, (LengthTL * PixelPerHour) / 2, newTLbmp.Height));
                Bitmap bmpPart2 = ImageToDinA4(newTLbmp);
                if (fileP1 == null || fileP2 == null)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Title = "Save Timeline",
                        FileName = "Prüfungen-" + date + "_Part1.png",
                        DefaultExt = "png",
                        Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                        FilterIndex = 2,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                        bmpPart1.Save(sfd.FileName, ImageFormat.Png);
                    SaveFileDialog sfd2 = new SaveFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Title = "Save Timeline",
                        FileName = "Prüfungen-" + date + "_Part2.png",
                        DefaultExt = "png",
                        Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                        FilterIndex = 2,
                        RestoreDirectory = true
                    };
                    if (sfd2.ShowDialog() == DialogResult.OK)
                        bmpPart2.Save(sfd2.FileName, ImageFormat.Png);
                }
                else
                {
                    bmpPart1.Save(fileP1, ImageFormat.Png);
                    bmpPart2.Save(fileP2, ImageFormat.Png);
                }
            }

            this.Panel.Controls.Add(panel_side_room);
            this.Panel.Controls.Add(panel_time_line);
            Colors.ColorTheme(tempTheme);
            UpdateTimeline(); // render on export colorchange
            panel_side_top.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            Bitmap ImageToDinA4(Bitmap bmp1)
            {
                float fullWidth1 = panel_side_room.Width + panel_top_time.Width / 2;
                float fullHeight1 = fullWidth1 / 297f * 210f;
                Bitmap newTLbmp1 = new Bitmap(Convert.ToInt32(fullWidth1), Convert.ToInt32(fullHeight1));
                Graphics g1 = Graphics.FromImage(newTLbmp1);
                g1.DrawImage(bmp1, new Rectangle(0, 0, Convert.ToInt32(fullWidth1), panel_side_room.Height));
                return newTLbmp1;
            }
        }

        public void PrintPng()
        {
            string file = Path.GetTempPath() + "\\Prüfungstag_" + date + ".png";
            ExportPNG(split: false, file: file);
            string fileP1 = Path.GetTempPath() + "\\Prüfungstag_P1_" + date + ".png";
            string fileP2 = Path.GetTempPath() + "\\Prüfungstag_P2_" + date + ".png";
            ExportPNG(split: true, fileP1: fileP1, fileP2: fileP2);

            //FileInfo f0 = new FileInfo(file);
            FileInfo f1 = new FileInfo(fileP1);
            FileInfo f2 = new FileInfo(fileP2);
            FileInfo[] FileList = new FileInfo[] { f1, f2 };

            /*foreach (FileInfo f in FileList)
            {
                var p = new Process();
                p.StartInfo.FileName = f.FullName;
                // p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Verb = "Print";
                p.Start();
            }*/
        }

    }
}
