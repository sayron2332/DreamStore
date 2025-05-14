using Ardalis.Specification;
using DreamStore.Core.Entites.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Specification
{
    public class TokenSpecification
    {
        public class GetByToken : Specification<RefreshToken>
        {
            public GetByToken(string token)
            {
                Query.Where(t => t.Token == token);
            }
        }
      
    }
}
