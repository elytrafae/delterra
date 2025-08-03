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
    public class AutoBuster : AbstractRudeBuster {

        public override Color BusterColor => new Color(0.9f, 0.9f, 0.2f);
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            base.OnHitNPC(target, hit, damageDone);
            SpawnImpact(target.Hitbox);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            base.OnHitPlayer(target, info);
        }

        private void SpawnImpact(Rectangle targetHitbox) {
            if (Projectile.TryGetOwner(out Player player) && player.whoAmI == Main.myPlayer) {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetHitbox.Center.ToVector2(), Vector2.Zero, ModContent.ProjectileType<AutoBusterImpact>(), Projectile.damage/5, 0, player.whoAmI);
            }
            
        }

    }
}
