﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Domain.User.Entitys
{
    /// <summary>
    /// 人员角色对应
    /// </summary>
    public class UserRoles:Entity<long>
    {
        private UserRoles()
        {

        }
        public UserRoles(long userForId, long roleId)
        {
            this.UserId = userForId;
            this.RoleId = roleId;
        }
        public long UserId { get; private set; }
        public long RoleId { get; private set; }
    }
}
