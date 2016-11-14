using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ETML
/// Author : Raphaël Balmori
/// 
/// Date : 03.10.2016
/// Version : 1.0.0
/// Description : ...
/// 
/// Date : 31.10.2016
/// Version 1.2.0
/// Dscription : Modification of the DisplayPlayerLife methode and creation of the ErasePlayerLife methode
/// </summary>
namespace SpicyInvader
{
    public static class UserInterface
    {
        const char INTERFACE_CHAR = '-';
        const int LIFE_MARGIN = 6;

        public static void CreateInterface()
        {
            //Temporal string
            string interfaceBorder = "";

            //Create the interface limite
            for (int i = 0; i < Constant.Level.WINDOWS_WIDTH; i++)
            {
                interfaceBorder += INTERFACE_CHAR;
            }
            Level.Write(0, Constant.Level.INTERFACE_BORDER, new string[] { Convert.ToString(interfaceBorder) }, ConsoleColor.DarkCyan);

            DisplayPlayerLife();

            DisplayScore();


        }

        /// <summary>
        /// Display the player lifes
        /// </summary>
        public static void DisplayPlayerLife()
        {
            ///The play lifes's color change according the number of life
            for (int i = 0; i < Level.PlayerLife; i++)
            {
                if (Level.PlayerLife >= 3)
                {
                    Level.Write((Constant.Level.WINDOWS_WIDTH - 5) - (i * LIFE_MARGIN), 2, new string[] { " X ", "XXX" }, ConsoleColor.Green);
                }
                else if (Level.PlayerLife == 2)
                {
                    Level.Write((Constant.Level.WINDOWS_WIDTH - 5) - (i * LIFE_MARGIN), 2, new string[] { " X ", "XXX" }, ConsoleColor.Yellow);
                }
                else
                {
                    Level.Write((Constant.Level.WINDOWS_WIDTH - 5) - (i * LIFE_MARGIN), 2, new string[] { " X ", "XXX" }, ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Erase the player lifes
        /// </summary>
        public static void ErasePlayerScore()
        {
            for (int i = 0; i < Level.PlayerLife; i++)
            {
                Level.Erase((Constant.Level.WINDOWS_WIDTH - 5) - (i * LIFE_MARGIN), 2, new string[] { " X ", "XXX" });
            }
        }

        /// <summary>
        /// Display the player score
        /// </summary>
        public static void DisplayScore()
        {
            Level.Write(3, 2, new string[] { "score : " + Level.Score });
        }
    }
}
