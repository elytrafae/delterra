using Delterra.Systems;
using FaeLibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    internal class Trance : ModBuff, IFaeBuff {

        public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetDamage(DamageClass.Magic) *= 1.5f;
            player.GetCritChance(DamageClass.Magic) += 100;
            GrazingPlayer.Get(player).TP += 1;
        }

        void IFaeBuff.UpdateBadLifeRegen(Player player, ref int buffIndex) {
            if (player.lifeRegen > 0) {
                player.lifeRegen = 0;
            }
            player.lifeRegen -= 10;
        }

    }
}
