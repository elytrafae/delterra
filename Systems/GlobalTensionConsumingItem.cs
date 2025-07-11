using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class GlobalTensionConsumingItem : GlobalItem {

        private static string PLACE_COST_AFTER = "UseMana";
        private static string[] PLACE_COST_BEFORE = ["Placeable", "Ammo", "Consumable", "Material", "Tooltip0", "BestiaryNotes", "SpecialPrice", "Price"];

        public static LocalizedText TensionCostText { get; private set; }

        public override void SetStaticDefaults() {
            TensionCostText = Mod.GetLocalization("TensionCostText");
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
            return entity.ModItem is ITensionConsumingItem;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            TooltipLine line = new TooltipLine(Mod, "TensionCost", TensionCostText.Format(GetTensionCost(item, Main.LocalPlayer) / 100));
            int lineIndex = tooltips.FindIndex(line => line.Name == PLACE_COST_AFTER);
            if (lineIndex != -1) {
                tooltips.Insert(lineIndex + 1, line);
                return;
            }
            foreach (string spot in PLACE_COST_BEFORE) {
                lineIndex = tooltips.FindIndex(line => line.Name == spot);
                if (lineIndex != -1) {
                    tooltips.Insert(lineIndex, line);
                    return;
                }
            }
            // Failsafe
            tooltips.Add(line);
        }

        public static int GetTensionCost(Item item, Player player) {
            if (item.ModItem is ITensionConsumingItem tensionItem) { 
                return GetTensionCost(tensionItem, player);
            }
            return 0;
        }

        public static int GetTensionCost(ITensionConsumingItem tensionItem, Player player) {
            // TODO: TP cost modifying items go here
            return tensionItem.GetBaseTPCost(player);
        }

        public override bool CanUseItem(Item item, Player player) {
            if (item.ModItem is not ITensionConsumingItem tensionItem) {
                return true;
            }
            if (tensionItem.IsTPConsumedOnUse(player)) {
                return GetTensionCost(tensionItem, player) <= GrazingPlayer.Get(player).TP;
            }
            return false;
        }

        public override bool? UseItem(Item item, Player player) {
            if (item.ModItem is not ITensionConsumingItem tensionItem) {
                return null;
            }
            if (tensionItem.IsTPConsumedOnUse(player)) {
                GrazingPlayer.Get(player).TP -= GetTensionCost(tensionItem, player);
            }
            return null;
        }

    }
}
