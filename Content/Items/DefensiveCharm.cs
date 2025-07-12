using Delterra.Content.Buffs;
using Delterra.Systems;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items {
    public class DefensiveCharm : ModItem {

        public override void SetDefaults() {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = (Item.useAnimation = 30);
            Item.buffTime = 8 * 60; // 8 seconds
            Item.buffType = ModContent.BuffType<Defending>();
            Item.maxStack = 1;
            Item.value = Terraria.Item.sellPrice(gold:2);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = MySoundStyles.Petrify; // TODO: Discuss sound
        }

    }
}
