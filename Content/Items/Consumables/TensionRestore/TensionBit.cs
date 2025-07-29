using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Consumables.TensionRestore {
    public class TensionBit : AbstractTPRestoreItem {
        public override float TPHeal => 32;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = 0;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.UseSound = MySoundStyles.TensionBit;
        }

    }
}
