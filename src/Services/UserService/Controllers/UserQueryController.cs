using Applicatiion.UserServiceContracts.Query.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceContracts;
using Volo.Abp.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserQueryController : AbpController
    {
        private readonly IUserQuery _UserQuery;

        public UserQueryController(IUserQuery  UserQuery)
        {
            _UserQuery = UserQuery;
        }

        [HttpGet]
        public async Task<List<GetUserDto>> GetUsers(int pageIndex, int pageSize)
        {
            return await _UserQuery.GetUsers(pageIndex,pageSize);
        }

        [HttpGet("GetUser")]
        public async Task<GetUserDto> GetUser(long id)
        {
            return await _UserQuery.GetUser(id);
        }
    }
}
