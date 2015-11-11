using System;
using CarpMuffin.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin
{
    /// <summary>
    /// The main rendering engine
    /// </summary>
    public class Engine
        : Game
    {
        #region Variables

        public static Engine Instance { get; private set; }

        public Color ClearColor { get; set; }
        public GraphicsDeviceManager Graphics { get; set; }
        public SceneManager Scenes { get; set; }
        public Vector2 Resolution { get; private set; }
        public Random Random { get; private set; }
        public bool IsDebug { get; set; }

        #endregion

        #region Initialization

        public Engine()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ClearColor = Color.SteelBlue;
            Random = new Random();

            Instance = this;
            IsDebug = true;
        }

        protected override void Initialize()
        {
            Scenes = new SceneManager(Content);
            base.Initialize();
        }

        #endregion

        #region Content

        protected override void LoadContent()
        {
            OnLoad();
        }

        protected override void UnloadContent()
        {

        }

        public virtual void OnLoad() { }

        #endregion

        #region Update

        protected override void Update(GameTime gameTime)
        {
            Scenes.Update(gameTime);
        }

        #endregion

        #region Draw

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearColor);

            Scenes.Draw(null);

            if (IsDebug)
            {
                
            }

            base.Draw(gameTime);
        }

        #endregion

        #region Utility Methods

        public void SetResolution(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;

            Graphics.PreferMultiSampling = true;
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;

            Graphics.ApplyChanges();
            Resolution = new Vector2(width, height);
        }

        public Texture2D CreateWhitePixel()
        {
            var ret = new Texture2D(GraphicsDevice, 1, 1);
            ret.SetData(new[] { Color.White });
            return ret;
        }

        #endregion
    }
}