using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delterra.Content.Projectiles;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Spells.Axes {
    public class ManeAx : ModItem, ITensionConsumingItem {
        

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 30;
            Item.useTime = (Item.useAnimation = 50);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<RudeBuster>();
            Item.shootSpeed = 7;
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
            damage *= 3;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            GrazingPlayer.Get(player).TP -= GlobalTensionConsumingItem.GetTensionCost(Item, player);
            return true;
        }

        public override float UseSpeedMultiplier(Player player) {
            return player.altFunctionUse == 2 ? 3f : 1f;
        }

        int ITensionConsumingItem.GetBaseTPCost(Player player) {
            return GrazingPlayer.GetTPForPercent(50);
        }

        bool ITensionConsumingItem.IsTPConsumedOnUse(Player player) {
            return false;
        }

        

    }
}
