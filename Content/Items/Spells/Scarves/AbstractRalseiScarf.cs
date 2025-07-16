using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Delterra.Systems;
using System;

namespace Delterra.Content.Items.Spells.HealPrayer {
    public abstract class AbstractRalseiScarf : ModItem, ITensionConsumingItem {

        public abstract int Heal { get; }
        public virtual int TPCost => GrazingPlayer.GetTPForPercent(32);

        public override void SetDefaults() {
            Item.UseSound = MySoundStyles.Heal;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.useAnimation = Item.useTime = 60;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 0);
            Item.healLife = Heal;
            // Shoot stats should be applied on each individual scarf
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer && player.altFunctionUse == 2) {
                GrazingPlayer.Get(player).TP -= GlobalTensionConsumingItem.GetTensionCost(Item, player);
            }
            return true;
        }

        int ITensionConsumingItem.GetBaseTPCost(Player player) {
            return TPCost;
        }

        bool ITensionConsumingItem.IsTPConsumedOnUse(Player player) {
            return false;
        }

        public override bool AltFunctionUse(Player player) {
            return GrazingPlayer.Get(player).TP >= GlobalTensionConsumingItem.GetTensionCost(Item, player);
        }

        public override void GetHealLife(Player player, bool quickHeal, ref int healValue) {
            if (player.altFunctionUse == 2 || (Main.HoverItem != null && (!Main.HoverItem.IsAir))) {
                return;
            }
            healValue = 0;
        }

    }
}
