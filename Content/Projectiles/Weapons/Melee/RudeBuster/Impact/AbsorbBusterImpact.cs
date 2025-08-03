using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    public class AbsorbBusterImpact : BraveBusterImpact {

        public override Color BusterColor => new Color(1f, 0.2f, 0.2f);

        public override void OnKill(int timeLeft) {
            if (Projectile.owner == Main.myPlayer) {
                Vector2 spawnPos = Projectile.Center;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPos, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, Projectile.owner, Projectile.owner, 8);
            }
            
        }

    }
}
