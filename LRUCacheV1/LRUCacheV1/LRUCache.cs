using System;
using System.Collections.Generic;

namespace LRUCacheV1
{
    public class LRUCache<T> where T : class
    {

        public long Size
        {
            get
            {
                return _items.Length;
            }
        }

        private LRUItem<T> Newest;

        private LRUItem<T> Oldest;

        private long _cacheSize;

        private LRUItem<T>[] _items;

        public LRUCache(int cacheSize)
        {
            this._cacheSize = cacheSize;
            this._items = new LRUItem<T>[] { };
        }

        public void Add(int key, T value)
        {
            var item = GetRef(key);
            if (item != null)
            {
                Console.WriteLine($"Refreshing: {item.key}");
                // Key is already present on the array
                var oldNewest = Newest;
                Newest = item;

                item.next.last = item.last;
                item.last.next = item.next;

                if (Oldest == item)
                {
                    Oldest = item.last;
                    Oldest.next = Newest;
                }

                item.last = Oldest;
                item.next = oldNewest;
                item.value = value;
                oldNewest.last.next = item;
                oldNewest.last = item;


            }
            else
            {
                var newItem = new LRUItem<T>()
                {
                    key = key,
                    value = value
                };


                if (_items.Length + 1 > _cacheSize)
                {
                    Console.WriteLine($"Adding {newItem.key} to a full Array (replacing {Oldest.key})");
                    // Adding items on a full array
                    var oldNewest = Newest;
                    var oldOldest = Oldest;
                    Newest = newItem;

                    newItem.last = oldOldest.last;
                    newItem.next = oldNewest;
                    oldOldest.last.next = Newest;
                    oldNewest.last = Newest;
                    Oldest = oldOldest.last;

                    var index = Array.IndexOf(_items, oldOldest);

                    _items[index] = Newest;
                }
                else
                {
                    Console.WriteLine($"Adding {newItem.key}");
                    if (_items.Length == 0)
                    {
                        // Adding items when array is empty
                        Newest = newItem;
                        Oldest = newItem;
                        newItem.next = newItem;
                        newItem.last = newItem;
                    }

                    // Adding items on a non full array
                    var oldNewest = Newest;

                    newItem.last = Oldest;
                    newItem.next = oldNewest;

                    oldNewest.last.next = newItem;
                    oldNewest.last = newItem;

                    Newest = newItem;

                    var newItemPosition = _items.Length;

                    _items = _items.EnsureCapacity(_items.Length + 1);

                    _items[newItemPosition] = newItem;
                }

                _items.QuickSort();
            }
        }

        public T Get(int key)
        {
            var item = _items.BinarySearch(key);
            if (item != null)
            {
                RenewCache(item);
                return item.value;
            }
            else
            {
                return null;
            }
        }

        private void RenewCache(LRUItem<T> item)
        {
            Add(item.key, item.value);
        }

        private LRUItem<T> GetRef(int key)
        {
            return _items.BinarySearch(key);
        }

        public void PrintCache()
        {
            Console.WriteLine(new String('-', 100));
            Console.WriteLine($"Newest: {Newest.key} - Oldest: {Oldest.key}");

            var item = Newest;
            do
            {
                Console.WriteLine($"<({item.last.key})  {item.key}  ({item.next.key})>");
                item = item.next;
            } while (item != Newest);

            Console.WriteLine(new String('-', 100));
        }
    }


    public class LRUItem<T>
    {
        public int key;

        public T value;

        public LRUItem<T> next;

        public LRUItem<T> last;

    }


}
