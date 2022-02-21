using System.Drawing;

namespace ExamManager
{
    public class Colors
    {
        public enum Theme { light, dark };
        public static Theme theme = Theme.dark;
        //public static Color roomBorderColor = Color.LightSlateGray;
        //public static Color roomBgColor = Color.LightSkyBlue;
        public static Color TL_TimeLineBorder = Color.FromArgb(36, 36, 36);
        public static Color TL_Entity = Color.FromArgb(140, 210, 240);
        public static Color TL_EntityBorder = Color.FromArgb(70, 70, 70);
        public static Color TL_TimeBg = Color.FromArgb(128, 128, 128);
        public static Color TL_TimeBorder = Color.FromArgb(90, 90, 90);
        public static Color TL_RoomBg = Color.FromArgb(128, 128, 128);
        public static Color TL_RoomEntityBg = Color.FromArgb(0, 140, 255);
        public static Color TL_RoomBorder = Color.FromArgb(0, 80, 150);
        public static Color TL_Bg = Color.FromArgb(60, 60, 60);
        public static Color TL_TimeLineBg = Color.FromArgb(128,128,128);
        public static Color Edit_Bg = Color.FromArgb(80, 80, 80);
        public static Color Menu_Bg = Color.FromArgb(120, 120, 120);
        public static Color Edit_ModeBg = Color.FromArgb(170, 170, 170);



        public static void ColorTheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.light:
                    TL_TimeLineBorder = Color.FromArgb(90, 90, 90);
                    TL_Entity = Color.FromArgb(173, 216, 230);
                    TL_EntityBorder = Color.FromArgb(80, 80, 80);
                    TL_TimeBg = Color.FromArgb(160, 160, 160);
                    TL_TimeBorder = Color.FromArgb(110, 110, 110);
                    TL_RoomBg = Color.FromArgb(180, 180, 180);
                    TL_RoomEntityBg = Color.FromArgb(90, 180, 255);
                    TL_RoomBorder = Color.FromArgb(30, 130, 230);
                    TL_Bg = Color.FromArgb(200, 200, 200);
                    TL_TimeLineBg = Color.FromArgb(180, 180, 180);
                    Edit_Bg = Color.FromArgb(180, 180, 180);
                    Menu_Bg = Color.FromArgb(120, 120, 120);
                    Edit_ModeBg = Color.FromArgb(150, 150, 150);

                    break;
                case Theme.dark:
                    TL_TimeLineBorder = Color.FromArgb(36, 36, 36);
                    TL_Entity = Color.FromArgb(140, 210, 240);
                    TL_EntityBorder = Color.FromArgb(70, 70, 70);
                    TL_TimeBg = Color.FromArgb(128, 128, 128);
                    TL_TimeBorder = Color.FromArgb(90, 90, 90);
                    TL_RoomBg = Color.FromArgb(128, 128, 128);
                    TL_RoomEntityBg = Color.FromArgb(0, 140, 255);
                    TL_RoomBorder = Color.FromArgb(0, 80, 150);
                    TL_Bg = Color.FromArgb(60, 60, 60);
                    TL_TimeLineBg = Color.FromArgb(128,128,128);
                    Edit_Bg = Color.FromArgb(80, 80, 80);
                    Menu_Bg = Color.FromArgb(120, 120, 120);
                    Edit_ModeBg = Color.FromArgb(170, 170, 170);
                    break;
            }
        }

        // darkBlueBlack 25,29,31
        // darkBlueBlack 45,45,48
        // lightBlueBlack 62,62,66
        // darkRedBlack 30,30,30
        // blue 50,130,160
        // blue 20,90,120


    }
}
