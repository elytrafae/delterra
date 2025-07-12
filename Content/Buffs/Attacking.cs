using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    public class Attacking : ModBuff {

        public override void Update(Player player, ref int buffIndex) {
            GrazingPlayer.Get(player).TP += 1;
        }

    }
}
