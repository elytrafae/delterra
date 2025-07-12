using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Delterra.Content.Items.TensionRestore {
    public abstract class AbstractTPRestoreItem : ModItem {

        public abstract int TPHeal { get; }
        public abstract int PotionSicknessTime { get; }
        public LocalizedText TPTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".TPRestoreText");

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = SoundID.Item103;
            Item.maxStack = Item.CommonMaxStack;
        }

        public override bool CanUseItem(Player player) {
            return GrazingPlayer.Get(player).TP < GrazingPlayer.MAXTP && !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool? UseItem(Player player) {
            GrazingPlayer.Get(player).TP += TPHeal;
            player.AddBuff(BuffID.PotionSickness, PotionSicknessTime);
            return true;
        }

        public override LocalizedText Tooltip => TPTooltip.WithFormatArgs(TPHeal / GrazingPlayer.TP_PER_PERCENT, PotionSicknessTime/60, base.Tooltip);

    }
}
