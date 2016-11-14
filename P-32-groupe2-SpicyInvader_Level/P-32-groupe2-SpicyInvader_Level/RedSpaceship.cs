using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader
{
    class RedSpaceship
    {
        private static System.Timers.Timer tmrMooveRedSpaceship = new System.Timers.Timer();

        static string[] redSpaceshipStyle = new string[2] { " _ _ ", "<_o_>"};
        int redSpaceshipSpeed = 80;
        static int posX = Constant.Level.WINDOWS_WIDTH - redSpaceshipStyle[0].Length;
        static int posY = 6;

        /// <summary>
        /// Builder
        /// </summary>
        public RedSpaceship()
        {
            //Initialisation timer
            tmrMooveRedSpaceship.Interval = redSpaceshipSpeed;
            tmrMooveRedSpaceship.Elapsed += new System.Timers.ElapsedEventHandler(Program.MoovRedSpacShipTimer);

            StartRedSpaceship();
        }

        /// <summary>
        /// Make the red spaceship moove
        /// </summary>
        public static void MoovRedSpaceship()
        {
            //Erase the red spaceship
            Level.RemoveHitBox(posX, posY, redSpaceshipStyle);
            Level.Erase(posX, posY, redSpaceshipStyle);

            posX -= 1;

            //Write the red spaceship
            Level.SetHitBox(posX, posY, redSpaceshipStyle, Constant.Level.ID_SPECIAL);
            Level.Write(posX, posY, redSpaceshipStyle, ConsoleColor.Red);
        }

        /// <summary>
        /// Start and write the red spaceship
        /// </summary>
        public static void StartRedSpaceship()
        {

                tmrMooveRedSpaceship.Start();
                Level.Write(posX, posY, redSpaceshipStyle, ConsoleColor.Red);
            
        }

        /// <summary>
        /// Stop and erase the red spaceship
        /// </summary>
        public static void StopRedSpaceship()
        {
            if (posX == redSpaceshipStyle[0].Length*3)
            {
                tmrMooveRedSpaceship.Stop();
                Level.Erase(posX, posY, redSpaceshipStyle);
            }
        }

        public static void DestroyRedSpaceship()
        {
            Level.Erase(posX, posY, redSpaceshipStyle);
            Level.Score += 500;

        }


    }
}
