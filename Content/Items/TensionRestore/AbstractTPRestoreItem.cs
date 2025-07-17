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
using Delterra.Systems.TPSources;

namespace Delterra.Content.Items.TensionRestore {
    public abstract class AbstractTPRestoreItem : ModItem {

        public abstract float TPHeal { get; }
        public abstract int PotionSicknessTime { get; }
        public LocalizedText TPTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".TPRestoreText");

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = MySoundStyles.Tension;
            Item.maxStack = Item.CommonMaxStack;
        }

        public override bool CanUseItem(Player player) {
            return GrazingPlayer.Get(player).TP < GrazingPlayer.MAXTP && !player.HasBuff(BuffID.PotionSickness);
        }

        public override bool? UseItem(Player player) {
            GrazingPlayer.Get(player).GainTP(TPHeal, new TPGainConsumeItemContext(Item));
            int potionSicknessTime = PotionSicknessTime;
            if (EquipmentEffectPlayer.Get(player).tensionRestorePotionSicknessReduced) {
                potionSicknessTime = (int)(potionSicknessTime * 0.85f);
            }
            player.AddBuff(BuffID.PotionSickness, potionSicknessTime);
            return true;
        }

        public override LocalizedText Tooltip => TPTooltip.WithFormatArgs(TPHeal, PotionSicknessTime/60, base.Tooltip);

    }
}
