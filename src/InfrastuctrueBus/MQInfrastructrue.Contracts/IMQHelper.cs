using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQInfrastructrue.Contracts
{
    public interface IMQHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<bool> SendMsg(string exchangeName, string queName, byte[] body);

        ///// <summary>
        ///// 发送消息
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exchangeName"></param>
        ///// <param name="queName"></param>
        ///// <param name="msg"></param>
        ///// <param name="delay"></param>
        ///// <param name="expires"></param>
        ///// <param name="ttl"></param>
        ///// <returns></returns>
        //Task<bool> SendMsg<T>(string exchangeName, string queName, T msg, bool delay = false, int expires = 0, int ttl = 0) where T : class;

        ///// <summary>
        /////  发送多条消息
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exchangeName"></param>
        ///// <param name="queName"></param>
        ///// <param name="msgs"></param>
        ///// <param name="delay"></param>
        ///// <param name="expires"></param>
        ///// <param name="ttl"></param>
        ///// <returns></returns>
        //Task<bool> SendMessages<T>(string exchangeName, string queName, List<T> msgs, bool delay = false, int expires = 0, int ttl = 0) where T : class;
    }
}
