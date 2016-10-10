using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace ConsoleApplication1
{
    public static class Sound
    {
        public static void SoundShot()
        {
            SoundPlayer sound;
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 0)
            {
                sound = new SoundPlayer("Shot1");
            }
            else
            {
                sound = new SoundPlayer("Shot2");
            }
            sound.Play();
        }

        public static void SoundHit()
        {
            SoundPlayer sound = new SoundPlayer("Hit");
            sound.Play();
        }

        public static void SoundMenu()
        {
            SoundPlayer sound = new SoundPlayer("MenuMove");
            sound.Play();
        }

        public static void SoundExplosion()
        {
            SoundPlayer sound = new SoundPlayer("Explosion");
            sound.Play();
        }
    }
}
