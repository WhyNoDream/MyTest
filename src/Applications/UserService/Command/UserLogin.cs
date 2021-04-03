using AutoMapper;
using UserServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceContracts.Dto;
using Volo.Abp.Application.Services;
using Domain.User.Entitys;
using Volo.Abp.Domain.Repositories;
using Domain.User.IRepositories;
using Volo.Abp.Uow;

namespace Applicatiion.UserService
{
    [UnitOfWork]
    public class UserLogin : ApplicationService, IUserLogin
    {

       // private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        //private readonly IRepository<User,long> _userRepository; 
        
        //IMapper mapper,

        public UserLogin(IUserRepository  userRepository)
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
            var userInfo = _userRepository.GetDbContext();//  (o => o.Account == account);
            if (userInfo == null)
            {
                throw new Exception("用户不存在");
            }
            //if (userInfo.Login(account, password))
            //{
            //    // return _mapper.Map<LoginDto>(userInfo);
            //    return new LoginDto();
            //}
            throw new Exception("用户名密码不正确");
        }
    }
}
