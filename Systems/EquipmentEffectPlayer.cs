using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class EquipmentEffectPlayer : ModPlayer {


        public override bool CanUseItem(Item item) {
            return !Player.HasBuff<Defending>();
        }

    }
}
