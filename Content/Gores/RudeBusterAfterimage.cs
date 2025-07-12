using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Delterra.Content.Gores {
    public class RudeBusterAfterimage : ModGore {

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = 45;
            if (source is EntitySource_Parent parentSource) {
                if (parentSource.Entity is Projectile proj) {
                    gore.rotation = proj.rotation;
                }
            }
            
        }

        public override bool Update(Gore gore) {
            gore.timeLeft--;
            return gore.timeLeft <= 0;
        }

        public override Color? GetAlpha(Gore gore, Color lightColor) {
            return lightColor * (gore.timeLeft / 40f);
        }

    }
}
