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
    public class Devilsknife : AbstractSusieAxe {

        public override double RudeBusterCost => 40;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 55;
            Item.knockBack = 3.5f;
            Item.useTime = (Item.useAnimation = 40);
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            Item.shootSpeed = 9;
            Item.scale = 1.2f;
        }

    }
}
