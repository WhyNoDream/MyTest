using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Domain.User.Entitys
{
    public class Role:Entity<long>
    {
        public string Name { get; set; }
    }
}
