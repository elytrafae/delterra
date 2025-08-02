using Delterra.Systems;
using Delterra.Systems.TPSources;
using FaeLibrary.API;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    public class Zapped : ModBuff, IFaeBuff {

        public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            if (Main.rand.NextBool(3)) {
                Dust.NewDust(player.position, player.width, player.height, DustID.MartianSaucerSpark);
            }
        }

        public override void Update(NPC npc, ref int buffIndex) {
            if (Main.rand.NextBool(2)) {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.MartianSaucerSpark);
            }
        }

        void IFaeBuff.UpdateBadLifeRegen(Player player, ref int buffIndex) {
            player.lifeRegen -= 20;
        }

        void IFaeBuff.UpdateNPCLifeRegen(NPC npc, ref int buffIndex) {
            npc.lifeRegen -= 60;
        }

    }
}
