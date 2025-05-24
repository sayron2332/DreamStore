using AutoMapper;
using DreamStore.Core.Dtos.Attribute;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.AutoMappers.Attributes
{
    internal class AutoMapperAttributeProfile : Profile
    {
        public AutoMapperAttributeProfile()
        {
            CreateMap<AppAttribute, AttributeDto>().ReverseMap();
        }
    }
}
