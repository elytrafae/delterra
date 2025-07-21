using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria.ID;
using Terraria;

namespace Delterra.Content.Items.TensionRestore {
    public class TensionMax : AbstractTPRestoreItem {

        public override float TPHeal => 150;
        public override int TPTime => 45;

        public override int PotionSicknessTime => 80 * 60;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = 0;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Lime;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.UseSound = MySoundStyles.TensionMax;
        }

        public override void AddRecipes() {
            CreateRecipe(10)
                .AddIngredient<TensionBit>(20)
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.BeetleHusk, 10)
                .Register();
        }

    }
}
