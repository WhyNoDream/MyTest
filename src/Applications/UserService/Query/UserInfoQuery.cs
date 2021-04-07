using Applicatiion.UserServiceContracts.Query.Dto;
using Domain.User.Entitys;
using Domain.User.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceContracts;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Applicatiion.UserService.Query
{
    public class UserInfoQuery : ApplicationService, IUserInfoQuery
    {

        private readonly IRepository<User, long> _userRepository;
        private readonly IUserRepository _userInfoRepository;
        private readonly IObjectMapper<UserApplicationServiceModule> _mapper;
        

        public UserInfoQuery(IRepository<User, long> userRepository, IUserRepository userInfoRepository, IObjectMapper<UserApplicationServiceModule> mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userInfoRepository = userInfoRepository;
        }
        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetUserInfoDto> GetUserInfo(long id)
        {
            try
            {
                var userInfo =await _userInfoRepository.GetAsync(o => o.Id == 1,false);
                //var userInfo =  _userRepository.WithDetails(o=>o.UserRole).FirstOrDefault(o => o.Id == 1);
                return _mapper.Map<User, GetUserInfoDto>(userInfo);
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
        public async Task<List<GetUserInfoDto>> GetUserInfos(int pageIndex,int pageSize)
        {
            var userInfos=await _userRepository.GetPagedListAsync(pageIndex, pageSize, "Id");
            return _mapper.Map<List<User>,List<GetUserInfoDto>>(userInfos);
        }
    }
}
