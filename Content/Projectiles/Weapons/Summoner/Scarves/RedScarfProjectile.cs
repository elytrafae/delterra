using FaeLibrary.API;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Summoner.Scarves {
    public class RedScarfProjectile : AbstractScarfProjectile {

        public override int Range => 400;

        public override void OnPullback(Player player, Vector2 finalPos) {
            if (player.whoAmI == Main.myPlayer) {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), finalPos, Vector2.Zero, ProjectileID.Celeb2Rocket, Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
        }
    }
}
