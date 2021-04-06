//using AutoMapper;
using UserServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceContracts.Dto;
using Volo.Abp.Application.Services;
using Domain.User.Entitys;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using System.Linq;
using Applicatiion.UserServiceContracts.Query.Dto;

namespace Applicatiion.UserService
{
    public class UserLoginCommand : ApplicationService, IUserLoginCommand
    {

        //private readonly IUserRolesRepository _userRepository;
        private readonly IRepository<User, long> _userRepository; 
        

        public UserLoginCommand(IRepository<User, long> userRepository)
        {
            //_mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<LoginDto> Login(string account, string password)
        {

            GetUserInfoDto getUserInfoDto = new GetUserInfoDto() { Account = "11", Email="11", Id=1, Name="1", Phone="1" };
            ObjectMapper.Map<GetUserInfoDto, LoginDto>(getUserInfoDto);
            var userInfo =  _userRepository.FirstOrDefault(o => o.Account == account);
            if (userInfo == null)
            {
                throw new Exception("用户不存在");
            }
            if (userInfo.Login(account, password))
            {
                return ObjectMapper.Map<User, LoginDto>(userInfo);
            }
            throw new Exception("用户名密码不正确");
        }
    }
}
