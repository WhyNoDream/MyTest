using Domain.User.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EFCore.User.Repository
{
    public class UserRepository : EfCoreRepository<UserDbContext, Domain.User.Entitys.User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<UserDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
