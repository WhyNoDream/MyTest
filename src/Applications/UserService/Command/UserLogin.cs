using AutoMapper;
using UserServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDomain.IRepositories;
using UserServiceContracts.Dto;
using Volo.Abp.Application.Services;

namespace UserService
{
    public class UserLogin : ApplicationService, IUserLogin
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserLogin(IMapper mapper, IUserRepository userRepository)
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
            var userInfo=  await _userRepository.GetAsync(o => o.Account == account);
            if (userInfo == null)
            {
                throw new Exception("用户不存在");
            }
            if (userInfo.Login(account, password))
            {
                return _mapper.Map<LoginDto>(userInfo);
            }
            throw new Exception("用户名密码不正确");
        }
    }
}
