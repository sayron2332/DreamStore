using DreamStore.Core.Entites;
using DreamStore.Core.Interfaces;
using DreamStore.Core.Specification;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IRepository<AppRole> _roleRepo;
        private readonly IMemoryCache _cache;
         
        public RoleService(IRepository<AppRole> roleRepo, IMemoryCache cache)
        {
            _cache = cache;
            _roleRepo = roleRepo;
        }
        public async Task<AppRole?> GetById(int Id)
        {
            return await _roleRepo.GetByID(Id);
        }
        public async Task<AppRole?> GetRoleByNameAsync(string roleName)
        {
            if (!_cache.TryGetValue(roleName, out AppRole? role))  
            {
                role = await _roleRepo.GetItemBySpec(new RoleSpecification.GetByName(roleName));

                if (role != null)
                {
                    _cache.Set(roleName, role, TimeSpan.FromHours(1));
                }
            }

            return role;
        }
        public async Task<ServiceResponse> GetAll()
        {
            return new ServiceResponse
            {
                Success = true,
                Payload = await _roleRepo.GetAll(),
                Message = "All roles Load"
            };
        }
    }
}
