using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Delterra.Systems.TPSources;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {

    [AutoloadEquip(EquipType.Face)]
    public class WhiteRibbon : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Accessories";

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.White;
            Item.value = Terraria.Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            GrazingPlayer.Get(player).TPChangeStats[TPGainType.RESTORE_ITEM] += 0.25f;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .Register();
        }

    }
}
