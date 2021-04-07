using System;
using System.Collections.Generic;
using System.Text;

namespace Applicatiion.UserServiceContracts.Query.Dto
{
    public class GetUserDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
