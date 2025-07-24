using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Delterra.Content.Items.Weapons.Magic.Rings {
    public class ThornRing : AbstractNoelleRing {

        public override double IceShockCost => base.IceShockCost/2;
        public override double SnowGraveCost => base.SnowGraveCost / 2;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 400;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<GlacialFragment>(600)
                .AddIngredient(ItemID.Stinger, 10)
                .AddIngredient(ItemID.Cactus, 10)
                .AddIngredient(ItemID.SilverCoin, 19)
                .AddIngredient(ItemID.CopperCoin, 97)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
        public override void HoldItem(Player player) {
            if (Main.myPlayer != player.whoAmI) {
                return;
            }
            if (GrazingPlayer.Get(player).TP >= GlobalTensionConsumingItem.GetTensionCost(SnowGraveCost, player)) {
                ActiveSound activeSound = SoundEngine.SoundPlayer.FindActiveSound(MySoundStyles.SnowgraveBell);
                if (activeSound == null || !activeSound.IsPlaying) {
                    SoundEngine.PlaySound(MySoundStyles.SnowgraveBell, null, soundInstance => {
                        return Main.LocalPlayer.HeldItem?.type == Type && GrazingPlayer.Get(player).TP >= GlobalTensionConsumingItem.GetTensionCost(SnowGraveCost, player);
                    });
                }
            }
        }

    }
}
