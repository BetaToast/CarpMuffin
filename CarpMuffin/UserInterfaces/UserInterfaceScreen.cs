using System;
using System.Collections.Generic;
using System.Linq;
using CarpMuffin.Screens;
using CarpMuffin.UserInterfaces.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces
{
    /// <summary>
    /// The base screen for a user interface
    /// </summary>
    public abstract class UserInterfaceScreen
        : Screen
    {
        #region Fields / Properties

        public Rectangle MousePart { get; set; }

        public List<IControl> Controls { get; set; }
        public UserInterfaceSkin Skin { get; set; }
        public SpriteFont Font { get; set; }
        public bool UseSoftwareMouse { get; set; }

        #endregion

        protected UserInterfaceScreen()
        {
            Controls = new List<IControl>();
            MousePart = Rectangle.Empty;
        }

        public override void LoadContent(ContentManager content)
        {
            LoadUserInterfaceContent();
        }

        public abstract void LoadUserInterfaceContent();

        public override void Update(GameTime gameTime)
        {
            if (UseSoftwareMouse && Engine.Instance.IsMouseVisible)
            {
                Engine.Instance.IsMouseVisible = false;
            }
            if (UseSoftwareMouse && MousePart == Rectangle.Empty)
            {
                MousePart = Skin[UserInterfacePartNames.CursorPointer3dShadow];
            }
            UpdateUserInterface(gameTime);
        }

        public virtual void UpdateUserInterface(GameTime gameTime)
        {
            foreach (var control in Controls.Where(control => control.IsEnabled))
            {
                control.UpdateInput(Input);
                control.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawUserInterface(gameTime);
        }

        public virtual void DrawUserInterface(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);

            foreach (var control in Controls.Where(control => control.IsVisible))
            {
                control.Draw(gameTime);
            }

            if (UseSoftwareMouse && MousePart != Rectangle.Empty)
            {
                SpriteBatch.Draw(Skin.Texture, Input.Mouse.CurrentPosition, MousePart, Color.White);
            }

            SpriteBatch.End();
        }

        public T AddControl<T>()
            where T : IControl, new()
        {
            if (Skin == null) throw new Exception("No Skin Loaded.");
            if (Font == null) throw new Exception("No Font Loaded.");
            var control = new T
            {
                Skin = Skin,
                SpriteBatch = SpriteBatch,
                Texture = Skin.Texture,
                Font = Font
            };
            control.LoadParts();
            Controls.Add(control);
            return control;
        }
    }
}