using System.Collections;
using System.Collections.Generic;

namespace CarpMuffin.Animations
{
    public class AnimationEnumerator
        : IEnumerator<Animation>
    {
        #region Variables

        private AnimationCollection _collection;
        private int _index;

        public Animation Current { get; private set; }

        object IEnumerator.Current => Current;

        #endregion

        public AnimationEnumerator(AnimationCollection collection)
        {
            _collection = collection;
            _index = -1;
            Current = default(Animation);
        }

        public void Dispose()
        {
            _collection = null;
            Current = default(Animation);
            _index = -1;
        }

        public bool MoveNext()
        {
            if (++_index >= _collection.Count) return false;
            else Current = _collection[_index];
            return true;
        }

        public void Reset()
        {
            Current = default(Animation);
            _index = -1;
        }
    }
}