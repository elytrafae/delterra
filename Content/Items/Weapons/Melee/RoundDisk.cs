using Delterra.Content.Projectiles.Weapons.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Melee {
    public class RoundDisk : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Weapons.Melee";

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 20;
            Item.knockBack = 7f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = Item.useAnimation = 20;
            Item.shoot = ModContent.ProjectileType<RoundDiskProjectile>();
            Item.shootSpeed = 10f;
            Item.rare = ItemRarityID.Green;
            Item.value = Terraria.Item.sellPrice(silver: 70);
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) {
            return player.ownedProjectileCounts[Item.shoot] <= 0;
        }

    }
}
