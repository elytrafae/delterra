using System;
using FaeLibrary.API;
using FaeLibrary.API.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YetToBeNamed.Systems {
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

        public const int MAXTP = 10000;
        public const int BASE_GRAZE_WIDTH = 64;
        public const int BASE_GRAZE_HEIGHT = 96;

        public const int TP_BURST_PER_BULLET = 100; // 1%
        public const int TP_PER_TICK_PER_BULLET = 1; // 0.01%

        private int immuneAllowedToGatherTP = 0;
        private int dangerTime = 0;

        public override void PostUpdate() {
            // Everyone handles their own TP and grazing locally!
            // This is why the NPC/Projectile variables are not synced. They only apply to the current player.
            if (Main.myPlayer == Player.whoAmI && IsAllowedToGetTPByGrazing()) {
                Rectangle grazeArea = GetGrazeRectangle();
                foreach (Projectile projectile in Main.ActiveProjectiles) {
                    if (projectile.hostile && grazeArea.Intersects(projectile.Hitbox)) {
                        GrazingProjectile modProj = GrazingProjectile.Get(projectile);
                        if (!modProj.wasGrazedBefore) {
                            modProj.wasGrazedBefore = true;
                            TriggerTPBurst();
                        } else {
                            TriggerTPPerTick();
                        }
                    }
                }

                foreach (NPC npc in Main.ActiveNPCs) {
                    if ((!(npc.friendly || npc.CountsAsACritter || Main.npcCatchable[npc.type])) && grazeArea.Intersects(npc.Hitbox)) {
                        GrazingNPC modNPC = GrazingNPC.Get(npc);
                        if (!modNPC.wasGrazedBefore) {
                            modNPC.wasGrazedBefore = true;
                            TriggerTPBurst();
                        } else {
                            TriggerTPPerTick();
                        }
                    }
                }
            }

            if (immuneAllowedToGatherTP > 0) {
                immuneAllowedToGatherTP--;
            }
            if (dangerTime > 0) { 
                dangerTime--;
            }

            //ChatHelper.DisplayMessage(NetworkText.FromLiteral("TP: " + TP / 100f + "%"), Color.Pink, 255);
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
                (Player.immuneTime <= 0 && 
                Player.hurtCooldowns[0] <= 0 && 
                Player.hurtCooldowns[1] <= 0 && 
                Player.hurtCooldowns[2] <= 0 &&
                Player.hurtCooldowns[3] <= 0 &&
                Player.hurtCooldowns[4] <= 0)
                ) && (!Player.shimmering) && (!Player.dead);
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
            // TODO: Add effects and sound
        }

        public void TriggerTPPerTick() {
            TP += TP_PER_TICK_PER_BULLET;
            dangerTime = 60;
            // TODO: Add effects and sound (occasionally)
        }

        public Rectangle GetGrazeRectangle() {
            Vector2 center = Player.Center;
            int totalWidth = BASE_GRAZE_WIDTH;
            int totalHeight = BASE_GRAZE_HEIGHT;
            return new Rectangle((int)(center.X - totalWidth/2), (int)(center.Y - totalHeight/2), totalWidth, totalHeight);
        }

        public static GrazingPlayer Get(Player player) {
            return player.GetModPlayer<GrazingPlayer>();
        }

    }
}
