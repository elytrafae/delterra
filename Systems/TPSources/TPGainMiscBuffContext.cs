using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delterra.Systems.TPSources {
    internal class TPGainMiscBuffContext : ITPGainBuffContext {
        private readonly int _buffID;

        public TPGainMiscBuffContext(int buffID) {
            _buffID = buffID;
        }

        public int buffID => _buffID;

        public TPGainType Type => TPGainType.CUSTOM;
    }
}
