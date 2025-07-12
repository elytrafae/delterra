using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.PlayerDrawLayers {
    internal class GrazeDrawLayer : PlayerDrawLayer {

        private const string SpritePrefix = nameof(Delterra) + "/Assets/MiscSprites/";
        private static Asset<Texture2D> regularGrazeTexture;
        private static Asset<Texture2D> pinkRibbonGrazeTexture;

        public override void SetStaticDefaults() {
            regularGrazeTexture = ModContent.Request<Texture2D>(SpritePrefix + "Graze");
            pinkRibbonGrazeTexture = ModContent.Request<Texture2D>(SpritePrefix + "GrazePinkRibbon");
        }

        public override Position GetDefaultPosition() {
            return Terraria.DataStructures.PlayerDrawLayers.BeforeFirstVanillaLayer;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.drawPlayer.whoAmI == Main.myPlayer && drawInfo.shadow == 0) { // ONLY FOR *OUR REAL* PLAYER!
                GrazingPlayer modPlayer = GrazingPlayer.Get(drawInfo.drawPlayer);
                Rectangle rect = modPlayer.GetGrazeRectangle();
                Color color = Color.White * modPlayer.GrazeAreaAlpha();
                //Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, rect.TopLeft() - Main.screenPosition, new Rectangle(0, 0, 1, 1), color, 0, Vector2.Zero, rect.Size(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
                Main.EntitySpriteDraw((modPlayer.bigGrazeAreaStat ? pinkRibbonGrazeTexture : regularGrazeTexture).Value, rect.TopLeft() - Main.screenPosition, null, color, 0, Vector2.Zero, 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            }
        }
    }
}
