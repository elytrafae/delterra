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

        public override int TPHeal => GrazingPlayer.GetTPForPercent(100);

        public override int PotionSicknessTime => 80 * 60;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = 0;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = MySoundStyles.TensionMax;
        }

    }
}
