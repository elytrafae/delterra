using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Axes {
    public class ManeAx : AbstractSusieAxe {
        

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 30;
            Item.useTime = (Item.useAnimation = 50);
            // TODO: Add shop values
        }

    }
}
