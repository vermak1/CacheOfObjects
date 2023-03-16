using System;
using System.IO;

namespace CacheOfObjects
{
    internal class Item : ICacheItem
    {
        private readonly StreamWriter _stream;
        public DateTime LastRequest { get; private set; }

        public Item(String pathToFile)
        {
            LastRequest = DateTime.Now;
            _stream = new StreamWriter(pathToFile, true);
        }

        public void WriteIntoFile()
        {
            _stream.WriteLine("[{0}] Last request time [{1}] was updated", DateTime.Now, LastRequest);
            LastRequest = DateTime.Now;
        }

        public void Dispose()
        {
            _stream.WriteLine("[{0}] DISPOSE", DateTime.Now);
            _stream?.Dispose();
        }
    }
}
