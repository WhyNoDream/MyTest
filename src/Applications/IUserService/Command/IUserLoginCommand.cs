using System;
using System.Threading.Tasks;
using UserServiceContracts.Dto;
using Volo.Abp.Application.Services;

namespace UserServiceContracts
{
    public interface IUserLoginCommand: IApplicationService
    {
        Task<LoginDto> Login(string account, string password);
    }
}
