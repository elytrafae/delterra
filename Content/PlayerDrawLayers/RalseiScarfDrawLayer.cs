using Delterra.Content.Items.Spells.HealPrayer;
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
using Terraria.ModLoader;

namespace Delterra.Content.PlayerDrawLayers {
    internal class RalseiScarfDrawLayer : PlayerDrawLayer {
        public override Position GetDefaultPosition() {
            return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.NeckAcc);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.drawPlayer.neck <= 0) { // Don't draw Ralsei scarf if another neck accessory is visible
                if (drawInfo.drawPlayer.HeldItem?.ModItem is AbstractRalseiScarf ralScarf) {
                    // TODO: very shit code for testing. Will have to make a system where Ralsei Scarves load their neck texture themselves into a registry
                    // TODO: make another sprite for when the scarf is mid-attack! If the attack sprite does not exist, and the wear sprite does, assume the artist intended them to be the same
                    Asset<Texture2D> neckAsset = ModContent.Request<Texture2D>(ralScarf.Texture + "_Neck");
                    if (neckAsset.IsLoaded) {
                        DrawData item = new DrawData(neckAsset.Value, new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2((float)(drawInfo.drawPlayer.bodyFrame.Width / 2), (float)(drawInfo.drawPlayer.bodyFrame.Height / 2)), drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect);
                        //item.shader = drawInfo.cNeck;
                        drawInfo.DrawDataCache.Add(item);
                    }
                }
            }
        }
    }
}
