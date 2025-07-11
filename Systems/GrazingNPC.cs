using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class GrazingNPC : GlobalNPC {
        public override bool InstancePerEntity => true;

        public bool wasGrazedBefore = false;

        public static GrazingNPC Get(NPC npc) {
            return npc.GetGlobalNPC<GrazingNPC>();
        }

    }
}
