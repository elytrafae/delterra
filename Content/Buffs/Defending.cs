using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    public class Defending : ModBuff {

        private int DAMAGE_REDUCTION = 15;
        private int DEFENSE = 5;
        private int TP_PER_SECOND = GrazingPlayer.GetTPForPercent(2) + 4;

        public override LocalizedText Description => base.Description.WithFormatArgs(DAMAGE_REDUCTION, DEFENSE, TP_PER_SECOND/GrazingPlayer.TP_PER_PERCENT);

        public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.endurance += (DAMAGE_REDUCTION/100f);
            player.statDefense += DEFENSE;
            GrazingPlayer.Get(player).TP += (TP_PER_SECOND / 60);
        }

    }
}
