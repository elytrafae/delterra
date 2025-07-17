using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Delterra.Systems.TPSources {
    public class TPGainDefendContext : ITPGainBuffContext {

        private readonly int _buffID;

        public TPGainDefendContext(int buffID) {
            _buffID = buffID;
        }

        public int buffID => _buffID;

        public TPGainType Type => TPGainType.DEFEND;
    }
}
