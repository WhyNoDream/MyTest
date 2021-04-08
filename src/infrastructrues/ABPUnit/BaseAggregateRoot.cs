using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace ABPUnit
{
    public class BaseAggregateRoot<T>:   AggregateRoot<T>
    {
        public BaseAggregateRoot() : base()
        {

        }
        [NotMapped]
        private  List<BaseEventHandler<T>> EventHandlers { get;  set; }

        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="data"></param>
        public virtual void AddEventHandler(string content, object data)
        {
            BaseEventHandler<T> eventHandler = new BaseEventHandler<T>(this.Id, content, data);
            EventHandlers.Add(eventHandler);
        }

        /// <summary>
        /// 获取领域事件
        /// </summary>
        public virtual List<BaseEventHandler<T>> GetEventsHandler()
        {
            return EventHandlers;
        }
        /// <summary>
        /// 清空领域事件
        /// </summary>
        public virtual void ClearEventHandler()
        {
            EventHandlers.Clear();
        }

        ////移除事件：暂时无效
        //public virtual void RemoveEvent(BaseEventHandler eventHandler)
        //{
        //    EventHandlers.Remove(eventHandler);
        //}
    }
}
