using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee {
    public class RoundDiskProjectile : ModProjectile {

        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.LightDisc);
        }

    }
}
