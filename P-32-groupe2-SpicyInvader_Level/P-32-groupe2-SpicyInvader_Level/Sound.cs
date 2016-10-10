using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace SpicyInvader
{
    public static class Sound
    {
        public static void SoundShot()
        {
            SoundPlayer sound;
            Random rnd = new Random();
            Stream str;
            if (rnd.Next(0, 2) == 0)
            {
                str = P_32_groupe2_SpicyInvader_Level.resources.Shot1;
                sound = new SoundPlayer(str);
            }
            else
            {
                str = P_32_groupe2_SpicyInvader_Level.resources.Shot2;
                sound = new SoundPlayer(str);
            }
            sound.Play();
        }

        public static void SoundHit()
        {
            Stream str;
            str = P_32_groupe2_SpicyInvader_Level.resources.Hit;
            SoundPlayer sound = new SoundPlayer(str);
            sound.Play();
        }

        public static void SoundMenu()
        {
            Stream str;
            str = P_32_groupe2_SpicyInvader_Level.resources.MenuMove;
            SoundPlayer sound = new SoundPlayer(str);
            sound.Play();
        }

        public static void SoundExplosion()
        {
            Stream str;
            str = P_32_groupe2_SpicyInvader_Level.resources.Explosion;
            SoundPlayer sound = new SoundPlayer(str);
            sound.Play();
        }
    }
}
