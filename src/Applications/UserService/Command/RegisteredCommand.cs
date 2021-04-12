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
using Applicatiion.UserServiceContracts.Command.Dto;
using Applicatiion.UserServiceContracts.Command;
using Domain.User.IRepositories;

namespace Applicatiion.UserService.Command
{
    public class RegisteredCommand : ApplicationService, IRegisteredCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IObjectMapper<UserApplicationServiceModule> _mapper;

        public RegisteredCommand(IUserRepository userRepository, IObjectMapper<UserApplicationServiceModule> mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        /// <summary>
        /// 注册
        /// 
        /// </summary>
        /// <param name="registeredDto"></param>
        /// <returns></returns>
        public async Task<bool> Registered(RegisteredDto registeredDto)
        {
            try
            {
                var user = _userRepository.FirstOrDefault(o => o.Account == registeredDto.Account);
                if (user != null)
                {
                    throw new Exception("用户已存在");
                }
                var userMapEntity= _mapper.Map<RegisteredDto, User>(registeredDto);
                userMapEntity.Registered();
                await _userRepository.Save(userMapEntity);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            throw new Exception("用户名密码不正确");
        }
    }
}
