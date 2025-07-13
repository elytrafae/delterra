using Delterra.Content.Mounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    internal class TheKingsChariotBuff : ModBuff {

        // Use the vanilla DisplayName ("Minecart")
        //public override LocalizedText DisplayName => Language.GetText("BuffName.MinecartLeft");
        // But for the sake of funny, we will use a custom name

        public override void SetStaticDefaults() {
            // Handles automatically mounting the player within Update, and setting Main.buffNoTimeDisplay/buffNoSave (no need to write yourself like in ExampleMountBuff)
            BuffID.Sets.BasicMountData[Type] = new BuffID.Sets.BuffMountData() {
                mountID = ModContent.MountType<TheKingsChariot>()
            };
        }

    }
}
