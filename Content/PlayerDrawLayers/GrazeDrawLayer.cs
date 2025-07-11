using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.PlayerDrawLayers {
    internal class GrazeDrawLayer : PlayerDrawLayer {
        public override Position GetDefaultPosition() {
            return Terraria.DataStructures.PlayerDrawLayers.BeforeFirstVanillaLayer;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.drawPlayer.whoAmI == Main.myPlayer && drawInfo.shadow == 0) { // ONLY FOR *OUR REAL* PLAYER!
                GrazingPlayer modPlayer = GrazingPlayer.Get(drawInfo.drawPlayer);
                Rectangle rect = modPlayer.GetGrazeRectangle();
                Color color = Color.Chocolate * modPlayer.GrazeAreaAlpha();
                Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, rect.TopLeft() - Main.screenPosition, new Rectangle(0, 0, 1, 1), color, 0, Vector2.Zero, rect.Size(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            }
        }
    }
}
