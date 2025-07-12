using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Buffs;
using Delterra.Systems;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items {
    // This is just a material.
    public class MaliusHammer : ModItem {

        public override void SetDefaults() {
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.value = Terraria.Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }

    }
}
