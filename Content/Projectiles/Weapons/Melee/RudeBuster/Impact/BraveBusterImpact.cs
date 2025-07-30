using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    public class BraveBusterImpact : ModProjectile {

        public virtual Color BusterColor => new Color(0xAD, 0x4B, 0xFE);

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.penetrate = 4;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI() {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

            Timer++;
            int alpha = Math.Clamp(Timer * 3 - 120, 0, 255);
            Projectile.alpha = alpha;
            Projectile.velocity *= 0.98f;
            if (alpha >= 255) {
                Projectile.timeLeft = 0;
            }
            Lighting.AddLight(Projectile.Center, BusterColor.ToVector3() * Projectile.Opacity);
        }

        public override bool PreDraw(ref Color lightColor) {
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(Projectile.width / 2, Projectile.height / 2), 1f, SpriteEffects.None);
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

    }
}
