using Delterra.Content.Items.Spells.Scarves;
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
using Terraria.ModLoader;

namespace Delterra.Content.PlayerDrawLayers {
    internal class RalseiScarfDrawLayer : PlayerDrawLayer {
        public override Position GetDefaultPosition() {
            return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.NeckAcc);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            Player p = drawInfo.drawPlayer;
            if (p.neck <= 0) { // Don't draw Ralsei scarf if another neck accessory is visible
                
                if (p.HeldItem != null && RalseiScarfSpriteRegistry.TryGetScarfSprites(
                            p.HeldItem.type, 
                            out Asset<Texture2D> idleAsset,
                            out Asset<Texture2D> attackingAsset)) { 
                    Asset<Texture2D> neckAsset = p.ownedProjectileCounts[p.HeldItem.shoot] > 0 ? attackingAsset : idleAsset;
                    DrawData item = new DrawData(neckAsset.Value, new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(p.bodyFrame.Width / 2) + (float)(p.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)p.height - (float)p.bodyFrame.Height + 4f)) + p.bodyPosition + new Vector2((float)(p.bodyFrame.Width / 2), (float)(p.bodyFrame.Height / 2)), p.bodyFrame, drawInfo.colorArmorBody, p.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect);
                    //item.shader = drawInfo.cNeck;
                    drawInfo.DrawDataCache.Add(item);
                }
            }
        }
    }
}
