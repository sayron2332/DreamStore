
using AutoMapper;
using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RozetkaBackEnd.Core.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRoleService _roleService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AuthService(IJwtService jwtService, IRoleService roleService, IMapper mapper,
             IUserService userService)
        {
            
            _jwtService = jwtService;
            _roleService = roleService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResponse> LoginUser(SignInUserDto model)
        {
            var user = await _userService.GetByEmail(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found or email or password incorrect",
                };
            }

            var result = new PasswordHasher<AppUser>()
                .VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new ServiceResponse
                {
                    Message = "Verefication failed pleas check your password",
                    Success = false,
                };

            }
           

            TokensDto tokens = await _jwtService.CreateTokenResponse(user);
            return new ServiceResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                Message = "Logged in successfully",
                Success = true,
            };
        }

        public async Task<ServiceResponse> RegisterUser(SignUpUserDto model)
        {
            var user = await _userService.GetByEmail(model.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User Already exist"
                };
            }

            AppUser newUser = _mapper.Map<AppUser>(model);
            newUser.PasswordHash =
                new PasswordHasher<AppUser>().HashPassword(newUser, model.Password);

            var role = await _roleService.GetRoleByNameAsync("user");
            if (role != null) 
            {
                newUser.RoleId = role.Id;
            }

          
            ServiceResponse result = await _userService.Create(newUser);
            if (result.Success)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User succsesfely created"
                };
            }

            return result;

        }
    }
}
