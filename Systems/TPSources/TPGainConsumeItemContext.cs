using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Delterra.Systems.TPSources {
    internal class TPGainConsumeItemContext : ITPGainContext {
        public TPGainType Type => TPGainType.RESTORE_ITEM;

        Item item;

        public TPGainConsumeItemContext(Item item) { 
            this.item = item; 
        }
    }
}
