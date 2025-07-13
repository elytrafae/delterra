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
    public class IceShockSmallSnowflake : ModGore {

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = 20;
        }

        public override bool Update(Gore gore) {
            Lighting.AddLight(gore.position + new Vector2(gore.Width / 2, gore.Height / 2)*gore.scale, IceShock.LIGHT * 0.8f * ((float)(255 - gore.alpha) / 255));
            return true;
        }

    }
}
