using System.Collections;
using System.Collections.Generic;
using CarpMuffin.Graphics;

namespace CarpMuffin.Managers
{
    /// <summary>
    /// Whips those Entities into order
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityManager<T>
        : ICollection<T> where T : IEntity
    {
        #region Variables

        public T this[string alias] => Items[GetIndex(alias)];

        public T this[int index] => Items[index];

        public List<T> Items { get; } = new List<T>();

        public List<string> Names { get; } = new List<string>();

        public int Count => Items.Count;
        public bool IsReadOnly { get; }

        #endregion

        public EntityManager()
        {
            IsReadOnly = true;
        }

        #region Manager Methods

        protected int GetIndex(string alias)
        {
            return Names.IndexOf(alias);
        }

        public bool Exists(string alias)
        {
            return Names.Contains(alias);
        }

        public T Get(string alias)
        {
            return Items[GetIndex(alias)];
        }

        public virtual void Clear()
        {
            Items.Clear();
            Names.Clear();
        }

        public virtual T Add(string alias, T item)
        {
            if (Exists(alias))
            {
                var index = GetIndex(alias);
                item.Name = alias;
                Items[index] = item;
            }
            else
            {
                item.Name = alias;
                Names.Add(alias);
                Items.Add(item);
            }
            return item;
        }

        public virtual T Add(T item)
        {
            Add(item.Name, item);
            return item;
        }

        public virtual void Remove(string alias)
        {
            var index = GetIndex(alias);
            Items.RemoveAt(index);
            Names.RemoveAt(index);
        }

        public virtual void Remove(T item)
        {
            Remove(item.Name);
        }

        #endregion

        #region ICollection Methods

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            try
            {
                var index = Items.IndexOf(item);
                Items.RemoveAt(index);
                Names.RemoveAt(index);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EntityEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EntityEnumerator<T>(this);
        }

        #endregion
    }
}