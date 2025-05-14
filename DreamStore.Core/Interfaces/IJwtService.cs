using DreamStore.Core.Entites;
using DreamStore.Core.Services;
using RozetkaBackEnd.Core.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Interfaces
{
    public interface IJwtService
    {
        public Task<TokensDto> CreateTokenResponse(AppUser? user);
        public Task<ServiceResponse> RefreshTokensAsync(string token);
    }
}
