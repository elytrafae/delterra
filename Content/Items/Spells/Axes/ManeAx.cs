using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Axes {
    public class ManeAx : ModItem {

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 30;
            Item.useTime = (Item.useAnimation = 50);

        }

    }
}
