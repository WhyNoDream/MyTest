using Domain.User.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Applicatiion.UserService.EventHandle
{
    /// <summary>
    /// 已注册事件接收处理
    /// </summary>
    public class RegisteredEventHandle: INotificationHandler<RegisteredEvent>
    {
        /// <summary>
        /// 接收广播事件
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
