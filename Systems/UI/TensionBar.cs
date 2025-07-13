using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using System;
using Delterra.Content;

namespace Delterra.Systems.UI {
    
    internal class TensionBar : UIState {
        private UIElement area;
        private UIImage logo;
        private UIImage backing;
        private UIImageWithFrame background_filling;
        private UIImageWithFrame main_filling;
        private UIImageWithFrame hover_filling;
        private UIImage marker;
        private UIImage cover;
        private UITensionText text;

        private int currentDisplayedTP = 0;
        private int currentDisplayedRedTP = 0;
        private int hoverBlinkTime = 0;
        private const int MAX_TP_SCROLL_SPEED_PER_TICK = 70;
        private const int MAX_RED_TP_SCROLL_SPEED_PER_TICK = 45;

        private Color BackgroundFillingColor = new Color(255, 0, 0);
        private Color MainFillingColor = new Color(255, 127, 39);
        private Color HoverFillingColor = new Color(255, 255, 255);
        private Color HoverFillingNotEnoughColor = Color.Black * 0.4f;

        private const string SpritePrefix = nameof(Delterra) + "/Assets/MiscSprites/";

        public override void OnInitialize() {
            area = new UIElement();
            area.Width.Set(60, 0f);
            area.Height.Set(200, 0f);
            area.Top.Set(17, 0f);
            area.Left.Set(-area.Width.Pixels - 300, 1f);

            logo = new UIImage(ModContent.Request<Texture2D>(SpritePrefix + "tension_logo"));
            logo.Left.Set(0, 0f);
            logo.Top.Set(30, 0f);
            logo.Width.Set(22, 0f);
            logo.Height.Set(44, 0f);
            area.Append(logo);

            backing = new UIImage(ModContent.Request<Texture2D>(SpritePrefix + "tension_backing"));
            backing.Left.Set(35, 0f);
            backing.Top.Set(0, 0f);
            backing.Width.Set(25, 0f);
            backing.Height.Set(196, 0f);
            area.Append(backing);

            background_filling = new UIImageWithFrame(ModContent.Request<Texture2D>(SpritePrefix + "tension_filling"));
            background_filling.Color = BackgroundFillingColor;
            background_filling.Left.Set(35, 0f);
            background_filling.Top.Set(0, 0f);
            background_filling.Width.Set(25, 0f);
            background_filling.Height.Set(196, 0f);
            area.Append(background_filling);

            main_filling = new UIImageWithFrame(ModContent.Request<Texture2D>(SpritePrefix + "tension_filling"));
            main_filling.Color = MainFillingColor;
            main_filling.Left.Set(35, 0f);
            main_filling.Top.Set(0, 0f);
            main_filling.Width.Set(25, 0f);
            main_filling.Height.Set(196, 0f);
            area.Append(main_filling);

            hover_filling = new UIImageWithFrame(ModContent.Request<Texture2D>(SpritePrefix + "tension_filling"));
            hover_filling.Color = Color.Transparent;
            hover_filling.Left.Set(35, 0f);
            hover_filling.Top.Set(0, 0f);
            hover_filling.Width.Set(25, 0f);
            hover_filling.Height.Set(196, 0f);
            area.Append(hover_filling);

            marker = new UIImage(ModContent.Request<Texture2D>(SpritePrefix + "tension_marker"));
            marker.Left.Set(38, 0f);
            marker.Top.Set(30, 0f);
            marker.Width.Set(19, 0f);
            marker.Height.Set(2, 0f);
            area.Append(marker);

            cover = new UIImage(ModContent.Request<Texture2D>(SpritePrefix + "tension_cover_of_shame"));
            cover.Color = HoverFillingColor;
            cover.Left.Set(35, 0f);
            cover.Top.Set(0, 0f);
            cover.Width.Set(25, 0f);
            cover.Height.Set(196, 0f);
            area.Append(cover);

            text = new UITensionText(ModContent.Request<Texture2D>(SpritePrefix + "tension_font"));
            text.Width.Set(30, 0f);
            text.Height.Set(80, 0f);
            text.Top.Set(80, 0f);
            text.Left.Set(2, 0f);
            area.Append(text);

            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            // Add draw condition here, if ever relevant
            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime) {
            GrazingPlayer modPlayer = GrazingPlayer.Get(Main.LocalPlayer);
            if (currentDisplayedTP != modPlayer.TP) {
                if (Math.Abs(currentDisplayedTP - modPlayer.TP) < MAX_TP_SCROLL_SPEED_PER_TICK) {
                    currentDisplayedTP = modPlayer.TP;
                } else if (currentDisplayedTP < modPlayer.TP) {
                    currentDisplayedTP += MAX_TP_SCROLL_SPEED_PER_TICK;
                } else {
                    currentDisplayedTP -= MAX_TP_SCROLL_SPEED_PER_TICK;
                }
            }
            if (currentDisplayedRedTP != currentDisplayedTP) {
                if (currentDisplayedRedTP < currentDisplayedTP) {
                    currentDisplayedRedTP = currentDisplayedTP;
                } else {
                    currentDisplayedRedTP -= MAX_RED_TP_SCROLL_SPEED_PER_TICK;
                }
            }
            // TODO: Add white thingy when the TP bar fills up quickly
            UpdateBar(background_filling, currentDisplayedRedTP);
            UpdateBar(main_filling, currentDisplayedTP);
            if (currentDisplayedTP < GrazingPlayer.MAXTP-100) {
                marker.Color = Color.White;
                marker.Top.Set(196 - (int)((float)currentDisplayedTP / GrazingPlayer.MAXTP * 196) - 2, 0f);
            } else {
                marker.Color = Color.Transparent;
            }

            text.Number = (int)modPlayer.TPPercent;
            
            hoverBlinkTime += 3;
            if (hoverBlinkTime >= 360) {
                hoverBlinkTime -= 360;
            }
            int heldTPCost = HeldItemTPCost(Main.LocalPlayer);
            if (heldTPCost > 0) {
                if (heldTPCost > modPlayer.TP) {
                    UpdateBar(hover_filling, currentDisplayedTP, 0);
                    hover_filling.Color = HoverFillingNotEnoughColor;
                } else {
                    UpdateBar(hover_filling, modPlayer.TP, modPlayer.TP - heldTPCost);
                    hover_filling.Color = HoverFillingColor * (float)(Math.Sin(MathHelper.ToRadians(hoverBlinkTime)) / 2 + 0.5);
                }
            } else {
                hover_filling.Color = Color.Transparent;
            }
            base.Update(gameTime);
        }

        private int HeldItemTPCost(Player player) {
            if (player.HeldItem == null || player.HeldItem.IsAir) {
                return 0;
            }
            if (player.HeldItem.ModItem == null) {
                return 0;
            }
            if (player.HeldItem.ModItem is ITensionConsumingItem tensionItem) {
                return tensionItem.GetBaseTPCost(player);
            }
            return 0;
        }

        private void UpdateBar(UIImageWithFrame image, int topTP, int bottomTP = 0) {
            int pixelsOffBottom = (int)((float)bottomTP / GrazingPlayer.MAXTP * 196);
            int pixelsOffTop = 196 - (int)((float)topTP / GrazingPlayer.MAXTP * 196);
            int height = 196 - pixelsOffBottom - pixelsOffTop;
            int y = pixelsOffTop;

            image.sourceRectangle = new Rectangle(0, y, 25, height);
            image.Top.Set(y, 0f);
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class ExampleResourceUISystem : ModSystem {
        private UserInterface TensionBarUserInterface;

        internal TensionBar TensionBar;

        public override void Load() {
            TensionBar = new();
            TensionBarUserInterface = new();
            TensionBarUserInterface.SetState(TensionBar);
        }

        public override void UpdateUI(GameTime gameTime) {
            TensionBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1) {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    nameof(Delterra) + ": Tension Bar",
                    delegate {
                        TensionBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
