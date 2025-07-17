using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Delterra.Systems.TPSources {
    internal class TPGainHitPlayerContext<T> : ITPGainContext where T : Entity {
        public TPGainType Type => TPGainType.ATTACK;

        public T damageDealer;

        public Player target;

        public Player.HurtInfo hurtInfo;

        public TPGainHitPlayerContext(T damageDealer, Player target, Player.HurtInfo hurtInfo) {
            this.damageDealer = damageDealer;
            this.target = target;
            this.hurtInfo = hurtInfo;
        }
    }
}
