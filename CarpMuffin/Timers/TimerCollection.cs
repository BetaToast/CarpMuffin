using System;
using System.Collections;
using System.Collections.Generic;

namespace CarpMuffin.Timers
{
    /// <summary>
    /// A Collection of Timers
    /// </summary>
    public class TimerCollection
        : ICollection<Timer>
    {
        #region Variables

        private readonly ArrayList _timers = new ArrayList();
        private readonly List<string> _names = new List<string>();

        public bool IsReadOnly => _timers.IsReadOnly;

        public int Count => _timers.Count;

        public virtual Timer this[string alias]
        {
            get { return (Timer)_timers[_names.IndexOf(alias)]; }
            set { _timers[_names.IndexOf(alias)] = value; }
        }

        public virtual Timer this[int index]
        {
            get { return (Timer)_timers[index]; }
            set { _timers[index] = value; }
        }

        #endregion

        #region ICollection Methods

        public void Add(Timer timer)
        {
            Add(Guid.NewGuid().ToString(), timer);
        }

        public void Add(string alias, Timer timer)
        {
            _timers.Add(timer);
            _names.Add(alias);
        }

        public void Add(string alias, int length, Action<Timer> onComplete = null, Action onTick = null)
        {
            var timer = new Timer
            {
                Length = length,
                OnComplete = onComplete,
                OnTick = onTick
            };
            Add(alias, timer);
        }

        public bool Remove(Timer timer)
        {
            try
            {
                var index = _timers.IndexOf(timer);
                _timers.Remove(timer);
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
            _timers.RemoveAt(index);
            _names.RemoveAt(index);
        }

        public bool Contains(Timer timer)
        {
            return _timers.Contains(timer);
        }

        public void Clear()
        {
            _timers.Clear();
            _names.Clear();
        }

        public void CopyTo(Timer[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Timer> GetEnumerator()
        {
            return new TimerEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TimerEnumerator(this);
        }

        #endregion
    }
}