using Basics.Units;
using System;
using Xunit;

namespace XunitTest
{
    public class CacheTests
    {
        [Fact]
        public void CachesItemWithinTimeSpan()
        {
            var cacheClass = new Cache(TimeSpan.FromDays(1));
            cacheClass.Add(new Cache.Item("url", "content", DateTime.Now));

            var contains = cacheClass.Contains("url");

            Assert.True(contains);
        }

        [Fact]
        public void Contains_ReturnsFalse_WhenOutsideTimeSpan()
        {
            var cacheClass = new Cache(TimeSpan.FromDays(1));
            cacheClass.Add(new Cache.Item("url", "content", DateTime.Now.AddDays(-2)));

            var contains = cacheClass.Contains("url");

            Assert.False(contains);
        }


        [Fact]
        public void Contains_ReturnsFalse_WhenDoesntContainItem()
        {
            var cacheClass = new Cache(TimeSpan.FromDays(1));

            var contains = cacheClass.Contains("url");

            Assert.False(contains);
        }
    }
}
