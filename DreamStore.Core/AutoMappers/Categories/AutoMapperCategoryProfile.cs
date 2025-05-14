using AutoMapper;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.AutoMappers.Categories
{
    internal class AutoMapperCategoryProfile : Profile
    {
        public AutoMapperCategoryProfile()
        {
            CreateMap<AppCategory, CategoryDto>().ReverseMap();
        }
    }
}
