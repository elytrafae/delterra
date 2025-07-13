using Delterra.Content.Projectiles;
using Delterra.Systems;
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

namespace Delterra.Content.Items.Spells.Rings {
    public abstract class AbstractNoelleRing : ModItem, ITensionConsumingItem {

        public LocalizedText IceShockTooltip => Language.GetOrRegister("Mods." + nameof(Delterra) + ".IceShockTooltip");
        public override LocalizedText Tooltip => IceShockTooltip.WithFormatArgs(base.Tooltip);

        public virtual int IceShockCost => GrazingPlayer.GetTPForPercent(16);
        public virtual int SnowGraveCost => GrazingPlayer.GetTPForPercent(200);
        private bool IsSnowGraveUnlocked => SnowGraveCost <= GrazingPlayer.MAXTP;

        public override void SetDefaults() {
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.mana = 20;
            Item.noUseGraphic = true;
            //Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<IceShockHM>();
            Item.shootSpeed = 0;
            Item.useTime = (Item.useAnimation = 30);
            Item.knockBack = 5f;
        }

        public override bool AltFunctionUse(Player player) {
            return IsSnowGraveUnlocked;
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
            if (player.altFunctionUse == 2) {
                mult *= 10;
            }
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            position = Main.MouseWorld;
            if (IsSnowGraveUnlocked && player.altFunctionUse == 2) {
                type = ModContent.ProjectileType<SnowGrave>();
                position.Y = player.Center.Y + Main.screenHeight/2 + 50;
                damage *= 3;
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage) {
            if (EquipmentEffectPlayer.Get(player).secretRingBuff) {
                damage *= 1.2f;
            }
        }

        public override bool? UseItem(Player player) {
            Vector2 baseDustPos = player.Center + new Vector2(player.direction * 10, -10);
            Vector2 baseDustVelocity = new Vector2(player.direction * 2f, -1f);
            for (int i = 0; i < 5; i++) {
                Dust.NewDust(baseDustPos, 5, 5, DustID.Snow, baseDustVelocity.X, baseDustVelocity.Y, 0, default, 0.8f);
            }
            return true;
        }

        int ITensionConsumingItem.GetBaseTPCost(Player player) {
            if (IsSnowGraveUnlocked && player.altFunctionUse == 2) {
                return SnowGraveCost;
            }
            return IceShockCost;
        }

        

    }
}
