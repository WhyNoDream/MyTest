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

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        /// <param name="expires"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        bool SendMsg<T>(string exchangeName, string queName, T msg, bool delay = false, int expires = 0, int ttl = 0) where T:class;

        /// <summary>
        /// 发送多条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <param name="msgs"></param>
        /// <param name="delay"></param>
        /// <param name="expires"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        bool SendMessages<T>(string exchangeName, string queName, List<T> msgs, bool delay = false, int expires = 0, int ttl = 0) where T : class;

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        void Receive<T>(string exchangeName, string queName, Action<T> received, bool delay = false, int expires = 0, int ttl = 0) where T : class;

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        void Receive(string exchangeName, string queName, Action<byte[]> received, bool delay = false, int expires = 0, int ttl = 0);

        /// <summary>
        /// 释放链接
        /// </summary>
        void Dispose();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void ReceiveClose();

        /// <summary>
        /// 获取队列数量
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <returns></returns>
        int GetMessageCount(string exchangeName, string queName);
    }
}
