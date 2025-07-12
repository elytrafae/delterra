using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {
    internal class PinkRibbon : ModItem {

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.White;
            Item.value = Terraria.Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            GrazingPlayer.Get(player).bigGrazeAreaStat = true;
        }

        /*
        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.PinkGel, 10)
                .AddIngredient(ItemID.PinkPearl)
                .AddTile(TileID.Solidifier)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.PinkGel, 10)
                .AddIngredient(ItemID.ShadowScale, 10)
                .AddTile(TileID.Solidifier)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.PinkGel, 10)
                .AddIngredient(ItemID.TissueSample, 10)
                .AddTile(TileID.Solidifier)
                .Register();
        }
        */

    }
}
