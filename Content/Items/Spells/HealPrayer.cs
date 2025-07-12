using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Delterra.Systems;

namespace Delterra.Content.Items.Spells {
    public class HealPrayer : ModItem, ITensionConsumingItem {

        public override void SetDefaults() {
            Item.UseSound = MySoundStyles.Heal;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.useAnimation = Item.useTime = 60;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 0);
        }

        public override bool? UseItem(Player player) {
            player.Heal(60);
            return true;
        }

        public int GetBaseTPCost(Player player) {
            return 3200;
        }
    }
}
