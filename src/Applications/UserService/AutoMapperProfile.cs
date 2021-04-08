using Applicatiion.UserServiceContracts.Command.Dto;
using Applicatiion.UserServiceContracts.Query.Dto;
using AutoMapper;
using Domain.User.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using UserServiceContracts.Dto;

namespace Applicatiion.UserService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<List<User>, List<GetUserDto>>().ReverseMap();
            CreateMap<User, RegisteredDto>().ReverseMap();
        }
    }
}
