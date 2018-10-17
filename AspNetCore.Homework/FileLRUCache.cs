using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace AspNetCore.Homework
{
    public class FileLRUCache
    {
        private readonly Dictionary<int, LinkedListNode<LRUCacheItem<int, string>>> cacheMap =
            new Dictionary<int, LinkedListNode<LRUCacheItem<int, string>>>();

        private readonly int capacity;
        private readonly LinkedList<LRUCacheItem<int, string>> lruList = new LinkedList<LRUCacheItem<int, string>>();
        private readonly string storagePath;

        public FileLRUCache(int capacity, string storagePath)
        {
            this.capacity = capacity;
            this.storagePath = storagePath;

            if (!Directory.Exists(storagePath))
                Directory.CreateDirectory(storagePath);
           
            foreach (var filePath in Directory.GetFiles(storagePath))
            {
                var file = new FileInfo(filePath);
                var key = int.Parse(file.Name);
                var cacheItem =
                    new LRUCacheItem<int, string>(key, filePath);
                var node = new LinkedListNode<LRUCacheItem<int, string>>(cacheItem);
                lruList.AddLast(node);
                cacheMap.Add(key, node);
            }           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public byte[] Get(int key)
        {
            LinkedListNode<LRUCacheItem<int, string>> node;
            if (cacheMap.TryGetValue(key, out node))
            {
                var value = node.Value.value;
                lruList.Remove(node);
                lruList.AddLast(node);

                if (File.Exists(value))
                    return File.ReadAllBytes(value);
            }

            return default(byte[]);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(int key, byte[] val)
        {
            if (cacheMap.Count >= capacity) RemoveFirst();

            var filePath = Path.Combine(storagePath, key.ToString());
            File.WriteAllBytes(filePath, val);

            var cacheItem =
                new LRUCacheItem<int, string>(key, filePath);
            var node = new LinkedListNode<LRUCacheItem<int, string>>(cacheItem);
            lruList.AddLast(node);
            cacheMap.Add(key, node);
        }

        protected void RemoveFirst()
        {
            // Remove from LRUPriority
            var node = lruList.First;
            lruList.RemoveFirst();

            cacheMap.Remove(node.Value.key);

            var filePath = node.Value.value;

            File.Delete(filePath);
        }
    }

    internal class LRUCacheItem<K, V>
    {
        public K key;
        public V value;

        public LRUCacheItem(K k, V v)
        {
            key = k;
            value = v;
        }
    }
}