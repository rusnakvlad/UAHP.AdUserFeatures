using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;

    public CacheService(IDistributedCache distributedCache) => this.distributedCache = distributedCache;

    public T Get<T>(string key)
    {
        var cacheObj = distributedCache.Get(key);
        if (cacheObj != null)
        {
            var serializedObj = Encoding.UTF8.GetString(cacheObj);
            var deserializedObj = JsonConvert.DeserializeObject<T>(serializedObj);
            return deserializedObj;
        }

        throw new Exception($"Not found any record of key: {key} in cache");
    }

    public List<T> GetList<T>(string key)
    {
        var cacheObj = distributedCache.Get(key);
        if (cacheObj != null)
        {
            var serializedObjects = Encoding.UTF8.GetString(cacheObj);
            var objects = JsonConvert.DeserializeObject<List<T>>(serializedObjects);
            return objects;
        }
        return null;
    }

    public void Remove(string key)
    {
        distributedCache.Remove(key);
    }

    public void Set<T>(string key, T value, bool isPartOfList)
    {
        string serializedObject;
        if (isPartOfList)
        {
            var previousElements = GetList<T>(key);
            if(previousElements == null)
            {
                serializedObject = JsonConvert.SerializeObject(new List<T> { value });
            }
            else
            {
                previousElements.Add(value);
                serializedObject = JsonConvert.SerializeObject(previousElements);
            }
        }
        else
        {
            serializedObject = JsonConvert.SerializeObject(value);
        }

        var cahceObj = Encoding.UTF8.GetBytes(serializedObject);
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(20))
            .SetSlidingExpiration(TimeSpan.FromMinutes(15));

        distributedCache.Set(key, cahceObj, options);
    }
}
