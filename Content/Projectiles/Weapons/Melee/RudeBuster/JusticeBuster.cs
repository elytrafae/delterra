using System;
using Delterra.Content.Gores;
using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster {
    public class JusticeBuster : BraveBuster {

        public override Color BusterColor => new Color(0x9B, 0xFC, 0x4E);
        public override int ImpactProjectileType => ModContent.ProjectileType<JusticeBusterImpact>();

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            base.ModifyHitNPC(target, ref modifiers);
            modifiers.ScalingArmorPenetration += 0.5f;
        }

    }
}
