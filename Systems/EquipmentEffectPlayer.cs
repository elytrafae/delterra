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

        public bool tensionRestorePotionSicknessReduced = false;

        public override void ResetEffects() {
            tensionRestorePotionSicknessReduced = false;
        }

        public override bool CanUseItem(Item item) {
            return !Player.HasBuff<Defending>();
        }

        public static EquipmentEffectPlayer Get(Player player) { 
            return player.GetModPlayer<EquipmentEffectPlayer>();
        }

    }
}
