using System.Collections;
using System.Collections.Generic;

namespace CarpMuffin.Timers
{
    /// <summary>
    /// An enumerator for timers
    /// </summary>
    public class TimerEnumerator
        : IEnumerator<Timer>
    {
        #region Variables

        private TimerCollection _collection;
        private int _index;

        public Timer Current { get; private set; }

        object IEnumerator.Current => Current;

        #endregion

        public TimerEnumerator(TimerCollection collection)
        {
            _collection = collection;
            _index = -1;
            Current = null;
        }

        public void Dispose()
        {
            _collection = null;
            Current = null;
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
            Current = null;
            _index = -1;
        }
    }
}