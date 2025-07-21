using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Delterra.Systems;
using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace Delterra.Content.Items.Spells.Scarves {

    public abstract class AbstractRalseiScarf : ModItem, ITensionConsumingItem {

        public abstract int Heal { get; }
        public virtual double MinTPCost => 32;
        public virtual double MaxTPCost => MinTPCost*2;
        public override string LocalizationCategory => base.LocalizationCategory + ".Weapons.RalseiScarves";

        public override void SetDefaults() {
            //Item.UseSound = MySoundStyles.Heal;
            Item.useStyle = ItemUseStyleID.HiddenAnimation;
            Item.useTurn = true;
            Item.useAnimation = Item.useTime = 18;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 0);
            Item.healLife = Heal;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
            Item.shootSpeed = 1;
            // Shoot stats and damage and such should be applied on each individual scarf
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                if (player.altFunctionUse == 2) {
                    EquipmentEffectPlayer.Get(player).currentScarfUses = 0;
                    SoundEngine.PlaySound(MySoundStyles.Heal);
                } else {
                    EquipmentEffectPlayer.Get(player).currentScarfUses++;
                    SoundEngine.PlaySound(SoundID.Item1);
                }
            }
            return true;
        }

        public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            position += GetShootOffset(player);
        }

        public override bool CanShoot(Player player) {
            return player.altFunctionUse != 2;
        }

        double ITensionConsumingItem.GetBaseTPCost(Player player) {
            EquipmentEffectPlayer modPlayer = EquipmentEffectPlayer.Get(player);
            if (modPlayer.currentScarfType != Type) {
                return MaxTPCost;
            }
            return Math.Max(MinTPCost, MaxTPCost - modPlayer.currentScarfUses * 4);
        }

        bool ITensionConsumingItem.IsTPConsumedOnUse(Player player) {
            return player.altFunctionUse == 2;
        }

        public override void GetHealLife(Player player, bool quickHeal, ref int healValue) {
            if (player.altFunctionUse == 2 || (Main.HoverItem != null && (!Main.HoverItem.IsAir))) {
                return;
            }
            healValue = 0;
        }


        public static Vector2 GetShootOffset(Player player) {
            return new(0, -player.gravDir * 4);
        }

    }
}
