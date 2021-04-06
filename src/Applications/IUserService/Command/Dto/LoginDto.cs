using System;
using System.Collections.Generic;
using System.Text;

namespace UserServiceContracts.Dto
{
    public class LoginDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get;  set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get;  set; }

        public string Phone { get;  set; }

        public string Email { get;  set; }
    }
}
