using CarpMuffin.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CarpMuffin.Scenes
{
    /// <summary>
    /// Manages all scenes
    /// </summary>
    public class SceneManager
        : EntityManager<IScene>
    {
        private readonly ContentManager _content;
        private IScene _currentScene;

        public SceneManager(ContentManager content)
        {
            _content = content;
        }

        public override IScene Add(string alias, IScene item)
        {
            item.LoadContent(_content);
            return base.Add(alias, item);
        }

        public T Add<T>(string alias)
            where T : IScene, new()
        {
            var scene = new T();
            return (T)Add(alias, scene);
        }

        public override IScene Add(IScene item)
        {
            item.LoadContent(_content);
            return base.Add(item);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var scene in Items)
            {
                if (scene.IsEnabled) scene.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (_currentScene != null && _currentScene.IsVisible) _currentScene?.Draw(gameTime);
        }

        public IScene ChangeScene(string alias)
        {
            if (_currentScene != null)
            {
                _currentScene.IsEnabled = false;
                _currentScene.IsVisible = false;
            }

            _currentScene = Get(alias);
            _currentScene.OnSceneChange();
            _currentScene.IsEnabled = true;
            _currentScene.IsVisible = true;
            return _currentScene;
        }
    }
}