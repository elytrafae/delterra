using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using YetToBeNamed.Systems;
using Terraria.ModLoader;

namespace YetToBeNamed.Content.Items {
    public class HealPrayer : ModItem, ITensionConsumingItem {

        public override void SetDefaults() {
            Item.UseSound = SoundID.Item100;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.useAnimation = (Item.useTime = 60);
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.buyPrice(0, 1, 0);
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
