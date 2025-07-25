using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.CustomModTypes.ModSetBonuses {
    public class ModSetBonusLoader : ILoadable {

        internal static readonly List<ModSetBonus> setBonuses = [];

        // Expose victoryPoses as a ReadOnlyList to other mods to prevent accidental manipulations. 
        public static IReadOnlyList<ModSetBonus> SetBonuses => setBonuses;

        internal static int Add(ModSetBonus victoryPose) {
            int type = setBonuses.Count;
            setBonuses.Add(victoryPose);
            return type;
        }

        public void Load(Mod mod) {
        }

        public void Unload() {
        }

        public static ModSetBonus? GetFirstThatMatches(Item head, Item body, Item legs) {
            return setBonuses.Find((bonus) => bonus.IsSetMatching(head, body, legs));
        }

    }
}
