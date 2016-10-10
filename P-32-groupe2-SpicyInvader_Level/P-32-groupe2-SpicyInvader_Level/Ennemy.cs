//Contain the Ennemy Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// ETML
/// Author : Raphaël Balmori
/// 
/// Date : 05.09.2016
/// Version : 1.0.0
/// Description : Containe the Ennemy class (Style, Display, Timer and Movment)
/// 
/// Date : 12.09.2016
/// Version : 1.1.0
/// Description : Removing the timer
/// 
/// Date : 26.09.2016
/// Version : 2.0.0
/// Description : Adding destroy methode and hit methode and the ennemies can fire
/// 
/// Date : 03.10.2016
/// Version : 2.1.0
/// Description : the ennemies sprites can now being modified (size)
/// </summary>
namespace SpicyInvader
{
    class Ennemy
    {
        //Conatain the enemies style and the animations (2 frames by Ennemy)
        static string[,,] ennemyStyle = new string[3, 2, 2] { { { "XX","><"}, { "XX","<>"} },
                { { "♦■","/\\"},{ "♦■","\\/"} },
                { { "==", "¦¦"},{ "==","¦¦"} } };

        //Contain the ennemy shoot style
        static string[] shootStyle = new string[3] { "I", "|", "!" };

        //Contain the Ennemy style
        private int numberOfStyle;

        //Containe the Ennemy shoot style
        private int numberOfShootStyle;

        //Contain the Ennemy position (EnnemyPos[0] = posX, EnnemyPos[1] = posY)
        int[] ennemyPos = new int[2];

        //Contain de shoot speed
        private int shootSpeed;

        private int frameNumber = 0;

        //true if the ennemy is alive
        public bool isAlive;

        //Ennemy armo style
        const string ENNEMY_ARMO = ":";

        bool isFinish = true;

        Mutex mut = new Mutex();

        int startShotX = 0;
        int startShotY = 0;

        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="_style"></param>
        /// <param name="_posX"></param>
        /// <param name="_posY"></param>
        /// <param name="_shootSpeed"></param>
        /// <param name="_shootStyle"></param>
        /// <param name="_alive"></param>
        public Ennemy(int _style = 0, int _posX = 3, int _posY = 2, int _shootSpeed = 50, int _shootStyle = 3, bool _alive = true)
        {
            numberOfStyle = _style;
            ennemyPos[0] = _posX;
            ennemyPos[1] = _posY;
            shootSpeed = _shootSpeed;
            numberOfShootStyle = _shootSpeed;
            isAlive = _alive;
            
            //SetEnnemyTimer();
        }

        /// <summary>
        /// Move ennemy left / right
        /// </summary>
        /// <param name="_right"></param>
        /// <returns></returns>
        public bool MoveEnnemy(bool _right)
        {
            //Erase the ennemies
            EnnemyErase();
            frameNumber++;

            //Make ennemy moves left or right
            if (_right)
            {
                ennemyPos[0]++;
            }
            else
            {
                ennemyPos[0]--;
            }


            //Make ennemy moves down (if the methode return is true, the programm execut the move down methode)
            if (ennemyPos[0] == (Constant.Level.WINDOWS_WIDTH - ennemyStyle[0,0,0].Length) && isAlive || ennemyPos[0] == 0 && isAlive)
            {
                return true;
            }
            
            //Write only the alive ennemies
            EnnemyWrite();

            return false;
        }

        /// <summary>
        /// Move ennemy down
        /// </summary>
        public void MoveEnnemyDown()
        {

            EnnemyErase();
            ennemyPos[1]++;

            EnnemyWrite();

        }

        public void Destroy()
        {
            isAlive = false;
            EnnemyErase();

            Sound.SoundHit();

            Level.Score += 100;
            //UserInterface.DisplayScore();
        }

        /// <summary>
        /// I don't know (for the moment)
        /// </summary>
        /// <param name="_posX"></param>
        /// <param name="_posY"></param>
        /// <returns></returns>
        public bool Hit(int _posX, int _posY)
        {
            _posX -= ennemyPos[0];
            _posY -= ennemyPos[1];

            if (_posX < ennemyStyle[numberOfStyle, 0, 0].Length && _posY < ennemyStyle.GetLength(2) && _posX >= 0 && _posY >= 0)
            {
                Destroy();
                return true;
            }

            return false;
        }


        public void Fire ()
        {
            new Thread(Shoot).Start();
        }

        private void Shoot()
        {
            isFinish = false;

            startShotX = ennemyPos[0];
            startShotY = ennemyPos[1] + 3;

            while (!isFinish)
            {
                mut.WaitOne();
                OnTimedEvent(ref isFinish);
                Thread.Sleep(shootSpeed);
                mut.ReleaseMutex();
            }

        }

        public void OnTimedEvent(ref bool isFinish)
        {
            mut.WaitOne();
            int? objectHit = Level.CheckIfObjectHere(startShotX, startShotY);
            if (objectHit == Constant.Level.ID_BARRICADE)
            {
                Level.ShootBaricade(startShotX, startShotY);
                isFinish = true;
            }
            else if (objectHit == Constant.Level.ID_PLAYER)
            {
                Program.player.SpaceShipHitted();
                isFinish = true;
            }
            else
            {
                Level.Erase(startShotX, startShotY, new string[] { ENNEMY_ARMO });
                Level.Write(startShotX, startShotY += 1, new string[] { ENNEMY_ARMO }, ConsoleColor.Red);

                if (startShotY > Constant.Level.WINDOWS_HEIGHT-2)
                {
                    Level.Erase(startShotX, startShotY, new string[] { ENNEMY_ARMO });
                    isFinish = true;

                }
            }
            mut.ReleaseMutex();
        }
    
        /// <summary>
        /// Erase the ennemy (style and hitbox)
        /// </summary>
        public void EnnemyErase()
        {
            Level.RemoveHitBox(ennemyPos[0], ennemyPos[1], new string[] { ennemyStyle[numberOfStyle, (frameNumber % 2), 0], ennemyStyle[numberOfStyle, (frameNumber % 2), 1] });
            Level.Erase(ennemyPos[0], ennemyPos[1], new string[] { ennemyStyle[numberOfStyle, (frameNumber % 2), 0], ennemyStyle[numberOfStyle, (frameNumber % 2), 1] });
        }

        /// <summary>
        /// Write the ennemy (style and hitbox)
        /// </summary>
        public void EnnemyWrite()
        {
            Level.SetHitBox(ennemyPos[0], ennemyPos[1], new string[] { ennemyStyle[numberOfStyle, (frameNumber % 2), 0], ennemyStyle[numberOfStyle, (frameNumber % 2), 1] }, Constant.Level.ID_ENNEMY);
            Level.Write(ennemyPos[0], ennemyPos[1], new string[] { ennemyStyle[numberOfStyle, (frameNumber % 2), 0], ennemyStyle[numberOfStyle, (frameNumber % 2), 1] });
        }
    }
}
