using DreamStore.Core.Dtos.User;
using DreamStore.Core.Entites;
using DreamStore.Core.Services;
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
        public Task<ServiceResponse> Create(AppUser user);
        public Task<AppUser?> GetById(int Id);
        public Task<ServiceResponse> DeletebyId(int Id);
    }
}
