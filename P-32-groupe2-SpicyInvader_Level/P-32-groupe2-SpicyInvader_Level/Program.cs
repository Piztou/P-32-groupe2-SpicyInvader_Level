using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader
{
    public static class Player
    {

        public static SpaceShip player = new SpaceShip();



        private static System.Timers.Timer tmrMooveEnememy = new System.Timers.Timer();


        static void Main(string[] args)
        {
            tmrMooveEnememy.Interval = 420;
            tmrMooveEnememy.Elapsed += new System.Timers.ElapsedEventHandler(MooveEnemy);
            tmrMooveEnememy.Start();
            Console.CursorVisible = false;
            Console.SetBufferSize(Constant.Level.WINDOWS_WIDTH,Constant.Level.WINDOWS_HEIGHT);
            Console.SetWindowSize(Constant.Level.WINDOWS_WIDTH, Constant.Level.WINDOWS_HEIGHT);
            Console.CursorVisible = false;
            Program.CreateEnnemy();
            player.spawnSpaceShip();
            //initializes all the barricades
            Level.InitBaricades();
            player.Move();
            Console.ReadLine();
        }
        
        public static void MooveEnemy(object sender, EventArgs e)
        {
            Program.SleepTimer();
        }

    }
}
