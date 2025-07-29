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
using FaeLibrary.API;

namespace Delterra.Content.Items.TensionRestore {
    public abstract class AbstractTPRestoreItem : ModItem, IFaeModItem {

        public abstract float TPHeal { get; }
        public virtual int TPTime => 1;
        public virtual float PotionSicknessMultiplier => 1f;
        public LocalizedText TPTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".TPRestoreText");

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = MySoundStyles.Tension;
            Item.maxStack = Item.CommonMaxStack;
            Item.potion = true;
        }

        /* This is done automatically now
        public override bool CanUseItem(Player player) {
            return GrazingPlayer.Get(player).TP < GrazingPlayer.MAXTP && !player.HasBuff(BuffID.PotionSickness);
        }
        */

        public override bool? UseItem(Player player) {
            GrazingPlayer.Get(player).GainTPOverTime(TPHeal, TPTime, new TPGainConsumeItemContext(Item));
            // player.AddBuff(BuffID.PotionSickness, GetPotionSicknessTime(player)); // This is done automatically now
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            TooltipLine TPRestoreLine = new TooltipLine(Mod, "TPRestore", 
                TPTooltip.Format(
                    (int)GrazingPlayer.Get(Main.LocalPlayer).CalculateTPGain(TPHeal, new TPGainConsumeItemContext(Item), false)
                )
            );
            int index = tooltips.FindIndex(line => line.Name == "Consumable");
            if (index > -1) {
                tooltips.Insert(index, TPRestoreLine);
            } else {
                index = tooltips.FindIndex(line => line.Name == "Tooltip0");
                if (index > -1) {
                    tooltips.Insert(index, TPRestoreLine);
                } else { 
                    // Failsafe
                    tooltips.Add(TPRestoreLine);
                }
            }
        }

        void IFaeModItem.ModifyPotionDelay(Player player, ref int delay) {
            delay = (int)(delay * PotionSicknessMultiplier);
        }

    }
}
