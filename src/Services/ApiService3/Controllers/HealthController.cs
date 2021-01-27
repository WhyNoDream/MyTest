using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiService3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        //private readonly ICapPublisher _capBus;

        //public HealthController(ICapPublisher capPublisher)
        //{
        //    _capBus = capPublisher;
        //}

        /// <summary>
        /// 健康检测
        /// </summary>
        /// <returns></returns>
        [HttpGet("/status")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetStatus()
        {
            return "OK";
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Health()
        {
            return "OK";
        }

        /// <summary>
        /// cap测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("/captest")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public string GetCapTest()
        {
            //_capBus.Publish("sample.rabbitmq.test", DateTime.Now);
            return "OK";
        }

        [NonAction]
        //[CapSubscribe("sample.rabbitmq.test")]
        public void Subscriber(DateTime p)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber invoked, Info: {p}");
        }
    }
}
