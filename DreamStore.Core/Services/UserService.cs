using AutoMapper;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using RozetkaBackEnd.Core.Dtos.Token;


namespace DreamStore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<AppUser> _userRepo;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService( IRepository<AppUser> userRepo,
             ILogger<UserService> logger, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AppUser?> GetByEmail(string email)
        {
            return await _userRepo.GetItemBySpec(new UserSpecification.FindByEmail(email)); ;
        }
        public async Task<ServiceResponse> Create(AppUser user)
        {
            try
            {
                await _userRepo.Insert(user);
                await _userRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User Created"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred with create user");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with create user"
                };
            }
          
        }
        public async Task<AppUser?> GetById(int Id)
        {
            return await _userRepo.GetByID(Id);
        }
        public async Task<ServiceResponse> DeletebyId(int Id)
        {
            try
            {
                await _userRepo.Delete(Id);
                await _userRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User Deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Delete User with Id {Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with delete User"
                };
            }
        }
        
    }
    
}
