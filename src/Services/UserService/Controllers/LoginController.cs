using CommonConfBus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceContracts;
using UserServiceContracts.Dto;
using Volo.Abp.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : AbpController
    {
        private readonly IUserLoginCommand _iUserLogin;

        public LoginController(IUserLoginCommand userLogin)
        {
            _iUserLogin = userLogin;
        }

        [HttpGet]
        public async Task<LoginDto> Login(string account, string password)
        {
            return await _iUserLogin.Login(account, password);
        }
    }
}
