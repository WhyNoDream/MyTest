using Applicatiion.UserServiceContracts.Command;
using Applicatiion.UserServiceContracts.Command.Dto;
using CommonConfBus;
using Domain.User.Events;
using MediatR;
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
        private readonly IRegisteredCommand  _registered;
        private readonly IMediator _mediator;

        public LoginController(IUserLoginCommand userLogin, IRegisteredCommand registered, IMediator mediator)
        {
            _iUserLogin = userLogin;
            _registered = registered;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<LoginDto> Login(string account, string password)
        {
            return await _iUserLogin.Login(account, password);
        }


        /// <summary>
        /// 注册
        /// 
        /// </summary>
        /// <param name="registeredDto"></param>
        /// <returns></returns>
        [HttpPost("Registered")]
        public async Task<bool> Registered([FromBody] RegisteredDto registeredDto)
        {
            return await _registered.Registered(registeredDto);
        }
    }
}
