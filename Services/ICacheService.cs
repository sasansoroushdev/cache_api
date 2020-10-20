using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCaching.Services
{
    public interface ICacheService
    {
        Task<List<string>> GetList(string cacheKey);
        Task<bool> SetList(List<string> dataList, string cacheKey);
    }
}
