using System;
using Delterra.Content.Gores;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles {
    public class RudeBuster : ModProjectile {

        public const int ACTUAL_WIDTH = 80;
        public const int ACTUAL_HEIGHT = 80;

        public override void SetDefaults() {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.extraUpdates = 1;
        }

        int Timer {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI() {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            Timer++;
            if (Timer == 1) {
                SoundEngine.PlaySound(MySoundStyles.RudeBusterSwing, Projectile.Center);
            }
            if (Timer % 10 == 0) {
                Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center - new Vector2(ACTUAL_WIDTH / 2, ACTUAL_HEIGHT / 2), Vector2.Zero, ModContent.GoreType<RudeBusterAfterimage>());
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, lightColor, Projectile.rotation, new Vector2(ACTUAL_WIDTH / 2, ACTUAL_HEIGHT / 2), 1f, SpriteEffects.None);
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            Rectangle newHitbox = new Rectangle(0, 0, 80, 80);
            newHitbox.X = (int)Projectile.Center.X - newHitbox.Width/2;
            newHitbox.Y = (int)Projectile.Center.Y - newHitbox.Height/2;
            hitbox = newHitbox;
        }

        public override void OnKill(int timeLeft) {
            if (timeLeft > 0) {
                SoundEngine.PlaySound(MySoundStyles.RudeBusterHit, Projectile.Center);
                // Create additional gore for the hit effects... ;_;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            modifiers.SetCrit();
        }

    }
}
