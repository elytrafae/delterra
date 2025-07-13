using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class EquipmentEffectPlayer : ModPlayer {

        public bool tensionRestorePotionSicknessReduced = false;
        public float healingMultiplier = 1f;
        public int commonLifeRegen = 0;
        public float greenLightLevel = 0f;

        public override void ResetEffects() {
            tensionRestorePotionSicknessReduced = false;
            healingMultiplier = 1f;
            commonLifeRegen = 0;
            greenLightLevel = 0f;
        }

        public override void PostUpdateMiscEffects() {
            if (greenLightLevel > 0f) {
                Lighting.AddLight(Player.Center, new Vector3(0.6f, 1f, 0.6f) * greenLightLevel);
            }
        }

        public override bool CanUseItem(Item item) {
            return !Player.HasBuff<Defending>();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue) {
            healValue = (int)(healValue * healingMultiplier);
        }

        public override void UpdateLifeRegen() {
            Player.lifeRegen += commonLifeRegen;
        }

        public static EquipmentEffectPlayer Get(Player player) { 
            return player.GetModPlayer<EquipmentEffectPlayer>();
        }
        

    }
}
