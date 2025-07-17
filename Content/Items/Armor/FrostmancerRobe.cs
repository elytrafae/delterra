using Delterra.Content.Buffs;
using Delterra.Systems;
using Delterra.Systems.TPSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.Items.Armor {

    [AutoloadEquip(EquipType.Body)]
    internal class FrostmancerRobe : ModItem {

        public static readonly int MagicDamageBonusPercent = 20;
        public static readonly int MagicCritBonus = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBonusPercent, MagicCritBonus);

        public override void Load() {
            // The code below runs only if we're not loading on a server
            if (Main.netMode == NetmodeID.Server) {
                return;
            }

            // By passing this (the ModItem) into the item parameter we can reference it later in GetEquipSlot with just the item's name
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);

            /* Here is example code for supporting a female-specifig legs equip texture. See SetMatch as well.
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}_Female", EquipType.Legs, this, Name + "_Female");
			*/
        }

        public override void SetStaticDefaults() {
            // HidesHands defaults to true which we don't want.
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }

        public override void SetDefaults() {
            Item.width = 18;
            Item.height = 14;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 14);
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player) {
            GrazingPlayer grazePlayer = GrazingPlayer.Get(player);
            grazePlayer.TPChangeStats[TPGainType.DEFEND] += 1f;
            grazePlayer.TPChangeStats[TPGainType.ATTACK] += 1f;
            player.GetDamage(DamageClass.Magic) += (MagicDamageBonusPercent / 100f);
            player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
            player.iceSkate = true;
        }

        public override void SetMatch(bool male, ref int equipSlot, ref bool robes) {
            // By changing the equipSlot to the leg equip texture slot, the leg texture will now be drawn on the player
            // We're changing the leg slot so we set this to true
            robes = true;
            // Here we can get the equip slot by name since we referenced the item when adding the texture
            // You can also cache the equip slot in a variable when you add it so this way you don't have to call GetEquipSlot
            equipSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);

            /* Here is example code for supporting a female-specifig legs equip texture. See Load as well.
			if (!male) {
				equipSlot = EquipLoader.GetEquipSlot(Mod, Name + "_Female", EquipType.Legs);
			}
			*/
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 15)
                .AddIngredient<GlacialFragment>(15)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

    }
}
