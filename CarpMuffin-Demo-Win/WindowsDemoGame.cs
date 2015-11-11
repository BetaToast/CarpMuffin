using CarpMuffin;
using CarpMuffin.Graphics;
using CarpMuffin_Demo_Win.Scenes;
using CarpMuffin_Demo_Win.Screens;
using CarpMuffin_Demo_Win.Screens.Splash;

namespace CarpMuffin_Demo_Win
{
    public class WindowsDemoGame 
        : Engine
    {
        public override void OnLoad()
        {
            SetResolution(1280, 720);
            Window.Title = "CarpMuffin - Windows Demo";
            IsMouseVisible = true;

            Program.Camera = new Camera2D();

            var splashScene = Scenes.Add<SplashScene>(SceneNames.Splash);
            splashScene.AddScreen<CarpMuffinSplashScreen>(ScreenNames.CarpMuffinSplash).PushToScene(ScreenNames.CarpMuffinSplash);

            var titleScene = Scenes.Add<TitleScene>(SceneNames.Title);

            Scenes.ChangeScene(SceneNames.Splash);
        }
    }
}
