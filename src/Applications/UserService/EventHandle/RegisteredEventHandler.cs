using Domain.User.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Applicatiion.UserService.EventHandle
{
    /// <summary>
    /// 已注册事件接收处理
    /// </summary>
    public class RegisteredEventHandler : INotificationHandler<RegisteredEvent>
    {
        public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("注册已完成事件");
            return Task.CompletedTask;
        }
    }
}
