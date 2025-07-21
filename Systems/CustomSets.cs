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

        public static float[] NPCTPGainRate = ProjectileID.Sets.Factory.CreateNamedSet("NPCTPGainRate")
            .Description("The effective TP gain of NPCs. 1f means normal TP gain, 0f or less means TP gain is disabled.")
            .RegisterFloatSet(1f);

    }
}
