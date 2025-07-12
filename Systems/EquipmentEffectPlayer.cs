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
        public float healingMultiplier = 1f;

        public override void ResetEffects() {
            tensionRestorePotionSicknessReduced = false;
            healingMultiplier = 1f;
        }

        public override bool CanUseItem(Item item) {
            return !Player.HasBuff<Defending>();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue) {
            healValue = (int)(healValue * healingMultiplier);
        }

        public static EquipmentEffectPlayer Get(Player player) { 
            return player.GetModPlayer<EquipmentEffectPlayer>();
        }
        

    }
}
