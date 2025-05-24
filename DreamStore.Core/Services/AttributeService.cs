using AutoMapper;
using DreamStore.Core.Dtos.Attribute;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Product;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IRepository<AppAttribute> _attributeRepo;
        private readonly ILogger<AttributeService> _logger;
        private readonly IMapper _mapper;
        public AttributeService(IRepository<AppAttribute> attributeRepo, ILogger<AttributeService> logger,
             IMapper mapper)
        {
            _attributeRepo = attributeRepo;
            _logger = logger;
            _mapper = mapper;
        }
    
        public async Task<ServiceResponse> Create(AttributeDto model)
        {
            try
            {
                await _attributeRepo.Insert(new AppAttribute { Name = model.Name });
                await _attributeRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "attribute created"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Create new Attribute with Name {model.Name}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "attribute with this name exist or some problem with database insert"
                };

            }
            
        }

        public async Task<ServiceResponse> DeletebyId(int Id)
        {
            try
            {
                await _attributeRepo.Delete(Id);
                await _attributeRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "attribute Deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Deleted attribute with Id: {Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with delete attribute"
                };
            }
        }

        public async Task<ServiceResponse> GetAll(int pageIndex)
        {
            int pageSize = 10;
            int skip = (pageIndex - 1) * pageSize;
            IEnumerable<AppAttribute> result =
                await _attributeRepo.GetListBySpec(new AttributeSpecification.GetListByPagination(skip, pageSize));

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
                Payload = result.Select(a => _mapper.Map<AttributeDto>(a))
            };
        }

        public async Task<AppAttribute?> GetById(int Id)
        {
            return await _attributeRepo.GetByID(Id);
        }

        public async Task<ServiceResponse> Update(AttributeDto model)
        {
            var oldModel = await _attributeRepo.GetByID(model.Id);
            if (oldModel == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "old attribute not found"
                };
            }
            oldModel.Name = model.Name;
            try
            {
                await _attributeRepo.Update(oldModel);
                await _attributeRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "attribute Updated"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update attribute with Id {oldModel.Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with update attribute"
                };
            }
        }
    }
}
