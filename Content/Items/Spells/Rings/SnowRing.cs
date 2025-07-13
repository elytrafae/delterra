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

namespace Delterra.Content.Items.Spells.Rings
{
    public class SnowRing : ModItem {

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.mana = 20;
            Item.noUseGraphic = true;
            //Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<IceShock>();
            Item.shootSpeed = 0;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            position = Main.MouseWorld;
        }

    }
}
