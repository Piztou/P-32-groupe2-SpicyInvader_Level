using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader
{
    public class Player
    {





        private static System.Timers.Timer tmrMooveEnememy = new System.Timers.Timer();


        static void Main(string[] args)
        {
            tmrMooveEnememy.Interval = 420;
            tmrMooveEnememy.Elapsed += new System.Timers.ElapsedEventHandler(MooveEnemy);
            tmrMooveEnememy.Start();
            Console.CursorVisible = false;
            Program.CreateEnnemy();
            SpaceShip player = new SpaceShip();
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
