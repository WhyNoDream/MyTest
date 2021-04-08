using ABPUnit;
using CommonUnit.Encryption;
using CommonUnit.StringUnit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.Domain.Entities;

namespace Domain.User.Entitys
{
    /// <summary>
    /// 用户聚合根
    /// </summary>
    public class User : BaseAggregateRoot<long>
    {
        #region 构造函数
        private User()
        {

        }

        public User(string Account, string Password, string Name, string Phone, string Email)
        {
            this.Account = Account;
            this.Password = Password;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
            //this.UserRole = new List<UserRoles>() ;
        }
        #endregion

        #region 基础属性
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

        /// <summary>
        /// 用户角色映射
        /// </summary>
        public List<UserRoles> UserRole { get; private set; }
        #endregion

        #region 行为与业务

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string account,string password)
        {
            var pwdEncry = PasswordEncryHelper.PwdEncry(password, "password");
            if (account == this.Account && pwdEncry == this.Password)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public bool Registered()
        {
            //业务
            if(CheckHelper.CheckPhone(this.Phone))
            {
                throw new Exception("手机号格式不正确");
            }


            //添加注册完成领域事件
            AddEventHandler(JsonConvert.SerializeObject(this), this);
            return true;
        }
        #endregion



        #region 领域事件


        #endregion

    }
}
