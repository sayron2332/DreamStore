using AutoMapper;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DreamStore.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<AppCategory> _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IRepository<AppCategory> categoryRepo, IMapper mapper,
             ILogger<CategoryService> logger)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse> Create(CategoryDto model)
        {
            var category = await GetByName(model.Name);
            if (category != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category already exist"
                };
            }

            try
            {
                await _categoryRepo.Insert(new AppCategory { Name = model.Name });
                await _categoryRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Category created"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create and save new category with name {name}", model.Name);
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some  problem with create category"
                };
            }

        }
        public async Task<AppCategory?> GetByName(string name)
        {
            return await _categoryRepo.GetItemBySpec(new CategorySpecification.GetByName(name)); 
        }
        public async Task<ServiceResponse> DeletebyId(int Id)
        {
            try
            {
                await _categoryRepo.Delete(Id);
                await _categoryRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Category Deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Deleted with Id {Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with delete category"
                };
            }
        }
        public async Task<ServiceResponse> GetAll(int pageIndex)
        {
            int pageSize = 10;
            int skip = (pageIndex - 1) * pageSize;
            IEnumerable<AppCategory> result =
                await _categoryRepo.GetListBySpec(new CategorySpecification.GetListByPagination(skip, pageSize));

            if (result == null || !result.Any())
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with pageIndex or database"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Payload = result.Select(a => _mapper.Map<CategoryDto>(a))
            };
        }
        public async Task<AppCategory?> GetById(int Id)
        {
            return await _categoryRepo.GetByID(Id);
        }
        public async Task<ServiceResponse> Update(CategoryDto updatedModel)
        {
            var oldModel = await _categoryRepo.GetByID(updatedModel.Id);
            if (oldModel == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "old category not found"
                };
            }
            oldModel.Name = updatedModel.Name;
            try
            {
                await _categoryRepo.Update(oldModel);
                await _categoryRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Category Updated"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update category with Id {oldModel.Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with update category"
                };
            }
          
        }
    }
}
