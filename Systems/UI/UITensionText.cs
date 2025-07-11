using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Delterra.Systems.UI {
    // because OFC adding custom fonts is hard
    public class UITensionText : UIElement {

        private Asset<Texture2D> fontTexture;

        private static readonly Rectangle BaseNumberRect = new Rectangle(0, 0, 12, 18);
        private static readonly Rectangle PercentRect = new Rectangle(99, 32, 14, 18);
        private static readonly Rectangle MaxRect = new Rectangle(0, 21, 20, 58);
        private static readonly Vector2[] NumberLocations = [new(0, 0), new(13, 0), new(26, 0), new(39, 0), new(52, 0), new(65, 0), new(78, 0), new(91, 0), new(36, 24), new(50, 24)];
        

        public int Number = 0;

        public UITensionText(Asset<Texture2D> fontTexture) {
            this.fontTexture = fontTexture;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            CalculatedStyle dimensions = GetDimensions();
            if (Number >= 100) {
                // If on 100%, display the MAX text
                spriteBatch.Draw(fontTexture.Value, dimensions.Position(), MaxRect, Color.Yellow);
                return;
            }

            if (Number < 0) {
                Number = 0; // Bounds check
            }

            // Draw number
            if (Number >= 10) {
                DrawDigit(spriteBatch, dimensions.Position(), Number/10);
            }
            DrawDigit(spriteBatch, dimensions.Position() + new Vector2(16, 0), Number % 10);

            // Draw percent sign
            spriteBatch.Draw(fontTexture.Value, dimensions.Position() + new Vector2(6, 30), PercentRect, Color.White);
        }

        private void DrawDigit(SpriteBatch spriteBatch, Vector2 position, int digit) {
            Rectangle rect = new Rectangle((int)NumberLocations[digit].X, (int)NumberLocations[digit].Y, BaseNumberRect.Width, BaseNumberRect.Height);
            spriteBatch.Draw(fontTexture.Value, position, rect, Color.White);
        }


    }
}
