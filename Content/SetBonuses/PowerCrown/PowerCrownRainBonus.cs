using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownRainBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.RainCoat;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 5;
            player.endurance += 0.2f;
        }
    }
}
