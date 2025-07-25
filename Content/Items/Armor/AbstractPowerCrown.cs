using Delterra.Content.CustomModTypes.ModSetBonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Armor {

    public abstract class AbstractPowerCrown : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Armor";

        public override void SetDefaults() {
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
            Item.defense = 0;
        }

        ModSetBonus setBonusCache = null;

        public override bool IsArmorSet(Item head, Item body, Item legs) {
            setBonusCache = ModSetBonusLoader.GetFirstThatMatches(head, body, legs);
            return setBonusCache != null;
        }

        public override void UpdateArmorSet(Player player) {
            player.setBonus = setBonusCache.SetBonusText.Value;
            setBonusCache.UpdateSetBonus(player);
        }

    }
}
