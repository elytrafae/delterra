using Delterra.Content.CustomModTypes.ModSetBonuses;
using Delterra.Content.Items.Armor;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.SetBonuses.PowerCrown {
    public abstract class AbstractPowerCrownBonus : ModSetBonus {

        public override string LocalizationCategory => base.LocalizationCategory + ".PowerCrown";

        public sealed override bool IsSetMatching(Item head, Item body, Item legs) {
            return (head.type == ModContent.ItemType<PowerCrownGold>() || head.type == ModContent.ItemType<PowerCrownPlatinum>()) && IsSetMatching(body, legs);
        }

        public abstract bool IsSetMatching(Item body, Item legs);

    }
}
