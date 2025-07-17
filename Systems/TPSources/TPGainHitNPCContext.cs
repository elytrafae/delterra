using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Delterra.Systems.TPSources {
    internal class TPGainHitNPCContext<T> : ITPGainContext where T : Entity {
        public TPGainType Type => TPGainType.ATTACK;

        public T damageDealer;

        public NPC target;

        public NPC.HitInfo hitInfo;

        public TPGainHitNPCContext(T damageDealer, NPC target, NPC.HitInfo hitInfo) { 
            this.damageDealer = damageDealer;
            this.target = target;
            this.hitInfo = hitInfo;
        }
    }
}
