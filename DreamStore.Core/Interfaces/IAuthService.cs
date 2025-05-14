using DreamStore.Core.Dtos.User;
using DreamStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IAuthService
    {
        public  Task<ServiceResponse> LoginUser(SignInUserDto model);
        public  Task<ServiceResponse> RegisterUser(SignUpUserDto model);
    }
}
