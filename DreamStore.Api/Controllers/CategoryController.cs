﻿using DreamStore.Core.Dtos.Category;
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
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CategoryDto category)
        { 
            CategoryValidation validator = new CategoryValidation();
            ValidationResult validationResult = await validator.ValidateAsync(category);
            if (validationResult.IsValid)
            {
                var result = await _categoryService.Create(category);
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
            var result = await _categoryService.DeletebyId(Id);
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
            var result = await _categoryService.GetById(Id);
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
            var result = await _categoryService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]CategoryDto category)
        {
            CategoryValidation validator = new CategoryValidation();
            ValidationResult validationResult = await validator.ValidateAsync(category);
            if (validationResult.IsValid)  
            {
                category.Id = Id;
                var result = await _categoryService.Update(category);
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
