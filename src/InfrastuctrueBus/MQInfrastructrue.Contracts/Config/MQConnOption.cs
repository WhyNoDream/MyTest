using System;
using System.Collections.Generic;
using System.Text;

namespace MQInfrastructrue.Contracts
{
    /// <summary>
    /// mq连接配置选项
    /// </summary>
    public class MQConnOption
    {
        /// <summary>
        /// mq的主机名称
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 0;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///密码
        /// </summary>
        public string Password { get; set; }
    }
}
