using AutoMapper;
using Domain.User.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserServiceContracts.Dto
{
    [AutoMap(typeof(User))]
    public class LoginDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; private set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }
    }
}
