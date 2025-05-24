using DreamStore.Core.Dtos.Attribute;
using DreamStore.Core.Entites.Product;
using DreamStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IAttributeService
    {
        public Task<ServiceResponse> Create(AttributeDto model);
        public Task<ServiceResponse> DeletebyId(int Id);
        public Task<AppAttribute?> GetById(int Id);
        public Task<ServiceResponse> GetAll(int pageIndex);
        public Task<ServiceResponse> Update(AttributeDto model);
    }
}
