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
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Buffs {
    internal class Trance : ModBuff, IFaeBuff {

        public override LocalizedText Description => base.Description;

        public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetDamage(DamageClass.Magic) *= 1.5f;
            player.GetCritChance(DamageClass.Magic) += 100;
            GrazingPlayer.Get(player).GainTP(0.015f, new TPGainMiscBuffContext(ModContent.BuffType<Trance>()));
            if (Main.rand.NextBool(3)) {
                Dust.NewDustPerfect(player.Center, DustID.Blood, new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0f)));
            }
        }

        void IFaeBuff.UpdateBadLifeRegen(Player player, ref int buffIndex) {
            player.lifeRegen -= 40;
        }

    }
}
