using ReLogic.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.CustomModTypes.ModSetBonuses {
    public class ModSetBonusID {

        [ReinitializeDuringResizeArrays]
        public static class Sets {
            public static SetFactory Factory = new SetFactory(ModSetBonusLoader.SetBonuses.Count, nameof(Delterra) + "/PowerCrownSetBonusID", Search);

            /*
            public static bool[] NonBoss = Factory.CreateNamedSet("NonBoss")
                .Description("Victory poses in this set are options to be chosen when defeating a regular enemy")
                .RegisterBoolSet(false);
            */
        }

        public static IdDictionary Search = IdDictionary.Create<ModSetBonusID, int>();

    }
}
