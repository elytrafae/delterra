using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace YetToBeNamed.Content {
    public interface ITensionConsumingItem {

        public abstract int GetBaseTPCost(Player player);

        public virtual bool IsTPConsumedOnUse(Player player) {
            return true;
        }

    }
}
