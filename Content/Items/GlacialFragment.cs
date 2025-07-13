using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items {
    public class GlacialFragment : ModItem {

        public override void SetStaticDefaults() {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;

            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults() {
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.value = Terraria.Item.buyPrice(0, 0, 19, 97);
            Item.rare = ItemRarityID.Cyan;
        }

        public override void HoldItem(Player player) {
            player.AddBuff(BuffID.Frostburn2, 30);
        }

    }
}
