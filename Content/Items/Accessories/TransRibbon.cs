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
    public class TransRibbon : ModItem {

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Green;
            Item.value = Terraria.Item.sellPrice(0, 5, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            GrazingPlayer.Get(player).bigGrazeAreaStat = true;
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.tensionRestorePotionSicknessReduced = true;
            modPlayer.healingMultiplier += 0.25f;
            modPlayer.commonLifeRegen += 2;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<TwinRibbon>()
                .AddIngredient<BlueRibbon>()
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddTile(TileID.Solidifier)
                .Register();
        }

    }
}
