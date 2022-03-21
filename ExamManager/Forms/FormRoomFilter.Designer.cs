
namespace ExamManager
{
    partial class FormRoomFilter
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
            this.clb_rooms = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // clb_rooms
            // 
            this.clb_rooms.CheckOnClick = true;
            this.clb_rooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_rooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clb_rooms.FormattingEnabled = true;
            this.clb_rooms.Location = new System.Drawing.Point(0, 0);
            this.clb_rooms.Name = "clb_rooms";
            this.clb_rooms.Size = new System.Drawing.Size(234, 292);
            this.clb_rooms.TabIndex = 0;
            // 
            // FormRoomFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 292);
            this.Controls.Add(this.clb_rooms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormRoomFilter";
            this.Text = "Raum Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRoomFilter_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clb_rooms;
    }
}