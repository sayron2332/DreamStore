using DreamStore.Core.Dtos.User;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Services;
using DreamStore.Core.Validation.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class UsersController(IUserService userService,IRoleService roleService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto model)
        {
            CreateUserValidation validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.Create(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest(validationResult.Errors);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse result = await _userService.DeleteById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            var result = await _userService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromForm]UpdateUserDto model)
        {
            UpdateUserValidation validator = new UpdateUserValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                model.Id = id;
                var result = await _userService.Update(model);
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
