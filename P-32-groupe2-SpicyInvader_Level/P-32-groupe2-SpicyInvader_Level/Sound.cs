using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace ConsoleApplication1
{
    class Sound
    {
        private void SoundShot()
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

        private void SoundHit()
        {
            SoundPlayer sound = new SoundPlayer("Hit");
            sound.Play();
        }

        private void SoundMenu()
        {
            SoundPlayer sound = new SoundPlayer("MenuMove");
            sound.Play();
        }

        private void SoundExplosion()
        {
            SoundPlayer sound = new SoundPlayer("Explosion");
            sound.Play();
        }
    }
}
