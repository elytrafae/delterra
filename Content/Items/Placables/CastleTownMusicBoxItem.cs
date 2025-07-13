using Delterra.Content.Scenes;
using Delterra.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Placables {
    public class CastleTownMusicBoxItem : ModItem {
        public override void SetStaticDefaults() {
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, RalseiTownScene.MUSIC_PATH), ModContent.ItemType<CastleTownMusicBoxItem>(), ModContent.TileType<CastleTownMusicBox>());
        }

        public override void SetDefaults() {
            Item.DefaultToMusicBox(ModContent.TileType<CastleTownMusicBox>(), 0);
        }
    }
}
