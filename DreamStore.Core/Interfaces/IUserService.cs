using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using DreamStore.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser?> GetByEmail(string email);
        public Task<ServiceResponse> Create(CreateUserDto user);
        public Task<ServiceResponse> DeleteById(int Id);
        public Task<ServiceResponse> GetAll(int pageIndex);
        public Task<ServiceResponse> Update(UpdateUserDto model);
        public Task<UserDto?> GetById(int Id);
    }
}
