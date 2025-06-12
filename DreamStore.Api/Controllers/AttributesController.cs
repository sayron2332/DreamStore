using DreamStore.Core.Dtos.Attribute;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Services;
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
    [Authorize(Roles ="admin")]
    public class AttributesController(IAttributeService attributeService) : ControllerBase
    {
        private readonly IAttributeService _attributeService = attributeService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeDto attribute)
        {
          
                var result = await _attributeService.Create(attribute);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
         
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attributeService.DeletebyId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _attributeService.GetById(id);
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
            var result = await _attributeService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttributeDto attribute)
        {
             attribute.Id = id;
             var result = await _attributeService.Update(attribute);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
        }
    }
}
