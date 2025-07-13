using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace Delterra.Content.Items.Spells.Axes {
    internal class AbsorbAx : AbstractSusieAxe {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 120;
            Item.knockBack = 3f;
            Item.useTime = (Item.useAnimation = 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Terraria.Item.buyPrice(0, 0, 30, 0);
            Item.scale = 1f;
            Item.shootSpeed = 11;
            Item.scale = 1.1f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
            if (!target.canGhostHeal) {
                return;
            }
            int healthToRestore = Math.Min(damageDone / 20, (int)player.lifeSteal);
            if (healthToRestore > 0) {
                player.lifeSteal -= healthToRestore;
                player.Heal(healthToRestore);
            }
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo) {
            int healthToRestore = Math.Min(hurtInfo.Damage / 20, (int)player.lifeSteal);
            if (healthToRestore > 0) {
                player.lifeSteal -= healthToRestore;
                player.Heal(healthToRestore);
            }
        }

    }
}
