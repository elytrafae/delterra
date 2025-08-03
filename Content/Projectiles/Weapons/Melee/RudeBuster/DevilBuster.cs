using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class DevilBuster : AbstractRudeBuster {

        public override Color BusterColor => new Color(0xA5, 0xB9, 0xFF);

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            if (Projectile.TryGetOwner(out Player player) && player.whoAmI == Main.myPlayer) {
                SpawnImpactSpade(new Vector2(1, 0), target.Center);
                SpawnImpactSpade(new Vector2(-1, 0), target.Center);
                SpawnImpactSpade(new Vector2(0, 1), target.Center);
                SpawnImpactSpade(new Vector2(0, -1), target.Center);
                SpawnImpactDiamond(new Vector2(1, 1), target.Center);
                SpawnImpactDiamond(new Vector2(1, -1), target.Center);
                SpawnImpactDiamond(new Vector2(-1, 1), target.Center);
                SpawnImpactDiamond(new Vector2(-1, -1), target.Center);
            }
        }

        private void SpawnImpactSpade(Vector2 direction, Vector2 targetCenter) {
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetCenter + direction * 10, direction * 3, ModContent.ProjectileType<DevilBusterSpade>(), (int)(Projectile.damage * 0.15f), 0, Projectile.owner);
        }

        private void SpawnImpactDiamond(Vector2 direction, Vector2 targetCenter) {
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetCenter + direction * 10, direction * 4, ModContent.ProjectileType<DevilBusterDiamond>(), (int)(Projectile.damage * 0.15f), 0, Projectile.owner);
        }

    }
}
