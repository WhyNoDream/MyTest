using ABPEFCoreMySqlUnit;
using Domain.User.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EFCore.User.Repository
{
    public class UserRepository : BaseRepository<UserDbContext, Domain.User.Entitys.User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<UserDbContext> dbContextProvider, IMediator mediator) : base(dbContextProvider, mediator)
        {

        }
    }
}
