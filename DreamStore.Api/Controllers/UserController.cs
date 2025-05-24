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
    [Authorize]
    public class UserController(IUserService userService,IRoleService roleService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
           var result = await _roleService.GetAll();
           if (result.Success)
           {
                return Ok(result);
           }
            return BadRequest(result);
        } 

        [HttpPost("create")]
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
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            ServiceResponse result = await _userService.DeleteById(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var user = await _userService.GetById(Id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int pageIndex)
        {
            var result = await _userService.GetAll(pageIndex);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromForm]UpdateUserDto model)
        {
            UpdateUserValidation validator = new UpdateUserValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
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
