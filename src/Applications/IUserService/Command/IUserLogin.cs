using System;
using System.Threading.Tasks;
using UserServiceContracts.Dto;

namespace UserServiceContracts
{
    public interface IUserLogin
    {
        Task<LoginDto> Login(string account, string password);
    }
}
