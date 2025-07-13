using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Axes {
    public class ManeAx : AbstractSusieAxe {
        

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 32;
            Item.knockBack = 3f;
            Item.useTime = (Item.useAnimation = 50);
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 10, 0);
            Item.shootSpeed = 7;
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
