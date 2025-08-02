using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class AbsorbBuster : AbstractRudeBuster {

        public override Color BusterColor => new Color(1f, 0.2f, 0.2f);
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            SpawnImpact(target.Hitbox);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            base.OnHitPlayer(target, info);
        }

        private void SpawnImpact(Rectangle targetHitbox) {
            if (Projectile.TryGetOwner(out Player player) && player.whoAmI == Main.myPlayer) {
                for (int i = 0; i < 5; i++) {
                    Vector2 spawnPos = targetHitbox.Center.ToVector2() + new Vector2(Main.rand.NextFloat(-targetHitbox.Width / 4, targetHitbox.Width / 4), Main.rand.NextFloat(-targetHitbox.Height / 4, targetHitbox.Height / 4));
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPos, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, player.whoAmI, player.whoAmI, 5);
                }
            }
            
        }

    }
}
