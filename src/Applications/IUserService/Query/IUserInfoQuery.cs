using Applicatiion.UserServiceContracts.Query.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserServiceContracts
{
    public interface IUserInfoQuery: IApplicationService
    {
        Task<List<GetUserInfoDto>> GetUserInfos(int pageIndex, int pageSize);
        Task<GetUserInfoDto> GetUserInfo(long id);
    }
}
