using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {
    internal class GlowWrist : ModItem {

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 30, 0);
            Item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.greenLightLevel = 1.6f;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Shackle)
                .AddIngredient(ItemID.Glowstick, 50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

    }
}
