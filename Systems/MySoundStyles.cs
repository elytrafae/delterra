using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;

namespace Delterra.Systems {
    public class MySoundStyles {

        // To anyone who wants to help me add sound effects,
        // all you have to do is copy the following line and change "SoundNameInCode" and 
        // "sound_filename" (etc.) for the name of the sound file inside the SoundEffects folder (without .ogg!)
        // public static readonly SoundStyle SoundNameInCode = RegisterSound("sound_filename"); 
        // public static readonly SoundStyle MusicNameInCode = RegisterMusic("music_filename"); 
        // public static readonly SoundStyle AmbientNameInCode = RegisterAmbient("ambient_filename"); 

        public static readonly SoundStyle Graze = RegisterSound("Graze");
        public static readonly SoundStyle Heal = RegisterSound("Heal");
        public static readonly SoundStyle Iceshock = RegisterSound("Iceshock");
        public static readonly SoundStyle Pacify = RegisterSound("Pacify");
        public static readonly SoundStyle Petrify = RegisterSound("Petrify");
        public static readonly SoundStyle RudeBusterHit = RegisterSound("Rudebuster_Hit");
        public static readonly SoundStyle RudeBusterSwing = RegisterSound("Rudebuster_Swing");
        public static readonly SoundStyle SnowgraveBell = RegisterSound("Snowgrave_Bell");
        public static readonly SoundStyle SnowgraveCast = RegisterSound("Snowgrave_Cast");
        public static readonly SoundStyle Tension = RegisterSound("Tension");


        private static SoundStyle RegisterSound(string name) { 
            return new SoundStyle(nameof(Delterra) + "/Assets/SoundEffects/" + name, SoundType.Sound);
        }

        private static SoundStyle RegisterMusic(string name) {
            return new SoundStyle(nameof(Delterra) + "/Assets/Music/" + name, SoundType.Music);
        }

        private static SoundStyle RegisterAmbient(string name) {
            return new SoundStyle(nameof(Delterra) + "/Assets/AmbientSounds/" + name, SoundType.Ambient);
        }

    }
}
