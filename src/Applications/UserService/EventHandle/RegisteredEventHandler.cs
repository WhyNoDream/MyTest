using Domain.User.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MQInfrastructrue.Contracts;
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
        private readonly IServiceProvider  _serviceProvider;

        public RegisteredEventHandler(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("注册已完成事件");
            var mQHelper = _serviceProvider.GetRequiredService<IMQHelper>();
            string exchangeName = "myTest";  
            string queName = "myTest";
            byte[] body = Encoding.Unicode.GetBytes("test");
            var  mqResult= mQHelper.SendMsg(exchangeName, queName, body);
            return Task.CompletedTask;
        }
    }
}
