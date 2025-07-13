using System;
using System.Media;
using Delterra.Content.Buffs;
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

        private int _TP = 0;
        /// <summary>
        /// NOTE: TP here goes from 0 to <see cref="MAXTP"/>  for more precise control
        /// </summary>
        public int TP {
            get {
                return _TP;
            }
            set {
                _TP = Math.Clamp(value, 0, MAXTP);
            }
        }

        public float TPPercent {
            get {
                return _TP * 100f / MAXTP;
            }
        }

        public const int TP_PER_PERCENT = 100;
        public const int MAXTP = TP_PER_PERCENT*100;
        public const int BASE_GRAZE_WIDTH = 100;
        public const int BASE_GRAZE_HEIGHT = 100;

        public const int TP_BURST_PER_BULLET = 100; // 1%
        public const int TP_PER_TICK_PER_BULLET = 1; // 0.01%

        private int immuneAllowedToGatherTP = 0;
        private int dangerTime = 0;
        private int grazeNoiseCooldown = 0;

        public bool bigGrazeAreaStat = false;

        public override void PostUpdate() {
            // Everyone handles their own TP and grazing locally!
            // This is why the NPC/Projectile variables are not synced. They only apply to the current player.
            bool weGrazedThisTick = false;
            if (Main.myPlayer == Player.whoAmI && IsAllowedToGetTPByGrazing()) {
                Rectangle grazeArea = GetGrazeRectangle();
                foreach (Projectile projectile in Main.ActiveProjectiles) {
                    if (projectile.hostile && projectile.damage > 0 && grazeArea.Intersects(projectile.Hitbox)) {
                        GrazingProjectile modProj = GrazingProjectile.Get(projectile);
                        if (!modProj.wasGrazedBefore) {
                            modProj.wasGrazedBefore = true;
                            TriggerTPBurst();
                        } else {
                            TriggerTPPerTick();
                        }
                        weGrazedThisTick = true;
                    }
                }

                foreach (NPC npc in Main.ActiveNPCs) {
                    if (!(npc.friendly || npc.CountsAsACritter || Main.npcCatchable[npc.type]) && npc.damage > 0 && grazeArea.Intersects(npc.Hitbox)) {
                        GrazingNPC modNPC = GrazingNPC.Get(npc);
                        if (!modNPC.wasGrazedBefore) {
                            modNPC.wasGrazedBefore = true;
                            TriggerTPBurst();
                        } else {
                            TriggerTPPerTick();
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

            //ChatHelper.DisplayMessage(NetworkText.FromLiteral("TP: " + TP / 100f + "%"), Color.Pink, 255);
        }

        public override void ResetEffects() {
            bigGrazeAreaStat = false;
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
            if (!IsAllowedToGetTPByGrazing()) {
                return 0f;
            }
            return Math.Min(dangerTime/30f, 1f);
        }

        public void TriggerTPBurst() {
            TP += TP_BURST_PER_BULLET;
            dangerTime = 60;
            SoundEngine.PlaySound(MySoundStyles.Graze, Player.Center);
        }

        public void TriggerTPPerTick() {
            TP += TP_PER_TICK_PER_BULLET;
            dangerTime = 60;
            if (grazeNoiseCooldown <= 0) {
                grazeNoiseCooldown = 40;
                SoundEngine.PlaySound(MySoundStyles.Graze, Player.Center);
            }
        }

        public Rectangle GetGrazeRectangle() {
            Vector2 center = Player.Center;
            int totalWidth = BASE_GRAZE_WIDTH;
            int totalHeight = BASE_GRAZE_HEIGHT;
            if (bigGrazeAreaStat) { // Increase graze area by 25%
                totalWidth += (totalWidth / 4);
                totalHeight += (totalHeight / 4);
            }
            return new Rectangle((int)(center.X - totalWidth/2), (int)(center.Y - totalHeight/2), totalWidth, totalHeight);
        }

        public static int GetTPForPercent(float percent) { 
            return (int)(percent * TP_PER_PERCENT);
        }
        public static GrazingPlayer Get(Player player) {
            return player.GetModPlayer<GrazingPlayer>();
        }

        public override void OnHitAnything(float x, float y, Entity victim) {
            Player.AddBuff(ModContent.BuffType<Attacking>(), 60);
        }

    }
}
