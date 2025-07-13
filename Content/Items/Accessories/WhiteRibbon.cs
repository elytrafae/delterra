using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {

    [AutoloadEquip(EquipType.Face)]
    public class WhiteRibbon : ModItem {

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.White;
            Item.value = Terraria.Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            EquipmentEffectPlayer.Get(player).tensionRestorePotionSicknessReduced = true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .Register();
        }

    }
}
