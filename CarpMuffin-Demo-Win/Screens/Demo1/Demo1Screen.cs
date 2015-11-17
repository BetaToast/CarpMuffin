using System.Collections.Generic;
using System.Linq;
using CarpMuffin.Screens;
using CarpMuffin.Sprites;
using CarpMuffin_Demo_Win.Demo1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Screens.Demo1
{
    public class Demo1Screen
        : Screen
    {
        private Texture2D _background;
        private Texture2D _bullet;
        private Texture2D _enemyShip1;
        private Texture2D _enemyShip2;
        private Texture2D _enemyShip3;
        private Texture2D _enemyShip4;
        private Texture2D _enemyUfo;
        private Texture2D _meteor;
        private Texture2D _playerShip;

        private List<Enemy> _enemies = new List<Enemy>();
        private List<Meteor> _meteors = new List<Meteor>(); 
        private Player _player;
        private List<Bullet> _bullets = new List<Bullet>();
        private List<Bullet> _deadBullets = new List<Bullet>();

        private int _enemiesKilled;

        #region Initialization / Content

        public override void LoadContent(ContentManager content)
        {
            _background = Textures.Load(TextureNames.Demo1.BlackBackground);
            _bullet = Textures.Load(TextureNames.Demo1.Bullet);
            _enemyShip1 = Textures.Load(TextureNames.Demo1.EnemyShip1);
            _enemyShip2 = Textures.Load(TextureNames.Demo1.EnemyShip2);
            _enemyShip3 = Textures.Load(TextureNames.Demo1.EnemyShip3);
            _enemyShip4 = Textures.Load(TextureNames.Demo1.EnemyShip4);
            _enemyUfo = Textures.Load(TextureNames.Demo1.EnemyUFO);
            _meteor = Textures.Load(TextureNames.Demo1.Meteor);
            _playerShip = Textures.Load(TextureNames.Demo1.PlayerShip);

            LoadGameObjects();
        }

        private void LoadGameObjects()
        {
            var viewport = Engine.Graphics.GraphicsDevice.Viewport;

            _player = new Player(_playerShip)
            {
                Position = new Vector2((viewport.Width / 2) - (_playerShip.Width / 2), viewport.Height - (_playerShip.Height * 1.5f)),
                FireWeapons = PlayerFireWeapons
            };

            for (var i = 0; i < 3; i++)
            {
                _meteors.Add(new Meteor(_meteor)
                {
                    Position =
                        new Vector2((viewport.Width / 4) + ((viewport.Width / 4) * i) - (_meteor.Width / 2), viewport.Height - (_meteor.Height * 2.5f))
                });
            }

            for (var i = 0; i < 8; i++)
            {
                var x = 128 + (i * 128);
                var y = 32;
                var enemy = new Enemy(_enemyShip1)
                {
                    Position = new Vector2(x, y),
                    Group = "Group1",
                    LeftViewportCollision = ViewportCollision,
                    RightViewportCollision = ViewportCollision
                };
                _enemies.Add(enemy);
            }

            for (var i = 0; i < 8; i++)
            {
                var x = 128 + (i * 128);
                var y = 128;
                var enemy = new Enemy(_enemyShip2)
                {
                    Position = new Vector2(x, y),
                    MovementDirection = -1,
                    Group = "Group2",
                    LeftViewportCollision = ViewportCollision,
                    RightViewportCollision = ViewportCollision
                };
                _enemies.Add(enemy);
            }

            for (var i = 0; i < 8; i++)
            {
                var x = 128 + (i * 128);
                var y = 224;
                var enemy = new Enemy(_enemyShip3)
                {
                    Position = new Vector2(x, y),
                    Group = "Group3",
                    LeftViewportCollision = ViewportCollision,
                    RightViewportCollision = ViewportCollision
                };
                _enemies.Add(enemy);
            }

            for (var i = 0; i < 8; i++)
            {
                var x = 128 + (i * 128);
                var y = 320;
                var enemy = new Enemy(_enemyShip4)
                {
                    Position = new Vector2(x, y),
                    MovementDirection = -1,
                    Group = "Group4",
                    LeftViewportCollision = ViewportCollision,
                    RightViewportCollision = ViewportCollision
                };
                _enemies.Add(enemy);
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            UpdateInput(gameTime);

            UpdateEnemies(gameTime);
            UpdateMeteors(gameTime);
            UpdatePlayer(gameTime);
            UpdateBullets(gameTime);
        }

        private void UpdateInput(GameTime gameTime)
        {
            _player.UpdateInput(Input);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Update(gameTime);
            }
        }

        private void UpdateMeteors(GameTime gameTime)
        {
            for (var i = 0; i < _meteors.Count; i++)
            {
                _meteors[i].Update(gameTime);
            }
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            _player.Update(gameTime);
        }

        private void UpdateBullets(GameTime gameTime)
        {
            _deadBullets.Clear();
            for (var i = 0; i < _bullets.Count; i++)
            {
                var bullet = _bullets[i];
                bullet.Update(gameTime);

                for (var j = 0; j < _enemies.Count; j++)
                {
                    var enemy = _enemies[j];
                    if (!enemy.IsEnabled || !enemy.IsVisible) continue;
                    if (bullet.Bounds.Intersects(enemy.Bounds)) EnemyBlowUp(bullet, enemy);
                }

                if (!bullet.IsAlive) _deadBullets.Add(bullet);
            }

            foreach (var bullet in _deadBullets)
            {
                _bullets.Remove(bullet);
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                DrawBullets(gameTime);
                DrawEnemies(gameTime);
                DrawPlayer(gameTime);
                DrawMeteors(gameTime);
            SpriteBatch.End();
        }

        private void DrawEnemies(GameTime gameTime)
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Draw(SpriteBatch);
            }
        }

        private void DrawMeteors(GameTime gameTime)
        {
            for (var i = 0; i < _meteors.Count; i++)
            {
                _meteors[i].Draw(SpriteBatch);
            }
        }

        private void DrawPlayer(GameTime gameTime)
        {
            _player.Draw(SpriteBatch);
        }

        private void DrawBullets(GameTime gameTime)
        {
            for (var i = 0; i < _bullets.Count; i++)
            {
                var bullet = _bullets[i];
                bullet.Draw(SpriteBatch);
            }
        }

        #endregion

        #region Player Methods

        private void PlayerFireWeapons(Player player)
        {
            _bullets.Add(new Bullet(_bullet)
            {
                Position = new Vector2(_player.Position.X + (_player.Size.X / 2), _player.Position.Y)
            });
        }

        #endregion

        #region Enemy Methods

        private void ViewportCollision(Enemy affectedEnemy)
        {
            var group = _enemies.Where(e => e.Group == affectedEnemy.Group).ToList();
            foreach (var enemy in group)
            {
                if (enemy == affectedEnemy) continue;
                if (enemy.MovementDirection == -1)
                {
                    enemy.MovementDirection = 1;
                    enemy.Position += new Vector2(0f, enemy.Speed);
                }
                else if (enemy.MovementDirection == 1)
                {
                    enemy.MovementDirection = -1;
                    enemy.Position += new Vector2(0f, enemy.Speed);
                }
            }
        }

        #endregion

        #region Utility Methods

        private void Restart()
        {
            _enemies.Clear();
            _meteors.Clear();
            _bullets.Clear();

            _enemiesKilled = 0;

            LoadGameObjects();
        }

        private void EnemyBlowUp(Bullet bullet, Enemy enemy)
        {
            bullet.IsAlive = false;
            enemy.IsEnabled = false;
            enemy.IsVisible = false;

            _enemiesKilled++;

            if (_enemiesKilled >= _enemies.Count)
            {
                Restart();
            }
        }

        #endregion
    }
}