using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Delterra.Systems;
using System;

namespace Delterra.Content.Items.Spells.HealPrayer {

    public abstract class AbstractRalseiScarf : ModItem, ITensionConsumingItem {

        public abstract int Heal { get; }
        public virtual int MinTPCost => GrazingPlayer.GetTPForPercent(32);
        public virtual int MaxTPCost => MinTPCost*2;

        public override void SetDefaults() {
            Item.UseSound = MySoundStyles.Heal;
            Item.useStyle = ItemUseStyleID.HiddenAnimation;
            Item.useTurn = true;
            Item.useAnimation = Item.useTime = 18;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 0);
            Item.healLife = Heal;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
            // Shoot stats and damage and such should be applied on each individual scarf
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                if (player.altFunctionUse == 2) {
                    GrazingPlayer.Get(player).TP -= GlobalTensionConsumingItem.GetTensionCost(Item, player);
                    EquipmentEffectPlayer.Get(player).currentScarfUses = 0;
                } else {
                    EquipmentEffectPlayer.Get(player).currentScarfUses++;
                }
            }
            return true;
        }

        public override bool CanShoot(Player player) {
            return player.altFunctionUse != 2;
        }

        int ITensionConsumingItem.GetBaseTPCost(Player player) {
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            if (modPlayer.currentScarfType != Type) {
                return MaxTPCost;
            }
            return Math.Max(MinTPCost, MaxTPCost - modPlayer.currentScarfUses * GrazingPlayer.GetTPForPercent(4));
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
