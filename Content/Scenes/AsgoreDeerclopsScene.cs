using Delterra.Content.Mounts;
using Delterra.Content.NPCs;
using Delterra.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Scenes {
    internal class AsgoreDeerclopsScene : ModSceneEffect {

        public const string MUSIC_PATH = "Assets/Music/asgore_trap_remix";

        public override int Music => MusicLoader.GetMusicSlot(Mod, MUSIC_PATH);

        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;

        public override float GetWeight(Player player) {
            return 1f;
        }

        // We have to make the radius of this a couple pixels longer so that the regular town theme doesn't start playing before this does!
        public override bool IsSceneEffectActive(Player player) {
            return EquipmentEffectPlayer.Get(player).asgoreTruckGloryTime > 0 || (player.mount.Type == ModContent.MountType<TheKingsChariot>() && NPC.FindFirstNPC(NPCID.Deerclops) >= 0);
        }

    }
}
