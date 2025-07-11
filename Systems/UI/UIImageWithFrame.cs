using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace Delterra.Systems.UI {
    public class UIImageWithFrame : UIElement {
        private Asset<Texture2D> _texture;
        public float ImageScale = 1f;
        public float Rotation;
        public Rectangle? sourceRectangle = null;
        public bool AllowResizingDimensions = true;
        public Color Color = Color.White;
        public Vector2 NormalizedOrigin = Vector2.Zero;
        public bool RemoveFloatingPointsFromDrawPosition;
        private Texture2D _nonReloadingTexture;

        public UIImageWithFrame(Asset<Texture2D> texture) {
            SetImage(texture);
        }

        public UIImageWithFrame(Texture2D nonReloadingTexture) {
            SetImage(nonReloadingTexture);
        }

        public void SetImage(Asset<Texture2D> texture) {
            _texture = texture;
            _nonReloadingTexture = null;
            if (AllowResizingDimensions) {
                Width.Set(_texture.Width(), 0f);
                Height.Set(_texture.Height(), 0f);
            }
        }

        public void SetImage(Texture2D nonReloadingTexture) {
            _texture = null;
            _nonReloadingTexture = nonReloadingTexture;
            if (AllowResizingDimensions) {
                Width.Set(_nonReloadingTexture.Width, 0f);
                Height.Set(_nonReloadingTexture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            CalculatedStyle dimensions = GetDimensions();
            Texture2D texture2D = null;
            if (_texture != null)
                texture2D = _texture.Value;

            if (_nonReloadingTexture != null)
                texture2D = _nonReloadingTexture;

            Vector2 vector = texture2D.Size();
            Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
            if (RemoveFloatingPointsFromDrawPosition)
                vector2 = vector2.Floor();

            spriteBatch.Draw(texture2D, vector2, sourceRectangle, Color, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
        }
    }

}
