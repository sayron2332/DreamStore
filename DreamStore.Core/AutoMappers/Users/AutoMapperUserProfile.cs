using AutoMapper;
using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.AutoMappers.Users
{
    internal class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<CreateUserDto, AppUser>();
            CreateMap<AppUser, UserDto>();
            CreateMap<SignUpUserDto, CreateUserDto>();
        }
    }
}
