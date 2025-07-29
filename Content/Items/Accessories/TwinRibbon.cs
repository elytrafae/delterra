using Delterra.Systems;
using Delterra.Systems.TPSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {

    [AutoloadEquip(EquipType.Face)]
    public class TwinRibbon : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Accessories";

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            GrazingPlayer grazePlayer = GrazingPlayer.Get(player);
            grazePlayer.grazeAreaMult += 0.25f;
            grazePlayer.TPChangeStats[TPGainType.RESTORE_ITEM] += 0.25f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
            int[] incompatibleItems = [ModContent.ItemType<WhiteRibbon>(), ModContent.ItemType<PinkRibbon>()];
            bool incompatible = (equippedItem.type == Type && incompatibleItems.Contains(incomingItem.type)) || (incomingItem.type == Type && incompatibleItems.Contains(equippedItem.type));
            return !incompatible;
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
