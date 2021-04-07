using Applicatiion.UserServiceContracts.Query.Dto;
using Domain.User.Entitys;
using Domain.User.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using UserServiceContracts;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Applicatiion.UserService.Query
{
    public class UserQuery : ApplicationService, IUserQuery
    {

        private readonly IRepository<User, long> _userRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IObjectMapper<UserApplicationServiceModule> _mapper;
        

        public UserQuery(IRepository<User, long> userRepository, IUserRepository UserRepository, IObjectMapper<UserApplicationServiceModule> mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _UserRepository = UserRepository;
        }
        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetUserDto> GetUser(long id)
        {
            try
            {
                var user = _UserRepository.WithDetails(o=>o.UserRole).FirstOrDefault(o => o.Id == 1);
                //var User =  _userRepository.WithDetails(o=>o.UserRole).FirstOrDefault(o => o.Id == 1);
                return _mapper.Map<User, GetUserDto>(user);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取分页用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetUserDto>> GetUsers(int pageIndex,int pageSize)
        {
            var users=await _userRepository.GetPagedListAsync(pageIndex, pageSize, "Id");
            return _mapper.Map<List<User>,List<GetUserDto>>(users);
        }
    }
}
