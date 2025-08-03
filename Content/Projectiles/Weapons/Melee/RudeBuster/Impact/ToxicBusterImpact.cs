using Delterra.Content.Buffs;
using FaeLibrary.API.ClassExtensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    internal class ToxicBusterImpact : ModProjectile {

        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 4;
        }

        private int MaxTime => 180;

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
            Projectile.timeLeft = MaxTime;
        }

        public override void AI() {
            if (Projectile.timeLeft <= 30) {
                Projectile.Opacity = Projectile.timeLeft / 30f;
                Projectile.frame = Projectile.timeLeft < 15 ? 3 : 2;
            } else {
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 20) {
                    Projectile.frameCounter = 0;
                    Projectile.frame = (Projectile.frame + 1) % 2;
                }
            }
            if (Main.rand.NextBool(2)) {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Venom);
                dust.noGravity = true;
            }
            Lighting.AddLight(Projectile.Center, new Color(0xB9, 0x80, 0xC1).ToVector3() * 0.6f * Projectile.Opacity);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(BuffID.Venom, 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            target.AddBuff(BuffID.Venom, 300);
        }

    }
}
