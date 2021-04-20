using CacheInfrastructrue.Contracts;
using Domain.User.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MQInfrastructrue.Contracts;
using Newtonsoft.Json;
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
        private readonly ICacheHelper  _cacheHelper;

        public RegisteredEventHandler(IServiceProvider provider, ICacheHelper cacheHelper)
        {
            _serviceProvider = provider;
            _cacheHelper = cacheHelper;
        }

        public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("注册已完成事件");

            #region mq发送测试
            var mQHelper = _serviceProvider.GetRequiredService<IMQHelper>();
            string exchangeName = "myTest";  
            string queName = "myTest";
            byte[] body = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(notification));
            var  mqResult= mQHelper.SendMsg(exchangeName, queName, body);
            #endregion

            #region 缓存测试
            _cacheHelper.Set("test", "测试",60*60*1000);
            #endregion

            return Task.CompletedTask;
        }
    }
}
