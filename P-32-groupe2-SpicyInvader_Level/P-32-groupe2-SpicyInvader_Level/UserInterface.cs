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
    public class UserInterface
    {
        const char INTERFACE_CHAR = '-';
        public UserInterface()
        {

        }

        public void CreateInterface()
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
        public void DisplayPlayerLife()
        {
            for(int i = 0; i < 3;i++)
            {
                Level.Write(( Constant.Level.WINDOWS_WIDTH - 5) - (i * 5), 2, new string[] { " X ", "XXX" });
            }
        }

        public void DisplayScore()
        {
            Console.SetCursorPosition(3,4);
            Console.Write("Score : " + Level.score);
        }
    }
}
