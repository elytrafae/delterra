using Delterra.Systems;
using Delterra.Systems.TPSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    public class Defending : ModBuff {

        private int DAMAGE_REDUCTION = 15;
        private int DEFENSE = 5;
        private double TP_PER_SECOND = 2;

        public override LocalizedText Description => base.Description.WithFormatArgs(DAMAGE_REDUCTION, DEFENSE, TP_PER_SECOND);

        public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.endurance += (DAMAGE_REDUCTION/100f);
            player.statDefense += DEFENSE;
            GrazingPlayer.Get(player).GainTP((float)TP_PER_SECOND / 60, new TPGainDefendContext(ModContent.BuffType<Defending>()));
        }

    }
}
