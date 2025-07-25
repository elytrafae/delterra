using FaeLibrary.API.ClassExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public class PowerCrownGoldPlatinumBonus : AbstractPowerCrownBonus {
        public override bool IsSetMatching(Item body, Item legs) {
            return body.type == ItemID.GoldChainmail && legs.type == ItemID.GoldGreaves || body.type == ItemID.PlatinumChainmail && legs.type == ItemID.PlatinumGreaves;
        }

        public override void UpdateSetBonus(Player player) {
            player.statDefense += 5;
            player.GetCommonPositiveRegenStat() += 2;
        }
    }
}
