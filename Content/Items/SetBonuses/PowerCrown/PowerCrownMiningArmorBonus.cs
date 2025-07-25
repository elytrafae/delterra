using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.Items.SetBonuses.PowerCrown {
    public class PowerCrownMiningArmorBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.MiningShirt && legs.type == ItemID.MiningPants;
        }

        public override void UpdateSetBonus(Player player) {
            EquipmentEffectPlayer.Get(player).SetRedLight(1f);
            // TODO: Finish set bonus
        }
    }
}
