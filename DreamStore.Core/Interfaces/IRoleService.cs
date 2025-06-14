﻿using DreamStore.Core.Entites;
using DreamStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IRoleService
    {
        public Task<AppRole?> GetById(int Id);
        public Task<AppRole?> GetRoleByNameAsync(string roleName);
        public Task<ServiceResponse> GetAll();
    }
}
