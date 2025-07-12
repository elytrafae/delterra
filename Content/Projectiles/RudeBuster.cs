using Delterra.Content.Gores;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles {
    public class RudeBuster : ModProjectile {

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
            Timer++;
            if (Timer == 1) {
                SoundEngine.PlaySound(MySoundStyles.RudeBusterSwing, Projectile.Center);
            }
            if (Timer % 20 == 0) {
                Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.GoreType<RudeBusterAfterimage>());
            }
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

    }
}
