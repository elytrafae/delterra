using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownSnowBonus : AbstractPowerCrownBonus {

        private static readonly int[] SNOW_COATS = [ItemID.EskimoCoat, ItemID.PinkEskimoCoat];
        private static readonly int[] SNOW_PANTS = [ItemID.EskimoPants, ItemID.PinkEskimoPants];

        public override bool IsSetMatching(Item body, Item legs) {
            return SNOW_COATS.Contains(body.type) && SNOW_PANTS.Contains(legs.type);
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 4;
            EquipmentEffectPlayer.Get(player).damageAgainstSnowEnemies += 0.15f;
        }
    }
}
