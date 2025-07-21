using FaeLibrary.API;
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

namespace Delterra.Content.Projectiles {
    public class CollisionTest : ModProjectile {

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 360;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.timeLeft = 10;
        }

        public override void AI() {
            Projectile.rotation += 0.0015f;
            Projectile.timeLeft = 2;
        }

        public override bool? CanHitNPC(NPC target) {
            return true; //FaeCollisionUtils.OrientedRactanglesCollide(Projectile.Hitbox, Projectile.rotation, target.Hitbox, 0) ? true : false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            //hitbox = new Rectangle((int)Projectile.position.X - Projectile.width, (int)Projectile.position.Y - Projectile.width, Projectile.width*2, Projectile.width*2);
        }

        public override bool PreDraw(ref Color lightColor) {
            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);
            Main.EntitySpriteDraw(
                TextureAssets.MagicPixel.Value, 
                hitbox.Center() - Main.screenPosition, 
                new Rectangle(0, 0, 1, 1), 
                Color.Orange * 0.4f, 
                Projectile.rotation,
                new Vector2(0.5f, 0.5f), 
                new Vector2(hitbox.Width, hitbox.Height), 
                SpriteEffects.None
            );
            return true;
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
