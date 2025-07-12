using Delterra.Content.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Scenes {
    internal class RalseiTownScene : ModSceneEffect {

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/castletown");

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        // We have to make the radius of this a couple pixels longer so that the regular town theme doesn't start playing before this does!
        public override bool IsSceneEffectActive(Player player) {
            float npcSlots = 0;
            bool ralFound = false;
            int townWidth = NPC.sWidth + 10;
            int townHeight = NPC.sHeight + 10;
            foreach (NPC npc in Main.ActiveNPCs) {
                if (npc.townNPC) {
                    Rectangle rect = new Rectangle((int)(npc.position.X + (npc.width / 2) - townWidth), (int)(npc.position.Y + (npc.height / 2) - townHeight), townWidth * 2, townHeight * 2);
                    if (rect.Intersects(player.Hitbox)) {
                        npcSlots += npc.npcSlots;
                        if (npc.type == ModContent.NPCType<Ralsei>()) {
                            ralFound = true;
                        }
                    }
                }
                
            }
            return npcSlots > 2f && ralFound;
        }

        /*
        public override bool IsSceneEffectActive(Player player) {
            if (player.townNPCs <= 2f) {
                return false;
            }
            int ralIndex = NPC.FindFirstNPC(ModContent.NPCType<Ralsei>());
            if (ralIndex < 0) {
                return false;
            }
            NPC ral = Main.npc[ralIndex];
            int townWidth = NPC.sWidth + 20;
            int townHeight = NPC.sHeight + 20;
            Rectangle rect = new Rectangle((int)(ral.position.X + (ral.width / 2) - townWidth), (int)(ral.position.Y + (ral.height / 2) - townHeight), townWidth * 2, townHeight * 2);
            return rect.Intersects(player.Hitbox);
        }
        */

    }
}
