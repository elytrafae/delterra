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
    public class ManeAx : AbstractSusieAxe {
        

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 45;
            Item.knockBack = 3f;
            Item.useTime = Item.useAnimation = 45;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.shootSpeed = 7;
            Item.shoot = ModContent.ProjectileType<ManeBuster>();
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.IronAxe)
                .AddIngredient(ItemID.RottenChunk, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.IronAxe)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadAxe)
                .AddIngredient(ItemID.RottenChunk, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadAxe)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
