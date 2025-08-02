using Delterra.Content.Gores;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Magic
{
    public class IceShock : ModProjectile {

        public static readonly Vector3 LIGHT = new Vector3(0.5f, 0.5f, 0.9f);
        public static int TimeForFirstSnowflake => 5;
        public static int TimeBetweenSnowflakes => 7;
        public static int TimeForExplosion => 25;
        public static int ExplosionDuration => 5;
        public static int TotalTime => 45;
        public virtual int DebuffToInflict => BuffID.Frostburn;

        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.extraUpdates = 0;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = TotalTime;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

        private bool CanHitAnything() {
            return Timer >= TimeForExplosion && Timer <= TimeForExplosion + ExplosionDuration;
        }

        public override bool? CanHitNPC(NPC target) {
            return CanHitAnything() ? null : false;
        }

        public override bool CanHitPlayer(Player target) {
            return CanHitAnything();
        }

        public override bool CanHitPvp(Player target) {
            return CanHitAnything();
        }

        int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI() {
            Timer++;
            if (Timer == 1) {
                SoundEngine.PlaySound(MySoundStyles.Iceshock, Projectile.Center);
            }
            if ((Timer - TimeForFirstSnowflake) % TimeBetweenSnowflakes == 0 && Timer <= TimeBetweenSnowflakes * 2 + TimeForFirstSnowflake) {
                Vector2 gorePos = Projectile.position + new Vector2(Main.rand.Next(0, Projectile.width - 43), Main.rand.Next(0, Projectile.height - 43));
                Gore gore = Gore.NewGorePerfect(Projectile.GetSource_FromAI(), gorePos, Vector2.Zero, ModContent.GoreType<IceShockBigSnowflake>());
                gore.timeLeft = TimeForExplosion - Timer;
            }
            if (Timer >= TimeForExplosion) {
                Projectile.Opacity = (float)(TotalTime-Timer) / (TotalTime - TimeForExplosion);
                Lighting.AddLight(Projectile.Center, LIGHT * Projectile.Opacity);
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 5) {
                    Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
                    Projectile.frameCounter = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            return Timer >= TimeForExplosion;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(DebuffToInflict, 4 * 60);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hit) {
            target.AddBuff(DebuffToInflict, 4 * 60);
        }

    }
}
