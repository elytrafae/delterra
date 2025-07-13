using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.Rings {
    public class FreezeRing : AbstractNoelleRing {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 250;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<SnowRing>()
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.FrostCore)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
