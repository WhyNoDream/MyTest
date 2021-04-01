using System;
using System.Collections.Generic;
using System.Text;
using UserDomain.Entitys;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace UserDomain.IRepositories
{
    public interface IUserRepository : IRepository<User, long>, ITransientDependency
    {

    }
}
