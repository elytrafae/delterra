using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.Axes {
    public class AutoAx : AbstractSusieAxe {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 80;
            Item.knockBack = 5f;
            Item.useTime = (Item.useAnimation = 50);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
            Item.shootSpeed = 10;
            Item.scale = 1.1f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
            if (hit.Crit) {
                GrazingPlayer.Get(player).TP += GrazingPlayer.GetTPForPercent(1);
            }
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<BraveAx>()
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.Wire, 50)
                .AddTile(TileID.MythrilAnvil);
        }

    }
}
