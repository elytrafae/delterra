using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using YetToBeNamed.Systems;

namespace YetToBeNamed.Content.Items {
    public class TensionBit : ModItem {

        public override void SetDefaults() {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useTurn = true;
            Item.useAnimation = (Item.useTime = 17);
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.buyPrice(0, 0, 20);
            Item.width = 32;
            Item.height = 32;
        }

        public override void OnConsumeItem(Player player) {
            // "run on local client only"
            GrazingPlayer.Get(player).TP += 20;
            player.AddBuff(BuffID.PotionSickness, 45*60);
        }

    }
}
