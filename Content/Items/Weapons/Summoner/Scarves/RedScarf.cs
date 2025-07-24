using Delterra.Content.Items.Spells.HealPrayer;
using Delterra.Content.Projectiles;
using Delterra.Content.Projectiles.Weapons.Summoner.Scarves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Summoner.Scarves {
    internal class RedScarf : AbstractRalseiScarf {
        public override int Heal => 70;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.shoot = ModContent.ProjectileType<RedScarfProjectile>();//ProjectileID.CopperShortswordStab;
            Item.damage = 20;
            Item.useTime = Item.useAnimation = 90;
            Item.autoReuse = true;
        }
    }
}
