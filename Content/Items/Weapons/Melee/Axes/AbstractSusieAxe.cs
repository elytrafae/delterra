using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Systems;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster;

namespace Delterra.Content.Items.Weapons.Melee.Axes {
    public abstract class AbstractSusieAxe : ModItem, ITensionConsumingItem {

        public LocalizedText RudeBusterTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".RudeBusterTooltip");
        public override LocalizedText Tooltip => RudeBusterTooltip.WithFormatArgs(base.Tooltip);
        public virtual double RudeBusterCost => 50;
        public override string LocalizationCategory => base.LocalizationCategory + ".Weapons.Melee.SusieAxes";

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            //Item.shoot = ModContent.ProjectileType<AbstractRudeBuster>();
            Item.shootSpeed = 9;
        }

        public override bool CanUseItem(Player player) {
            return true;
        }

        public override bool AltFunctionUse(Player player) {
            return GrazingPlayer.Get(player).TP >= GlobalTensionConsumingItem.GetTensionCost(Item, player);
        }

        public override bool CanShoot(Player player) {
            return player.altFunctionUse == 2;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            damage *= 2;
        }

        public override float UseSpeedMultiplier(Player player) {
            return player.altFunctionUse == 2 ? 3f : 1f;
        }

        double ITensionConsumingItem.GetBaseTPCost(Player player) {
            return RudeBusterCost;
        }

        bool ITensionConsumingItem.IsTPConsumedOnUse(Player player) {
            return player.altFunctionUse == 2;
        }

    }
}
