using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class DualBusterSpawner : ModProjectile {

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
        }

        public override bool? CanDamage() {
            return false;
        }

        public override bool? CanHitNPC(NPC target) {
            return false;
        }

        public override bool CanHitPlayer(Player target) {
            return false;
        }

        int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI() {
            Timer++;
            if (Timer == 1) {
                if (Projectile.owner == Main.myPlayer && Projectile.TryGetOwner(out Player player)) {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center, Projectile.velocity, ModContent.ProjectileType<BraveBuster>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            } else if (Timer == 40) {
                if (Projectile.owner == Main.myPlayer && Projectile.TryGetOwner(out Player player)) {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center, Projectile.velocity, ModContent.ProjectileType<JusticeBuster>(), Projectile.damage + Projectile.damage/2, Projectile.knockBack * 2, Projectile.owner);
                }
                Projectile.timeLeft = 0;
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

    }
}
