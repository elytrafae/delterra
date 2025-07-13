using Delterra.Content.Items.Accessories;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.PlayerDrawLayers {
    internal class DealmakerDrawLayer : PlayerDrawLayer {

        static Asset<Texture2D> Sprite1;
        static Asset<Texture2D> Sprite2;

        public override void SetStaticDefaults() {
            string texturePath = ModContent.GetModItem(ModContent.ItemType<Dealmaker>()).Texture;
            Sprite1 = ModContent.Request<Texture2D>(texturePath + "_Face");
            Sprite2 = ModContent.Request<Texture2D>(texturePath + "_Face2");
        }
        public override Position GetDefaultPosition() {
            return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.FaceAcc);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            Vector2 vector = Vector2.Zero;
            if (drawInfo.drawPlayer.mount.Active && drawInfo.drawPlayer.mount.Type == 52) {
                vector = new(28f, -2f);
            }
            vector *= drawInfo.drawPlayer.Directions;
            if (EquipmentEffectPlayer.Get(drawInfo.drawPlayer).dealmakerVisible) {
                Vector2 vector2 = Vector2.Zero;
                DrawData item = new((drawInfo.drawPlayer.Directions.X > 0 ? Sprite2 : Sprite1).Value, vector2 + vector + new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.headPosition + drawInfo.headVect, drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorHead, drawInfo.drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect);
                item.shader = drawInfo.cFace;
                drawInfo.DrawDataCache.Add(item);
            }
        }
    }
}
