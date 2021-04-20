using System;
using System.Collections.Generic;
using System.Text;

namespace CacheInfrastructrue.Contracts
{
    public interface ICacheHelper
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        //
        // 摘要:
        //     设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        //
        // 参数:
        //   key:
        //     不含prefix前辍
        //
        //   value:
        //     值
        //
        //   expireSeconds:
        //     过期(秒单位)
         bool Set(string key, object value, int expireSeconds = -1);

        //
        // 摘要:
        //     设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        //
        // 参数:
        //   key:
        //     不含prefix前辍
        //
        //   value:
        //     值
        //
        //   expireSeconds:
        //     过期(秒单位)
        //
        //   exists:  Nx   Only set the key if it does not already exist
        //     Nx, Xx  
        bool NxSet(string key, object value, int expireSeconds = -1);


        //
        // 摘要:
        //     设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        //
        // 参数:
        //   key:
        //     不含prefix前辍
        //
        //   value:
        //     值
        //
        //   expireSeconds:
        //     过期(秒单位)
        //
        //   exists:   Xx   Only set the key if it already exists
        //     Nx, Xx  
        bool XxSet(string key, object value, int expireSeconds = -1);

    }
}
