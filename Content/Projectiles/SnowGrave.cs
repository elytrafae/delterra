using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles {
    public class SnowGrave : ModProjectile {

        public static readonly Vector3 LIGHT = new Vector3(0.5f, 0.5f, 0.9f) * 2;

        private static int AscendSpeed => 5;
        private static int WindupTime => 60;
        private static int StartToEndGrowth => 600;

        public override void SetDefaults() {
            Projectile.width = 200;
            Projectile.height = 0;
            Projectile.extraUpdates = 3;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 14;
            Projectile.timeLeft = 100000;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

        int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        float EndHeight {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void AI() {
            Timer++;
            if (Timer == 1) {
                SoundEngine.PlaySound(MySoundStyles.SnowgraveCast);
                EndHeight = Projectile.position.Y - Main.screenHeight - 100;
            }
            if (Timer > WindupTime) {
                Projectile.position.Y -= AscendSpeed;
                if (Timer < StartToEndGrowth) {
                    Projectile.height += AscendSpeed;
                }
            }

            if (Projectile.position.Y + Projectile.height < EndHeight) {
                Projectile.Kill();
            }

            Rectangle rect = new Rectangle(Projectile.Hitbox.X, Projectile.Hitbox.Y, Projectile.Hitbox.Width, Projectile.Hitbox.Height);
            ModifyDamageHitbox(ref rect);
            for (int i = rect.Y; i < rect.Y + rect.Height; i += 30) {
                Lighting.AddLight(new Vector2(rect.Center.X, i), LIGHT);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            int inactiveHeight = (int)Math.Max(0, EndHeight - hitbox.Top);
            hitbox.Height -= inactiveHeight;
            hitbox.Y += inactiveHeight;
        }

        public override bool PreDraw(ref Color lightColor) {
            if (Timer <= WindupTime) {
                return false;
            }
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            for (int i = 0; i < Projectile.height; i += 20) {
                float margin = (float)(Math.Sin(MathHelper.ToRadians(Timer * 1 + i)) * 20 + 10);
                float allowedWidth = Projectile.width - margin * 2;
                for (int j = 0; j < 10; j++) {
                    if (Projectile.position.Y + i < EndHeight) {
                        continue;
                    }
                    Vector2 drawpos = new Vector2(Projectile.position.X + allowedWidth/10*j + margin, Projectile.position.Y + i) - Main.screenPosition;
                    Main.EntitySpriteDraw(texture, drawpos, null, Color.White, 0, new Vector2(21.5f, 21.5f), 1f, SpriteEffects.None);
                }
            }

            for (int i = -10; i < Projectile.height; i += 20) {
                float margin = (float)(Math.Sin(MathHelper.ToRadians(Timer * 0.6f + i* 0.7f)) * 10);
                float allowedWidth = Projectile.width - margin * 2;
                for (int j = 0; j < 10; j++) {
                    if (Projectile.position.Y + i < EndHeight) {
                        continue;
                    }
                    Vector2 drawpos = new Vector2(Projectile.position.X + allowedWidth / 10 * j + margin, Projectile.position.Y + i) - Main.screenPosition;
                    Main.EntitySpriteDraw(texture, drawpos, null, Color.White, 0, new Vector2(21.5f, 21.5f), 1f, SpriteEffects.None);
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(BuffID.Frostburn2, 20 * 60);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            target.AddBuff(BuffID.Frostburn2, 20 * 60);
        }

    }
}
