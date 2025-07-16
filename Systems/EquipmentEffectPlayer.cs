using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Buffs;
using Delterra.Content.Gores;
using Delterra.Content.Items.Spells.HealPrayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.NPC.HitModifiers;

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
        public bool dealmakerVisible = false;


        public int currentScarfType = 0;
        public int currentScarfUses = 0;
        public int asgoreTruckGloryTime = 0;

        public override void ResetEffects() {
            tensionRestorePotionSicknessReduced = false;
            healingMultiplier = 1f;
            commonLifeRegen = 0;
            greenLightLevel = 0f;
            additionalLootChance = 0f;
            secretRingBuff = false;
            tpCost = new();
            frostmancerSet = false;
            dealmakerVisible = false;
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
                    Vector2 spawnTopLeft = Player.Center - new Vector2(spawnWidth / 2, 45);
                    Vector2 spawnPos = spawnTopLeft + new Vector2(Main.rand.NextFloat(spawnWidth), Main.rand.NextFloat(spawnHeight));
                    Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Snow, new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), 0.3f));
                    dust.noGravity = false;
                }
            }
            if (asgoreTruckGloryTime > 0) {
                asgoreTruckGloryTime--;
            }
            if (Player.HeldItem?.ModItem is AbstractRalseiScarf) {
                if (Player.HeldItem.type != currentScarfType) {
                    currentScarfUses = 0;
                    currentScarfType = Player.HeldItem.type;
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


        // This part uses the precise order in which OnHit calls are made. First the generic one is called, then the other ones.
        // With that, we can rule out the use of items and projectiles for damage dealing!
        public bool asgoreTruckHit = false;
        
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            if (Player.HasBuff<TheKingsChariotBuff>()) {
                asgoreTruckHit = true;
                modifiers.ModifyHitInfo += GetAsgoresTruckModifiers(target);
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers) {
            asgoreTruckHit = false;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers) {
            asgoreTruckHit = false;
        }

        private HitInfoModifier GetAsgoresTruckModifiers(NPC target) {
            return (ref NPC.HitInfo info) => {
                if (asgoreTruckHit == true) {
                    if (target.type == NPCID.Deerclops) {
                        info.Damage = 999999999;
                        asgoreTruckGloryTime = 120;
                    }
                    SoundEngine.PlaySound(MySoundStyles.RealisticExplosion, target.Center);
                    Gore.NewGorePerfect(Player.GetSource_OnHit(target), target.Center - new Vector2(35.5f, 50), Vector2.Zero, ModContent.GoreType<RealisticExplosion>());
                }
            };
        }

        private void Modifiers_AsgoresTruck(ref NPC.HitInfo info) {
            if (asgoreTruckHit == true) {
                info.Damage = 999999999;
            }
        }
    }
}
