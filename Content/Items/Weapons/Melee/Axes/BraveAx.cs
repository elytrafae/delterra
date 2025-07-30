using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Melee.Axes {
    public class BraveAx : AbstractSusieAxe {
        

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 50;
            Item.knockBack = 3.5f;
            Item.useTime = Item.useAnimation = 50;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.shoot = ModContent.ProjectileType<BraveBuster>();
            Item.shootSpeed = 8;
            Item.scale = 1.1f;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<ManeAx>()
                .AddIngredient(ItemID.ShadowScale, 10)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient<ManeAx>()
                .AddIngredient(ItemID.TissueSample, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
