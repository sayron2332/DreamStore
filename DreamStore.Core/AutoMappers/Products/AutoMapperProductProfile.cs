using AutoMapper;
using DreamStore.Core.Dtos.Product;
using DreamStore.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.AutoMappers.Products
{
    internal class AutoMapperProductProfile : Profile
    {
        public AutoMapperProductProfile()
        {
            CreateMap<CreateUpdateProductDto, AppProduct>();
            CreateMap<AppProduct, ProductDto>().ReverseMap();
        }
    }
}
