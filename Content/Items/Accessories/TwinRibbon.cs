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
    public class TwinRibbon : ModItem {

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            EquipmentEffectPlayer.Get(player).tensionRestorePotionSicknessReduced = true;
            GrazingPlayer.Get(player).bigGrazeAreaStat = true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<WhiteRibbon>()
                .AddIngredient<PinkRibbon>()
                .AddIngredient<MaliusHammer>()
                .AddTile(TileID.Hellforge)
                .Register();
        }

    }
}
