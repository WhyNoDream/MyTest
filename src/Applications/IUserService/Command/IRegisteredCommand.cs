using Applicatiion.UserServiceContracts.Command.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Applicatiion.UserServiceContracts.Command
{
    public interface IRegisteredCommand: IApplicationService
    {
        Task<bool> Registered(RegisteredDto registeredDto);
    }
}
