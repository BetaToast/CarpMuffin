using System.Collections;
using System.Collections.Generic;

namespace CarpMuffin.Collections
{
    /// <summary>
    /// Generic Resource Enumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourceEnumerator<T>
        : IEnumerator<T>
    {
        #region Variables

        private ResourceCollection<T> _collection;
        private int _index;

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        #endregion

        public ResourceEnumerator(ResourceCollection<T> collection)
        {
            _collection = collection;
            _index = -1;
            Current = default(T);
        }

        public void Dispose()
        {
            _collection = null;
            Current = default(T);
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
            Current = default(T);
            _index = -1;
        }
    }
}