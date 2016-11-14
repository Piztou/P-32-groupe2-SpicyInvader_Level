using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

/// <summary>
/// ETML
/// Author : Raphaël Balmori
/// Date : 05.09.2016
/// Version 1.0.0
/// 
/// Date : 12.09.2016
/// Version 1.1.0
/// Description : Adding the timer in the Program Class
/// 
/// Date : 12.09.2016
/// Version 2.0.0
/// Description : Adding ennemies movements whit a thread sleep
/// 
/// Date : 26.09.2016
/// Version 2.1.0
/// Description : Adding new methode (CreateEnnemy, WhoShoot and HitEnnemy)
/// 
/// Date : 31.10.2016
/// Version 2.1.1
/// Description : Fix the WhoSoot methode's bug
/// </summary>
namespace SpicyInvader
{
    class Ennemies
    {

        private static System.Timers.Timer tmrMooveEnememy = new System.Timers.Timer();

        static Random rdm = new Random();

        //Number of display ennemies
        const int ENNEMY_ARRAY_X = 9;
        const int ENNEMY_ARRAY_Y = 4;

        //Ennemies margins
        const int HORIZONTAL_MARGIN = 5;
        const int VERTICAL_MARGIN = 3;

        //Ennemies speed
        public static int ennemySpeed = 350;

        //ennemies shoot speed
        public static int ennemyShootSpeed = 40;

        //ennemies shoot tic
        const int ENNEMY_SHOOT_TIC = 6;

        public static int curentTic = 0;

        //if true : ennemies move right and if false : ennemies move left
        static bool moveRight = true;

        //This array contains every ennemies
        public static Ennemy[,] ennemyArray = new Ennemy[ENNEMY_ARRAY_X, ENNEMY_ARRAY_Y];

        public static int numberOfEnnemies = 0;

        static Mutex mut = new Mutex();

        //TO DO: Delate


        /// <summary>
        /// Check every alive ennemy and check if the ennemy positions is the same of the shoot positions
        /// </summary>
        /// <param name="_posX">Player shoot position X</param>
        /// <param name="_posY">Player shoot position Y</param>
        public static void HitEnnemy(int _posX, int _posY)
        {
            foreach (Ennemy ennemy in ennemyArray)
            {
                if (ennemy.isAlive)
                {
                    ennemy.Hit(_posX, _posY);
                }
            }
        }

        /// <summary>
        /// Make ennemies move
        /// </summary>
        public static void SleepTimer()
        {
            if (!Level.pause && !Level.noMorePlayerLife)
            {
                curentTic++;

                if (curentTic == ENNEMY_SHOOT_TIC)
                {

                    WhoShoot();
                    curentTic = 0;
                }

                //if true : ennemy move down once
                bool arrivedAtBorder = false;

                arrivedAtBorder = false;

                //CHeck every ennemies for know if one touches the border if it is alive
                foreach (Ennemy ennemy in ennemyArray)
                {
                    if (ennemy.isAlive && ennemy.MoveEnnemy(moveRight))
                    {
                        arrivedAtBorder = true;
                    }
                }

                //Move every ennemies down
                if (arrivedAtBorder)
                {
                    RedSpaceship aSpaceShip = new RedSpaceship();
                    foreach (Ennemy ennemy in ennemyArray)
                    {
                        if (ennemy.isAlive)
                        {
                            ennemy.MoveEnnemyDown();
                        }
                    }

                    //Check if the ennemies have to move left or right
                    if (moveRight)
                    {
                        moveRight = false;
                    }
                    else
                    {
                        moveRight = true;
                    }

                }

                //Console.Beep(410, 200);

                Thread.Sleep(300);

            }
        }

        /// <summary>
        /// Create every ennemies and dispaly them
        /// </summary>
        public static void CreateEnnemy()
        {
            //Initialisation timer
            tmrMooveEnememy.Interval = ennemySpeed;
            tmrMooveEnememy.Elapsed += new System.Timers.ElapsedEventHandler(Program.MooveEnemy);
            tmrMooveEnememy.Start();

            bool isAlive = true;

            //Create all the ennemies and add they in the ennemy array
            for (int i = 1; i <= ENNEMY_ARRAY_X; i++)
            {
                for (int j = 1; j <= ENNEMY_ARRAY_Y; j++)
                {
                    //Create the ennemy and add it in the ennemy array
                    Ennemy ennemy = new Ennemy(j % 3, i * HORIZONTAL_MARGIN, (j * VERTICAL_MARGIN) + 4, ennemyShootSpeed, 3, isAlive);
                    ennemyArray[i - 1, j - 1] = ennemy;

                    numberOfEnnemies++;
                }
            }


        }

        /// <summary>
        /// Choose whitch ennemy have to shot
        /// TODO : Write it on the Level.cs
        /// </summary>
        static void WhoShoot()
        {
            int rdmEnnemyX;
            int rdmEnnemyY;

            //Choose a random value for an alive ennemy
            do
            {
                rdmEnnemyX = rdm.Next(0, ENNEMY_ARRAY_X);
                rdmEnnemyY = rdm.Next(0, ENNEMY_ARRAY_Y);
            } while (!ennemyArray[rdmEnnemyX, rdmEnnemyY].isAlive);

            //mut.WaitOne();

            ennemyArray[rdmEnnemyX, rdmEnnemyY].Fire();

            //mut.ReleaseMutex();
        }
    }


}
