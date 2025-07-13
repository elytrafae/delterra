using Delterra.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Delterra.Content.Items.Spells.Axes {
    internal class JusticeAx : AbstractSusieAxe {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 250;
            Item.knockBack = 5f;
            Item.useTime = (Item.useAnimation = 50);
            Item.rare = ItemRarityID.Yellow;
            Item.value = Terraria.Item.buyPrice(1, 0, 0, 0);
            Item.scale = 2f;
            Item.shoot = ModContent.ProjectileType<JusticeRudeBuster>();
            Item.shootSpeed = 13;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            damage += damage / 2;
        }

    }
}
