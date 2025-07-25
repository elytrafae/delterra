using Delterra.Content.CustomModTypes.ModSetBonuses;
using Delterra.Content.Items.Armor;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Items.SetBonuses.PowerCrown {
    public abstract class AbstractPowerCrownBonus : ModSetBonus {

        public override string LocalizationCategory => base.LocalizationCategory + ".PowerCrown";

        public sealed override bool IsSetMatching(Item head, Item body, Item legs) {
            return head.type == ModContent.ItemType<Armor.PowerCrown>() && IsSetMatching(body, legs);
        }

        public abstract bool IsSetMatching(Item body, Item legs);

    }
}
