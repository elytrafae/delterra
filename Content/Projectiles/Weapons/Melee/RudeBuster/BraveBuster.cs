using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class BraveBuster : AbstractRudeBuster {

        public virtual int ImpactProjectileType => ModContent.ProjectileType<BraveBusterImpact>();

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            if (Projectile.TryGetOwner(out Player player) && player.whoAmI == Main.myPlayer) {
                SpawnImpact(new Vector2(1, 1), target.Center);
                SpawnImpact(new Vector2(1, -1), target.Center);
                SpawnImpact(new Vector2(-1, 1), target.Center);
                SpawnImpact(new Vector2(-1, -1), target.Center);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            base.OnHitPlayer(target, info);
            // CANNOT SPAWN PROJECTILES HERE!
            // THIS IS CALLED ON THE CLIENT TAKING DAMAGE!
            //SpawnImpact(new Vector2(1, 1));
            //SpawnImpact(new Vector2(1, -1));
            //SpawnImpact(new Vector2(-1, 1));
            //SpawnImpact(new Vector2(-1, -1));
        }

        private void SpawnImpact(Vector2 direction, Vector2 targetCenter) {
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetCenter + direction * 18, direction, ImpactProjectileType, Projectile.damage / 4, 0, Projectile.owner);
        }

    }
}
