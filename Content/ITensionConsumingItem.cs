using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Delterra.Content {
    public interface ITensionConsumingItem {

        public abstract double GetBaseTPCost(Player player);

        public virtual bool IsTPConsumedOnUse(Player player) {
            return true;
        }

    }
}
