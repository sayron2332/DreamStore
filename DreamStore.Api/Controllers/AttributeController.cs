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
    public class AttributeController(IAttributeService attributeService) : ControllerBase
    {
        private readonly IAttributeService _attributeService = attributeService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AttributeDto attribute)
        {
            AttributeValidation validator = new AttributeValidation();
            ValidationResult validationResult = await validator.ValidateAsync(attribute);
            if (validationResult.IsValid)
            {
                var result = await _attributeService.Create(attribute);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(validationResult.Errors);
        }
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _attributeService.DeletebyId(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _attributeService.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            var result = await _attributeService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] AttributeDto attribute)
        {
            AttributeValidation validator = new AttributeValidation();
            ValidationResult validationResult = await validator.ValidateAsync(attribute);
            if (validationResult.IsValid)
            {
                attribute.Id = Id;
                var result = await _attributeService.Update(attribute);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(validationResult.Errors);

        }
    }
}
