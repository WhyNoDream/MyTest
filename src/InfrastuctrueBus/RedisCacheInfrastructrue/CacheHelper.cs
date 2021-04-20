using CacheInfrastructrue.Contracts;
using System;

namespace RedisCacheInfrastructrue
{
    public class CacheHelper : ICacheHelper
    {
        public TEntity Get<TEntity>(string key)
        {
            return  RedisHelper.Get<TEntity>(key);
        }

        public bool NxSet(string key, object value, int expireSeconds = -1)
        {
            return RedisHelper.Set(key, value, expireSeconds, CSRedis.RedisExistence.Nx);
        }

        public bool Set(string key, object value, int expireSeconds = -1)
        {
            return RedisHelper.Set(key, value, expireSeconds);
        }

        public bool XxSet(string key, object value, int expireSeconds = -1)
        {
            return  RedisHelper.Set(key,value,expireSeconds,CSRedis.RedisExistence.Xx);
        }
    }
}
