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
        private static Asset<Texture2D> Graze9;

        public override void SetStaticDefaults() {
            Graze9 = ModContent.Request<Texture2D>(SpritePrefix + "Graze9");
        }

        public override Position GetDefaultPosition() {
            return Terraria.DataStructures.PlayerDrawLayers.BeforeFirstVanillaLayer;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.drawPlayer.whoAmI == Main.myPlayer && drawInfo.shadow == 0) { // ONLY FOR *OUR REAL* PLAYER!
                GrazingPlayer modPlayer = GrazingPlayer.Get(drawInfo.drawPlayer);
                Rectangle rect = modPlayer.GetGrazeRectangle();
                Color color = Color.White * modPlayer.GrazeAreaAlpha();

                // DEBUG
                //Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, rect.TopLeft() - Main.screenPosition, new Rectangle(0, 0, 1, 1), Color.Orange * 0.4f, 0, Vector2.Zero, rect.Size(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None);

                Texture2D texture = Graze9.Value;
                int textureSliceWidth = texture.Width / 3;
                int textureSliceHeight = texture.Height / 3;

                // Corners
                DrawData dataTopLeft = new DrawData(
                    texture, 
                    rect.TopLeft() - Main.screenPosition,
                    GetSlice(0, 0, textureSliceWidth, textureSliceHeight), 
                    color, 
                    0, 
                    Vector2.Zero, 
                    1f, 
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataTopLeft);

                DrawData dataBottomLeft = new DrawData(
                    texture, 
                    rect.BottomLeft() - Main.screenPosition - new Vector2(0, textureSliceHeight),
                    GetSlice(0, 2, textureSliceWidth, textureSliceHeight), 
                    color, 
                    0, 
                    Vector2.Zero, 
                    1f, 
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataBottomLeft);

                DrawData dataTopRight = new DrawData(
                    texture,
                    rect.TopRight() - Main.screenPosition - new Vector2(textureSliceWidth, 0),
                    GetSlice(2, 0, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataTopRight);

                DrawData dataBottomRight = new DrawData(
                    texture,
                    rect.BottomRight() - Main.screenPosition - new Vector2(textureSliceWidth, textureSliceHeight),
                    GetSlice(2, 2, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataBottomRight);

                // Sides
                DrawData dataTop = new DrawData(
                    texture,
                    rect.TopLeft() - Main.screenPosition + new Vector2(textureSliceWidth, 0),
                    GetSlice(1, 0, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    new Vector2(((float)rect.Width - textureSliceWidth*2)/textureSliceWidth, 1f),
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataTop);

                DrawData dataLeft = new DrawData(
                    texture,
                    rect.TopLeft() - Main.screenPosition + new Vector2(0, textureSliceHeight),
                    GetSlice(0, 1, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    new Vector2(1f, ((float)rect.Height - textureSliceHeight * 2) / textureSliceHeight),
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataLeft);

                DrawData dataRight = new DrawData(
                    texture,
                    rect.TopRight() - Main.screenPosition + new Vector2(-textureSliceWidth, textureSliceHeight),
                    GetSlice(2, 1, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    new Vector2(1f, ((float)rect.Height - textureSliceHeight * 2) / textureSliceHeight),
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataRight);

                DrawData dataBottom = new DrawData(
                    texture,
                    rect.BottomLeft() - Main.screenPosition + new Vector2(textureSliceWidth, -textureSliceHeight),
                    GetSlice(1, 2, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    new Vector2(((float)rect.Width - textureSliceWidth * 2) / textureSliceWidth, 1f),
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataBottom);

                // Center
                DrawData dataCenter = new DrawData(
                    texture,
                    rect.TopLeft() - Main.screenPosition + new Vector2(textureSliceWidth, textureSliceHeight),
                    GetSlice(1, 1, textureSliceWidth, textureSliceHeight),
                    color,
                    0,
                    Vector2.Zero,
                    new Vector2(((float)rect.Width - textureSliceWidth * 2) / textureSliceWidth, ((float)rect.Height - textureSliceHeight * 2) / textureSliceHeight),
                    SpriteEffects.None
                );
                drawInfo.DrawDataCache.Add(dataCenter);
            }
        }

        private static Rectangle GetSlice(int x, int y, int width, int height) { 
            return new Rectangle(x*width, y*height, width, height);
        }
    }
}
