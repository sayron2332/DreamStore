using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Dtos.User
{
    public class SignInUserDto
    {
        public string Email { get; set; } = string.Empty;   
        public string Password { get; set; } = string.Empty ;
    }
}
