using Delterra.Content.Mounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Mounts {
    internal class IceColdBeer : ModItem {

        public override void SetDefaults() {
            Item.mountType = ModContent.MountType<TheKingsChariot>();
            Item.width = 34;
            Item.height = 22;
            Item.value = Terraria.Item.sellPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Minecart)
                .AddIngredient(ItemID.FlowerBoots)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddIngredient(ItemID.Ale, 20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

    }
}
