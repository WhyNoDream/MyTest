﻿using Applicatiion.UserServiceContracts.Query.Dto;
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
            CreateMap<GetUserInfoDto, LoginDto>();
            CreateMap<User, LoginDto>();
            //CreateMap<LoginDto,User>().ForMember(o=>o.ConcurrencyStamp,option=>option.Ignore())
            //    .ForMember(o => o.ExtraProperties, option => option.Ignore());
        }
    }
}