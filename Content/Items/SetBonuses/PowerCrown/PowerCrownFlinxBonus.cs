using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.SetBonuses.PowerCrown {
    public class PowerCrownFlinxBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.FlinxFurCoat;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 4;
            player.whipRangeMultiplier += 0.1f;
            //player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.07f;
            player.GetDamage(DamageClass.Summon) += 0.05f;
        }
    }
}
