using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Gores {
    public class RudeBusterAfterimage : ModGore {

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = 35;
            if (source is EntitySource_Parent parentSource) {
                if (parentSource.Entity is Projectile proj) {
                    gore.rotation = proj.rotation;
                }
            }
        }

        public override bool Update(Gore gore) {
            gore.timeLeft--;
            Lighting.AddLight(gore.position + new Vector2(gore.Width/2, gore.Height/2), RudeBuster.LIGHT * (gore.timeLeft / 30f));
            return gore.timeLeft <= 0;
        }

        public override Color? GetAlpha(Gore gore, Color lightColor) {
            Vector2 drawPos = gore.position + new Vector2(gore.Width/2f, gore.Height/2f) - Main.screenPosition;
            Color drawColor = lightColor * (gore.timeLeft / 30f);
            Main.EntitySpriteDraw(TextureAssets.Gore[gore.type].Value, drawPos, null, drawColor, gore.rotation, new Vector2(gore.Width / 2f, gore.Height / 2f), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            return Color.Transparent;
        }

    }
}
