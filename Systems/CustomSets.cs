using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Systems {

    [ReinitializeDuringResizeArrays]
    public class CustomSets {

        public static float[] ProjectileTPGainRate = ProjectileID.Sets.Factory.CreateNamedSet("ProjectileTPGainRate")
            .Description("The effective TP gain of projectiles. 1f means normal TP gain, 0f or less means TP gain is disabled.")
            .RegisterFloatSet(1f, 
                ProjectileID.AshBallFalling, 0f, 
                ProjectileID.CrimsandBallFalling, 0f,
                ProjectileID.EbonsandBallFalling, 0f,
                ProjectileID.PearlSandBallFalling, 0f,
                ProjectileID.SandBallFalling, 0f
            );

        public static float[] NPCTPGainRate = NPCID.Sets.Factory.CreateNamedSet("NPCTPGainRate")
            .Description("The effective TP gain of NPCs. 1f means normal TP gain, 0f or less means TP gain is disabled.")
            .RegisterFloatSet(1f);

        public static bool[] WizardRobes = ItemID.Sets.Factory.CreateNamedSet("GemRobes")
            .Description("true for every item that can be used in the chest slot for the \"Wizard Set\" (https://terraria.wiki.gg/wiki/Wizard_set)")
            .RegisterBoolSet(false, 
                ItemID.AmberRobe, 
                ItemID.AmethystRobe, 
                ItemID.DiamondRobe, 
                ItemID.EmeraldRobe, 
                ItemID.RubyRobe, 
                ItemID.SapphireRobe, 
                ItemID.TopazRobe, 
                ItemID.GypsyRobe
            );

    }
}
