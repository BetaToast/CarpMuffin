using CarpMuffin.UserInterfaces;
using CarpMuffin.UserInterfaces.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Screens.Title
{
    public class TitleScreen
        : UserInterfaceScreen
    {
        public override void LoadUserInterfaceContent()
        {
            var viewport = Engine.Graphics.GraphicsDevice.Viewport;

            UseSoftwareMouse = true;
            Skin = UserInterfaceSkin.Load(Content, "ui/sheet.skin");
            Font = Content.Load<SpriteFont>("fonts/default");

            var overlay = AddControl<ScreenOverlay>();
            overlay.Tint = new Color(63, 124, 182);

            var menuPanel = AddControl<Panel>();
            menuPanel.ReloadPartsWith(UserInterfacePartNames.MetalPanelRedcorner);
            menuPanel.Size = new Vector2(268f, 210f);
            menuPanel.Position = new Vector2((viewport.Width / 2) - (menuPanel.Size.X / 2), (viewport.Height / 2) - (menuPanel.Size.Y / 2));

            var menuLabel = menuPanel.AddChild<Label>();
            menuLabel.Text = "CarpMuffin Demo";
            menuLabel.Tint = Color.White;
            menuLabel.Position = new Vector2(8f, 6f);

            var partsLabel = menuPanel.AddChild<Label>();
            partsLabel.Text = "Select a demo";
            partsLabel.Tint = Color.DimGray;
            partsLabel.HasShadow = false;
            partsLabel.Position = new Vector2(25f, 50f);

            var plate = menuPanel.AddChild<Panel>();
            plate.ReloadPartsWith(UserInterfacePartNames.MetalPanelPlate);
            plate.Position = new Vector2(12f, 84f);
            plate.Size = new Vector2(240f, 116f);
            plate.IsDraggable = false;

            var demoButton1 = menuPanel.AddChild<Button>();
            demoButton1.Position = new Vector2(30f, 100f);
            demoButton1.Size = new Vector2(204f, 26f);
            demoButton1.Text = "Demo 1";
            demoButton1.OnClick = b =>
            {
                
            };

            var demoButton2 = menuPanel.AddChild<Button>();
            demoButton2.Position = new Vector2(30f, 132f);
            demoButton2.Size = new Vector2(204f, 26f);
            demoButton2.Text = "Demo 2";
            demoButton2.OnClick = b =>
            {
                
            };

            var demoButton3 = menuPanel.AddChild<Button>();
            demoButton3.Position = new Vector2(30f, 164f);
            demoButton3.Size = new Vector2(204f, 26f);
            demoButton3.Text = "Demo 3";
            demoButton3.OnClick = b =>
            {
                
            };
        }
    }
}