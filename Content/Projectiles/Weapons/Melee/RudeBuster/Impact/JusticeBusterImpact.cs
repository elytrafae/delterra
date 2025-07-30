using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Melee.RudeBuster.Impact {
    public class JusticeBusterImpact : BraveBusterImpact {

        public override Color BusterColor => new Color(0x9B, 0xFC, 0x4E);

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            base.ModifyHitNPC(target, ref modifiers);
            modifiers.ScalingArmorPenetration += 0.5f;
        }

    }
}
