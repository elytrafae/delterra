using Delterra.Content.Projectiles.Weapons.Melee.RudeBuster;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Weapons.Melee.Axes {
    internal class ToxicAx : AbstractSusieAxe {

        public override void SetDefaults() {
            base.SetDefaults();
            Item.damage = 190;
            Item.knockBack = 3f;
            Item.useTime = Item.useAnimation = 30;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.scale = 1.25f;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<ToxicBuster>();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(BuffID.Venom, 2 * 60);
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo) {
            target.AddBuff(BuffID.Venom, 2 * 60);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<AutoAx>()
                .AddIngredient<MaliusHammer>()
                .AddIngredient(ItemID.VialofVenom, 50)
                .AddTile(TileID.AdamantiteForge)
                .Register();
        }

    }
}
