using System;
using System.Collections.Generic;
using System.Text;

namespace ABPUnit
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public class BaseEventHandler<T>
    {
        public BaseEventHandler(T id,string content,object data)
        {
            this.Id = id;
            this.Content = content;
            this.Data = data;
        }
        /// <summary>
        /// 聚合根id
        /// </summary>
        public T Id { get;private set; }
        /// <summary>
        /// 字符串内容
        /// </summary>
        public string Content { get;private set; }
        /// <summary>
        /// 对象内容
        /// </summary>
        public object Data { get;private set; }
    }
}
