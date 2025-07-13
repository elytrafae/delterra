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

namespace Delterra.Content.Gores 
{
    public class IceShockBigSnowflake : ModGore {

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = 1;
        }

        public override bool Update(Gore gore) {
            gore.timeLeft--;
            Lighting.AddLight(gore.position + new Vector2(gore.Width / 2, gore.Height / 2), IceShock.LIGHT);
            return gore.timeLeft <= 0;
        }

        public override Color? GetAlpha(Gore gore, Color lightColor) {
            return base.GetAlpha(gore, lightColor);
        }

    }
}
