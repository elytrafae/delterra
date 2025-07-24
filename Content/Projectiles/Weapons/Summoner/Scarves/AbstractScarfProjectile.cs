using Delterra.Content.Items.Weapons.Summoner.Scarves;
using FaeLibrary.API;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Delterra.Content.Projectiles.Weapons.Summoner.Scarves {
    public abstract class AbstractScarfProjectile : ModProjectile {

        public abstract int Range { get; }
        public virtual float ReleaseRatio => 0.25f;
        public virtual bool PreciseRangeControl => true;

        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 60000;
        }

        public override void AI() {
            if (Projectile.TryGetOwner(out Player player)) {
                if (player.ItemTimeIsZero || player.itemTime >= player.itemTimeMax && OnWayBack) {
                    Projectile.timeLeft = 0;
                    return;
                }

                Vector2 playerCenter = player.Center + AbstractRalseiScarf.GetShootOffset(player);
                Vector2 diff = (Projectile.Center - playerCenter) / 2f;

                if (IsOnFirstFrame && player.whoAmI == Main.myPlayer) {
                    IsOnFirstFrame = false;
                    StartingPosition = Projectile.Center;
                    FinalPosBeforePullback = Projectile.Center + Projectile.velocity * Range;
                    // More precise range control
                    if (PreciseRangeControl && playerCenter.DistanceSQ(Main.MouseWorld) < playerCenter.DistanceSQ(FinalPosBeforePullback)) {
                        FinalPosBeforePullback = Main.MouseWorld;
                    }
                    Projectile.velocity = Vector2.Zero;
                    Projectile.netUpdate = true;
                }

                Projectile.rotation = (float)Math.Atan2(diff.Y, diff.X);

                if (player.itemTime < player.itemTimeMax * ReleaseRatio && !OnWayBack) {
                    OnWayBack = true;
                    // Reset immunity on the way back
                    Projectile.ResetLocalNPCHitImmunity();
                    OnPullback(player, FinalPosBeforePullback);
                }

                if (!OnWayBack) {
                    // The scarf is going forward!
                    float outgoingFrames = player.itemTimeMax * (1f - ReleaseRatio);
                    Projectile.position = (FinalPosBeforePullback - StartingPosition) / outgoingFrames * (player.itemTimeMax - player.itemTime) + StartingPosition;
                } else {
                    // The scarf is rolling back!
                    Projectile.position = (FinalPosBeforePullback - playerCenter) / (player.itemTimeMax * ReleaseRatio) * player.itemTime + playerCenter;
                }
            }
        }

        byte State = 0; // Don't read directly! 0 means on first frame, 1 means it's going out, 2 means it's pulling back
        Vector2 StartingPosition = new Vector2();
        Vector2 FinalPosBeforePullback = new Vector2();

        bool IsOnFirstFrame {
            get {
                return State == 0;
            }
            set {
                State = (byte)(value ? 0 : 1);
            }
        }

        bool OnWayBack {
            get {
                return State == 2;
            }
            set {
                State = (byte)(value ? 2 : 1);
            }
        }

        
        // TODO: Write rendering code
        public sealed override void SendExtraAI(BinaryWriter writer) {
            writer.WriteVector2(StartingPosition);
            writer.Write(State);
            writer.WriteVector2(FinalPosBeforePullback);
        }

        public sealed override void ReceiveExtraAI(BinaryReader reader) {
            StartingPosition = reader.ReadVector2();
            State = reader.ReadByte();
            FinalPosBeforePullback = reader.ReadVector2();
        }

        public sealed override void ModifyDamageHitbox(ref Rectangle hitbox) {
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

        public abstract void OnPullback(Player owner, Vector2 finalPos);

    }
}
