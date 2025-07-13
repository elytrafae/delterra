using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class EquipmentEffectPlayer : ModPlayer {

        public bool tensionRestorePotionSicknessReduced = false;
        public float healingMultiplier = 1f;
        public int commonLifeRegen = 0;
        public float greenLightLevel = 0f;
        public float additionalLootChance = 0f;
        public bool secretRingBuff = false;
        public StatModifier tpCost = new();
        public bool frostmancerSet = false;

        public override void ResetEffects() {
            tensionRestorePotionSicknessReduced = false;
            healingMultiplier = 1f;
            commonLifeRegen = 0;
            greenLightLevel = 0f;
            additionalLootChance = 0f;
            secretRingBuff = false;
            tpCost = new();
            frostmancerSet = false;
        }

        public override void PostUpdateMiscEffects() {
            if (greenLightLevel > 0f) {
                Lighting.AddLight(Player.Center, new Vector3(0.6f, 1f, 0.6f) * greenLightLevel);
            }
            if (frostmancerSet) {
                Lighting.AddLight(Player.Center, new Vector3(0.6f, 0.6f, 0.6f));
                if (Main.rand.NextBool(11)) {
                    int spawnWidth = 60;
                    int spawnHeight = 10;
                    Vector2 spawnTopLeft = Player.Center - new Vector2(spawnWidth/2, 45);
                    Vector2 spawnPos = spawnTopLeft + new Vector2(Main.rand.NextFloat(spawnWidth), Main.rand.NextFloat(spawnHeight));
                    Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Snow, new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), 0.3f));
                    dust.noGravity = false;
                }
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

        public override void ArmorSetBonusActivated() {
            if (frostmancerSet) {
                Player.AddBuff(ModContent.BuffType<Trance>(), 4 * 60);
            }
        }

        public static EquipmentEffectPlayer Get(Player player) { 
            return player.GetModPlayer<EquipmentEffectPlayer>();
        }
        

    }
}
