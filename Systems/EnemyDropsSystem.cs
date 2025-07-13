using Delterra.Content.Items;
using Delterra.Content.Items.Accessories;
using Delterra.Content.Items.Spells.Axes;
using Delterra.Content.Items.Spells.Rings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Systems {
    public class EnemyDropsNPC : GlobalNPC {

        private static int[] CelestialCreatures = [
            NPCID.SolarCorite,
            NPCID.SolarCrawltipedeHead,
            NPCID.SolarDrakomire,
            NPCID.SolarDrakomireRider,
            NPCID.SolarSroller,
            NPCID.SolarSolenian,
            NPCID.SolarSpearman,
            NPCID.VortexHornet,
            NPCID.VortexHornetQueen,
            NPCID.VortexRifleman,
            NPCID.VortexSoldier,
            NPCID.NebulaBeast,
            NPCID.NebulaBrain,
            NPCID.NebulaHeadcrab,
            NPCID.NebulaSoldier,
            NPCID.StardustCellSmall,
            NPCID.StardustJellyfishBig,
            NPCID.StardustSoldier,
            NPCID.StardustSpiderBig,
            NPCID.StardustWormHead
        ];

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.Deerclops) {
                AddLootToBoss<Devilsknife>(npcLoot);
                AddLootToBoss<SnowRing>(npcLoot);
            }
            if (npc.type == NPCID.DD2Betsy) {
                AddLootToBoss<JusticeAx>(npcLoot);
            }
            if (npc.type == NPCID.UndeadMiner) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlowWrist>(), 2));
            }
            if (NPCID.Sets.BelongsToInvasionPirate[npc.type]) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Dealmaker>(), 50));
            }
            if (CelestialCreatures.Contains(npc.type)) {
                LeadingConditionRule killedByRingCondition = new LeadingConditionRule(new KilledByNoelleRingCondition());
                killedByRingCondition.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GlacialFragment>(), 1, 1, 6));
                npcLoot.Add(killedByRingCondition);
            }
        }

        private void AddLootToBoss<T>(NPCLoot npcLoot) where T : ModItem {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<T>()));
            npcLoot.Add(notExpertRule);
        }

        public override void OnKill(NPC npc) {
            if ((!npc.boss) && npc.lastInteraction < 255) {
                Player lastPlayer = Main.player[npc.lastInteraction];
                float bonusLootChance = EquipmentEffectPlayer.Get(lastPlayer).additionalLootChance;
                if (Main.rand.NextFloat() < bonusLootChance) {
                    MethodInfo? dropItems = typeof(NPC).GetMethod("NPCLoot_DropItems", BindingFlags.NonPublic | BindingFlags.Instance);
                    dropItems?.Invoke(npc, [lastPlayer]);

                    MethodInfo? dropMoney = typeof(NPC).GetMethod("NPCLoot_DropMoney", BindingFlags.NonPublic | BindingFlags.Instance);
                    dropMoney?.Invoke(npc, [lastPlayer]);
                }
            }
        }

    }

    public class KilledByNoelleRingCondition : IItemDropRuleCondition {
        public bool CanDrop(DropAttemptInfo info) {
            //Console.WriteLine("Checking condition... " + (ContentSamples.ItemsByType[info.item].ModItem is AbstractNoelleRing) + " || " + info.item);
            //return ContentSamples.ItemsByType[info.item].ModItem is AbstractNoelleRing;
            return info.player?.HeldItem?.ModItem is AbstractNoelleRing;
        }

        public bool CanShowItemDropInUI() {
            return false;
        }

        public string GetConditionDescription() {
            return "If killed by any of Noelle's Rings";
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
