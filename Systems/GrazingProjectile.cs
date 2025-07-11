using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class GrazingProjectile : GlobalProjectile {

        public override bool InstancePerEntity => true;

        public bool wasGrazedBefore = false;

        public static GrazingProjectile Get(Projectile proj) {
            return proj.GetGlobalProjectile<GrazingProjectile>();
        }

    }
}
