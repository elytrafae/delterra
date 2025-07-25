using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownNinjaBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.NinjaShirt && legs.type == ItemID.NinjaPants;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 3;
            player.jumpSpeedBoost += 1.75f;
            player.moveSpeed += 0.15f;
            player.runAcceleration *= 1.15f;
        }
    }
}
