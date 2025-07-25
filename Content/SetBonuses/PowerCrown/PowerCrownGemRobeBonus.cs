using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownGemRobeBonus : AbstractPowerCrownBonus {

        public override bool IsSetMatching(Item body, Item legs) {
            return CustomSets.WizardRobes[body.type];
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 4;
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.manaRegenBonus += 50;
        }
    }
}
