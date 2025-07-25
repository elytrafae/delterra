using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.SetBonuses.PowerCrown {
    public class PowerCrownMiningBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.MiningShirt && legs.type == ItemID.MiningPants;
        }

        public override void UpdateSetBonus(Player player) {
            EquipmentEffectPlayer.Get(player).SetRedLight(1.2f);
            player.statDefense += 4;
            if (player.ZoneDirtLayerHeight) {
                player.GetDamage(DamageClass.Generic) += 0.03f;
            } else if (player.ZoneRockLayerHeight || player.ZoneUnderworldHeight) {
                player.GetDamage(DamageClass.Generic) += 0.07f;
            }
        }
    }
}
