using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Delterra.Systems.TPSources {
    public class TPGainGrazingContext<T> : ITPGainContext where T : Entity {
        public TPGainType Type => TPGainType.GRAZE;

        public T grazedEntity;
        public bool initialContact;

        public TPGainGrazingContext(T grazedEntity, bool initialContact) {
            this.grazedEntity = grazedEntity;
            this.initialContact = initialContact;
        }
    }
}
