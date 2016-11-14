using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader
{
    public static class Program
    {
        public static Level currentLevel;
        
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(Constant.Level.WINDOWS_WIDTH,Constant.Level.WINDOWS_HEIGHT);
            Console.SetWindowSize(Constant.Level.WINDOWS_WIDTH, Constant.Level.WINDOWS_HEIGHT);
            Console.CursorVisible = false;

            //TEMP : Initialize the level
            currentLevel = new Level();
            Console.ReadLine();
        }
        
        public static void MooveEnemy(object sender, EventArgs e)
        {
            Ennemies.SleepTimer();
        }

        public static void MoovRedSpacShipTimer(object sender, EventArgs e)
        {
            RedSpaceship.MoovRedSpaceship();
        }

    }
}
