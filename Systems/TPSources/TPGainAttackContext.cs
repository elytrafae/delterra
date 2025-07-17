using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delterra.Systems.TPSources {
    public class TPGainAttackContext : ITPGainBuffContext {

        private readonly int _buffID;

        public TPGainAttackContext(int buffID) {
            _buffID = buffID;
        }

        public int buffID => _buffID;

        public TPGainType Type => TPGainType.ATTACK;

    }
}
