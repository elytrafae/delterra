using Delterra.Content.Items.Spells.Scarves;
using FaeLibrary.API;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Scarves {
    public class RedScarfProjectile : ModProjectile {

        public int Range => 400;
        public float ReleaseRatio => 0.25f;

        // TODO: For some reason, more melee speed = more range now...?

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 60000;
            //Projectile.extraUpdates = 4;
        }

        public override void AI() {
            if (Projectile.TryGetOwner(out Player player)) {
                Vector2 playerCenter = player.Center + AbstractRalseiScarf.GetShootOffset(player);
                Vector2 diff = (Projectile.Center - playerCenter) / 2f;

                if (IsOnFirstFrame) {
                    IsOnFirstFrame = false;
                    StartingPosition = Projectile.Center;
                    FinalPosBeforePullback = Projectile.Center + (Projectile.velocity * Range); // This assumes that the velocity is 1!
                    Projectile.velocity = Vector2.Zero;
                }

                Projectile.rotation = (float)Math.Atan2(diff.Y, diff.X);
                /*
                while (Projectile.rotation < 0) {
                    Projectile.rotation += MathHelper.TwoPi;
                }
                */

                if (player.itemTime < player.itemTimeMax * ReleaseRatio && !OnWayBack) {
                    OnWayBack = true;
                    // Reset immunity on the way back
                    Projectile.ResetLocalNPCHitImmunity();
                }

                if (!OnWayBack) {
                    // The scarf is going forward!
                    float outgoingFrames = player.itemTimeMax * (1f - ReleaseRatio);
                    Projectile.position = (FinalPosBeforePullback - StartingPosition) / outgoingFrames * (player.itemTimeMax - player.itemTime) + StartingPosition;
                } else {
                    // The scarf is rolling back!
                    Projectile.position = (FinalPosBeforePullback - playerCenter) / (player.itemTimeMax * ReleaseRatio) * (player.itemTime) + playerCenter;
                }

                if (player.ItemTimeIsZero || (player.itemTime >= player.itemTimeMax && OnWayBack)) {
                    Projectile.timeLeft = 0;
                }
            }
        }

        bool IsOnFirstFrame {
            get {
                return Projectile.ai[0] < 0.5f;
            }
            set {
                Projectile.ai[0] = value ? 0f : 1f;
            }
        }

        bool OnWayBack {
            get {
                return Projectile.ai[0] > 1.5f;
            }
            set {
                Projectile.ai[0] = value ? 2f : 1f;
            }
        }

        Vector2 FinalPosBeforePullback {
            get {
                return new(Projectile.ai[1], Projectile.ai[2]);
            }
            set {
                Projectile.ai[1] = value.X;
                Projectile.ai[2] = value.Y;
            }
        }

        Vector2 StartingPosition = new Vector2();

        public override void SendExtraAI(BinaryWriter writer) {
            writer.WriteVector2(StartingPosition);
        }

        public override void ReceiveExtraAI(BinaryReader reader) {
            StartingPosition = reader.ReadVector2();
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            if (Projectile.TryGetOwner(out Player player)) {
                Vector2 playerCenter = player.Center + AbstractRalseiScarf.GetShootOffset(player);
                Vector2 diff = Projectile.Center - playerCenter;
                Vector2 halfway = diff / 2f + playerCenter;

                int width = (int)diff.Length() + Projectile.width;
                int height = Projectile.height;
                int x = (int)halfway.X - width / 2;
                int y = (int)halfway.Y - height / 2;
                hitbox = new Rectangle(x, y, width, height);
            }
        }


        public override bool PreDraw(ref Color lightColor) {
            FaeCollisionUtils.DrawOrientedRactangleHitboxDebug(this);
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            return FaeCollisionUtils.OrientedRactanglesCollide(projHitbox, Projectile.rotation, targetHitbox, 0);
        }

        public override bool? CanDamage() {
            return true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac) {
            return false;
        }

    }
}
