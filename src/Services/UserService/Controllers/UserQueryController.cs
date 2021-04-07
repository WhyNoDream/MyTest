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
        private readonly IUserInfoQuery _userInfoQuery;

        public UserQueryController(IUserInfoQuery  userInfoQuery)
        {
            _userInfoQuery = userInfoQuery;
        }

        [HttpGet]
        public async Task<List<GetUserInfoDto>> GetUserInfos(int pageIndex, int pageSize)
        {
            return await _userInfoQuery.GetUserInfos(pageIndex,pageSize);
        }

        [HttpGet("GetUserInfo")]
        public async Task<GetUserInfoDto> GetUserInfo(long id)
        {
            return await _userInfoQuery.GetUserInfo(id);
        }
    }
}
