using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader
{
    public class Constant
    {
        public class Level
        {
            /// <summary>
            /// Default color used
            /// </summary>
            public const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

            public const int WINDOWS_WIDTH = 80;

            public const int WINDOWS_HEIGHT = 30;

            public const int INTERFACE_BORDER = 5;

            public class Barricades
            {

                public static string[] LIFE_STRING = new string[4] { " ", "x", "#", "█" };

                /// <summary>
                /// Give the look of the barricades, and his life points
                /// </summary>
                public static int[,] life = new int[3, 5]{{0,3,3,3,0},
                {3,3,3,3,3},
                {3,0,0,0,3}};

                /// <summary>
                /// Barricades origin coords, x,y   x = Barricade number, y = x/y
                /// </summary>
                public static int[,] coords = new int[4, 2] { { 15, WINDOWS_HEIGHT - 8 }, { 30, WINDOWS_HEIGHT - 8 }, { 45, WINDOWS_HEIGHT - 8 }, { 60, WINDOWS_HEIGHT - 8 } };
            }

            public const int ID_BARRICADE = 3;
            public const int ID_PLAYER = 1;
            public const int ID_ENNEMY = 2;
            public const int ID_SPECIAL = 4;
        }
    }
}
