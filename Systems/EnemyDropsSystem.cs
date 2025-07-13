using Delterra.Content.Items.Accessories;
using Delterra.Content.Items.Spells.Axes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class EnemyDropsNPC : GlobalNPC {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.Deerclops) {
                AddLootToBoss<Devilsknife>(npcLoot);
            }
            if (npc.type == NPCID.DD2Betsy) {
                AddLootToBoss<JusticeAx>(npcLoot);
            }
            if (npc.type == NPCID.UndeadMiner) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlowWrist>(), 2));
            }
        }

        private void AddLootToBoss<T>(NPCLoot npcLoot) where T : ModItem {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<T>()));
            npcLoot.Add(notExpertRule);
        }

    }

    public class EnemyDropsItem : GlobalItem {

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.DeerclopsBossBag) {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Devilsknife>()));
            }
            if (item.type == ItemID.BossBagBetsy) {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<JusticeAx>()));
            }
        }

    }
}
