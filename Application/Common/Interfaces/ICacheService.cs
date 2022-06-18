using StackExchange.Redis;

namespace Application.Common.Interfaces;

public interface ICacheService
{
    void Set<T>(string key, T value, bool isPartOfList);
    List<T> GetList<T>(string key);
    T Get<T>(string key);
    void Remove(string key);
}
