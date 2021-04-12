using System;
using System.Collections.Generic;
using System.Text;
using ABPUnit;
using Domain.User.Entitys;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Domain.User.IRepositories
{

    public interface IUserRepository : IBaseRepository<Domain.User.Entitys.User, long>
    {
    }
}
