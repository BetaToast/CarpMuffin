using System;
using System.Collections.Generic;
using CarpMuffin.Graphics;
using CarpMuffin.Messages;
using CarpMuffin.Screens;

namespace CarpMuffin.Scenes
{
    /// <summary>
    /// Defines a scene
    /// </summary>
    public interface IScene
        : IEntity
    {
        ScreenManager Screens { get; set; }

        IScene AddScreen(string alias, IScreen screen);

        IScene AddScreen<T>(string alias) where T : IScreen, new();

        IScene AddScreens(params KeyValuePair<string, IScreen>[] screens);

        IScene PushToScene(string alias);

        void PopFromScene();

        void SendMessage(ScreenMessage message);

        void RegisterMessageListener(string id, Action<ScreenMessage> action);

        void OnSceneChange();
    }
}