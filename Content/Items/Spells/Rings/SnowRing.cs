using Delterra.Content.Projectiles;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Rings
{
    public class SnowRing : AbstractNoelleRing {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 45;
            // TODO: Add shop values
        }
    }
}
