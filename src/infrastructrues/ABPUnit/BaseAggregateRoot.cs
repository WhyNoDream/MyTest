using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace ABPUnit
{
    public class BaseAggregateRoot<T>: AggregateRoot<T>
    {
        public List<BaseEventHandler> EventHandlers { get; private set; }

        public  void AddEvent(BaseEventHandler eventHandler)
        {
            EventHandlers.Add(eventHandler);
        }

        public void RemoveEvent(BaseEventHandler eventHandler)
        {
            EventHandlers.Remove(eventHandler);
        }
        public void ClearEvent(BaseEventHandler eventHandler)
        {
            EventHandlers.Clear();
        }
    }
}
