using DreamStore.Core.Dtos.User;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Validation.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamStore.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IJwtService jwtService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost("login")]
        public async Task<IActionResult> SignInUser(SignInUserDto model)
        { 
           SignInUserValidation validations = new SignInUserValidation();
           ValidationResult validationResult = await validations.ValidateAsync(model);
           if (validationResult.IsValid)
           {
               var result = await _authService.LoginUser(model);
               if (result.Success)
               {
                   return Ok(result);
               }
               return BadRequest(result);
           }
           return BadRequest(validationResult.Errors);
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUpUser(SignUpUserDto model)
        {
            SignUpUserValidation validations = new SignUpUserValidation();
            ValidationResult validationResult = await validations.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _authService.RegisterUser(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(validationResult.Errors);
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _jwtService.RefreshTokensAsync(refreshToken);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
