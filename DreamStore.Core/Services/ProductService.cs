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
        public async Task<ServiceResponse> Create(CreateUpdateProductDto model)
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
            AppProduct? product = await _productRepo.GetByID(Id);
            if (product is null)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "product not found some problem with Id",
                };
            }

            if (product.ImageName != "default.png")
            {
                var productImageFolder = _config.GetValue<string>("ImageSettings:ProductImage");
                string image = Path.Combine(Directory.GetCurrentDirectory(), productImageFolder!, product.ImageName);
                File.Delete(image);
            }
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

        public async Task<ServiceResponse> Update(CreateUpdateProductDto model)
        {
            AppProduct? oldProduct = await _productRepo.GetByID(model.Id);
            if (oldProduct == null)
            {
                return new ServiceResponse 
                {
                    Success = false,
                    Message = "Product not found"
                };
            }
          
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

                if (oldProduct.ImageName != "default.png" && ProductImageFolder != null)
                {
                    string oldImage = Path.Combine(Directory.GetCurrentDirectory(), ProductImageFolder, oldProduct.ImageName);
                    File.Delete(oldImage);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), ProductImageFolder!);


                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

                oldProduct = _mapper.Map<AppProduct>(model);
                oldProduct.ImageName = fileName;
            }
       
            try
            {
                await _productRepo.Update(oldProduct);
                await _productRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Product Updated"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update  Product with name {name}", model.Name);
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with Update Product"
                };
            }
        }
    }
}
