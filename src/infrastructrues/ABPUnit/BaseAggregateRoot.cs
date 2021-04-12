using MediatR;
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
            this.EventHandlers = new List<BaseEventHandler<T>>();
            this.NotificationEventHandlers = new List<INotification>();
        }

        #region 不经过MediatR领域事件

        [NotMapped]
        private List<BaseEventHandler<T>> EventHandlers { get; set; }

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
        #endregion


        #region 经过MediatR领域事件

        /// <summary>
        /// 广播领域事件
        /// </summary>
        [NotMapped]
        private List<INotification> NotificationEventHandlers { get; set; }

        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="data"></param>
        public virtual void AddNotificationEventHandler(INotification notification)
        {
            NotificationEventHandlers.Add(notification);
        }
        /// <summary>
        /// 获取领域事件
        /// </summary>
        public virtual List<INotification> GetNotificationEventsHandler()
        {
            return NotificationEventHandlers;
        }
        /// <summary>
        /// 清空领域事件
        /// </summary>
        public virtual void ClearNotificationEventHandler()
        {
            NotificationEventHandlers.Clear();
        }
        #endregion

    }
}
