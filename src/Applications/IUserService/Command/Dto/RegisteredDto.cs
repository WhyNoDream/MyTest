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
        public string Account { get;  set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get;  set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get;  set; }

        public string Phone { get;  set; }

        public string Email { get;  set; }
    }
}
