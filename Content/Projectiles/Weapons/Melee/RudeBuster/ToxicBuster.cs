using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class ToxicBuster : AbstractRudeBuster {

        public override Color BusterColor => new Color(0xB9, 0x80, 0xC1);

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            SpawnImpact(target.Hitbox);
        }

        private void SpawnImpact(Rectangle targetHitbox) {
            if (Projectile.TryGetOwner(out Player player) && player.whoAmI == Main.myPlayer) {
                int randomOffset = Main.rand.Next(40);
                for (int i = 0; i < 360; i += 24) {
                    float rad = MathHelper.ToRadians(i + randomOffset);
                    Vector2 direction = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetHitbox.Center.ToVector2() + direction * 16, direction * 3, ModContent.ProjectileType<ToxicBusterImpact>(), Projectile.damage / 4, 0, player.whoAmI);
                }
                
            }

        }

    }
}
