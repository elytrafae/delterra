using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.HealPrayer {
    public class HealPrayer1 : AbstractHealPrayer {
        public override int Heal => 70;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.buyPrice(1, 0, 0);
        }
    }
}
