using AutoMapper;
using DreamStore.Core.Dtos.Category;
using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;


namespace DreamStore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<AppUser> _userRepo;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IRoleService _roleService;
        public UserService( IRepository<AppUser> userRepo, IRoleService roleService,
             ILogger<UserService> logger, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _roleService = roleService;
        }

        public async Task<AppUser?> GetByEmail(string email)
        {
            return await _userRepo.GetItemBySpec(new UserSpecification.FindByEmail(email));
        }
        public async Task<ServiceResponse> Create(CreateUserDto user)
        {
            var mappedUser = _mapper.Map<AppUser>(user);
            mappedUser.PasswordHash =
              new PasswordHasher<AppUser>().HashPassword(mappedUser, user.Password);

            if (user.Photo != null)
            {
                try
                {
                    using var image = await Image.LoadAsync(user.Photo.OpenReadStream()); 
                }
                catch (UnknownImageFormatException)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Bad photo format"
                    };
                }
                var userImageFolder = _config.GetValue<string>("ImageSettings:UserImage");

                if (mappedUser.ImageName != "default.png" && userImageFolder != null)
                {
                    string oldImage = Path.Combine(Directory.GetCurrentDirectory(), userImageFolder, mappedUser.ImageName);
                    File.Delete(oldImage);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(user.Photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), userImageFolder!);


                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    user.Photo.CopyTo(fileStream);
                }

                mappedUser.ImageName = fileName;
            }

            try
            {
                await _userRepo.Insert(mappedUser);
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
        public async Task<UserDto?> GetById(int Id)
        {
            var user = await _userRepo.GetByID(Id);
            if (user != null)
            {
                var mappedUser = _mapper.Map<UserDto>(user);
                return mappedUser;
            }
           return null;
        }
        internal async Task<AppUser?> GetAppUserById(int Id)
        {
            return await _userRepo.GetByID(Id);
        }
        public async Task<ServiceResponse> DeleteById(int Id)
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
        public async Task<ServiceResponse> Update(UpdateUserDto updateUser)
        {
            AppUser? user = await _userRepo.GetByID(updateUser.Id);
           
            if (user is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found",
                };
            }
            if (updateUser.Photo != null)
            {
               
                try
                {
                    using var image = await Image.LoadAsync(updateUser.Photo.OpenReadStream());
                }
                catch (UnknownImageFormatException )
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Bad photo format"
                    };
                }
                var userImageFolder = _config.GetValue<string>("ImageSettings:UserImage");

                if (user.ImageName != "default.png" && userImageFolder != null)
                {
                    string oldImage = Path.Combine(Directory.GetCurrentDirectory(),  userImageFolder, user.ImageName);
                    File.Delete(oldImage);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateUser.Photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(),  userImageFolder!);


                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    updateUser.Photo.CopyTo(fileStream);
                }

                user.ImageName = fileName;
            }
            var currentRole = await _roleService.GetById(user.RoleId);
            if (currentRole == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "this role now exist in database or Id incorect",
                };
            }
            if (updateUser.RoleId != currentRole?.Id)
            {
                user.RoleId = updateUser.Id;
            }
            user.Email = updateUser.Email;
            user.Name = updateUser.Name;
            user.Surname = updateUser.Surname;
            user.PhoneNumber = updateUser.PhoneNumber;

            try
            {
                await _userRepo.Update(user);
                await _userRepo.Save();
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User succsesfly Updated if you change your Email please Verify",
                };
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update User with Id {user.Id}");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "some problem with update user",
                };
            }
        }
        public async Task<ServiceResponse> GetAll(int pageIndex)
        {
            int take = 10;
            int skip = (pageIndex - 1) * take;
            IEnumerable<AppUser> result =
              await _userRepo.GetListBySpec(new UserSpecification.GetListByPagination(skip, take));

            if (result == null || !result.Any())
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Some problem with pageIndex or database"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Payload = result.Select(a => _mapper.Map<UserDto>(a))
            };

        }

       
    }
    
}
