using Delterra.Content.Buffs;
using FaeLibrary.API.ClassExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Consumables {
    public class SelfCareMilk : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Consumables";

        public override void SetDefaults() {
            Item.DefaultToHealingPotion(16, 16, 60);
            Item.rare = ItemRarityID.Green;
        }

        public override bool? UseItem(Player player) {
            // We use this hack so that infinite buff mods don't pick this up and break everything
            player.AddBuff<SelfCare>(15 * 60); 
            return true;
        }

    }
}
