using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Accessories {

    [AutoloadEquip(EquipType.Face)]
    internal class Dealmaker : ModItem {

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.sellPrice(0, 10, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.additionalLootChance += 0.3f;
            modPlayer.secretRingBuff = true;
        }

        

    }
}
