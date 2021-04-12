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
            return null;
        }

        //Task<string> IRequestHandler<RegisteredEvent, string>.Handle(RegisteredEvent request, CancellationToken cancellationToken)
        //{
        //    Debug.WriteLine("注册已完成事件");

        //}

        //Task<Unit> IRequestHandler<RegisteredEvent, Unit>.Handle(RegisteredEvent request, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine("注册事件处理");
        //    return null;
        //}
    }
}
