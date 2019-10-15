using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheDemo.CahceDemo
{
    public class MySizedMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public MySizedMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
        }
    }
}
