using Iced.Intel;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class RalseiScarfSpriteRegistry : ModSystem {

        private static Dictionary<int, RalseiScarfSpriteData> spriteData = new();

        public override void Unload() {
            spriteData.Clear();
        }

        public static void Register(ModItem item) { 
            RalseiScarfSpriteData data = new RalseiScarfSpriteData();
            string prefix = item.Texture + "_Neck";
            data.idle = ModContent.Request<Texture2D>(prefix);
            if (!ModContent.RequestIfExists(prefix + "Attack", out data.attacking)) {
                data.attacking = data.idle;
            }
            spriteData[item.Type] = data;
        }

        public static bool TryGetScarfSprites(int itemType, out Asset<Texture2D> idleAsset, out Asset<Texture2D> attackingAsset) {
            idleAsset = null;
            attackingAsset = null;
            if (spriteData.TryGetValue(itemType, out RalseiScarfSpriteData data)) {
                if (data.idle.IsLoaded) { 
                    idleAsset = data.idle;
                    attackingAsset = data.attacking;
                    return true;
                }
                return false;
            }
            return false;
        }


        private struct RalseiScarfSpriteData {
            public Asset<Texture2D> idle = null;
            public Asset<Texture2D> attacking = null;

            public RalseiScarfSpriteData() { }
        }

    }
}
