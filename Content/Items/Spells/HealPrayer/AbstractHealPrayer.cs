using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Delterra.Systems;
using System;

namespace Delterra.Content.Items.Spells.HealPrayer {
    public abstract class AbstractHealPrayer : ModItem, ITensionConsumingItem {

        public abstract int Heal { get; }
        public virtual double TPCost => 32;

        public override void SetDefaults() {
            Item.UseSound = MySoundStyles.Heal;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.useAnimation = Item.useTime = 60;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 0);
            Item.healLife = Heal;
        }

        public override bool? UseItem(Player player) {
            return true;
        }

        double ITensionConsumingItem.GetBaseTPCost(Player player) {
            return TPCost;
        }

    }
}
