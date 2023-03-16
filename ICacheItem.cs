using System;

namespace CacheOfObjects
{
    internal interface ICacheItem : IDisposable
    {
        DateTime LastRequest { get; }
    }
}
