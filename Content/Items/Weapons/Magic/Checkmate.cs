using Delterra.Content.Projectiles.Weapons.Magic;
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

namespace Delterra.Content.Items.Weapons.Magic {
    public class Checkmate : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Weapons.Magic";

        public override void SetDefaults() {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<CheckmateProjectile>(), 40, 4f, true);
            Item.mana = 20;
            Item.damage = 12;
            Item.knockBack = 3f;
            Item.rare = ItemRarityID.Green;
            Item.value = Terraria.Item.sellPrice(silver: 70);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            for (int i = 0; i < 10; i++) {
                Projectile.NewProjectile(source, position, velocity + new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f)), type, damage, knockback, player.whoAmI);
            }
            return true;
        }

    }
}
