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

    [AutoloadEquip(EquipType.HandsOn)]
    internal class SpikeBand : ModItem {

        public override string LocalizationCategory => base.LocalizationCategory + ".Accessories";

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.sellPrice(0, 4, 0, 0);
            Item.defense = 7;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            modPlayer.greenLightLevel = 2.5f;
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.cactusThorns = true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<IronShackle>()
                .AddIngredient<GlowWrist>()
                .AddIngredient<MaliusHammer>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}
