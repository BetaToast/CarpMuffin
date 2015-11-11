using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CarpMuffin.Animations
{
    public class AnimationCollection
        : ICollection<Animation>
    {
        #region Variables

        private readonly ArrayList _items = new ArrayList();
        private readonly List<string> _names = new List<string>();

        public bool IsReadOnly => _items.IsReadOnly;

        public int Count => _items.Count;

        public int CurrentAnimationIndex { get; private set; }
        public string CurrentAnimationName { get; private set; }

        public virtual Animation this[string alias]
        {
            get { return (Animation)_items[_names.IndexOf(alias)]; }
            set { _items[_names.IndexOf(alias)] = value; }
        }

        public virtual Animation this[int index]
        {
            get { return (Animation)_items[index]; }
            set { _items[index] = value; }
        }

        public Animation CurrentAnimation => (Animation)_items[CurrentAnimationIndex];

        #endregion

        #region Initialization

        public AnimationCollection()
        {
            CurrentAnimationIndex = 0;
        }

        #endregion

        #region ICollection Methods

        public void Add(string alias, double frameLength, List<Rectangle> frames)
        {
            var animation = new Animation
            {
                Name = alias,
                Frames = frames,
                FrameLength = frameLength
            };
            Add(alias, animation);
        }

        public void Add(Animation item)
        {
            Add(Guid.NewGuid().ToString(), item);
        }

        public void Add(string alias, Animation item)
        {
            _items.Add(item);
            _names.Add(alias);
        }

        public bool Remove(Animation item)
        {
            try
            {
                var index = _items.IndexOf(item);
                _items.Remove(item);
                _names.RemoveAt(index);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Remove(string alias)
        {
            var index = _names.IndexOf(alias);
            _items.RemoveAt(index);
            _names.RemoveAt(index);
        }

        public bool Contains(Animation item)
        {
            return _items.Contains(item);
        }

        public void Clear()
        {
            _items.Clear();
            _names.Clear();
        }

        public void CopyTo(Animation[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Animation> GetEnumerator()
        {
            return new AnimationEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new AnimationEnumerator(this);
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
        }

        public void ChangeAnimation(string name)
        {
            var index = _names.IndexOf(name);
            CurrentAnimationIndex = index;
            CurrentAnimationName = name;
        }

        #endregion
    }
}