using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Validation.Attribute;
using DreamStore.Core.Validation.Category;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryDto category)
        { 
         
             var result = await _categoryService.Create(category);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
         
        }
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _categoryService.DeletebyId(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{Id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _categoryService.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            var result = await _categoryService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody]CategoryDto category)
        {
           
                category.Id = Id;
                var result = await _categoryService.Update(category);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
         
        }

    }
}
