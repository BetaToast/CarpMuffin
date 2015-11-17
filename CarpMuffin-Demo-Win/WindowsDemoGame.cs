using CarpMuffin;
using CarpMuffin.Graphics;
using CarpMuffin_Demo_Win.Scenes;
using CarpMuffin_Demo_Win.Screens;
using CarpMuffin_Demo_Win.Screens.Demo1;
using CarpMuffin_Demo_Win.Screens.Splash;
using CarpMuffin_Demo_Win.Screens.Title;

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
            titleScene.AddScreen<TitleScreen>(ScreenNames.TitleScreen).PushToScene(ScreenNames.TitleScreen);

            var demo1Scene = Scenes.Add<Demo1Scene>(SceneNames.Demo1);
            demo1Scene.AddScreen<Demo1Screen>(ScreenNames.Demo1).PushToScene(ScreenNames.Demo1);

            Scenes.ChangeScene(SceneNames.Splash);
        }
    }
}
