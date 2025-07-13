using System;
using Delterra.Content.Gores;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles {
    public class RudeBuster : ModProjectile {

        public const int ACTUAL_WIDTH = 62;
        public const int ACTUAL_HEIGHT = 62;
        public static readonly Vector3 LIGHT = new Vector3(0.8f, 0, 0.76f);

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
            Vector2 visualPosition = Projectile.Center - new Vector2(ACTUAL_WIDTH / 2, ACTUAL_HEIGHT / 2);
            if (Timer % Math.Max(5 - (int)(Projectile.velocity.Length() / 4), 1) == 0) {
                Gore.NewGore(Projectile.GetSource_FromAI(), visualPosition, Vector2.Zero, ModContent.GoreType<RudeBusterAfterimage>());
            }
            for (int i = 0; i < 1; i++) {
                Dust.NewDust(visualPosition, ACTUAL_WIDTH, ACTUAL_HEIGHT, DustID.ShimmerSpark);
            }
            Lighting.AddLight(Projectile.Center, LIGHT);
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
                Vector2 visualPosition = Projectile.Center - new Vector2(ACTUAL_WIDTH / 2, ACTUAL_HEIGHT / 2);
                for (int i = 0; i < 25; i++) {
                    Dust.NewDust(visualPosition, ACTUAL_WIDTH, ACTUAL_HEIGHT, DustID.ShimmerSpark);
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            modifiers.SetCrit();
            modifiers.ScalingArmorPenetration += 0.3f;
        }

    }
}
