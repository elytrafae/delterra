using FaeLibrary.API;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Scarves {
    public class RedScarfProjectile : ModProjectile {

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 600;
            Projectile.timeLeft = 600;
        }

        public override void AI() {
            // TODO: Update rotation from velocity so that collision starts working.
            // The below approach is not correct and is just my tired rambling
            //Projectile.rotation = (float)Math.Acos(Projectile.velocity.X);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            if (Projectile.TryGetOwner(out Player player)) {
                hitbox = new Rectangle((int)player.Center.X, (int)player.Center.Y, (int)hitbox.Center().Distance(player.Center) + Projectile.width, Projectile.height);
            }
            
            //hitbox = new Rectangle((int)Projectile.position.X - Projectile.width, (int)Projectile.position.Y - Projectile.width, Projectile.width*2, Projectile.width*2);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            return FaeCollisionUtils.OrientedRactanglesCollide(projHitbox, Projectile.rotation, targetHitbox, 0);
        }

        public override bool? CanDamage() {
            return true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

    }
}
