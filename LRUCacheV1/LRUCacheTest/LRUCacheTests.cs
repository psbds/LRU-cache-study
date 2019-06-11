using LRUCacheV1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUCacheTest
{
    [TestClass]
    public class LRUCacheTests
    {

        [TestMethod]
        public void Cache_Should_Remove_Last_Used_Item_When_Adding_A_New_One()
        {

            var cache = new LRUCache<string>(5);

            cache.Add(1, "val1");
            cache.Add(2, "val2");
            cache.Add(3, "val3");
            cache.Add(4, "val4");
            cache.Add(5, "val5");
            cache.Add(4, "val5");
            cache.Add(3, "val5");
            cache.Add(2, "val5");

            cache.PrintCache();


            Assert.AreEqual(cache.Size, 5);

            var cachedValue = cache.Get(1);
            Assert.AreEqual(cachedValue, "val1");

            cache.PrintCache();

            cache.Add(6, "val6");

            Assert.AreEqual(cache.Size, 5);

           var removedCachedValue = cache.Get(5);

            Assert.AreEqual(removedCachedValue, null);

            cache.PrintCache();

        }
    }
}
