using Delterra.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Delterra.Content.Gores {
    internal class RealisticExplosion : ModGore {

        private static int Frames => 17;
        private static int TimePerFrame => 3;

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = TimePerFrame * Frames;
            gore.numFrames = (byte)Frames;
            gore.frame = 0;
        }

        public override bool Update(Gore gore) {
            gore.timeLeft--;
            if (gore.timeLeft > 0) {
                if (gore.timeLeft % TimePerFrame == 0) {
                    gore.frame++;
                }
                Lighting.AddLight(gore.position + new Vector2(gore.Width / 2, gore.Height / 2), new Vector3(0.5f, 0.25f, 0f));
            }
            return gore.timeLeft <= 0;
        }

        public override Color? GetAlpha(Gore gore, Color lightColor) {
            return gore.timeLeft <= 0 ? Color.Transparent : lightColor;
        }


    }
}
