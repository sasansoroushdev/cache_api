using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreCaching.Services
{
    public class CatcheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CatcheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<List<string>> GetList(string cacheKey)
        {
            List<string> dataList;
            string serializedData;
            var encodedMovies = await _distributedCache.GetAsync(cacheKey.ToLower());
            serializedData = Encoding.UTF8.GetString(encodedMovies);
            dataList = JsonConvert.DeserializeObject<List<string>>(serializedData);

            return dataList;
        }
        public async Task<bool> SetList(List<string> dataList, string cacheKey)
        {
            string serializedData;
            serializedData = JsonConvert.SerializeObject(dataList);
            var encodedData = Encoding.UTF8.GetBytes(serializedData);
            var options = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(10));
            await _distributedCache.SetAsync(cacheKey.ToLower(), encodedData, options);
            return true;
        }
    }

}

