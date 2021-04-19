using CommonUnit.Config;
using MQInfrastructrue.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQInfrastructrue
{
    public static class RabbitMqConf
    {
        private static IConnection connection;

        private static ConnectionFactory connectionFactory;
        
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IConnection GetConnection(string name)
        {
            if (connectionFactory != null)
            {
                return connection = connectionFactory.CreateConnection(name); ;
            }
            return null;
        }
     
        /// <summary>
        /// 设置mq工厂
        /// </summary>
        /// <param name="mQConnOption"></param>
        /// <returns></returns>
        public static bool SetConnectionFactory(MQConnOption mQConnOption)
        {
            if (mQConnOption != null)
            {

                connectionFactory = new ConnectionFactory
                {
                    HostName = mQConnOption.Host,// ConfigManagerConf.GetValue("RabbitMQ:Host"),
                    Port = mQConnOption.Port, // string.IsNullOrEmpty(ConfigManagerConf.GetValue("RabbitMQ:Port")) ? 0 : int.Parse(ConfigManagerConf.GetValue("RabbitMQ:Port")),
                    UserName = mQConnOption.UserName,// ConfigManagerConf.GetValue("RabbitMQ:UserName"),
                    Password = mQConnOption.Password,// ConfigManagerConf.GetValue("RabbitMQ:Password")
                };

                return true;
            }
            return false;
        }

    }
}
