using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Delterra.Content.Buffs;
using Microsoft.Xna.Framework;

namespace Delterra.Content.PlayerDrawLayers {
    public class DefendingEffectDrawLayer : PlayerDrawLayer {

        private const string SpritePrefix = nameof(Delterra) + "/Assets/MiscSprites/";
        private static Asset<Texture2D> defendSprite;

        public override void SetStaticDefaults() {
            defendSprite = ModContent.Request<Texture2D>(SpritePrefix + "Defend");
        }

        public override Position GetDefaultPosition() {
            return Terraria.DataStructures.PlayerDrawLayers.AfterLastVanillaLayer;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.drawPlayer.whoAmI == Main.myPlayer && drawInfo.shadow == 0) { // ONLY FOR *OUR REAL* PLAYER!
                if (drawInfo.drawPlayer.HasBuff<Defending>()) {
                    Color color = Color.White;// * 0.2f;
                    Vector2 pos = drawInfo.drawPlayer.Center - Main.screenPosition - defendSprite.Size()/2;
                    // Main.spriteBatch.Draw(defendSprite.Value, pos, color);
                    //Main.EntitySpriteDraw(defendSprite.Value, pos, null, color, 0, Vector2.Zero, 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
                }
               
            }
        }

    }
}
