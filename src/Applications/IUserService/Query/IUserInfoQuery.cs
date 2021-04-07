using Applicatiion.UserServiceContracts.Query.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserServiceContracts
{
    public interface IUserQuery: IApplicationService
    {
        Task<List<GetUserDto>> GetUsers(int pageIndex, int pageSize);
        Task<GetUserDto> GetUser(long id);
    }
}
