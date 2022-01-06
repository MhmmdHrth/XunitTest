using Basics.Units;
using System;
using Xunit;

namespace XunitTest
{
    public class PropertyHashTests
    {
        [Fact]
        public void PropertyHash_ConcatenatesSelectedFieldsInOrder()
        {
            PropertyHash hasher = new PropertyHash();
            Cache.Item item = new Cache.Item("url", "content", DateTime.Now); 

            var hash = hasher.Hash<Cache.Item>(item, x => x.Url, x => x.Content);

            Assert.Equal("urlcontent", hash);
        }

        [Fact]
        public void AlgoruthmPropertyHash_AppliesHashingAlgorithmToSeed()
        {
            AlgorithmPropertyHash hasher = new AlgorithmPropertyHash("sha256");
            Cache.Item item = new Cache.Item("url", "content", DateTime.Now);

            var hash = hasher.Hash<Cache.Item>(item, x => x.Url, x => x.Content);

            Assert.Equal("9FyLxk+9z73XO8xhZ15emMaK+oa8aDg6LWiY6y40KyQ=", hash);
        }
    }
}
