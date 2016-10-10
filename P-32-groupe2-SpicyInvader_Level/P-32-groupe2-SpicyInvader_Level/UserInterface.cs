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
/// </summary>
namespace SpicyInvader
{
    public static class UserInterface
    {
        const char INTERFACE_CHAR = '-';
        const int LIFE_MARGIN = 6;

        public static void CreateInterface()
        {
            //Create the interface limite
            for (int i = 0; i < Constant.Level.WINDOWS_WIDTH; i++)
            {
                Level.Write(i, 5, new string[]{ Convert.ToString(INTERFACE_CHAR) }, ConsoleColor.DarkCyan);
            }

            DisplayPlayerLife();

            DisplayScore();


        }

        /// <summary>
        /// Display the player lifes
        /// </summary>
        public static void DisplayPlayerLife()
        {
            for(int i = 0; i < Level.PlayerLife;i++)
            {
                Level.Write(( Constant.Level.WINDOWS_WIDTH - 5) - (i * LIFE_MARGIN), 2, new string[] { " X ", "XXX" });
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
