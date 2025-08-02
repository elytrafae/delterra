using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons {
    public class ManaLifesteal : ModProjectile {

        public override void SetDefaults() {
            Projectile.width = Projectile.height = 3;
            Projectile.MaxUpdates = 3;
            Projectile.timeLeft *= Projectile.MaxUpdates;
        }

        int PlayerID {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        int Amount {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        float Velocity {
            get => Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        public override bool? CanHitNPC(NPC target) {
            return false;
        }

        public override bool CanHitPlayer(Player target) {
            return target.whoAmI == PlayerID;
        }

        public override void AI() {
            Player player = Main.player[PlayerID];
            if (player == null || !player.active) {
                Projectile.timeLeft = 0;
                return;
            }
            Vector2 diff = player.Center - Projectile.Center;
            Projectile.velocity = diff.SafeNormalize(Vector2.Zero) * Velocity;

            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ManaRegeneration);

            if (player.Hitbox.Intersects(Projectile.Hitbox)) {
                if (player.whoAmI == Main.myPlayer) {
                    int healedMana = Math.Min(player.statManaMax2 - player.statMana, Amount);
                    if (healedMana > 0) {
                        player.statMana += healedMana;
                        player.ManaEffect(healedMana);
                    }
                }
                Projectile.timeLeft = 0;
                return;
            }
        }



        public static Projectile CreateNew(IEntitySource source, Vector2 position, float velocity, int playerID, int amount) {
            Projectile proj = Projectile.NewProjectileDirect(
                source, 
                position, 
                Vector2.Zero, 
                ModContent.ProjectileType<ManaLifesteal>(), 
                0, 
                0, 
                playerID
            );
            if (proj != null && proj.active) { 
                ManaLifesteal modProj = proj.ModProjectile as ManaLifesteal;
                modProj.PlayerID = playerID;
                modProj.Amount = amount;
                modProj.Velocity = velocity;
            }
            return proj;
        }

    }
}
