using System;
using System.Collections.Generic;
using System.Media;
using Delterra.Content.Buffs;
using Delterra.Systems.Config;
using Delterra.Systems.TPSources;
using FaeLibrary.API;
using FaeLibrary.API.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class GrazingPlayer : ModPlayer, IFaeModPlayer {

        private double _TP = 0;
        public double TP {
            get {
                return _TP;
            }
            private set {
                _TP = Math.Clamp(value, 0, MAXTP);
            }
        }

        public const double MAXTP = 100;
        public const int BASE_GRAZE_WIDTH = 100;
        public const int BASE_GRAZE_HEIGHT = 100;

        public const float TP_BURST_PER_BULLET = 1f; // 1%
        public const float TP_PER_TICK_PER_BULLET = 0.015f; // 0.01%

        private int immuneAllowedToGatherTP = 0;
        private int dangerTime = 0;
        private int grazeNoiseCooldown = 0;

        public bool pinkRibbonGrazeArea = false;
        public bool frostmancerGrazeArea = false;

        public StatModifier AllTPChangeStat = new();
        public Dictionary<TPGainType, StatModifier> TPChangeStats = new();
        public StatModifier OldAllTPChangeStat = new();
        public Dictionary<TPGainType, StatModifier> OldTPChangeStats = new();

        private List<TPOverTimeEffect> TPOverTimeEffects = new List<TPOverTimeEffect>();

        public override void PostUpdate() {
            // Everyone handles their own TP and grazing locally!
            // This is why the NPC/Projectile variables are not synced. They only apply to the current player.
            bool weGrazedThisTick = false;
            if (Main.myPlayer == Player.whoAmI && IsAllowedToGetTPByGrazing()) {
                Rectangle grazeArea = GetGrazeRectangle();
                foreach (Projectile projectile in Main.ActiveProjectiles) {
                    if (projectile.hostile && projectile.damage > 0 && CustomSets.ProjectileTPGainRate[projectile.type] > 0f && grazeArea.Intersects(projectile.Hitbox)) {
                        GrazingProjectile modProj = GrazingProjectile.Get(projectile);
                        if (!modProj.wasGrazedBefore) {
                            modProj.wasGrazedBefore = true;
                            TriggerTPBurst(projectile);
                        } else {
                            TriggerTPPerTick(projectile);
                        }
                        weGrazedThisTick = true;
                    }
                }

                foreach (NPC npc in Main.ActiveNPCs) {
                    if (!(npc.friendly || npc.CountsAsACritter || Main.npcCatchable[npc.type]) && npc.damage > 0 && CustomSets.NPCTPGainRate[npc.type] > 0f && grazeArea.Intersects(npc.Hitbox)) {
                        GrazingNPC modNPC = GrazingNPC.Get(npc);
                        if (!modNPC.wasGrazedBefore) {
                            modNPC.wasGrazedBefore = true;
                            TriggerTPBurst(npc);
                        } else {
                            TriggerTPPerTick(npc);
                        }
                        weGrazedThisTick = true;
                    }
                }
            }

            if (weGrazedThisTick && grazeNoiseCooldown > 0) {
                grazeNoiseCooldown--;
            }

            if (immuneAllowedToGatherTP > 0) {
                immuneAllowedToGatherTP--;
            }
            if (dangerTime > 0) { 
                dangerTime--;
            }

            for (int i = 0; i < TPOverTimeEffects.Count; i++) { 
                TPOverTimeEffect effect = TPOverTimeEffects[i];
                TP += effect.tpGainPerFrame;
                effect.tpLeft -= effect.tpGainPerFrame;
                if (effect.tpLeft < effect.tpGainPerFrame) {
                    TP += effect.tpLeft;
                    TPOverTimeEffects.Remove(effect);
                    i--;
                }
            }

            //ChatHelper.DisplayMessage(NetworkText.FromLiteral("TP: " + TP / 100f + "%"), Color.Pink, 255);
        }

        public float CalculateTPGain(float amount, ITPGainContext context, bool isAboutToBeUsedUp = false) {
            // We use the old stats to make sure that we have everything, no matter the update phase
            StatModifier modifier = OldAllTPChangeStat.CombineWith(OldTPChangeStats[context.Type]);
            // TODO: Add the ability to add calls for more context-based control.
            return modifier.ApplyTo(amount);
        }

        public void GainTP(float amount, ITPGainContext context) {
            TP += CalculateTPGain(amount, context, true);
        }

        public void GainTPOverTime(float amount, int time, ITPGainContext context) {
            if (time <= 1) {
                GainTP(amount, context);
                return;
            }
            float tpGain = CalculateTPGain(amount, context, true);
            TPOverTimeEffects.Add(new(time, tpGain));
        }

        public void SpendTP(double amount) { // Calculating the final price for this is handled elsewhere, at least for now
            TP -= amount;
        }

        public override void ResetEffects() {
            pinkRibbonGrazeArea = false;
            frostmancerGrazeArea = false;

            // We save the stats from one tick before to use, because status effects run before other equipment, apparently
            OldAllTPChangeStat = AllTPChangeStat;
            AllTPChangeStat = new();
            foreach (TPGainType type in TPChangeStats.Keys) {
                OldTPChangeStats[type] = TPChangeStats[type];
            }
            foreach (TPGainType type in Enum.GetValues(typeof(TPGainType))) {
                TPChangeStats[type] = new StatModifier();
            }
        }

        void IFaeModPlayer.OnDodge(Player.HurtInfo info, DodgeType dodgeType) {
            immuneAllowedToGatherTP = 120;
        }

        /// <summary>
        /// ONLY CALL ON LOCAL CLIENT!!!
        /// </summary>
        /// <returns>true if this player is allowed to get TP by grazing</returns>
        public bool IsAllowedToGetTPByGrazing() { // I *may* clean this up AFTER the contest.
            return (immuneAllowedToGatherTP > 0 || 
                Player.immuneTime <= 0 && 
                Player.hurtCooldowns[0] <= 0 && 
                Player.hurtCooldowns[1] <= 0 && 
                Player.hurtCooldowns[2] <= 0 &&
                Player.hurtCooldowns[3] <= 0 &&
                Player.hurtCooldowns[4] <= 0
                ) && !Player.shimmering && !Player.dead;
        }

        public float GrazeAreaAlpha() {
            if (!ClientConfig.Get().ShowGrazeArea) {
                return 0f;
            }
            if (!IsAllowedToGetTPByGrazing()) {
                return 0f;
            }
            return Math.Min(dangerTime/30f, 1f);
        }

        public void TriggerTPBurst(Projectile projectile) {
            GainTP(TP_BURST_PER_BULLET * CustomSets.ProjectileTPGainRate[projectile.type], new TPGainGrazingContext<Projectile>(projectile, true));
            TriggerTPBurst();
        }

        public void TriggerTPBurst(NPC npc) {
            GainTP(TP_BURST_PER_BULLET * CustomSets.NPCTPGainRate[npc.type], new TPGainGrazingContext<NPC>(npc, true));
            TriggerTPBurst();
        }

        public void TriggerTPBurst() {
            dangerTime = 60;
            if (ClientConfig.Get().PlayGrazeSound) {
                SoundEngine.PlaySound(MySoundStyles.Graze, Player.Center);
            }
        }

        public void TriggerTPPerTick(Projectile projectile) {
            GainTP(TP_PER_TICK_PER_BULLET * CustomSets.ProjectileTPGainRate[projectile.type], new TPGainGrazingContext<Projectile>(projectile, false));
            TriggerTPPerTick();
        }

        public void TriggerTPPerTick(NPC npc) {
            GainTP(TP_PER_TICK_PER_BULLET * CustomSets.NPCTPGainRate[npc.type], new TPGainGrazingContext<NPC>(npc, false));
            TriggerTPPerTick();
        }

        public void TriggerTPPerTick() {
            dangerTime = 60;
            if (grazeNoiseCooldown <= 0 && ClientConfig.Get().PlayGrazeSound) {
                grazeNoiseCooldown = 40;
                SoundEngine.PlaySound(MySoundStyles.Graze, Player.Center);
            }
        }

        public Rectangle GetGrazeRectangle() {
            Vector2 center = Player.Center;
            int totalWidth = BASE_GRAZE_WIDTH;
            int totalHeight = BASE_GRAZE_HEIGHT;
            if (pinkRibbonGrazeArea && frostmancerGrazeArea) { // Increase graze area by 50%
                totalWidth += (totalWidth / 2);
                totalHeight += (totalHeight / 2);
            } else if (pinkRibbonGrazeArea || frostmancerGrazeArea) { // Increase graze area by 25%
                totalWidth += (totalWidth / 4);
                totalHeight += (totalHeight / 4);
            }
            return new Rectangle((int)(center.X - totalWidth/2), (int)(center.Y - totalHeight/2), totalWidth, totalHeight);
        }

        public static GrazingPlayer Get(Player player) {
            return player.GetModPlayer<GrazingPlayer>();
        }

        public override void OnHitAnything(float x, float y, Entity victim) {
            Player.AddBuff(ModContent.BuffType<Attacking>(), 60);
        }

        private class TPOverTimeEffect {
            public double tpLeft;
            public double tpGainPerFrame;

            public TPOverTimeEffect(int time, double tp) { 
                tpLeft = tp;
                tpGainPerFrame = tp / time;
            }
        }

    }

    
}
