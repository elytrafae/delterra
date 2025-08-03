using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class AbsorbBuster : BraveBuster {

        public override Color BusterColor => new Color(1f, 0.2f, 0.2f);

        public override int ImpactProjectileType => ModContent.ProjectileType<AbsorbBusterImpact>();

    }
}
