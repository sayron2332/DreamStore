using DreamStore.Core.Dtos.Product;
using DreamStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResponse> Create(CreateUpdateProductDto model);
        public Task<ServiceResponse> Update(CreateUpdateProductDto model);
        public Task<ProductDto?> GetById(int Id);
        public Task<ServiceResponse> DeleteById(int Id);
        public Task<ServiceResponse> GetAll(int pageIndex);
    }
}
