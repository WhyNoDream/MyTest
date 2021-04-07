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
using Volo.Abp.ObjectMapping;

namespace Applicatiion.UserService
{
    public class UserLoginCommand : ApplicationService, IUserLoginCommand
    {

        //private readonly IUserRolesRepository _userRepository;
        private readonly IRepository<User, long> _userRepository; 
        private readonly IObjectMapper<UserApplicationServiceModule> _mapper;


        public UserLoginCommand(IRepository<User, long> userRepository, IObjectMapper<UserApplicationServiceModule> mapper)
        {
            _mapper = mapper;
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
            try
            {
                var User = _userRepository.FirstOrDefault(o => o.Account == account);
                if (User == null)
                {
                    throw new Exception("用户不存在");
                }
                if (User.Login(account, password))
                {
                    return _mapper.Map<User, LoginDto>(User);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            throw new Exception("用户名密码不正确");
        }
    }
}
