using System.Collections;
using System.Collections.Generic;
using CarpMuffin.Graphics;

namespace CarpMuffin.Managers
{
    /// <summary>
    /// Enumerator of Entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityEnumerator<T>
        : IEnumerator<T> where T : IEntity
    {
        #region Variables

        protected EntityManager<T> Collection;
        private int _index;

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        #endregion

        public EntityEnumerator(EntityManager<T> collection)
        {
            Collection = collection;
            _index = -1;
            Current = default(T);
        }

        public void Dispose()
        {
            Collection = null;
            Current = default(T);
            _index = -1;
        }

        public bool MoveNext()
        {
            if (++_index >= Collection.Count) return false;
            else Current = Collection[_index];
            return true;
        }

        public void Reset()
        {
            Current = default(T);
            _index = -1;
        }
    }
}