using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.Items.SetBonuses.PowerCrown {
    public class PowerCrownAnglerBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.AnglerVest && legs.type == ItemID.AnglerPants;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 3;
            player.fishingSkill += 15;
        }
    }
}
