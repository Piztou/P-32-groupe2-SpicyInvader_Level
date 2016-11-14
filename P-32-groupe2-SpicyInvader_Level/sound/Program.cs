using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace sound
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            
        }
        static Stream str = Properties.Resources.While_She_Sleeps___False_Freedom_8_bit_;
        static SoundPlayer snd = new SoundPlayer(str);

        public static void PlaySound()
        {
            snd.PlayLooping();
        }

        public static void StopSound()
        {
            snd.Stop();
        }
    }
}
