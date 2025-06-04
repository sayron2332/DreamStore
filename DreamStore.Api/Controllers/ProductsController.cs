using DreamStore.Core.Dtos.Product;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Validation.Product;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto model)
        { 
            CreateProductValidation validations = new CreateProductValidation();
            ValidationResult validationResult = await validations.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _productService.Create(model);
                if (result.Success)
                { 
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(validationResult.Errors);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            var result = await _productService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
