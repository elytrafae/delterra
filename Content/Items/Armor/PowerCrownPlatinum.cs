using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Armor {

    [AutoloadEquip(EquipType.Head)]
    public class PowerCrownPlatinum : AbstractPowerCrown {

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PowerCrownGold>();
        }
    }
}
