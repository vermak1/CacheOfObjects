using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CacheOfObjects
{
    internal class Cache : IDisposable, IEnumerable<ICacheItem>
    {
        public Int32 CurrentCount { get { return _disposables.Count; } }
        private readonly List<ICacheItem> _disposables;
        private readonly TimeSpan _lifeTime;

        public Cache(Int32 capacity, TimeSpan lifeTime)
        {
            if (capacity <= 0)
                throw new ArgumentException(nameof(capacity));
            _lifeTime = lifeTime;
            _disposables = new List<ICacheItem>(capacity);
        }

        public void Add(ICacheItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (_disposables.Count < _disposables.Capacity)
            {
                _disposables.Add(item);
                return;
            }
            Boolean isRemoved = RemoveOutdatedObjects();
            if (isRemoved)
            {
                _disposables.Add(item);
                return;
            }
            throw new Exception("Cache is full");
        }

        public void Remove(ICacheItem item)
        {
            if (item == null)
                throw new ArgumentNullException();
            _disposables.Remove(item);
        }

        private Boolean RemoveOutdatedObjects()
        {
            Boolean isRemoved = false;
            foreach (ICacheItem itemInCache in _disposables.ToList())
            {
                if (DateTime.Now - itemInCache.LastRequest > _lifeTime)
                {
                    isRemoved = true;
                    _disposables.Remove(itemInCache);
                    itemInCache?.Dispose();
                }
            }
            return isRemoved;
        }

        private Boolean RemoveOutdatedObjects2Option()
        {
            Boolean isRemoved = false;
            List<ICacheItem> tempList = _disposables;
            foreach (ICacheItem itemInCache in tempList)
            {
                if (DateTime.Now - itemInCache.LastRequest > _lifeTime)
                {
                    isRemoved = true;
                    _disposables.Remove(itemInCache);
                    itemInCache?.Dispose();
                }
            }
            return isRemoved;
        }

        public IEnumerator<ICacheItem> GetEnumerator() =>
            _disposables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
            foreach (ICacheItem i in _disposables)
                i.Dispose();
        }
    }
}