using Delterra.Content.Items.Spells.HealPrayer;
using Delterra.Content.Projectiles;
using Delterra.Content.Projectiles.Scarves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Scarves {
    internal class RedScarf : AbstractRalseiScarf {
        public override int Heal => 70;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.shoot = ModContent.ProjectileType<RedScarfProjectile>();//ProjectileID.CopperShortswordStab;
            Item.shootSpeed = 3;
            Item.damage = 20;
        }
    }
}
