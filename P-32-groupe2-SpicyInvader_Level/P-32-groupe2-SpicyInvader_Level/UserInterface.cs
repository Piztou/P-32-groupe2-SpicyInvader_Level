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
            for(int i = 0; i < Constant.Level.WINDOWS_WIDTH;i++)
            {
                Console.SetCursorPosition(i, 5);
                Console.Write(INTERFACE_CHAR);
            }
        }
    }
}
