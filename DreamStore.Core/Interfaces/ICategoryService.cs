using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Entites;
using DreamStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<ServiceResponse> Create(CategoryDto model);
        public Task<ServiceResponse> Update(CategoryDto updatedModel);
        public Task<ServiceResponse> DeletebyId(int Id);
        public Task<AppCategory?> GetById(int Id);
        public Task<AppCategory?> GetByName(string name);
        public Task<ServiceResponse> GetAll(int pageIndex);

    }
}
