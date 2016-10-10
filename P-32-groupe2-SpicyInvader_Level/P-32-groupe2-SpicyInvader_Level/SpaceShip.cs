using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace SpicyInvader
{

    public class SpaceShip
    {

        public static Mutex mut = new Mutex();

        int x = 35;
        int y = 27;
        public int startShotY;
        public int startShotX;
        const string PLAYERAMMO = "¦";
        string[] SPACESHIP = new string[] { " | | ", "</^\\>", " \\H/ " };
        string[] SPACESHIPEXPLODE1 = new string[] { "   ", " x  ", "   " };
        string[] SPACESHIPEXPLODE2 = new string[] { "XXX", "X X", "XXX" };
        string[] SPACESHIPEXPLODE3 = new string[] { " XXX ", "X   X", "X   X", "X   X", " XXX " };
        const int MAXBORDERRIGHT = Constant.Level.WINDOWS_WIDTH;
        const int MAXBORDERLEFT = 0;
        const int SHOOTSPEED = 30;
        const int RESPAWNTIME = 2000;
        const int INVICIBLETIME = 3500;
        bool isFinish = true;
        bool canMoove = true;
        bool invincible = false;
        
        public void Move()
        {

            int previousPos = 0;
            ConsoleKeyInfo key;

            do
            {
                
                key = Console.ReadKey(true);

                if (canMoove && key.Key == ConsoleKey.Spacebar)
                {
                    mut.WaitOne();
                    if (isFinish)
                    {
                        isFinish = false;
                        new Thread(Shoot).Start();
                    }
                    mut.ReleaseMutex();

                }
                else if (canMoove && (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow && x < MAXBORDERRIGHT))
                {
                    previousPos = x;
                    x++;
                    RefreshSpaceShip(x, previousPos);
                }
                else if (canMoove && (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow && x > MAXBORDERLEFT))
                {
                    previousPos = x;
                    x--;
                    RefreshSpaceShip(x, previousPos);
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                }

            } while (true);

        }

        public void spawnSpaceShip()
        {
            if (invincible)
            {
                Level.Write(x, y - 1, SPACESHIP, ConsoleColor.Cyan);
            }
            else
            {
                Level.Write(x, y - 1, SPACESHIP);
            }
            Level.SetHitBox(x, y - 1, SPACESHIP, Constant.Level.ID_PLAYER);
        }

        public void RefreshSpaceShip(int x, int previousPos)
        {
            Level.RemoveHitBox(previousPos, y - 1, SPACESHIP);
            Level.SetHitBox(x, y, SPACESHIP, Constant.Level.ID_PLAYER);
            Level.Erase(previousPos, y - 1, SPACESHIP);
            if (invincible)
            {
                Level.Write(x, y - 1, SPACESHIP, ConsoleColor.Cyan);
            }
            else
            {
                Level.Write(x, y - 1, SPACESHIP);
            }
        }

        public void Shoot()
        {
            Sound.SoundShot();
            startShotY = y - 1;
            startShotX = x + 2;
            while (!isFinish)
            {
                mut.WaitOne();
                OnTimedEvent(ref isFinish);
                Thread.Sleep(SHOOTSPEED);
                mut.ReleaseMutex();
            }

        }

        public void SpaceShipHitted()
        {
            if (!invincible)
            {
                invincible = true;
                canMoove = false;
                Sound.SoundExplosion();
                Level.Erase(x, y - 1, SPACESHIP);
                Level.Write(x + 1, y - 1, SPACESHIPEXPLODE1, ConsoleColor.White);
                Thread.Sleep(500);
                Level.Write(x + 1, y - 1, SPACESHIPEXPLODE2, ConsoleColor.Yellow);
                Thread.Sleep(500);
                Level.Write(x, y - 2, SPACESHIPEXPLODE3, ConsoleColor.Red);
                Thread.Sleep(500);
                Level.Erase(x, y - 2, SPACESHIPEXPLODE3);
                DestroySpaceShip();
                Thread.Sleep(RESPAWNTIME);
                canMoove = true;
                invincible = true;
                spawnSpaceShip();
                Thread.Sleep(INVICIBLETIME);
                invincible = false;
            }
        }

        public void DestroySpaceShip()
        {
            Level.RemoveHitBox(x, y - 1, SPACESHIP);
            Level.Erase(x, y - 1, SPACESHIP);

        }

        public void OnTimedEvent(ref bool isFinish)
        {
            int? objectHit = Level.CheckIfObjectHere(startShotX, startShotY);
            if (objectHit == Constant.Level.ID_BARRICADE)
            {
                Level.ShootBaricade(startShotX, startShotY);
                isFinish = true;
            }
            else if (objectHit == Constant.Level.ID_ENNEMY)
            {
                Ennemies.HitEnnemy(startShotX, startShotY);
                isFinish = true;
            }
            else
            {
                Level.Erase(startShotX, startShotY, new string[] { PLAYERAMMO });
                Level.Write(startShotX, startShotY -= 1, new string[] { PLAYERAMMO }, ConsoleColor.Green);

                if (startShotY < 1)
                {
                    Level.Erase(startShotX, startShotY, new string[] { PLAYERAMMO });
                    isFinish = true;

                }
            }
        }
    }
}
