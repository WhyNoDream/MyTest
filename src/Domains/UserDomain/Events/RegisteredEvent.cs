using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.User.Events
{
    /// <summary>
    /// 注册完成事件
    /// </summary>
    public class RegisteredEvent: INotification
    {
        public RegisteredEvent(long id,string Account, string Password, string Name, string Phone, string Email)
        {
            this.Account = Account;
            this.Password = Password;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
            //this.UserRole = new List<UserRoles>() ;
        }
        public long Id { get; set; }

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
