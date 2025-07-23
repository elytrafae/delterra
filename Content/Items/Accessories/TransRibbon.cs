using Delterra.Systems;
using FaeLibrary.API.ClassExtensions;
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
    public class TransRibbon : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Accessories";

        public override void SetStaticDefaults() {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Green;
            Item.value = Terraria.Item.sellPrice(0, 5, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            GrazingPlayer.Get(player).grazeAreaMult += 0.25f;
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.tensionRestorePotionSicknessReduced = true;
            player.GetCommonPositiveRegenStat() += 2;
            player.GetPotionHealingStat() += 0.25f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
            int[] incompatibleItems = [ModContent.ItemType<WhiteRibbon>(), ModContent.ItemType<PinkRibbon>(), ModContent.ItemType<TwinRibbon>(), ModContent.ItemType<BlueRibbon>()];
            bool incompatible = (equippedItem.type == Type && incompatibleItems.Contains(incomingItem.type)) || (incomingItem.type == Type && incompatibleItems.Contains(equippedItem.type));
            return !incompatible;
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
