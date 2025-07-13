using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria.ID;

namespace Delterra.Content.Items.TensionRestore {
    public class TensionGem : AbstractTPRestoreItem {

        public override int TPHeal => GrazingPlayer.GetTPForPercent(50);

        public override int PotionSicknessTime => 60 * 60;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = 0;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 0, 15, 0);
            Item.UseSound = MySoundStyles.TensionGem;
        }

        public override void AddRecipes() {
            CreateRecipe(10)
                .AddIngredient<TensionBit>(20)
                .AddIngredient<MaliusHammer>()
                .Register();
        }

    }
}
