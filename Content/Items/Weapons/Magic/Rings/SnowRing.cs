using Delterra.Content.Projectiles;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Magic.Rings
{
    public class SnowRing : AbstractNoelleRing {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 120;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.shoot = ModContent.ProjectileType<IceShock>();
        }

    }
}
