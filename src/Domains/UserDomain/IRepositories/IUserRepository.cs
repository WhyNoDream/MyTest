using System;
using System.Collections.Generic;
using System.Text;
using Domain.User.Entitys;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Domain.User.IRepositories
{
    public interface IUserRepository : IRepository<Domain.User.Entitys.User, long>, ITransientDependency
    {

    }
}
