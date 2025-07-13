using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Armor {

    [AutoloadEquip(EquipType.Head)]
    internal class FrostmancerHood : ModItem {

        public static readonly int TPCostReductionPercent = 15;
        public static readonly int MagicDamageBonusPercent = 15;
        public static readonly int MagicCritBonus = 7;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBonusPercent, MagicCritBonus, TPCostReductionPercent);

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults() {
            // We are passing in "{0}" into WithFormatArgs to replace "{0}" with itself because we do the final formatting for this LocalizedText in UpdateArmorSet itself according to the players current ReversedUpDownArmorSetBonuses setting.
            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void SetDefaults() {
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Red; 
            Item.defense = 13; 
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) {
            return body.type == ModContent.ItemType<FrostmancerRobe>();
        }

        public override void UpdateEquip(Player player) {
            player.GetDamage(DamageClass.Magic) += (MagicDamageBonusPercent/100f);
            player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.tpCost -= (TPCostReductionPercent / 100f);
        }

        public override void UpdateArmorSet(Player player) {
            player.setBonus = SetBonusText.Value;
            GrazingPlayer.Get(player).frostmancerGrazeArea = true;
            EquipmentEffectPlayer.Get(player).frostmancerSet = true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 10)
                .AddIngredient<GlacialFragment>(10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

    }
}
