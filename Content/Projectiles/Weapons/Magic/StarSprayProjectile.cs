using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Magic {
    public class StarSprayProjectile : ModProjectile {

        private int LifeTime => 3 * 60;
        private int BlueStartTime => 2 * 60 + 30;

        public override void SetDefaults() {
            Projectile.width = Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        int Timer { 
            get {
                return (int)Projectile.ai[0];
            }
            set { 
                Projectile.ai[0] = value;
            }
        }

        public override void AI() {
            Timer++;
            //Projectile.scale = Math.Min(1f, Projectile.scale + 0.02f);
            if (Timer >= LifeTime) {
                if (Projectile.owner == Main.myPlayer) {
                    ManaLifesteal.CreateNew(Projectile.GetSource_FromAI(), Projectile.Center, 5f, Projectile.owner, 1);
                }
                for (int i = 0; i < 6; i++) {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ManaRegeneration);
                }
                Projectile.timeLeft = 0;
                return;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            int size = GetSize(GetScale());
            hitbox = new Rectangle(hitbox.Center.X - size/2, hitbox.Center.Y - size/2, size, size);
        }

        public override bool PreDraw(ref Color lightColor) {
            float scale = GetScale();
            int size = GetSize(scale);

            float blueComponent = Math.Max(0, ((float)Timer - BlueStartTime) / (LifeTime - BlueStartTime));
            Color blue = new Color(0.3f, 0.3f, 1f);
            Color color = new(lightColor.ToVector4() * (1f - blueComponent) + blue.ToVector4() * blueComponent);

            Vector2 pos = Projectile.Center - new Vector2(size/2, size/2) - Main.screenPosition;
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, pos, null, color, 0, Vector2.Zero, scale, SpriteEffects.None);
            return false;
        }

        private float GetScale() {
            return Math.Min(1f, 0.1f + Timer * 0.02f + Timer * Timer * 0.001f);
        }

        private int GetSize(float scale) { 
            return (int)(34 * scale);
        }

    }
}
