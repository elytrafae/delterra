using Delterra.Content.PlayerDrawLayers;
using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.Rings {
    public class FreezeRing : AbstractNoelleRing {

        public override double IceShockCost => 25;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 300;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<IceRing>()
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.IceQueenTrophy)
                .AddIngredient(ItemID.Glass, 20)
                .AddIngredient(ItemID.SnowBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}
