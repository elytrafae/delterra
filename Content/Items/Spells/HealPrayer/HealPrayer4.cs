using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.HealPrayer {
    public class HealPrayer4 : AbstractHealPrayer {
        public override int Heal => 200;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
            Item.value = Terraria.Item.buyPrice(80, 0, 0);
        }
    }
}
