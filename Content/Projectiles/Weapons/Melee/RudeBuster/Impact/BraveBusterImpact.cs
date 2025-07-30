using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    public class BraveBusterImpact : ModProjectile {

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.width = 31;
            Projectile.height = 31;
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
            int alpha = Math.Clamp(Timer * 2 - 100, 0, 255);
            Projectile.alpha = alpha;
            if (alpha >= 255) {
                Projectile.timeLeft = 0;
            }
        }

    }
}
