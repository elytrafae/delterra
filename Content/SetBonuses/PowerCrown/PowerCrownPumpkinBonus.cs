using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownPumpkinBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.PumpkinBreastplate && legs.type == ItemID.PumpkinLeggings;
        }

        public override void UpdateSetBonus(Player player) {
            player.ApplyEquipFunctional(ContentSamples.ItemsByType[ItemID.HermesBoots], true);
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
    }
}
