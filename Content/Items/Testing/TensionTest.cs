using Delterra.Systems;
using Delterra.Systems.TPSources;
using Terraria;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Testing {

#if DEBUG
    public class TensionTest : ModItem {

        public override void SetDefaults() {
            Item.DefaultToAccessory(16, 16);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            // The context is wrong, but this is just a test item, so I don't care
            GrazingPlayer.Get(player).GainTP(10, new TPGainConsumeItemContext(Item)); 
        }

    }
#endif
}
