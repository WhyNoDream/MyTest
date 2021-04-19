using CommonUnit.Config;
using CommonUnit.StringUnit;
using MQInfrastructrue.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQInfrastructrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebbitMQInfrastructrue
{
    public class RabbitMQHelper : IMQHelper
    {
        private  IConnection connection;

        private  void CreateConn(string name)
        {
            connection = RabbitMqConf.GetConnection(name);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public Task<bool> SendMsg(string exchangeName, string queName, byte[] body)
        {
            if (body == null)
            {
                return Task.FromResult<bool>(false) ;
            }
            try
            {
                if (connection == null || !connection.IsOpen)
                {
                    CreateConn(queName);
                }
                using (var channel = connection.CreateModel())
                {
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>
                        {
                            { "x-dead-letter-exchange", "exchange-direct" },//过期消息转向路由
                            { "x-dead-letter-routing-key", "routing-delay" }//过期消息转向路由相匹配routingkey
                        };
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    else
                    {
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, null);
                    }
                    var basicProperties = channel.CreateBasicProperties();
                    //1：非持久化 2：可持久化
                    basicProperties.DeliveryMode = 2;
                    var address = new PublicationAddress(ExchangeType.Direct, exchangeName, queName);
                    channel.BasicPublish(address, basicProperties, body);
                }
                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult<bool>(false);
            }
        }

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
        public  bool SendMsg<T>(string exchangeName, string queName, T msg, bool delay = false, int expires = 0, int ttl = 0) where T : class
        {
            if (msg == null)
            {
                return false;
            }
            try
            {
                if (connection == null || !connection.IsOpen)
                {
                    CreateConn(queName);
                }
                using (var channel = connection.CreateModel())
                {
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>
                        {
                            { "x-expires", expires * 1000 },
                            { "x-message-ttl", ttl * 1000 },//队列上消息过期时间，应小于队列过期时间
                            { "x-dead-letter-exchange", "exchange-direct" },//过期消息转向路由
                            { "x-dead-letter-routing-key", "routing-delay" }//过期消息转向路由相匹配routingkey
                        };
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, arguments: delay ? dic : null);
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    else
                    {
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, null);
                    }
                    var basicProperties = channel.CreateBasicProperties();
                    //1：非持久化 2：可持久化
                    basicProperties.DeliveryMode = 2;
                    var payload = Encoding.UTF8.GetBytes(msg.ToJson());
                    var address = new PublicationAddress(ExchangeType.Direct, exchangeName, queName);
                    channel.BasicPublish(address, basicProperties, payload);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
        public  bool SendMessages<T>(string exchangeName, string queName, List<T> msgs, bool delay = false, int expires = 0, int ttl = 0) where T : class
        {
            if (msgs == null && !msgs.Any())
            {
                return false;
            }
            try
            {
                if (connection == null || !connection.IsOpen)
                {
                    CreateConn(queName);
                }
                using (var channel = connection.CreateModel())
                {
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>
                        {
                            { "x-expires", expires * 1000 },
                            { "x-message-ttl", ttl * 1000 },//队列上消息过期时间，应小于队列过期时间
                            { "x-dead-letter-exchange", "exchange-direct" },//过期消息转向路由
                            { "x-dead-letter-routing-key", "routing-delay" }//过期消息转向路由相匹配routingkey
                        };
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    else
                    {
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, null);
                    }
                    var basicProperties = channel.CreateBasicProperties();
                    //1：非持久化 2：可持久化
                    basicProperties.DeliveryMode = 2;
                    var address = new PublicationAddress(ExchangeType.Direct, exchangeName, queName);
                    msgs.ForEach((msg) =>
                    {
                        var payload = Encoding.UTF8.GetBytes(msg.ToJson());
                        channel.BasicPublish(address, basicProperties, payload);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                var gewa = ex;
                return false;
            }
        }

        public  IModel GetChannel(string name)
        {
            if (connection == null || !connection.IsOpen)
            {
                CreateConn(name);
            }
            var channel = connection.CreateModel();
            return channel;
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        public  void Receive<T>(string exchangeName, string queName, IModel channel, Action<T> received, bool delay = false, int expires = 0, int ttl = 0) where T : class
        {
            try
            {
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>
                        {
                            { "x-expires", expires * 1000 },
                            { "x-message-ttl", ttl * 1000 },//队列上消息过期时间，应小于队列过期时间
                            { "x-dead-letter-exchange", "exchange-direct" },//过期消息转向路由
                            { "x-dead-letter-routing-key", "routing-delay" }//过期消息转向路由相匹配routingkey
                        };
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    else
                    {
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                    }
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    //事件基本消费者
                    var consumer = new EventingBasicConsumer(channel);
                    //接收到消息事件
                    consumer.Received += (ch, ea) =>
                    {
                        string message = Encoding.UTF8.GetString(ea.Body.ToArray());
                        var msg = message.ToObject<T>();
                        DateTime time = DateTime.Now;
                        received(msg);
                        var timeEnd = DateTime.Now - time;
                        //channel.DefaultConsumer.HandleBasicCancelOk(consumer.ConsumerTag);
                        if (channel.IsClosed)
                        {
                            return;
                        }
                        Console.WriteLine($"任务执行完成，用时 {timeEnd.TotalSeconds.ToString("0.00")}s {queName} 队列剩余任务数量： {channel.MessageCount(queName)}");
                        //确认该消息已被消费
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    //启动消费者 设置为手动应答消息
                    channel.BasicConsume(queName, false, consumer);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        public  void Receive(string exchangeName, string queName, IModel channel, Action<byte[]> received, bool delay = false, int expires = 0, int ttl = 0)
        {
            try
            {
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>
                        {
                            { "x-expires", expires * 1000 },
                            { "x-message-ttl", ttl * 1000 },//队列上消息过期时间，应小于队列过期时间
                            { "x-dead-letter-exchange", "exchange-direct" },//过期消息转向路由
                            { "x-dead-letter-routing-key", "routing-delay" }//过期消息转向路由相匹配routingkey
                        };
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    else
                    {
                        //声明一个队列
                        channel.QueueDeclare(queName, true, false, false, arguments: delay ? dic : null);
                    }
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    //事件基本消费者
                    var consumer = new EventingBasicConsumer(channel);
                    //接收到消息事件
                    consumer.Received += (ch, ea) =>
                    {
                        DateTime time = DateTime.Now;
                        received(ea.Body.ToArray());
                        var timeEnd = DateTime.Now - time;
                        //channel.DefaultConsumer.HandleBasicCancelOk(consumer.ConsumerTag);
                        if (channel.IsClosed)
                        {
                            return;
                        }
                        Console.WriteLine($"任务执行完成，用时 {timeEnd.TotalSeconds.ToString("0.00")}s {queName} 队列剩余任务数量： {channel.MessageCount(queName)}");
                        //确认该消息已被消费
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    //启动消费者 设置为手动应答消息
                    channel.BasicConsume(queName, false, consumer);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 释放链接
        /// </summary>
        public void Dispose()
        {
            connection.Close();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void ReceiveClose()
        {
        }

        /// <summary>
        /// 获取队列数量
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queName"></param>
        /// <returns></returns>
        public  int GetMessageCount(string exchangeName, string queName)
        {
            try
            {
                if (connection == null || !connection.IsOpen)
                {
                    CreateConn(queName);
                }
                using (var channel = connection.CreateModel())
                {
                    if (!string.IsNullOrEmpty(exchangeName))
                    {
                        //声明交换机
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                        //绑定队列，交换机，路由键
                        channel.QueueBind(queName, exchangeName, queName);
                    }
                    return channel.MessageCount(queName).ToInt32();
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
