using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;

namespace YetToBeNamed.Systems {
    public class MySoundStyles {

        // To anyone who wants to help me add sound effects,
        // all you have to do is copy the following line and change "SoundNameInCode" and 
        // "sound_filename" for the name of the sound file inside the SoundEffects folder (without .ogg!)
        // public static readonly SoundStyle SoundNameInCode = RegisterSound("sound_filename"); 



        private static SoundStyle RegisterSound(string name) { 
            return new SoundStyle(nameof(YetToBeNamed) + "/Assets/SoundEffects/" + name, SoundType.Sound);
        }

        private static SoundStyle RegisterMusic(string name) {
            return new SoundStyle(nameof(YetToBeNamed) + "/Assets/Music/" + name, SoundType.Music);
        }

        private static SoundStyle RegisterAmbient(string name) {
            return new SoundStyle(nameof(YetToBeNamed) + "/Assets/AmbientSounds/" + name, SoundType.Ambient);
        }

    }
}
