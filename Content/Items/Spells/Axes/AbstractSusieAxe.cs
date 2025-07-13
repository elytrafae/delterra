using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Delterra.Systems;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Delterra.Content.Items.Spells.Axes {
    public abstract class AbstractSusieAxe : ModItem, ITensionConsumingItem {

        public LocalizedText RudeBusterTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".RudeBusterTooltip");
        public override LocalizedText Tooltip => RudeBusterTooltip.WithFormatArgs(base.Tooltip);
        public virtual int RudeBusterCost => GrazingPlayer.GetTPForPercent(50);

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<RudeBuster>();
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            GrazingPlayer.Get(player).TP -= GlobalTensionConsumingItem.GetTensionCost(Item, player);
            return true;
        }

        public override float UseSpeedMultiplier(Player player) {
            return player.altFunctionUse == 2 ? 3f : 1f;
        }

        int ITensionConsumingItem.GetBaseTPCost(Player player) {
            return RudeBusterCost;
        }

        bool ITensionConsumingItem.IsTPConsumedOnUse(Player player) {
            return false;
        }

    }
}
