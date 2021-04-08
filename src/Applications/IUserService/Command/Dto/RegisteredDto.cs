using System;
using System.Collections.Generic;
using System.Text;

namespace Applicatiion.UserServiceContracts.Command.Dto
{
    public class RegisteredDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }
    }
}
