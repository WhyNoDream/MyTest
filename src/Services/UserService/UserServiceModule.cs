using Domain.User;
using EFCore.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace UserService
{

    [DependsOn(typeof(UserDoaminModule), typeof(UserEFCoreModule)
    )]
    public class UserServiceModule : AbpModule
    {

    }
}
