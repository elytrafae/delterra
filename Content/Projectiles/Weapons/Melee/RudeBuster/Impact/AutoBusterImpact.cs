using Delterra.Content.Buffs;
using FaeLibrary.API.ClassExtensions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    internal class AutoBusterImpact : ModProjectile {

        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults() {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        int Timer { 
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        Vector2 InitialPosition {
            get => new Vector2(Projectile.ai[1], Projectile.ai[2]);
            set { Projectile.ai[1] = value.X; Projectile.ai[2] = value.Y; }
        }

        public override void AI() {
            if (Timer == 0) {
                InitialPosition = Projectile.position + new Vector2(Projectile.width/4, Projectile.height/4);
            }
            Timer++;
            Projectile.width = Projectile.height = 32 + Timer * 2;
            Projectile.position = InitialPosition - new Vector2(Timer, Timer);
            Projectile.scale = Projectile.width / 64f;
            Projectile.alpha = Math.Clamp(Timer * 4 - 50, 0, 255);

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 10) {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
            }

            if (Projectile.alpha >= 255) {
                Projectile.timeLeft = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame*64, 64, 64), lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(32, 32), Projectile.scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff<Zapped>(600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            target.AddBuff<Zapped>(600);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

    }
}
