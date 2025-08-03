using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Delterra.Content.Emotes;
using Microsoft.Xna.Framework;
using Delterra.Content.Items.Accessories;
using Delterra.Content.Items;
using Delterra.Content.Items.Spells.HealPrayer;
using Delterra.Content.Items.Weapons.Melee.Axes;
using Delterra.Content.Items.Consumables.TensionRestore;
using Delterra.Content.Items.Placables;

namespace Delterra.Content.NPCs {

    [AutoloadHead]
    public class Ralsei : ModNPC {
        public const string ShopName = "Shop";

        private static int ShimmerHeadIndex;
        private static Profiles.StackedNPCProfile NPCProfile;

        public override void Load() {
            // Adds our Shimmer Head to the NPCHeadLoader.
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
        }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 2; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 25; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
            NPCID.Sets.ShimmerTownTransform[Type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

            // Connects this NPC with a custom emote.
            // This makes it when the NPC is in the world, other NPCs will "talk about him".
            // By setting this you don't have to override the PickEmote method for the emote to appear.
            NPCID.Sets.FaceEmote[Type] = ModContent.EmoteBubbleType<RalseiEmote>();

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Like)
            // TODO: Add NPC happiness data!
            ; // < Mind the semicolon!

            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
            NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
                new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
            );

            ContentSamples.NpcBestiaryRarityStars[Type] = 2; // We can override the default bestiary star count calculation by setting this.
        }

        public override void SetDefaults() {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange([
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				// Sets your NPC's flavor text in the bestiary. (use localization keys)
				new FlavorTextBestiaryInfoElement("Mods." + nameof(Delterra) + ".Bestiary.Ralsei")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit) {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++) {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
            }

            // Create gore when the NPC is killed.
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0) {
                // Ralsei does not use traditional gore. Maybe a Scarf and Hat gore will be added later, but for now I'll just use smoke + party hat
                int hatGore = NPC.GetPartyHatGore();

                // Spawn the gores. The positions of the arms and legs are lowered for a more natural look.
                if (hatGore > 0) {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
                }

                for (int i = 0; i < 6; i++) {
                    Vector2 pos = NPC.position + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Vector2 vel = NPC.velocity + new Vector2(Main.rand.NextFloat()-0.5f, Main.rand.NextFloat()-0.20f);
                    Gore.NewGore(NPC.GetSource_Death(), pos, NPC.velocity, 11 + Main.rand.Next(3));
                }
                
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs) { // Requirements for the town NPC to spawn.
            // Ralsei spawns as soon as he has valid housing. He isn't picky
            return true;
        }

        public override ITownNPCProfile TownNPCProfile() {
            return NPCProfile;
        }

        public override List<string> SetNPCNameList() {
            return new List<string>() {
                "Ralsei"
            };
        }

        public override string GetChat() {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0) {
                chat.Add(this.GetLocalization("Dialogue.PartyGirl").Format(Main.npc[partyGirl].GivenName));
            }

            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(this.GetLocalizedValue("Dialogue.Random1"));
            chat.Add(this.GetLocalizedValue("Dialogue.Random2"));
            chat.Add(this.GetLocalizedValue("Dialogue.Random3"));
            chat.Add(this.GetLocalizedValue("Dialogue.Random4"), 0.7f);
            chat.Add(this.GetLocalizedValue("Dialogue.Random5"), 0.5f);

            string partytext = "";

            if (Condition.BirthdayParty.IsMet()) {
                partytext = this.GetLocalizedValue("Dialogue.PartyNoItems");
                if (Main.hardMode) {
                    partytext = this.GetLocalizedValue("Dialogue.PartyCakeTea");
                    
                }
                chat.Add(partytext, 6);
            }

            string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

            if (chosenChat == partytext && Main.hardMode) {
                Main.npcChatCornerItem = Main.rand.NextBool(2) ? ItemID.SliceOfCake : ItemID.Teacup;
            }

            return chosenChat;
        }

        public override void SetChatButtons(ref string button, ref string button2) { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28"); // This is the key to the word "Shop"
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop) {
            if (firstButton) {
                shop = ShopName; // Name of the shop tab we want to open.
            }
        }

        // Not completely finished, but below is what the NPC will sell
        public override void AddShops() {
            var npcShop = new NPCShop(Type, ShopName)
                .Add<DefensiveCharm>()
                .Add<WhiteRibbon>()
                .Add<PinkRibbon>(Condition.DownedEowOrBoc)
                .Add<BlueRibbon>(Condition.DownedMechBossAny)
                .Add<TensionBit>()
                .Add<TensionGem>(Condition.Hardmode)
                .Add<TensionMax>(Condition.DownedCultist)
                .Add<HealPrayer1>(Condition.DownedEyeOfCthulhu)
                .Add<HealPrayer2>(Condition.DownedSkeletron)
                .Add<HealPrayer3>(Condition.DownedPlantera)
                .Add<HealPrayer4>(Condition.DownedCultist)
                .Add<MaliusHammer>(Condition.DownedSkeletron)
                .Add<AbsorbAx>(Condition.Eclipse)
                .Add(ItemID.SliceOfCake, Condition.BirthdayParty, Condition.Hardmode)
                .Add(ItemID.Teacup, Condition.BirthdayParty, Condition.Hardmode)
                .Add<CastleTownMusicBoxItem>();

            npcShop.Register();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WhiteRibbon>(), 10));
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered. Return toKingStatue for only King Statues. Return !toKingStatue for only Queen Statues. Return true for both.
        public override bool CanGoToStatue(bool toKingStatue) => toKingStatue;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 22;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 30;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ProjectileID.AmberBolt;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
            multiplier = 12f;
            randomOffset = 2f;
            // SparklingBall is not affected by gravity, so gravityCorrection is left alone.
        }

        // Let the NPC "talk about" minion boss
        public override int? PickEmote(Player closestPlayer, List<int> emoteList, WorldUIAnchor otherAnchor) {
            int type = EmoteID.EmotionLove;

            // Make the selection more likely by adding it to the list multiple times
            for (int i = 0; i < 3; i++) {
                emoteList.Add(type);
            }

            // Use this or return null if you don't want to override the emote selection totally
            return base.PickEmote(closestPlayer, emoteList, otherAnchor);
        }
    }

}
