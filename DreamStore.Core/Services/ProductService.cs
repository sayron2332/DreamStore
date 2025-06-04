using AutoMapper;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Dtos.Product;
using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Product;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace DreamStore.Core.Services
{
    internal class ProductService : IProductService
    {
        private readonly IRepository<AppProduct> _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _config;
        public ProductService(IRepository<AppProduct> productRepo, IMapper mapper,
            ILogger<ProductService> logger, IConfiguration config)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }
        public async Task<ServiceResponse> Create(CreateProductDto model)
        {
            var mappedProduct = _mapper.Map<AppProduct>(model);
            if (model.Photo != null)
            {
                try
                {
                    using var image = await Image.LoadAsync(model.Photo.OpenReadStream());
                }
                catch (UnknownImageFormatException)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Bad photo format"
                    };
                }
                var ProductImageFolder = _config.GetValue<string>("ImageSettings:ProductImage");

                if (mappedProduct.ImageName != "default.png" && ProductImageFolder != null)
                {
                    string oldImage = Path.Combine(Directory.GetCurrentDirectory(), ProductImageFolder, mappedProduct.ImageName);
                    File.Delete(oldImage);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), ProductImageFolder!);


                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

                mappedProduct.ImageName = fileName;
            }
            try
            {
                await _productRepo.Insert(mappedProduct);
                await _productRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Product created"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create and save new Product with name {name}", model.Name);
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some  problem with create Product"
                };
            }
        }

        public async Task<ServiceResponse> DeleteById(int Id)
        {
            try
            {
                await _productRepo.Delete(Id);
                await _productRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Product Deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Delete Product with Id {Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with delete Product"
                };
            }
        }

        public async Task<ServiceResponse> GetAll(int pageIndex)
        {
            int pageSize = 10;
            int skip = (pageIndex - 1) * pageSize;
            IEnumerable<AppProduct> result =
                await _productRepo.GetListBySpec(new ProductSpecification.GetListByPagination(skip, pageSize));

            if (result == null || !result.Any())
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with pageIndex or database or table empty"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Payload = result.Select(a => _mapper.Map<ProductDto>(a))
            };
        }

        public async Task<ProductDto?> GetById(int Id)
        {
            return _mapper.Map<ProductDto>(await _productRepo.GetByID(Id));
        }

        public Task<ServiceResponse> Update()
        {
            throw new NotImplementedException();
        }
    }
}
