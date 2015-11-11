using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace CarpMuffin.Collections
{
    /// <summary>
    /// Generic resource handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourceCollection<T>
        : ICollection<T>
    {
        #region Variables

        private readonly ArrayList _items = new ArrayList();
        private readonly List<string> _names = new List<string>();
        private ContentManager _content;

        public bool IsReadOnly => _items.IsReadOnly;

        public int Count => _items.Count;

        public virtual T this[string alias]
        {
            get { return (T)_items[_names.IndexOf(alias)]; }
            set { _items[_names.IndexOf(alias)] = value; }
        }

        public virtual T this[int index]
        {
            get { return (T)_items[index]; }
            set { _items[index] = value; }
        }

        #endregion

        #region Initialization

        public ResourceCollection() { }

        public ResourceCollection(ContentManager content)
        {
            _content = content;
        }

        #endregion

        #region ICollection Methods

        public void Add(T item)
        {
            Add(Guid.NewGuid().ToString(), item);
        }

        public void Add(string alias, T item)
        {
            _items.Add(item);
            _names.Add(alias);
        }

        public bool Remove(T item)
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

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void Clear()
        {
            _items.Clear();
            _names.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ResourceEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ResourceEnumerator<T>(this);
        }

        #endregion

        #region Methods

        public T Load(string assetName)
        {
            if (_content == null) throw new Exception("Content Manager not found on Resource Collection");
            return _content.Load<T>(assetName);
        }

        public T LoadInto(string alias, string assetName)
        {
            if (_content == null) throw new Exception("Content Manager not found on Resource Collection");
            var ret = _content.Load<T>(assetName);
            Add(alias, ret);
            return ret;
        }

        #endregion
    }
}