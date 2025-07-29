using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    public class SelfCare : ModBuff {

        public override void SetStaticDefaults() {

        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetDamage(DamageClass.Generic) += 0.15f;
        }

    }
}
