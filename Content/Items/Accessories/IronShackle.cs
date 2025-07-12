using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {
    public class IronShackle : ModItem {

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.White;
            Item.value = Terraria.Item.sellPrice(0, 0, 10, 50);
            Item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.GetDamage(DamageClass.Generic) += 0.01f;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Shackle)
                .AddIngredient(ItemID.Chain, 5)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }

    }
}
