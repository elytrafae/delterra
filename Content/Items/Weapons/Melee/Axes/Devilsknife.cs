using Delterra.Content.Projectiles;
using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Melee.Axes {
    public class Devilsknife : AbstractSusieAxe {

        public override double RudeBusterCost => 40;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 55;
            Item.knockBack = 3.5f;
            Item.useTime = Item.useAnimation = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.shoot = ModContent.ProjectileType<DevilBuster>();
            Item.shootSpeed = 9;
            Item.scale = 1.2f;
        }

    }
}
