using Delterra.Content.NPCs;
using Delterra.Systems.Config;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Scenes {
    internal class RalseiTownScene : ModSceneEffect {

        public const string MUSIC_PATH = "Assets/Music/castletown";

        public override int Music => MusicLoader.GetMusicSlot(Mod, MUSIC_PATH);

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        // We have to make the radius of this a couple pixels longer so that the regular town theme doesn't start playing before this does!
        public override bool IsSceneEffectActive(Player player) {
            if (!ClientConfig.Get().NaturallyPlayCastleTownMusic) {
                return false;
            }
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

    }
}
