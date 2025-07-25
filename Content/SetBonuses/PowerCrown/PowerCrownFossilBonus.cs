using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownFossilBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.FossilShirt && legs.type == ItemID.FossilPants;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 6;
            player.GetArmorPenetration(DamageClass.Ranged) += 4;
        }
    }
}
