using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class BraveBuster : AbstractRudeBuster {

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            SpawnImpact(new Vector2(1, 1));
            SpawnImpact(new Vector2(1, -1));
            SpawnImpact(new Vector2(-1, 1));
            SpawnImpact(new Vector2(-1, -1));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            base.OnHitPlayer(target, info);
            SpawnImpact(new Vector2(1, 1));
            SpawnImpact(new Vector2(1, -1));
            SpawnImpact(new Vector2(-1, 1));
            SpawnImpact(new Vector2(-1, -1));
        }

        private void SpawnImpact(Vector2 direction) {
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + direction * 18, direction, ModContent.ProjectileType<BraveBusterImpact>(), Projectile.damage / 4, 0, Projectile.owner);
        }

    }
}
